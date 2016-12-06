using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace Dr.Herb
{
    public partial class Formprint : Form
    {
        public Formprint()
        {
            InitializeComponent();
        }


        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["Form1"];
        private void Formprint_Load(object sender, EventArgs e)
        {
            CopyDataGridView(((Form1)f).GVPowder, GVPowder2);
            CopyDataGridView(((Form1)f).GVLinquor, GVLinquor2);
            CopyDataGridView(((Form1)f).GVHerb, GVHerb2);
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
           
            CopyDataGridView(((Form1)f).GVPowder, GVPowder2);
            CopyDataGridView(((Form1)f).GVLinquor, GVLinquor2);
            CopyDataGridView(((Form1)f).GVHerb, GVHerb2);
            //DataGridViewRow r2 = new DataGridViewRow();
            //if (((Form1)f).GVHerb.RowCount != 0)
            //{
            //    DataGridViewRowCollection gvRows = GVHerb2.Rows;

            //    foreach (DataGridViewRow row in (((Form1)f).GVHerb.Rows))
            //    {
            //        gvRows.Add(new Object[] { row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value });

            //    }



            //}
            //else
            //{
            //    MessageBox.Show("There is no data to export, please verify..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }


        private void CopyDataGridView(DataGridView GV1, DataGridView GV2 )
        {

            DataGridViewRow r2 = new DataGridViewRow();
            if (GV1.RowCount != 0)
            {
                DataGridViewRowCollection gv2Rows = GV2.Rows;

                foreach (DataGridViewRow row in GV1.Rows)
                {
                    gv2Rows.Add(new Object[] { row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value });

                }
            }
            else
            {
                MessageBox.Show("There is no data to export, please verify..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GV2.ClearSelection();

        }

        Bitmap memoryImage;
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = printDocument2;
            ppd.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            printDocument2.DefaultPageSettings.Landscape = true;
            printDocument2.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            printDocument2.OriginAtMargins = true;

            //this.ShowInTaskbar = false;
            CaptureScreen();
            //ppd.ShowDialog();
            ppd.Show();
            this.FormBorderStyle = FormBorderStyle.Sizable;

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
            //CaptureScreen();
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printDocument2.DefaultPageSettings.Landscape = true;
            printDocument2.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            printDocument2.OriginAtMargins = true;

            this.FormBorderStyle = FormBorderStyle.None;
            CaptureScreen();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            printDocument2.Print();
        }

        private void btnDGVprinter_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "DataGridView Report";
            printer.SubTitle = "An Easy to Use DataGridView Printing Object";
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |
            StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Your Company Name Here";
            printer.FooterSpacing = 15;
            //printer.PrintDataGridView(GVHerb2);
            printer.PrintPreviewNoDisplay(GVHerb2);
        }

        private void btnDGVpreivew_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "DataGridView Report";
            printer.SubTitle = "An Easy to Use DataGridView Printing Object";
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit |
            StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Your Company Name Here";
            printer.FooterSpacing = 15;
            //printer.PrintDataGridView(GVHerb2);
            printer.PrintPreviewNoDisplay(GVHerb2);
        }
    }
}
