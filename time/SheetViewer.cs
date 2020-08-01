using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class SheetViewer : Form
    {
        private List<Sheet> sheets;
        private int currentSheet;

        private int printIndex;

        private void initSheets()
        {
            foreach(PictureBox p in sheets)
            {
                panel1.Controls.Add(p);
                p.Hide();
                p.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        private SheetViewer()
        {
            InitializeComponent();
            arrowNavigators1.RightArrowClicked += NextSheet;
            arrowNavigators1.LeftArrowClicked += LastSheet;
        }
        public SheetViewer(List<Sheet> s) : this()
        {
            this.sheets = s;

            arrowNavigators1.MaxNavValue = this.sheets.Count;
            
            initSheets();

            sheets[0].Show();
        }
        private void updateSheet()
        {
            if (this.currentSheet == arrowNavigators1.NavigationIndex -1)
            {
                return;
            }

            sheets[this.currentSheet].Hide();
            this.currentSheet = arrowNavigators1.NavigationIndex - 1;
            sheets[this.currentSheet].Show();
        }
        private void NextSheet(object sender, EventArgs e)
        {
            updateSheet();
        }
        private void LastSheet(object sender, EventArgs e)
        {
            updateSheet();
        }
        private void B_Print_Click(object sender, EventArgs e)
        {
            printPhysical();
        }

        //Printing methods
        private Bitmap getBitmap(int index)
        {
            Sheet currSheet = sheets[index];

            Bitmap bmp = new Bitmap(currSheet.Width, currSheet.Height);
            Rectangle r = new Rectangle(0, 0, currSheet.Width, currSheet.Height);

            this.sheets[this.currentSheet].Hide();
            currSheet.Show();
            currSheet.DrawToBitmap(bmp, r);
            currSheet.Hide();
            this.sheets[this.currentSheet].Show();

            return bmp;
        }
        private PaperSize GetPaperSize(PrinterSettings ps, PaperKind kind)
        {
            foreach (PaperSize p in ps.PaperSizes)
            {
                if (p.Kind == kind)
                {
                    return p;
                }
            }
            return null;
        }
        private Margins GetMargins(PrinterSettings ps)
        {
            float m = 20.0F;
            float hm_x = ps.DefaultPageSettings.HardMarginX;
            float hm_y = ps.DefaultPageSettings.HardMarginY;

            int top = (int)(m > hm_y ? m - hm_y : 0);
            int left = (int)(m > hm_x ? m - hm_x : 0);

            return new Margins(left, (int)m, top, (int)m);
        }
        private void prepareSheetsForPrinting()
        {
            foreach (Sheet s in sheets)
            {
                s.PrepareForPrinting();
            }
        }
        private void printPhysical()
        {
            printIndex = 0;
            PrinterSettings ps = new PrinterSettings();
            ps.DefaultPageSettings.PaperSize = GetPaperSize(ps, this.sheets[0].PaperType);
            ps.DefaultPageSettings.Landscape = this.sheets[0].IsLandscape;
            ps.DefaultPageSettings.Margins = GetMargins(ps);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += printAllSheets;
            pd.PrinterSettings = ps;

            PrintDialog pDiag = new PrintDialog();

            if (pDiag.ShowDialog() == DialogResult.OK)
            {
                prepareSheetsForPrinting();
                pd.Print();
            }
            /*PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.ShowDialog();*/
        }
        private void printAllSheets(object sender, PrintPageEventArgs e)
        {
            Debug.WriteLine("Sheets: " + sheets.Count);

            Bitmap bmp = getBitmap(printIndex);
            e.Graphics.DrawImage(bmp, e.MarginBounds);

            if (++printIndex < sheets.Count)
            {
                e.HasMorePages = true;
            }
        }
        private void setPictureBoxImage(Image img)
        {
            if (pictureBox1.Image != null)
            {
                Image i = pictureBox1.Image;
                pictureBox1.Image = null;
                i.Dispose();
            }
            pictureBox1.Image = img;
        }
        private void anchorPicureBox()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(panel1.Width, panel1.Height);
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                AnchorStyles.Left | AnchorStyles.Right;
        }
        private void fullSizePictureBox()
        {
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
        private void scaleImage()
        {
            float val = ((float)nud_ScaleFactor.Value) / 100;

            pictureBox1.Scale(new SizeF(val, val));

            this.Refresh();
        }
        private void EnterViewMode()
        {
            Bitmap bmp = sheets[currentSheet].RenderSheet();

            setPictureBoxImage(bmp);

            fullSizePictureBox();

            sheets[currentSheet].Hide();

            pictureBox1.Show();

            nud_ScaleFactor.Show();
        }
        private void EnterEditMode()
        {

            pictureBox1.Hide();
            sheets[currentSheet].Show();

            nud_ScaleFactor.Hide();

            setPictureBoxImage(null);
        }

        private void SheetViewer_Load(object sender, EventArgs e)
        {
            Bitmap bmp = sheets[currentSheet].RenderSheet();
            sheets[currentSheet].Hide();
            pictureBox1.Image = bmp;
            pictureBox1.Show();
            this.Refresh();
        }

        private void Rb_ViewMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if(rb.Checked)
            {
                EnterViewMode();
            }
        }

        private void Rb_EditMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked)
            {
                EnterEditMode();
            }
        }

        private void Nud_ScaleFactor_ValueChanged(object sender, EventArgs e)
        {
            if (rb_EditMode.Checked)
            {
                return;
            }
        }
    }
}
