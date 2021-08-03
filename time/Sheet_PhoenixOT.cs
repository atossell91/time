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
        private List<TextBox> textboxes;

        private TextBox weekNo;
        private TextBox date;
        private TextBox name;

        private TextBox[] firstWeekDates;
        private TextBox[] secondWeekDates;

        private TextBox[,] firstWeek;
        private TextBox[,] secondWeek;

        //private List<work_period> codes;
        private List<PremiumCode> codes;
        private DateTime startDate;
        private PersonalInfo pInfo;

        public readonly static string[] ValidCodes = {"055", "260" };

        private const int STANDARD_FONT_SIZE = 18;
        private const int LARGE_FONT_SIZE = 26;

        private const int ScaleSubtraction = 8;

        private TextBox createStandardTextBox(Point location, int fontSize)
        {// 18 and 26
            TextBox tb = new TextBox();

            tb.Location = location;
            tb.Font = new Font("Arial", fontSize);

            textboxes.Add(tb);
            this.Controls.Add(tb);

            return tb;
        }
        private void FillDateAndWeek()
        {
            if (codes == null || codes.Count == 0)
            {
                return;
            }

            DateTime start = this.startDate;
            //DateTime end = this.startDate.AddDays(14.0);

            int startWeek = WeekNumber.GetWeekNumber(start);
            int endWeek = startWeek + 1;

            weekNo.Text = startWeek.ToString() + " & " + endWeek.ToString();

            string dateFormat = "yyyy-MM-dd";
            date.Text = start.ToString(dateFormat) + " - " + start.AddDays(13.0).ToString(dateFormat);
        }
        private void FillName(string str_name)
        {
            name = createStandardTextBox(PhoenixOTSheetDims.NameInput.TopLeft, LARGE_FONT_SIZE);
            PageTools.CentreTextBox(name, PhoenixOTSheetDims.NameInput);

            name.Text = str_name;
        }
        private void fillDateRow(ref TextBox[] row, Point startPoint, DateTime startDate)
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
                tb.Text = startDate.AddDays(n).ToString("dddd, MMMM dd");
            }
        }
        private void FillIndividualDates()
        {
            int count = 7;

            firstWeekDates = new TextBox[count];
            secondWeekDates = new TextBox[count];
            Size cellSize = PhoenixOTSheetDims.CellSize;

            /*
            if (codes.Count > count * 2)
            {
                Debug.WriteLine("Range (" + codes.Count + ") exceeds column count");
                return;
            }*/

            fillDateRow(ref firstWeekDates, PhoenixOTSheetDims.TableOneDatesStart, this.startDate);
            fillDateRow(ref secondWeekDates, PhoenixOTSheetDims.TableTwoDatesStart, this.startDate.AddDays(7.0));
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
        private int findFirstDateInstance(DateTime d, List<PremiumCode> c)
        {
            int index = c.BinarySearch(new PremiumCode(String.Empty, d.Date), PremiumCode.compareByStartDate());
            Debug.WriteLine("INDEX: " + index);

            if (index < 0)
            {
                return index;
            }

            for (; index > 0 && c[index-1].StartDate.Date == d.Date; --index) ;
            Debug.WriteLine("FINAL INDEX: " + index);

            return index;
        }
        private bool isValidCode(PremiumCode c)
        {
            return Array.FindIndex(ValidCodes, (x) => x == c.Code) >= 0;
        }
        private List<PremiumCode> GetPremiumCodes(DateTime d)
        {
            List<PremiumCode> outputCodes = new List<PremiumCode>();
            codes.Sort(PremiumCode.compareByStartDateAndCode());

            int index = findFirstDateInstance(d.Date, codes);
            for (int m = index; m >= 0 && m < codes.Count && codes[m].StartDate.Date == d.Date; ++m)
            {
                if (isValidCode(codes[m]))
                {
                    outputCodes.Add(codes[m]);
                }
            }
            return outputCodes;
        }
        private void addDataToGrid(ref TextBox[,] grid, DateTime tableStartDate)
        {
            DateTime d = tableStartDate;
            for (int n = 0; n < PhoenixOTSheetDims.ColumnsPerGrid; n += 2)
            {
                int row = 0;
                List<PremiumCode> pcodes = GetPremiumCodes(d);
                Debug.WriteLine(d.ToString() + ", number of codes: " + pcodes.Count);

                foreach (PremiumCode c in pcodes)
                {
                    grid[row, n].Text = c.Hours.TotalHours.ToString();
                    grid[row, n + 1].Text = c.Code;
                    ++row;
                }
                d = d.AddDays(1.0);
            }
        }
        public Sheet_PhoenixOT(PersonalInfo info, List<PremiumCode> range, DateTime startDate)
        {
            this.pInfo = info;
            this.startDate = startDate;

            textboxes = new List<TextBox>();

            this.IsLandscape = true;
            Debug.WriteLine("PAPER TYPE: " + this.PaperType.ToString());
            this.PaperType = System.Drawing.Printing.PaperKind.Letter;

            this.Image = time.Properties.Resources.Phoenix_Inspectors_Weekly_OT_Template_With_Blank_Form;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Location = new Point(0, 0);

            this.codes = range;

            //InitializeComponent();

            weekNo = createStandardTextBox(PhoenixOTSheetDims.WeekInput.TopLeft, LARGE_FONT_SIZE);
            PageTools.CentreTextBox(weekNo, PhoenixOTSheetDims.WeekInput);

            date = createStandardTextBox(PhoenixOTSheetDims.GlobalDate.TopLeft, LARGE_FONT_SIZE);
            PageTools.CentreTextBox(date, PhoenixOTSheetDims.GlobalDate);

            FillDateAndWeek();
            FillName(pInfo.GivenNames + " " + pInfo.Surname);

            FillIndividualDates();

            CreateLabelGrid(ref firstWeek, PhoenixOTSheetDims.TableOneStart, PhoenixOTSheetDims.CellSize,
                PhoenixOTSheetDims.ColumnsPerGrid, PhoenixOTSheetDims.RowsPerGrid, PhoenixOTSheetDims.BorderWidth);
            CreateLabelGrid(ref secondWeek, PhoenixOTSheetDims.TableTwoStart, PhoenixOTSheetDims.CellSize,
                PhoenixOTSheetDims.ColumnsPerGrid, PhoenixOTSheetDims.RowsPerGrid, PhoenixOTSheetDims.BorderWidth);

            addDataToGrid(ref firstWeek, startDate);
            addDataToGrid(ref secondWeek, startDate.AddDays(7.0));

        }
        public void scaleTextBoxFont(float scaleFactor)
        {
            scaleFactor /= 2;
            foreach (TextBox t in textboxes)
            {
                t.Font = new Font(t.Font.FontFamily.Name, (int)(t.Font.Height * scaleFactor));
            }
        }

        private void Paint(object sender, PaintEventArgs e)
        {
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
        public override string[] ExportData()
        {
            List<string> output = new List<string>();
            foreach (PremiumCode c in this.codes)
            {
                output.Add(c.ToString());
            }
            return output.ToArray();
        }
    }
}
