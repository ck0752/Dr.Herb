using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Dr.Herb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtherb.Text = listBox1.SelectedItem.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get List from file
            //Binding List to Listbox
            var HerbList = Program.GetHerbListFromCrv().ToArray();
            listBox1.Items.AddRange(HerbList);
         

            //Set Autocomplete function
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            allowedTypes.AddRange(HerbList);
            txtherb.AutoCompleteCustomSource = allowedTypes;
            txtherb.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtherb.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //Set default value
            ddlweight.SelectedIndex = 0;

            //Set comboEatWats
            comboEatWay.Items.Clear();
            var dicEatways = GetcomboEatways();
            comboEatWay.DataSource = new BindingSource(dicEatways, null);
            comboEatWay.DisplayMember = "Key";
            comboEatWay.ValueMember = "Value";

        }
        private Herb ComposeHerb(string name, int weight, string unit, string rate)
        {
            Herb hb = new Herb();
            hb.Name = name;
            hb.Weight = weight;
            hb.Unit = unit;
            hb.Rate = rate;

            return hb;
         }
        
        private void btnComfrm_Click(object sender, EventArgs e)
        {

            //listBox1_DoubleClick( sender,  e);

            var herbName = txtherb.Text.ToString();
            int herbWeight = 0;
            int.TryParse(txtweight.Text, out herbWeight);
            var herbUnit = ddlweight.SelectedItem.ToString();
            var herbRate = txtRate.Text.ToString();

            Herb hb = ComposeHerb(herbName, herbWeight, herbUnit, herbRate);
            var selectedDataGV = GetSelectedDataGV();
            GVaddrows(selectedDataGV, hb);
        }

        private DataGridView GetSelectedDataGV()
        {
            DataGridView GV = new DataGridView();

            switch (this.tabControl1.SelectedTab.Text)
            {
                case "藥草":
                    return this.GVHerb;
                   

                case "藥粉":
                    return this.GVPowder;
                    

                case "藥酒":
                    return this.GVLinquor;
                   
                /*
                    //藥粉
                default:
                    GVaddrows(this.GVHerb, hb);
                    break;
                    */
            }
             return GV;
        }

        private void GVaddrows(DataGridView GV, Herb Herb)
        {
            DataGridViewRowCollection gvRows = GV.Rows;
            //有比例的Column
            if (GV.ColumnCount == 4)
            {
                gvRows.Add(new Object[] { Herb.Name, Herb.Weight, Herb.Unit, Herb.Rate});
            }
            else
            {
                gvRows.Add(new Object[] { Herb.Name, Herb.Weight, Herb.Unit });
            }
            
        }

       

        private void btnExcel_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog save = new SaveFileDialog();
            
            //save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string basePath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            save.InitialDirectory = basePath;
            string datatime = DateTime.Now.ToString("yyyyMMddHHmmss");
            save.FileName = basePath + datatime+ "藥單";
            save.Filter = "*.xlsx|*.xlsx";
            save.OverwritePrompt = true;
            //if (save.ShowDialog() != DialogResult.OK) return;
            

            // Excel 物件
            Excel.Application xls = null;
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            try
            {
                xls = new Excel.Application();
                //xls.DisplayAlerts = false;
                // Excel WorkBook
                //Excel.Workbook book = xls.Workbooks.Add();
                //Excel.Workbook book = xls.Workbooks.Open(@"D:\Tina版本.xlsx");
                string basedirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
                book = xls.Workbooks.Open(basedirectory + "Tina版本.xlsx");
                //App.Path & “\值班表.xls”
                // Excel WorkBook，預設會產生一個 WorkSheet，索引從 1 開始，而非 0
                // 寫法1
                sheet = (Excel.Worksheet)book.Worksheets.Item[1];
                // 寫法2
                //Excel.Worksheet Sheet = (Excel.Worksheet)book.Worksheets[1];
                // 寫法3
                //Excel.Worksheet Sheet = xls.ActiveSheet;

                // 把 DataGridView 資料塞進 Excel 內
                //DataGridView2Excel(sheet, GVHerb);

                //多個
                List<DataGridView> listGV = new List<DataGridView>();
                listGV.Add(GVHerb);
                listGV.Add(GVPowder);
                listGV.Add(GVLinquor);

                DataGridView2Excel(sheet, listGV);

                // 儲存檔案
                book.SaveAs(save.FileName);
                //book.SaveAs(basedirectory + "test.xlsx");

              

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //注意: Excel是Unmanaged程式，要妥善結束才能乾淨不留痕跡
                //否則，很容易留下一堆excel.exe在記憶體中
                //所有用過的COM+物件都要使用Marshal.FinalReleaseComObject清掉
                //COM+物件的Reference Counter，以利結束物件回收記憶體
                if (sheet != null)
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(sheet);
                }
                if (book != null)
                {
                    book.Close(false); //忽略尚未存檔內容，避免跳出提示卡住
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(book);
                }
                if (xls != null)
                {
                    xls.Workbooks.Close();
                    xls.Quit();
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xls);
                }
                    GC.Collect();

                OpenExcel(save);
            }
        }

        private void OpenExcel(SaveFileDialog save)
        {
            /*
            OpenFileDialog open = new OpenFileDialog();
            string basePath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            open.InitialDirectory = basePath;
            open.FileName = basePath + "Excel Export demo jeff"+ ".xlsx";
            open.Filter = "*.xlsx|*.xlsx";
            open.OpenFile();
            */
            // this.Cursor = new Cursor(open.OpenFile()); 
            
            Process.Start(save.FileName + ".xlsx");


        }

        int startX = 3;
        int startY = 6;
        int wrapNum = 20;
        int shiftNum = 4;
        private void DataGridView2Excel(Excel.Worksheet Sheet, DataGridView GV)
        {

            // 下面方法二選一使用
            // 利用 DataGridView 
            // dataGridView1.Rows.Count-1 for null row
            // No need -1 , cause set AllUserToAddrows to false
            //for (int y = 0; y < dataGridView1.Rows.Count - 1; y++)
            /*
            for (int y = 0; y < GV.Rows.Count ; y++)
            {
                for (int x = 0; x < GV.Columns.Count; x++)
                {
                    string value = GV[x, y].Value.ToString();
                    //Excel template 從[6,3]開始
                    //Sheet.Cells[i + 1, j + 1] = value;
                    if ((y + startY) > wrapNum)
                    {
                        Sheet.Cells[y + startY-15, x + startX + shiftNum] = value;
                    }
                    else
                    {
                        Sheet.Cells[y + startY, x + startX] = value;
                    }; 
                   
                }
            }*/

            
            // 利用 List<Employ>
            var listhb = ConvertGVRowstoHerbList(GV);
            foreach (Herb herb in listhb)
            {
                int rowindex = listhb.IndexOf(herb) + startY;
                int colindex = startX;

                if (rowindex > wrapNum)
                {
                    rowindex -= 15;
                    colindex += shiftNum;
                }
              
                Sheet.Cells[rowindex, colindex++] = herb.Name;
                Sheet.Cells[rowindex, colindex++] = herb.Weight;
                Sheet.Cells[rowindex, colindex++] = herb.Unit;
                //Sheet.Cells[rowindex, colindex++] = herb.Salary;
            }
            MessageBox.Show("共有" + listhb.Count);

            /*
            List<Herb> empList = (List<Herb>)dataGridView1.DataSource;
            foreach (Herb herb in empList)
            {
                int
                    rowindex = empList.IndexOf(herb) + 1,
                    colindex = 1;

                Sheet.Cells[rowindex, colindex++] = herb.Name;
                Sheet.Cells[rowindex, colindex++] = herb.Weight;
                Sheet.Cells[rowindex, colindex++] = herb.Unit;
                //Sheet.Cells[rowindex, colindex++] = herb.Salary;
            }
            */

        }

        private void DataGridView2Excel(Excel.Worksheet Sheet, List<DataGridView> ListGV)
        {
            foreach (var GV in ListGV)
            {
               var listhb = ConvertGVRowstoHerbList(GV);
                foreach (Herb herb in listhb)
                {
                    SetStartPosByGVName(GV, out startX, out startY);

                    int rowindex = listhb.IndexOf(herb) + startY;
                    int colindex = startX;

                    if (rowindex > wrapNum)
                    {
                        rowindex -= 15;
                        colindex += shiftNum;
                    }

                    Sheet.Cells[rowindex, colindex++] = herb.Name;
                    Sheet.Cells[rowindex, colindex++] = herb.Weight;
                    Sheet.Cells[rowindex, colindex++] = herb.Unit;
                    //Sheet.Cells[rowindex, colindex++] = herb.Salary;
                }
                MessageBox.Show("共有" + listhb.Count);
            }
            
           
        }

      

        private void SetStartPosByGVName(DataGridView GV, out int StartX, out int StartY)
        {
            //StartX = 3;
            //StartY = 6;

            switch (GV.Name)
            {
                case "GVHerb":
                    StartX = 3;
                    StartY = 6;
                    break;

                case "GVPowder":
                    StartX = 12;
                    StartY = 6;
                    break;

                case "GVLinquor":
                    StartX = 16;
                    StartY = 6;
                    break;

                default:
                    StartX = 3;
                    StartY = 6;
                    break;
            }
        }

        private List<Herb> ConvertGVRowstoHerbList(DataGridView GV)
        { 
            //將DGV rows 轉換成List<Herb> 須重購
            var rowList = GV.Rows.Cast<DataGridViewRow>().ToList();
            List<Herb> listhb = new List<Herb>();
            foreach (var row in rowList)
            {
                Herb hb = new Herb();
                hb.Name = row.Cells[0].Value.ToString();
                hb.Weight = Convert.ToInt32(row.Cells[1].Value);
                hb.Unit = row.Cells[2].Value.ToString();
                listhb.Add(hb);
            };

            return listhb;

        }



        private void calculateGVByRate()
        {
            var GV = GetSelectedDataGV();
            var list = GV.Rows.Cast<DataGridViewRow>();

            //篩選掉 沒有比例的Row
            list = list.Where(row => row.Cells[3].FormattedValue.ToString() != "");

            var sumRate = list.Sum<DataGridViewRow>(
                 d => Convert.ToInt32(d.Cells[3].FormattedValue.ToString()));
            //var sumRate = list.GroupBy(datarow => datarow.Cells["比例"].FormattedValue).Sum();
            //Where(x => x.Cells["比例"])
            // var sumRate = list.Sum(datarow => Convert.ToInt32(datarow.Cells[3].FormattedValue));

            //計算一個單位 要多少匙
            //int days = Convert.ToInt32(txtDays.Text);

            int days = 0;
            int.TryParse(txtDays.Text, out days);
            //吃法
            int eatway = Convert.ToInt32(comboEatWay.SelectedValue);
            int eatAmt = 0;
            int.TryParse(txtEatAmt.Text, out eatAmt);
            //int eatAmt = Convert.ToInt32(txtEatAmt.Text);
            int perUnit = Convert.ToInt32((days * eatway * eatAmt) / sumRate);

            //重新計算 匙數
            list.ToList().ForEach(d => d.Cells[1].Value = Convert.ToInt32(perUnit * Convert.ToInt32(d.Cells[3].FormattedValue)));

        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            calculateGVByRate();
        }

        private Dictionary<string, int> GetcomboEatways()
        {
            Dictionary<string, int> dicEatways = new Dictionary<string, int>();
            dicEatways.Add("早空腹",1);
            dicEatways.Add("三餐飯後,睡前",4);
            dicEatways.Add("三餐飯前,睡前",4);
            dicEatways.Add("三餐飯後",3);
            dicEatways.Add("三餐飯前",3);
            dicEatways.Add("睡前",1);
   
            return dicEatways;
        }

      

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            txtherb.Text = listBox1.SelectedItem.ToString();
            txtweight.Focus();
         }

        //尚未使用
        private void AddrowsbyMultiSelect()
        {

            // 下面是 listbox 多選
          if (listBox1.SelectedItems.Count >= 1)
          {
              //var selectedItem = listBox1.Items.Cast<DataRow>().Where(item => item.Selected);
              var selectedItems = listBox1.SelectedItems;

              int herbWeight = 0;
              int.TryParse(txtweight.Text, out herbWeight);
              var herbUnit = ddlweight.SelectedItem.ToString();
              var herbRate = txtRate.Text.ToString();

              foreach (string item in selectedItems) 
                  {

                  var herbName = item.ToString();
                  var hb =  ComposeHerb(herbName, herbWeight, herbUnit, herbRate);

                  var selectedDataGV = GetSelectedDataGV();
                  GVaddrows(selectedDataGV, hb);
              }

          }
        }





        private void txtDays_TextChanged(object sender, EventArgs e)
        {
            calculateGVByRate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnComfrm.Focus();
            //    btnComfrm_Click(sender, e);
            //}


            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                Control ctrl = this.GetNextControl(this.ActiveControl, true);
                while (ctrl is TextBox == false || ctrl is ComboBox == false)
                {
                    ctrl = this.GetNextControl(ctrl, true);
                }
                ctrl.Focus();
            }
        }

        private void btnMultiPrint_Click(object sender, EventArgs e)
        {
            printDocument2.DefaultPageSettings.Landscape = true;
            printDocument2.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            printDocument2.OriginAtMargins = true;
            printDocument2.Print();
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            //int w = System.Math.Max(GVPowder.Width, GVLinquor.Width);
            //int h = GVPowder.Height + GVPowder.Height;

            ////int h = GVPowder.RowTemplate.Height + GVLinquor.RowTemplate.Height;
            //Bitmap bmp = new Bitmap(w, h);
            ////Rectangle r = new Rectangle(0, 0, GVPowder.Width, GVPowder.Height);
            //Rectangle r = new Rectangle(0, 0, GVPowder.Width, GVPowder.RowTemplate.Height);
            //GVPowder.DrawToBitmap(bmp, r);
            //r.Y = GVPowder.Height;
            //r.Width = GVLinquor.Width;
            //r.Height = GVLinquor.Height;
            //GVLinquor.DrawToBitmap(bmp, r);
            //e.Graphics.DrawImage(bmp, e.MarginBounds);
            CaptureScreen();
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void btnMultiPrePrint_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = printDocument2;
            ppd.WindowState = FormWindowState.Maximized;
            ppd.ShowDialog();

          
        }

        /// <summary>
        /// structure to hold printed page details
        /// </summary>
        /// <remarks></remarks>
        private struct pageDetails
        {
            public int columns;
            public int rows;
            public int startCol;
            public int startRow;
        }
        /// <summary>
        /// dictionary to hold printed page details, with index key
        /// </summary>
        /// <remarks></remarks>

        private Dictionary<int, pageDetails> pages;
        int maxPagesWide;
        int maxPagesTall;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument1.Print();
        }

        private void btnPrePrint_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = PrintDocument1;
            ppd.WindowState = FormWindowState.Maximized;
            ppd.ShowDialog();
        }

        /// <summary>
        /// the majority of this Sub is calculating printed page ranges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void PrintDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //'this removes the printed page margins
            PrintDocument1.OriginAtMargins = true;
            PrintDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

            pages = new Dictionary<int, pageDetails>();

            int maxWidth = Convert.ToInt32(PrintDocument1.DefaultPageSettings.PrintableArea.Width) - 40;
            int maxHeight = Convert.ToInt32(PrintDocument1.DefaultPageSettings.PrintableArea.Height) - 150 + Label1.Height;

            int pageCounter = 0;
            pages.Add(pageCounter, new pageDetails());

            int columnCounter = 0;

            int columnSum = GVHerb.RowHeadersWidth;

            for (int c = 0; c <= GVHerb.Columns.Count - 1; c++)
            {
                if (columnSum + GVHerb.Columns[c].Width < maxWidth)
                {
                    columnSum += GVHerb.Columns[c].Width;
                    columnCounter += 1;
                }
                else
                {
                    pages[pageCounter] = new pageDetails
                    {
                        columns = columnCounter,
                        rows = 0,
                        startCol = pages[pageCounter].startCol
                    };
                    columnSum = GVHerb.RowHeadersWidth + GVHerb.Columns[c].Width;
                    columnCounter = 1;
                    pageCounter += 1;
                    pages.Add(pageCounter, new pageDetails { startCol = c });
                }
                if (c == GVHerb.Columns.Count - 1)
                {
                    if (pages[pageCounter].columns == 0)
                    {
                        pages[pageCounter] = new pageDetails
                        {
                            columns = columnCounter,
                            rows = 0,
                            startCol = pages[pageCounter].startCol
                        };
                    }
                }
            }

            maxPagesWide = pages.Keys.Max() + 1;

            pageCounter = 0;

            int rowCounter = 0;

            int rowSum = GVHerb.ColumnHeadersHeight;

            for (int r = 0; r <= GVHerb.Rows.Count - 2; r++)
            {
                if (rowSum + GVHerb.Rows[r].Height < maxHeight)
                {
                    rowSum += GVHerb.Rows[r].Height;
                    rowCounter += 1;
                }
                else
                {
                    pages[pageCounter] = new pageDetails
                    {
                        columns = pages[pageCounter].columns,
                        rows = rowCounter,
                        startCol = pages[pageCounter].startCol,
                        startRow = pages[pageCounter].startRow
                    };
                    for (int x = 1; x <= maxPagesWide - 1; x++)
                    {
                        pages[pageCounter + x] = new pageDetails
                        {
                            columns = pages[pageCounter + x].columns,
                            rows = rowCounter,
                            startCol = pages[pageCounter + x].startCol,
                            startRow = pages[pageCounter + x].startRow
                        };
                    }

                    pageCounter += maxPagesWide;
                    for (int x = 0; x <= maxPagesWide - 1; x++)
                    {
                        pages.Add(pageCounter + x, new pageDetails
                        {
                            columns = pages[x].columns,
                            rows = 0,
                            startCol = pages[x].startCol,
                            startRow = r
                        });
                    }

                    rowSum = GVHerb.ColumnHeadersHeight + GVHerb.Rows[r].Height;
                    rowCounter = 1;
                }
                if (r == GVHerb.Rows.Count - 2)
                {
                    for (int x = 0; x <= maxPagesWide - 1; x++)
                    {
                        if (pages[pageCounter + x].rows == 0)
                        {
                            pages[pageCounter + x] = new pageDetails
                            {
                                columns = pages[pageCounter + x].columns,
                                rows = rowCounter,
                                startCol = pages[pageCounter + x].startCol,
                                startRow = pages[pageCounter + x].startRow
                            };
                        }
                    }
                }
            }

            maxPagesTall = pages.Count / maxPagesWide;

        }

       
        int startPage = 0;
        /// <summary>
        /// this is the actual printing routine.
        /// using the pagedetails i calculated earlier, it prints a title,
        /// + as much of the datagridview as will fit on 1 page, then moves to the next page.
        /// this is setup to be dynamic. try resizing the dgv columns or rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void PrintDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle rect = new Rectangle(20, 20, Convert.ToInt32(PrintDocument1.DefaultPageSettings.PrintableArea.Width), Label1.Height);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString("Dr.Herb", Label1.Font, Brushes.Black, rect, sf);

            sf.Alignment = StringAlignment.Near;

            int startX = 50;
            int startY = rect.Bottom;

            for (int p = startPage; p <= pages.Count - 1; p++)
            {
                Rectangle cell = new Rectangle(startX, startY, GVHerb.RowHeadersWidth, GVHerb.ColumnHeadersHeight);
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlLight), cell);
                e.Graphics.DrawRectangle(Pens.Black, cell);

                startY += GVHerb.ColumnHeadersHeight;

                for (int r = pages[p].startRow; r <= pages[p].startRow + pages[p].rows - 1; r++)
                {
                    cell = new Rectangle(startX, startY, GVHerb.RowHeadersWidth, GVHerb.Rows[r].Height);
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlLight), cell);
                    e.Graphics.DrawRectangle(Pens.Black, cell);
                    //e.Graphics.DrawString(GVHerb.Rows[r].HeaderCell.Value.ToString(), GVHerb.Font, Brushes.Black, cell, sf);
                    e.Graphics.DrawString((GVHerb.Rows[r].Index + 1).ToString(), GVHerb.Font, Brushes.Black, cell, sf);
                    startY += GVHerb.Rows[r].Height;
                }

                startX += cell.Width;
                startY = rect.Bottom;

                for (int c = pages[p].startCol; c <= pages[p].startCol + pages[p].columns - 1; c++)
                {
                    cell = new Rectangle(startX, startY, GVHerb.Columns[c].Width, GVHerb.ColumnHeadersHeight);
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlLight), cell);
                    e.Graphics.DrawRectangle(Pens.Black, cell);
                    e.Graphics.DrawString(GVHerb.Columns[c].HeaderCell.Value.ToString(), GVHerb.Font, Brushes.Black, cell, sf);
                    startX += GVHerb.Columns[c].Width;
                }

                startY = rect.Bottom + GVHerb.ColumnHeadersHeight;

                for (int r = pages[p].startRow; r <= pages[p].startRow + pages[p].rows - 1; r++)
                {
                    startX = 50 + GVHerb.RowHeadersWidth;
                    for (int c = pages[p].startCol; c <= pages[p].startCol + pages[p].columns - 1; c++)
                    {
                        cell = new Rectangle(startX, startY, GVHerb.Columns[c].Width, GVHerb.Rows[r].Height);
                        e.Graphics.DrawRectangle(Pens.Black, cell);
                        e.Graphics.DrawString(GVHerb[c, r].Value.ToString(), GVHerb.Font, Brushes.Black, cell, sf);
                        startX += GVHerb.Columns[c].Width;
                    }
                    startY += GVHerb.Rows[r].Height;
                }

                if (p != pages.Count - 1)
                {
                    startPage = p + 1;
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    startPage = 0;
                }

            }

        }

        Bitmap memoryImage;
        //System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
        private void CaptureScreen()
        {
           Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formprint frm = new Formprint();
            frm.Show();     
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedName = tabControl1.SelectedTab.Text;
           
            switch (selectedName)
            {
                case "藥粉":
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("匙");
                    break;

                case "藥酒":
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("毫升");
                    break;

                case "藥草":
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("錢");
                    break;

                default:
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("匙");
                    break;
            }
        }
    }
}
