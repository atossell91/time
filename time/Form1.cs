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
        private List<work_period> wp;
        private int currentYear;
        private int currentMonth;
        private readonly string filename;
        private row_interface_group displayedRows;
        private PersonalInfo personInfo;

        private void loadFromFile(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                work_period period;
                if (work_period.TryParse(line, out period))
                {
                    wp.Add(period);
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
            File.WriteAllLines(filename, getDataToSave());
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
        public Form1()
        {
            InitializeComponent();

            wp = new List<work_period>();

            promptForYear();
            filename = "data_" + currentYear + ".txt";
            loadFromFile(filename);

            currentMonth = DateTime.Now.Month;
            l_MonthName.Text = monthName(currentMonth);
            
            displayedRows = new row_interface_group(currentYear, currentMonth, wp);
            displayedRows.Location = new Point(10, 10);
            displayedRows.Size = new Size(this.Width-50, this.Height-100);
            displayedRows.BorderStyle = BorderStyle.Fixed3D;
            displayedRows.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(displayedRows);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveToFile(@"data.txt");
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
            l_MonthName.Text = monthName(currentMonth);
            displayedRows.changeMonth(currentMonth);
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
            displayedRows.ClearRow();
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
                Debug.WriteLine("Looking up " + dateToFind.Date.ToString("yyyy-MM-dd"));
                int dateIndex = wp.BinarySearch(dateToFind, work_period.CompareByDate());

                Debug.Write("Searching for " + dateToFind.Date.ToString() + ". ");
                if (dateIndex < 0)
                {
                    Debug.WriteLine("Search failed.");
                    outputRange.Add(new work_period(d));
                }
                else
                {
                    Debug.WriteLine("Search successful.");
                    outputRange.Add(wp[dateIndex]);
                }
            }

            return outputRange;
        }
        private void outputToTimesheet(DateTime date)
        {
            //DateTime date = DateTime.Now;

            DateTime rangeStart = getRangeStart(date);
            List<work_period> DayRange = getRange(rangeStart, 14);
            String name = this.personInfo.GivenNames + " " + this.personInfo.Surname;
            PhoenixOTSheet sheet = new PhoenixOTSheet(name, DayRange);
            sheet.ShowDialog();
        }
        private data_4600 createWashup4600(DateTime d1, DateTime d2)
        {
            int days = (int)d2.Subtract(d1).TotalDays;
            List<work_period> periodCovered = getRange(d1, days);


            double washupHours = 0.167;
            string washupCode = "290";
            string washupMessage = "Article 60 - Washup time";

            data_4600 sheet = new data_4600();
            foreach (work_period p in periodCovered)
            {
                if (p.WashupTime)
                {
                    sheet.FillNewRow(p.EndTime, p.EndTime.AddMinutes(10), ShiftInformation.LunchLength,
                        washupCode, 0.0, washupHours, 0.0, 0.0, washupMessage);
                }
            }
            DateTime d = new DateTime(2020, 07, 24, 7, 9, 0);
            return sheet;
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

            data_4600 sheet = createWashup4600(d1, d2);

            Render_4600 viewSheet = new Render_4600(this.personInfo, sheet);
            viewSheet.ShowDialog();
        }
    }
}
