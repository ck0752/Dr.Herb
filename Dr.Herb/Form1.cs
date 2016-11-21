using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            initListViewHerb();

            //this.AcceptButton = this.btnComfirm;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var herbName = txtherb.Text.ToString();
            var herbWeight = txtweight.Text.ToString();
            var herbUnit = ddlweight.SelectedItem.ToString();

           //Test
           // string herbInfo = String.Format("{0} {1}{2}", herbName, herbWeight, herbUnit);
           //MessageBox.Show(herbInfo, "藥草資訊", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //ListView
            var item = new ListViewItem(new[] { herbName, herbWeight, herbUnit });
            lvHerb.Items.Add(item);

            //GridView
            DataGridViewRowCollection gvRows = GVHerb.Rows;
            gvRows.Add(new Object[] { herbName, herbWeight, herbUnit });
           
        }


        private void initListViewHerb()
        {

            // Set the view to show details.
            lvHerb.View = View.Details;
            // Allow the user to edit item text.
            lvHerb.LabelEdit = true;
            // Allow the user to rearrange columns.
            lvHerb.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            lvHerb.FullRowSelect = true;
            // Display grid lines.
            lvHerb.GridLines = true;


            // Sort the items in the list in ascending order.
            //lvHerb.Sorting = SortOrder.Ascending;


            // Add columns
            /*
            lvHerb.Columns.Add("草藥", -2, HorizontalAlignment.Left);
            lvHerb.Columns.Add("重量", -2, HorizontalAlignment.Left);
            lvHerb.Columns.Add("單位", -2, HorizontalAlignment.Left);
            */

            lvHerb.Columns.Add("草藥");
            lvHerb.Columns.Add("重量");
            lvHerb.Columns.Add("單位");

            /*
            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("item1");
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("1");
            item1.SubItems.Add("2");
            item1.SubItems.Add("3");
            */

            /*
            var item1 = new ListViewItem(new[] { "id123", "Tom", "24" });
            var item2 = new ListViewItem(new[] { "eerwe", "Jeff", "2" });
            lvHerb.Items.Add(item1);
            lvHerb.Items.Add(item2);
            */

            //lvRegAnimals.Items.Add(item2);

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
                    e.Graphics.DrawString((GVHerb.Rows[r].Index+1).ToString(), GVHerb.Font, Brushes.Black, cell, sf);
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
    }
}
