using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class Form1 : Form
    {
        Exception PeriodNotFoundException;
        private const int DAY_CUTOFF = 35;
        private readonly WorkingDirectory MainDir;

        private List<work_period> wp;
        private int currentYear;
        private int currentMonth;
        private readonly string filename;
        //private row_interface_group displayedRows;
        //private row_interface_group row_interface_group1;
        private PersonalInfo personInfo;
        private bool splitSunday = false;

        private List<work_period> loadFromFile(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
            {
                return new List<work_period>();
            }

            List<work_period> p = new List<work_period>();
            string[] lines = System.IO.File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                work_period period;
                if (work_period.TryParse(line, out period))
                {
                    p.Add(period);
                }
                else
                {
                    Debug.WriteLine("Line parse failed: " + line);
                }
            }

            p.Sort(work_period.CompareByDate());
            //sortWorkPeriods(compareWorkPeriodDates);
            return p;
        }
        private List<string> getDataToSave()
        {
            List<string> list = new List<string>();
            foreach (work_period p in wp)
            {
                if (p.StartTime != DateTime.MinValue && p.EndTime != DateTime.MinValue)
                {
                    list.Add(p.ToString());
                }
            }
            return list;
        }
        private void saveToFile(string filepath)
        {
            wp.Sort(work_period.CompareByDate());
            Debug.WriteLine("Saving " + wp.Count + " records.");

            string outputfile = MainDir.DirectoryPath + "\\" + filename;
            System.IO.File.WriteAllLines(outputfile, getDataToSave());
        }
        private int getFiscalYear(int yr)
        {
            DateTime now = DateTime.Now;
            int year = now.Year;

            if (now < new DateTime(year, 4, 1))
            {
                --year;
            }
            return year;
        }
        private void promptForYear()
        {
            DateAndInfoSplash splash = new DateAndInfoSplash();
            splash.ShowDialog();

            this.currentYear = splash.Year;
            this.personInfo = splash.PersonalInfo;

            splash.Dispose();
        }
        private void logProgramUse()
        {
            string name = personInfo.GivenNames + " " + personInfo.Surname;
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            try
            {
                File.AppendAllText("usage_log.txt", date + "," + name + Environment.NewLine);
            }
            catch (Exception e) { }
        }
        public string dataFileNameFromYear(int year)
        {
            return "data_" + year.ToString() + ".txt";
        }
        public List<work_period> loadPeriodsByYear(int year)
        {
            string filename = dataFileNameFromYear(year);
            return loadFromFile(MainDir.DirectoryPath + "\\" + filename);
        }
        public Form1()
        {
            if (WorkingDirectory.RootDirectory() == "")
            {
                MessageBox.Show("No valid directories.", "Nowhere to go", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            InitializeComponent();

            MainDir = new WorkingDirectory("Time_Tracking");
            if (!Directory.Exists(MainDir.DirectoryPath))
            {
                Debug.WriteLine("Creating directory at: " + MainDir.DirectoryPath);
                Directory.CreateDirectory(MainDir.DirectoryPath);
            }
            else
            {
                Debug.WriteLine("Directory found at: " + MainDir.DirectoryPath);
            }

            wp = new List<work_period>();

            promptForYear();
            logProgramUse();

            this.filename = dataFileNameFromYear(this.currentYear);
            wp = loadFromFile(MainDir.DirectoryPath + "\\" + filename);

            currentMonth = DateTime.Now.Month;

            row_interface_group1.LeaveTimeBox += onLeaveTimeBox;

            row_interface_group1.Year = this.currentYear;

            row_interface_group1.Month = this.currentMonth;

            row_interface_group1.WorkPeriodData = wp;


            /*
            displayedRows = new row_interface_group(currentYear, currentMonth, wp);
            displayedRows.Location = new Point(10, 40);
            displayedRows.Size = new Size(this.Width-50, this.Height-130);
            displayedRows.BorderStyle = BorderStyle.Fixed3D;
            displayedRows.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(displayedRows);
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveToFile(MainDir.DirectoryPath + "\\" + @"data.txt");
        }
        private string monthName(int month)
        {
            int num = month - 1;

            if (num < 1 || num >11)
            {
                return "";
            }
            string[] months = {"January", "February", "March",
            "April", "May", "June", "July", "August",
            "September", "October", "November", "December"};

            return months[num];
        }
        private void changeMonth(int month)
        {
            currentMonth = month;
            row_interface_group1.selectMonth(currentMonth);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            row_interface_group1.ClearRow();
        }
        private DateTime getRangeStart(DateTime date)
        {
            int weekNum = WeekNumber.GetWeekNumber(date);

            if (weekNum % 2 == 0)
            {
                --weekNum;
            }

            return WeekNumber.GetDateFromWeek(date.Year, weekNum);
        }
        private List<work_period> getRange(DateTime rangeStart, int numDays)
        {
            List<work_period> outputRange = new List<work_period>();

            for (int n = 0; n < numDays; ++n)
            {
                DateTime d = rangeStart.AddDays(n);
                
                work_period dateToFind = new work_period(d);
                int dateIndex = wp.BinarySearch(dateToFind, work_period.CompareByDate());

                if (dateIndex < 0)
                {
                    outputRange.Add(new work_period(d));
                }
                else
                {
                    outputRange.Add(wp[dateIndex]);
                }
            }

            return outputRange;
        }
        private OvertimeInformation createOvertimeDataSheet(List<PremiumCode> codes, DateTime startDate)
        {
            OvertimeInformation oti = new OvertimeInformation(startDate);

            foreach(PremiumCode c in codes)
            {
                OvertimeInformation.day day = oti.getDayFromDate(c.StartDate);

                if (day != null)
                {
                    day.AddCode(c.Code, c.Hours.TotalHours.ToString());
                }
            }

            return oti;
        }
        private void outputToTimesheet(DateTime date)
        {
            //DateTime date = DateTime.Now;

            DateTime rangeStart = getRangeStart(date);
            List<work_period> DayRange = getRange(rangeStart.AddDays(-1.0), 15);

            String name = this.personInfo.GivenNames + " " + this.personInfo.Surname;

            List<PremiumCode> outCodes = CodeChecker.CheckCodes(DayRange, splitSunday);
            createOvertimeDataSheet(outCodes, date);

            Debug.WriteLine(outCodes.Count + " codes found.");

            Sheet_PhoenixOT sheet = new Sheet_PhoenixOT(personInfo, outCodes, rangeStart, wp);
            List<Sheet> sheets = new List<Sheet>();
            sheets.Add(sheet);

            SheetViewer sv = new SheetViewer(sheets);
            sv.ShowDialog();
        }
        private void setWashupLeaveHours(List<data_4600> dataSheets)
        {
            double totalHours = 0.0;
            foreach (data_4600 d4 in dataSheets)
            {
                totalHours += d4.GetCashHours();
                totalHours += d4.GetLeaveHours();
            }

            double requestedLeave = 0.0;
            GetLeaveOrCashHours loc = new GetLeaveOrCashHours(totalHours, 0.0);
            if (loc.ShowDialog() == DialogResult.OK)
            {
                requestedLeave = loc.LeaveHours;
            }

            loc.Dispose();

            foreach (data_4600 d4 in dataSheets)
            {
                double avHours = d4.GetCashHours() + d4.GetLeaveHours();
                if (avHours >= requestedLeave)
                {
                    d4.RequestLeaveHours(requestedLeave);
                    requestedLeave = 0.0;
                }
                else
                {
                    d4.RequestLeaveHours(avHours);
                    requestedLeave -= avHours;
                }

                if (requestedLeave <= 0)
                {
                    break;
                }
            }
        }
        private List<data_4600> createWashup4600(DateTime d1, DateTime d2)
        {
            int days = (int)d2.Subtract(d1).TotalDays +1; //Added one to correct for last day not appearing

            List<work_period> periodCovered = getRange(d1, days);

            List<data_4600> dataSheets = new List<data_4600>();
            dataSheets.Add(new data_4600());

            List<PremiumCode> codes = new List<PremiumCode>(
                CodeChecker.CheckCodes(periodCovered, splitSunday).Where((x) => x.Code == CodeChecker.Code290.DEFAULT_CODE)
                );

            int rowCount = 0;
            int sheetCount = 1;
            foreach(PremiumCode c in codes)
            {
                if (rowCount > 15)
                {
                    dataSheets.Add(new data_4600());
                    ++sheetCount;
                    rowCount = 0;
                }

                string washupMessage;

                if (c.StartDate.DayOfWeek == DayOfWeek.Saturday ||
                    c.StartDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    washupMessage = "Art 60 - Washup - Wknd OT";
                }
                else
                {
                    washupMessage = "Art 60 - Washup";
                }

                dataSheets[sheetCount - 1].FillNewRow(
                    c.StartDate, c.EndDate, ShiftInformation.LunchLength, CodeChecker.Code290.DEFAULT_CODE,
                    c.GetArrayHours(PremiumCode.HoursMultiplier.X100).TotalHours,
                    c.GetArrayHours(PremiumCode.HoursMultiplier.X150).TotalHours,
                    c.GetArrayHours(PremiumCode.HoursMultiplier.X175).TotalHours,
                    c.GetArrayHours(PremiumCode.HoursMultiplier.X200).TotalHours,
                    washupMessage);

                ++rowCount;
            }

            setWashupLeaveHours(dataSheets);

            foreach(data_4600 d4 in dataSheets)
            {
                foreach(string line in d4.exportRowInformation())
                {
                    Debug.WriteLine(line);
                }
            }

            return dataSheets;
        }
        private void b_ViewTime_Click(object sender, EventArgs e)
        {
            Number_Getter numGet = new Number_Getter(0, 53, WeekNumber.GetWeekNumber(DateTime.Now));
            numGet.ShowDialog();

            int weekNum = numGet.Number;
            DateTime date = WeekNumber.GetDateFromWeek(currentYear, weekNum);

            outputToTimesheet(date);
        }
        private void b_ViewWashup_Click(object sender, EventArgs e)
        {
            DateRangeGetter drg = new DateRangeGetter();
            drg.ShowDialog();

            DateTime d1 = drg.RangeStartDate;
            DateTime d2 = drg.RangeEndDate;

            drg.Dispose();

            List<data_4600> dataSheets = createWashup4600(d1, d2);
            List <Sheet> wash = new List<Sheet>();

            foreach (data_4600 d in dataSheets)
            {
                wash.Add(new Sheet_4600(this.personInfo, d));
            }

            SheetViewer sv = new SheetViewer(wash);
            sv.ShowDialog();

            //Render_4600 viewSheet = new Render_4600(this.personInfo, sheet);
            //viewSheet.ShowDialog();
        }
        private void tsm_AddShortcut_Click(object sender, EventArgs e)
        {
        }
        private void addCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comment_Creator cc = new Comment_Creator(personInfo);
            cc.ShowDialog();
            cc.Dispose();
        }
        private void viewStatuatoryHolidaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yearly_Holiday_Viewer yhv = new Yearly_Holiday_Viewer(this.currentYear);
            yhv.ShowDialog();
        }
        private work_period searchForPrevDay(List<work_period> days, DateTime start)
        {
            int startInd = days.FindIndex((a) =>
            {
                return a.Date == start;
            });
            for (int n = startInd -1; n > 0; --n)
            {
                if (days[n].StartTime != DateTime.MinValue && days[n].EndTime != DateTime.MinValue)
                {
                    return days[n];
                }
            }
            return null;
        }
        private TimeSpan findCarryMins(List<work_period> startData, DateTime day, int dayCutOff)
        {
            DateTime cutDate = DateTime.Now.AddDays(-dayCutOff);

            //Assume that the array is sorted by date -- may need to sort here
            work_period prevDay = searchForPrevDay(startData, day);
            if (prevDay != null)
            {
                if (prevDay.Date >= cutDate)
                {
                    return prevDay.CumulativeMins;
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }

            List<work_period> prevYr = loadPeriodsByYear(day.Year - 1);
            //Search for day in previous year
            prevDay = searchForPrevDay(prevYr, new DateTime(this.currentYear - 1, 12, 31));

            if (prevDay == null || prevDay.Date < cutDate)
            {
                return TimeSpan.Zero;
            }
            else
            {
                return prevDay.CumulativeMins;
            }
        }
        private void calcCumulMins(work_period p)
        {
            //  If date modified is more than 35 days ago, do nothing
            DateTime day = p.Date;
            if (day < DateTime.Now.AddDays(-DAY_CUTOFF))
            {
                return;
            }

            //  Calculate extra minutes
            TimeSpan diff = ShiftInformation.CalcExtraTime(p.StartTime, p.EndTime);
            diff = diff < TimeSpan.Zero ? TimeSpan.Zero : diff;

            //  Find cumulative minutes from before
            TimeSpan cumulMins = diff.Add(findCarryMins(wp, p.Date, DAY_CUTOFF));

            //  Set flag and reset minutes if >15
            if (cumulMins.TotalMinutes >= 15)
            {
                p.AddCumulativeOT = true;
                cumulMins = cumulMins.Subtract(new TimeSpan(0, 15, 0));
            }
            else
            {
                p.AddCumulativeOT = false;
            }

            p.CumulativeMins = cumulMins;
        }
        private void calculateMinutes(work_period period)
        {
            int index = wp.BinarySearch(period, work_period.CompareByDate());

            if (index < 0)
            {
                throw (PeriodNotFoundException);
            }

            work_period p = wp[index];
            while (index < wp.Count && p.Date < DateTime.Now.AddDays(DAY_CUTOFF))
            {
                p = wp[index];
                calcCumulMins(p);
                ++index;
            }
            calcCumulMins(period);
        }
        private void onLeaveTimeBox(object sender, RowInterfaceEventArgs e)
        {
            calculateMinutes(e.Period);
        }
    }
}
