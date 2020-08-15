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
        private readonly WorkingDirectory MainDir;

        private List<work_period> wp;
        private int currentYear;
        private int currentMonth;
        private readonly string filename;
        //private row_interface_group displayedRows;
        //private row_interface_group row_interface_group1;
        private PersonalInfo personInfo;

        private void loadFromFile(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
            {
                return;
            }

            string[] lines = System.IO.File.ReadAllLines(filepath);
            Fixer_Functions.SAVEBACKUPFILE(filepath, lines, "washup_data_type");

            foreach (string line in lines)
            {
                work_period period;
                if (work_period.TryParse(line, out period))
                {
                    wp.Add(period);
                }
                else
                {
                    Debug.WriteLine("Line parse failed: " + line);
                }
            }

            wp.Sort(work_period.CompareByDate());
            //sortWorkPeriods(compareWorkPeriodDates);
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

            this.filename = "data_" + this.currentYear.ToString() + ".txt";
            loadFromFile(MainDir.DirectoryPath + "\\" + filename);

            currentMonth = DateTime.Now.Month;

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

        private void b_April_Click(object sender, EventArgs e)
        {
            changeMonth(4);
        }

        private void b_May_Click(object sender, EventArgs e)
        {
            changeMonth(5);
        }

        private void b_June_Click(object sender, EventArgs e)
        {
            changeMonth(6);
        }

        private void b_July_Click(object sender, EventArgs e)
        {
            changeMonth(7);
        }

        private void b_August_Click(object sender, EventArgs e)
        {
            changeMonth(8);
        }

        private void b_September_Click(object sender, EventArgs e)
        {
            changeMonth(9);
        }

        private void b_October_Click(object sender, EventArgs e)
        {
            changeMonth(10);
        }

        private void b_November_Click(object sender, EventArgs e)
        {
            changeMonth(11);
        }

        private void b_December_Click(object sender, EventArgs e)
        {
            changeMonth(12);
        }

        private void b_January_Click(object sender, EventArgs e)
        {
            changeMonth(1);
        }

        private void b_February_Click(object sender, EventArgs e)
        {
            changeMonth(2);
        }

        private void b_March_Click(object sender, EventArgs e)
        {
            changeMonth(3);
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
        private void outputToTimesheet(DateTime date)
        {
            //DateTime date = DateTime.Now;

            DateTime rangeStart = getRangeStart(date);
            List<work_period> DayRange = getRange(rangeStart.AddDays(-1.0), 15);

            String name = this.personInfo.GivenNames + " " + this.personInfo.Surname;

            List<PremiumCode> outCodes = CodeChecker.CheckCodes(DayRange);

            Debug.WriteLine(outCodes.Count + " codes found.");
            foreach(PremiumCode c in outCodes)
            {
                Debug.WriteLine(c.ToString());
            }
           Sheet_PhoenixOT sheet = new Sheet_PhoenixOT(personInfo, outCodes, rangeStart);
            List<Sheet> sheets = new List<Sheet>();
            sheets.Add(sheet);

            SheetViewer sv = new SheetViewer(sheets);
            sv.ShowDialog();
        }
        private List<data_4600> createWashup4600(DateTime d1, DateTime d2)
        {
            int days = (int)d2.Subtract(d1).TotalDays +1; //Added one to correct for last day not appearing
            List<work_period> periodCovered = getRange(d1, days);


            double washupHours = 0.167;
            string washupCode = "290";
            string washupMessage = "Art 60 - Washup";

            List<data_4600> dataSheets = new List<data_4600>();
            dataSheets.Add(new data_4600());

            int rowCount = 0;
            int sheetCount = 0;
            foreach (work_period p in periodCovered)
            {
                if (p.WashupTime > TimeSpan.Zero)
                {
                    double washupMins = ShiftInformation.WashupTimeAmount.TotalMinutes;
                    if (ShiftInformation.IsRestDay(p.Date) || StatHoliday.IsStatDay(p.Date))
                    {
                        Double totalWashup = p.WashupTime.TotalMinutes;
                        double washup15, washup20 = 0.0;

                            washup15 = washupMins - totalWashup;
                            washup20 = totalWashup;

                        if (washup15 > 0.0)
                        {
                            dataSheets[sheetCount].FillNewRow(p.EndTime, p.EndTime.AddMinutes(washup15), ShiftInformation.LunchLength,
                                washupCode, 0.0, washup15 / 60.0, 0.0, 0.0, washupMessage + " Wknd OT");
                        }

                        if(washup20 > 0.0)
                        {
                            dataSheets[sheetCount].FillNewRow(p.EndTime.AddMinutes(washup15), p.EndTime.AddMinutes(washup15 + washup20), ShiftInformation.LunchLength,
                                washupCode, 0.0, 0.0, 0.0, washup20/60.0, washupMessage + " Wknd OT");
                        }

                        if (washup15 > 0.0 && washup20 > 0.0)
                        {
                            ++rowCount;
                        }
                    }
                    else
                    {
                        dataSheets[sheetCount].FillNewRow(p.EndTime, p.EndTime.AddMinutes(washupMins), ShiftInformation.LunchLength,
                            washupCode, 0.0, p.WashupTime.TotalMinutes/60.0, 0.0, 0.0, washupMessage);
                    }
                    ++rowCount;

                    if (rowCount > 15) //Might not fill last row. The row management should be handled elsewhere
                    {
                        rowCount = 0;
                        ++sheetCount;
                        dataSheets.Add(new data_4600());
                    }
                }
            }

            //Getting number of leave hours from user
            double totalHours = 0.0;
            foreach (data_4600 d4 in dataSheets)
            {
                totalHours += d4.GetCashHours();
                totalHours += d4.GetLeaveHours();
            }

            double requestedLeave = 0.0;
            Debug.WriteLine("TOTAL HOURS: " + totalHours);
            GetLeaveOrCashHours loc = new GetLeaveOrCashHours(totalHours, 0.0);
            if (loc.ShowDialog() == DialogResult.OK)
            {
                requestedLeave = loc.LeaveHours;
            }

            loc.Dispose();

            foreach(data_4600 d4 in dataSheets)
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
                
                if(requestedLeave <= 0)
                {
                    break;
                }
            }
            //End getting number of leave hours from user


            return dataSheets;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Number_Getter numGet = new Number_Getter(0, 53, WeekNumber.GetWeekNumber(DateTime.Now));
            numGet.ShowDialog();

            int weekNum = numGet.Number;
            DateTime date = WeekNumber.GetDateFromWeek(currentYear, weekNum);

            outputToTimesheet(date);
        }

        private void Button3_Click(object sender, EventArgs e)
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

        private void B_PAR_Click(object sender, EventArgs e)
        {
            Sheet_PAR par = new Sheet_PAR(personInfo);

            List<Sheet> sheets = new List<Sheet>();
            sheets.Add(par);

            SheetViewer sv = new SheetViewer(sheets);
            sv.ShowDialog();
        }
    }
}
