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
           var txt = listBox1.SelectedItem ?? "";
           txtherb.Text = txt.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get List from file
            //Binding List to Listbox
            var HerbList = Program.GetHerbListFromCrv().ToArray();
            listBox1.Items.AddRange(HerbList);
            //listBox1.DisplayMember = 
         
            
            //Set Autocomplete function
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            allowedTypes.AddRange(HerbList);
            txtherb.AutoCompleteCustomSource = allowedTypes;
            txtherb.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtherb.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //Set default value
            ddlweight.SelectedIndex = 4;

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

            /*單一選取
            var herbName = txtherb.Text.ToString();
            int herbWeight = 0;
            int.TryParse(txtweight.Text, out herbWeight);
            var herbUnit = ddlweight.SelectedItem.ToString();
            var herbRate = txtRate.Text.ToString();

            Herb hb = ComposeHerb(herbName, herbWeight, herbUnit, herbRate);
            var selectedDataGV = GetSelectedDataGV();
            GVaddrows(selectedDataGV, hb);
            */

            //多重選取
            AddrowsbyMultiSelect();

            resetInputbox();
        }

        private void resetInputbox()
        {
            txtherb.Text = "";
            txtweight.Text = "";
            txtRate.Text = "";
            txtEatAmt.Text = "";
            txtDays.Text = "";

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
            Cursor.Current = Cursors.WaitCursor;
            SaveFileDialog save = new SaveFileDialog();
            
            //save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string basePath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            save.InitialDirectory = basePath;
            string datatime = DateTime.Now.ToString("yyyyMMddHHmmss");
            save.FileName = basePath + datatime+ "藥單";
            save.Filter = "*.xlsx|*.xlsx";
            save.OverwritePrompt = true;
            
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
                listGV.Add(GVPowder);
                listGV.Add(GVPowder2);
                listGV.Add(GVLinquor);
                listGV.Add(GVLinquor2);
                listGV.Add(GVHerb);

                //排除沒有資料
                listGV = listGV.Where(gv => gv.Rows.Count > 0).ToList();


                //塞入Memo
                List<String> listTxt = new List<String>();
                listTxt.Add(txtPowder.Text.ToString());
                listTxt.Add(txtPowder2.Text.ToString());
                listTxt.Add(txtLinquor.Text.ToString());
                listTxt.Add(txtLinquor2.Text.ToString());
                listTxt.Add(txtHerbMemo.Text.ToString());
                listTxt = listTxt.Where(t => t.Length != 0).ToList();

                DataGridView2Excel(sheet, listGV, listTxt);

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

                //this.InitializeComponent();
                 
                Cursor.Current = Cursors.Default;
                resetGV();
                OpenExcel(save);
            }
        }
        private void resetGV()
        {
            
            GVPowder.Rows.Clear();
            GVPowder2.Rows.Clear();
            GVLinquor.Rows.Clear();
            GVLinquor2.Rows.Clear();
            GVHerb.Rows.Clear();

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

        

      

        
        //int startX = 3;
        int firstX = 2;
        
        int MemostartY = 25;
        int wrapNum = 23;
        int shiftNum = 5;
        int DayX = 18;
        int DayY = 3;
        int DateX = 13;
        int DateY = 3;
        int Maxrows = 18;
        

        private void DataGridView2Excel(Excel.Worksheet Sheet, List<DataGridView> ListGV, List<String> ListTxt)
        {
            int startX = firstX;
            int startY = 6;
            int GVcounter = 0;
            int Memocounter = 0;
            bool isWrapCol = false;

            foreach (var GV in ListGV)
            {   
                //這次開的種類
                int NumCounter = 0;

                var listhbRows = ConvertGVRowstoHerbList(GV);
                var listHeaderName = ConvertGVColumnsHeader(GV);
                
                //塞入表頭
                for (int i = 0; i < listHeaderName.Count; i++)
                {   //Header position
                    int rowindex = startY - 1;
                    int colindex = startX + 1;

                    Sheet.Cells[rowindex, colindex + i] = listHeaderName[i];
                }
                
                //塞入資料
                foreach (Herb herb in listhbRows)
                {
                    //SetStartPosByGVName(GV, out startX, out startY);

                    //old
                    /*
                     * int rowindex = listhbRows.IndexOf(herb) + startY;
                     * int colindex = startX;
                    */
    
                    int rowindex = listhbRows.IndexOf(herb) + startY;
                    rowindex = (isWrapCol) ? rowindex - Maxrows : rowindex;

                    int colindex = firstX + (shiftNum * GVcounter);

                    
                    //換行
                    if (rowindex >= wrapNum)
                    {
                        isWrapCol = true;
                        GVcounter++;
                        //colindex = firstX + (shiftNum * GVcounter);
                        
                    }

                    Sheet.Cells[rowindex, colindex++] = ++NumCounter; 
                    Sheet.Cells[rowindex, colindex++] = herb.Name;
                    Sheet.Cells[rowindex, colindex++] = herb.Weight;
                    Sheet.Cells[rowindex, colindex++] = herb.Unit;
                }

                //塞入吃法Memo
                {
                    int rowindex = MemostartY;
                    int colindex = startX;

                    Sheet.Cells[rowindex, colindex] = ListTxt[Memocounter].TrimEnd().TrimStart();
                }

                //塞入哩哩叩叩
                Sheet.Cells[DayY, DayX] = txtDays.Text.Trim();
                Sheet.Cells[DateY, DateX] = DateTime.Now.ToString("yyyy/MM/dd");

                //MessageBox.Show("共有" + listhb.Count);
                GVcounter++;
                Memocounter++;
                isWrapCol = false;
               
                startX = firstX + (shiftNum * GVcounter);
            }
            
           
        }

      

        private void SetStartPosByGVName(DataGridView GV, out int StartX, out int StartY)
        {
            switch (GV.Name)
            {
                case "GVPowder":
                    StartX = 3;
                    StartY = 6;
                    break;

                case "GVPowder2":
                    StartX = 8;
                    StartY = 6;
                    break;

                case "GVLinquor":
                    StartX = 13;
                    StartY = 6;
                    break;

                case "GVHerb":
                    StartX = 18;
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

        private List<String> ConvertGVColumnsHeader(DataGridView GV)
        {
            List<String> list = new List<string>();
            var trimList = GV.Columns.Cast<DataGridViewColumn>().ToList()
                            .Where(c => c.HeaderText != "比例").ToList();

            foreach (var item in trimList)
            {
                list.Add(item.HeaderText);
            }
            
            return list;
        }



        private void calculateGVByRate()
        {
            var GV = GetSelectedDataGV();

            //藥草TAB 跳出
            if (GV.Columns.Count < 4) return;

            var list = GV.Rows.Cast<DataGridViewRow>();

            

            //篩選掉 沒有比例的Row
            list = list.Where(row => row.Cells[3].FormattedValue.ToString() != "");

            var sumRate = list.Sum<DataGridViewRow>(
                 d => Convert.ToInt32(d.Cells[3].FormattedValue.ToString()));

            sumRate = (sumRate == 0) ? 1 : sumRate;

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
            list.ToList().ForEach(
                d => d.Cells[1].Value = Convert.ToInt32(perUnit * Convert.ToInt32(d.Cells[3].FormattedValue)));

            if (GV.Name.Contains("GVPowder"))
            {
                calculateGVbyDiscount(list, 0.8);
            }
                

        }


        private void calculateGVbyDiscount(IEnumerable<DataGridViewRow> list, double discount)
        {
            list.ToList().ForEach(
                d => d.Cells[1].Value = Convert.ToInt32(Convert.ToInt32(d.Cells[1].Value) * discount));
         }

        private void btnRate_Click(object sender, EventArgs e)
        {
            calculateGVByRate();
            //lbBag.Text = "建議" + GetAdviseBagSize();
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
            //lbBag.Text = "建議" + GetAdviseBagSize();
        }

        private string GetAdviseBagSize()
        {

            var list = GVPowder.Rows.Cast<DataGridViewRow>();
            
            var sumWeight = list.Sum<DataGridViewRow>(
                 d => Convert.ToDouble(d.Cells[1].FormattedValue.ToString()));

            if (sumWeight <= 40) return "3號袋";
            else if (sumWeight <= 70) return "4號袋";
            else if (sumWeight <= 120) return "5號袋";
            else if (sumWeight <= 220) return "6號袋";
            else if (sumWeight <= 300) return "7號袋";
            else return "大塑膠袋";
            
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
            //trim 最後一個數字
            string selectedName = this.tabControl1.SelectedTab.Text.Substring(0, 2);
            //string selectedName = tabControl1.SelectedTab.Text;
           
            switch (selectedName)
            {
                case "藥粉":
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("匙");
                    listBox1.ClearSelected();
                    listBox1.SelectedIndex = listBox1.FindStringExact("---常用---");
                    break;

                case "藥酒":
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("毫升");
                    listBox1.ClearSelected();
                    listBox1.SelectedIndex = listBox1.FindStringExact("---藥酒---");
                    break;

                case "藥草":
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("錢");
                    listBox1.ClearSelected();
                    listBox1.SelectedIndex = listBox1.FindStringExact("---常用---");
                    break;

                default:
                    ddlweight.SelectedIndex = ddlweight.FindStringExact("匙");
                    break;
            }
        }

        private void comboEatWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var selectTabName = tabControl1.SelectedTab.Name;
            //var selectedpage = tabControl1.Controls.Find(selectTabName, true).First();
            //selectedpage.Controls.Find("lbPowder2Eatway", true).First().Text = comboEatWay.SelectedText;

            //Label lb = (Label)tabControl1.Controls.Find("lbPowder2Eatway", true).First();
            //lb.Text = comboEatWay.Text;

            EditSelectedMemo();
        }

        private void EditSelectedMemo()
        {
            TextBox txt = GetSelectedMemo();

            if (txt.Name != "txtHerbMemo")
            {
                txt.Text = "吃法: \r\n" + comboEatWay.Text + txtEatAmt.Text + ddlweight.Text;
            }
            else
            {
                txt.Text = "煮法:\t\t\t\r\n    碗水煮成\t碗藥\r\n喝法:\t\t\t\r\n一天     碗 早喝到晚\t\r\n";
            }
            
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

                case "藥粉2":
                    return this.GVPowder2;

                case "藥酒":
                    return this.GVLinquor;

                case "藥酒2":
                    return this.GVLinquor2;
         
            }
            return GV;
        }


        private TextBox GetSelectedMemo()
        {
            TextBox txt = new TextBox();

            switch (this.tabControl1.SelectedTab.Text)
            {
                case "藥粉":
                    return this.txtPowder;

                case "藥粉2":
                    return this.txtPowder2;

                case "藥酒":
                    return this.txtLinquor;

                case "藥酒2":
                    return this.txtLinquor2;

                case "藥草":
                    return this.txtHerbMemo;

            }
            return txt;
        }

        private void txtEatAmt_TextChanged(object sender, EventArgs e)
        {
            EditSelectedMemo();
        }
    }
}
