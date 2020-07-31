using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class Sheet_PhoenixOT : Sheet
    {
        private TextBox weekNo;
        private TextBox date;
        private TextBox name;

        private TextBox[] firstWeekDates;
        private TextBox[] secondWeekDates;

        private TextBox[,] firstWeek;
        private TextBox[,] secondWeek;

        private List<work_period> range;

        private const int STANDARD_FONT_SIZE = 18;
        private const int LARGE_FONT_SIZE = 26;

        private TextBox createStandardTextBox(Point location, int fontSize)
        {// 18 and 26
            TextBox tb = new TextBox();

            tb.Location = location;
            tb.Font = new Font("Arial", fontSize);

            this.Controls.Add(tb);

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
        private void FillName(string str_name)
        {
            name = createStandardTextBox(PhoenixOTSheetDims.NameInput.TopLeft, LARGE_FONT_SIZE);
            PageTools.CentreTextBox(name, PhoenixOTSheetDims.NameInput);

            name.Text = str_name;
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

                row[n] = createStandardTextBox(startPoint, STANDARD_FONT_SIZE-4);
                TextBox tb = row[n];
                PageTools.CentreTextBox(tb, new Box(p1, p2));
                tb.Text = range[n + rangeStart].Date.DayOfWeek.ToString() + ", " + range[n + rangeStart].Date.ToString("MMMM dd");
            }
        }
        private void FillIndividualDates()
        {
            int count = 7;

            firstWeekDates = new TextBox[count];
            secondWeekDates = new TextBox[count];
            Size cellSize = PhoenixOTSheetDims.CellSize;

            if (range.Count > count * 2)
            {
                Debug.WriteLine("Range (" + range.Count + ") exceeds column count");
                return;
            }

            fillDateRow(ref firstWeekDates, PhoenixOTSheetDims.TableOneDatesStart, 0);
            fillDateRow(ref secondWeekDates, PhoenixOTSheetDims.TableTwoDatesStart, 7);
        }
        private void CreateLabelGrid(ref TextBox[,] week, Point GridStart, Size CellSize,
            int NumColumns, int NumRows, int BorderSize)
        {
            week = new TextBox[NumRows, NumColumns];

            for (int y = 0; y < NumRows; ++y)
            {
                for (int x = 0; x < NumColumns; ++x)
                {
                    int p1X = (GridStart.X + (CellSize.Width - 1 + BorderSize) * x);
                    int p2Y = (GridStart.Y + (CellSize.Height - 1 + BorderSize) * y);

                    Point p1 = new Point(p1X, p2Y);

                    week[y, x] = createStandardTextBox(p1, STANDARD_FONT_SIZE);
                    PageTools.CentreTextBox(week[y, x], new Box(p1,
                        new Point(p1.X + PhoenixOTSheetDims.CellSize.Width - 1, p1.Y + PhoenixOTSheetDims.CellSize.Height - 1)));
                }
            }
        }
        private void addDataToGrid(ref TextBox[,] grid, int rangeStartIndex)
        {
            for (int n = 0; n < PhoenixOTSheetDims.ColumnsPerGrid; n += 2)
            {
                int row = 0;
                Debug.WriteLine("RangeStart: " + rangeStartIndex + " n: " + n);
                double prems = range[rangeStartIndex + n / 2].ShiftPremiums.TotalHours;
                double ot = range[rangeStartIndex + n / 2].Overtime.TotalHours;
                if (prems > 0)
                {
                    grid[row, n].Text = prems.ToString();
                    grid[row, n + 1].Text = "055";
                    ++row;
                }
                if (ot > 0)
                {
                    grid[row, n].Text = ot.ToString();
                    grid[row, n + 1].Text = "260";
                }
            }
        }
        public Sheet_PhoenixOT(string name, List<work_period> range)
        {
            this.IsLandscape = true;
            this.PaperType = System.Drawing.Printing.PaperKind.Letter;

            this.Image = time.Properties.Resources.Phoenix_Inspectors_Weekly_OT_Template_With_Blank_Form;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Location = new Point(0, 0);

            this.range = range;

            //InitializeComponent();

            weekNo = createStandardTextBox(PhoenixOTSheetDims.WeekInput.TopLeft, LARGE_FONT_SIZE);
            PageTools.CentreTextBox(weekNo, PhoenixOTSheetDims.WeekInput);

            date = createStandardTextBox(PhoenixOTSheetDims.GlobalDate.TopLeft, LARGE_FONT_SIZE);
            PageTools.CentreTextBox(date, PhoenixOTSheetDims.GlobalDate);

            FillDateAndWeek();
            FillName(name);

            FillIndividualDates();

            CreateLabelGrid(ref firstWeek, PhoenixOTSheetDims.TableOneStart, PhoenixOTSheetDims.CellSize,
                PhoenixOTSheetDims.ColumnsPerGrid, PhoenixOTSheetDims.RowsPerGrid, PhoenixOTSheetDims.BorderWidth);
            CreateLabelGrid(ref secondWeek, PhoenixOTSheetDims.TableTwoStart, PhoenixOTSheetDims.CellSize,
                PhoenixOTSheetDims.ColumnsPerGrid, PhoenixOTSheetDims.RowsPerGrid, PhoenixOTSheetDims.BorderWidth);

            addDataToGrid(ref firstWeek, 0);
            addDataToGrid(ref secondWeek, 7);
        }

        private void setAllBorders(BorderStyle b)
        {
            weekNo.BorderStyle = b;
            date.BorderStyle = b;
            name.BorderStyle = b;

            for (int n = 0; n < firstWeekDates.Length; ++n)
            {
                firstWeekDates[n].BorderStyle = b;
                secondWeekDates[n].BorderStyle = b;
            }
            for (int r = 0; r < PhoenixOTSheetDims.RowsPerGrid; ++r)
            {
                for (int c = 0; c < PhoenixOTSheetDims.ColumnsPerGrid; ++c)
                {
                    firstWeek[r, c].BorderStyle = b;
                    secondWeek[r, c].BorderStyle = b;
                }
            }
        }
        public override void PrepareForPrinting()
        {
            setAllBorders(BorderStyle.None);
        }
    }
}
