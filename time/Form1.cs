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
        private row_interface_group rig;


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
                list.Add(p.ToString());
            }
            return list;
        }
        private void saveToFile(string filepath)
        {
            sortWorkPeriods(compareWorkPeriodDates);
            Debug.WriteLine("Saving " + wp.Count + " records.");
            File.WriteAllLines(@"data.txt", getDataToSave());
        }
        public Form1()
        {
            InitializeComponent();

            wp = new List<work_period>();
            loadFromFile(@"data.txt");
            Debug.WriteLine(wp.Count + " periods found");

            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;
            
            rig = new row_interface_group(currentYear, currentMonth, wp);
            rig.Location = new Point(10, 10);
            rig.Size = new Size(this.Width, this.Height);
            rig.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(rig);
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
        private void changeMonth(int month)
        {
            currentMonth = month;
            rig.changeMonth(currentMonth);
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
    }
}
