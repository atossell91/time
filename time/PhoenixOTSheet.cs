using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class PhoenixOTSheet : Form
    {
        private TextBox weekNo;
        private TextBox date;
        private TextBox name;

        private TextBox[] firstWeekDates;
        private TextBox[] secondWeekDates;

        private TextBox[,] firstWeek;
        private TextBox[,] secondWeek;

        private List<work_period> range;

        private void CentreTextBox(ref TextBox tb, Point p1, Point p2)
        {
            int xDif = Math.Abs(p2.X - p1.X);
            int yDif = Math.Abs(p2.Y - p1.Y);

            int xPos = p1.X;
            int yPos = p1.Y;

            tb.Size = new Size(xDif, tb.Size.Height);
            tb.TextAlign = HorizontalAlignment.Center;

            if (tb.Height < yDif)
            {
                int ySpace = yDif - tb.Height;
                yPos += ySpace / 2;
            }
            tb.Location = new Point(xPos, yPos);
        }
        private TextBox createStandardTextBox(Point location)
        {
            TextBox tb = new TextBox();

            tb.Location = location;
            tb.Font = new Font("Arial", 14);

            pictureBox1.Controls.Add(tb);

            return tb;
        }
        private TextBox createLargeTextBox(Point location)
        {
            TextBox tb = createStandardTextBox(location);
            tb.Font = new Font("Arial", 24);
            return tb;
        }
        private void FillDateAndWeek()
        {
            if (range == null || range.Count == 0)
            {
                return;
            }

            DateTime start = range[0].Date;
            DateTime end = range[range.Count - 1].Date;

            int startWeek = WeekNumber.GetWeekNumber(start);
            int endWeek = WeekNumber.GetWeekNumber(end);

            weekNo.Text = startWeek.ToString() + " & " + endWeek.ToString();

            string dateFormat = "yyyy-MM-dd";
            date.Text = start.ToString(dateFormat) + " - " + end.ToString(dateFormat);
        }
        private void fillDateRow(ref TextBox[] row, Point startPoint, int rangeStart)
        {
            Size cellSize = PhoenixOTSheetDims.CellSize;
            int count = 7;
            for (int n = 0; n < count; ++n)
            {
                int p1x = startPoint.X + ((cellSize.Width - 1 + PhoenixOTSheetDims.BorderWidth) * 2) * n;
                int p1y = startPoint.Y;

                int p2x = p1x + ((cellSize.Width - 1) * 2) + PhoenixOTSheetDims.BorderWidth;
                int p2y = p1y + cellSize.Height;

                Point p1 = new Point(p1x, p1y);
                Point p2 = new Point(p2x, p2y);

                row[n] = createStandardTextBox(startPoint);
                TextBox tb = row[n];
                CentreTextBox(ref tb, p1, p2);
                tb.Text = range[n + rangeStart].Date.DayOfWeek.ToString() + ", " + range[n + rangeStart].Date.ToString("MMMM dd");
            }
        }
        private void FillIndividualDates()
        {
            firstWeekDates = new TextBox[7];
            secondWeekDates = new TextBox[7];
            Size cellSize = PhoenixOTSheetDims.CellSize;
            int count = 7;

            if (range.Count > count *2)
            {
                Debug.WriteLine("Range exceeds column count");
                return;
            }

            fillDateRow(ref firstWeekDates, PhoenixOTSheetDims.TableOneDatesStart, 0);
            fillDateRow(ref secondWeekDates, PhoenixOTSheetDims.TableTwoDatesStart, 7);
        }
        private void CreateLabelGrid(ref TextBox[,] week, Point GridStart, Size CellSize,
            int NumColumns, int NumRows, int BorderSize)
        {
            week = new TextBox[NumRows,NumColumns];
            
            for (int y = 0; y < NumRows; ++y)
            {
                for (int x =0; x < NumColumns; ++x)
                {
                    int p1X = (GridStart.X + (CellSize.Width-1 + BorderSize) * x);
                    int p2Y = (GridStart.Y + (CellSize.Height-1 + BorderSize) * y);

                    Point p1 = new Point(p1X, p2Y);

                    week[y, x] = createStandardTextBox(p1);
                    CentreTextBox(ref week[y, x], p1,
                        new Point(p1.X + PhoenixOTSheetDims.CellSize.Width-1, p1.Y + PhoenixOTSheetDims.CellSize.Height-1));
                }
            }
        }
        public PhoenixOTSheet(List<work_period> range)
        {
            this.range = range;

            InitializeComponent();

            weekNo = createLargeTextBox(PhoenixOTSheetDims.WeekInput_TopLeft);
            CentreTextBox(ref weekNo, PhoenixOTSheetDims.WeekInput_TopLeft, PhoenixOTSheetDims.WeekInput_BottomRight);

            date = createLargeTextBox(PhoenixOTSheetDims.GlobalDate_TopLeft);
            CentreTextBox(ref date, PhoenixOTSheetDims.GlobalDate_TopLeft, PhoenixOTSheetDims.GlobalDate_BottomRight);

            FillDateAndWeek();

            FillIndividualDates();

            CreateLabelGrid(ref firstWeek, PhoenixOTSheetDims.TableOneStart, PhoenixOTSheetDims.CellSize,
                PhoenixOTSheetDims.ColumnsPerGrid, PhoenixOTSheetDims.RowsPerGrid, PhoenixOTSheetDims.BorderWidth);
            CreateLabelGrid(ref secondWeek, PhoenixOTSheetDims.TableTwoStart, PhoenixOTSheetDims.CellSize,
                PhoenixOTSheetDims.ColumnsPerGrid, PhoenixOTSheetDims.RowsPerGrid, PhoenixOTSheetDims.BorderWidth);
        }

        private void setAllBorders(BorderStyle b)
        {
            weekNo.BorderStyle = b;
            date.BorderStyle = b;
            //name.BorderStyle = b;

            for (int n =0; n < firstWeekDates.Length; ++n)
            {
                firstWeekDates[n].BorderStyle = b;
                secondWeekDates[n].BorderStyle = b;
            }
            for (int r = 0; r < PhoenixOTSheetDims.RowsPerGrid; ++r)
            {
                for (int c =0; c < PhoenixOTSheetDims.ColumnsPerGrid; ++c)
                {
                    firstWeek[r, c].BorderStyle = b;
                    secondWeek[r, c].BorderStyle = b;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Enabled = false;
            setAllBorders(BorderStyle.None);
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, r);
            bmp.Save(@"C:\Users\atoss\OneDrive\Documents\Filled_Phoenix_Form.png", ImageFormat.Png);
        }
    }
}
