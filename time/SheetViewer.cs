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

        private bool mouseDown = false;
        private int mouseX;
        private int mouseY;

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
            panel1.MouseWheel += panel1_MouseWheel;

            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(panel1.Width, panel1.Height);
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

            if(rb_EditMode.Checked)
            {
                EnterEditMode();
            }
            else
            {
                EnterViewMode();
            }
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
        private void setScrollBars()
        {
            if (rb_ViewMode.Checked)
            {
                panel1.AutoScroll = false;
                panel1.HorizontalScroll.Enabled = true;
                panel1.HorizontalScroll.Visible = true;
                panel1.VerticalScroll.Enabled = true;
                panel1.VerticalScroll.Visible = true;
            }
            else
            {
                panel1.AutoScroll = true;
            }
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
        private void resizePictureBox(int percentage)
        {
            int height = this.pictureBox1.Image.Height * percentage / 100;
            int width = this.pictureBox1.Image.Width * percentage / 100;

            this.pictureBox1.Size = new Size(width, height);
        }
        private void fitToPictureBoxToPanel()
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            int scaleFactor;

            Debug.WriteLine("Form Size: " + this.Size.ToString());
            Debug.WriteLine("Panel1 Size: " + panel1.Size.ToString());
            if (panel1.Width < panel1.Height)
            {
                scaleFactor = panel1.Width * 100 / pictureBox1.Image.Width;
            }
            else
            {
                scaleFactor = panel1.Height * 100 / pictureBox1.Image.Height;
            }

            nud_ScaleFactor.Value = scaleFactor;
        }
        private void offsetPictureBox()
        {
            int xOffset = 0;
            if (pictureBox1.Width < panel1.Width)
            {
                int xDiff = panel1.Width - pictureBox1.Width;
                xOffset = (xDiff / 2);
            }
            int yOffset = 0;
            if (pictureBox1.Height < panel1.Height)
            {
                int yDiff = panel1.Height - pictureBox1.Height;
                yOffset = (yDiff / 2);
            }

            pictureBox1.Location = new Point(xOffset, yOffset);
        }
        private void EnterViewMode()
        {
            sheets[currentSheet].Show();
            Bitmap bmp = sheets[currentSheet].RenderSheet();
            sheets[currentSheet].Hide();

            setPictureBoxImage(bmp);


            fitToPictureBoxToPanel();
            offsetPictureBox();

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
            EnterViewMode();
        }

        private void Rb_ViewMode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if(rb.Checked)
            {
                Debug.WriteLine("Entering view mode.");
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
            resizePictureBox((int)nud_ScaleFactor.Value);
            //offsetPictureBox();
            panel1.Refresh();
        }
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            nud_ScaleFactor.Value += e.Delta / SystemInformation.MouseWheelScrollDelta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fitToPictureBoxToPanel();
            offsetPictureBox();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (rb_ViewMode.Checked)
            {
                fitToPictureBoxToPanel();
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDown = true;
        }
        private void moveBox(int xMove, int yMove)
        {
            int xPos = this.pictureBox1.Location.X + xMove;
            int yPos = this.pictureBox1.Location.Y + yMove;

            pictureBox1.Location = new Point(xPos, yPos);
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouseDown)
            {
                moveBox(e.X - mouseX, e.Y - mouseY);
            }
            mouseX = e.X;
            mouseY = e.Y;
        }
    }
}
