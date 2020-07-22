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
        private string employee_name;

        private void swap<T>(List<T> l, int i1, int i2)
        {
            T temp = l[i1];
            l[i1] = l[i2];
            l[i2] = temp;
        }
        private bool compareWorkPeriodDates(work_period p1, work_period p2)
        {
            return p1.Date < p2.Date;
        }
        private void sortWorkPeriods (Func<work_period, work_period, bool> compare)
        {
            for (int n =0; n < wp.Count; ++n)
            {
                for (int m = n; m > 1 && compare(wp[m], wp[m-1]); --m)
                {
                    swap(wp, m, m-1);
                }
            }
        }
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

            sortWorkPeriods(compareWorkPeriodDates);
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
            sortWorkPeriods(compareWorkPeriodDates);
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
            date_name_getter d = new date_name_getter(1990, 2200, DateTime.Now.Year);
            d.ShowDialog();

            int year = getFiscalYear(d.Number);

            currentYear = d.Number;
            this.employee_name = d.Name;

            d.Dispose();
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
        private int findPeriodByDate(DateTime d, bool exactMatch)
        {
            sortWorkPeriods(compareWorkPeriodDates);
            for (int n =0; n < wp.Count; ++n)
            {
                if (wp[n].Date >= d)
                {
                    if (!exactMatch)
                    {
                        return n;
                    }
                    else if (wp[n].Date == d)
                    {
                        return n;
                    }
                }
            }
            return -1;
        }
        private List<work_period> getRange(DateTime rangeStart, int numDays)
        {
            List<work_period> outputRange = new List<work_period>();

            for (int n = 0; n < numDays; ++n)
            {
                DateTime d = rangeStart.AddDays(n);
                int dateIndex = findPeriodByDate(d, true);

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
            List<work_period> DayRange = getRange(rangeStart, 14);
            PhoenixOTSheet sheet = new PhoenixOTSheet(employee_name, DayRange);
            sheet.ShowDialog();
        }
        private data_4600 createWashup4600(DateTime d1, DateTime d2)
        {
            int days = (int)d2.Subtract(d1).TotalDays;
            List<work_period> periodCovered = getRange(d1, days);

            double[] hours = { 0, 0.167, 0, 0 };
            string washupCode = "155";

            data_4600 sheet = new data_4600();

            foreach (work_period p in periodCovered)
            {
                if (p.WashupTime)
                {
                    sheet.AddRow(p.StartTime, p.EndTime, ShiftInformation.LunchLength,
                        washupCode, hours);
                }
            }
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

            PersonalInfo pi = new PersonalInfo();
            Render_4600 viewSheet = new Render_4600(pi, sheet);
            viewSheet.ShowDialog();
        }
    }
}
