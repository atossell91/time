using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{

    public partial class DateRangeGetter : Form
    {
        public DateTime RangeStartDate { get; private set; }
        public DateTime RangeEndDate { get; private set; }

        public DateTime GetFirstDayOfMonth(DateTime now)
        {
            return new DateTime(now.Year, now.Month, 1);
        }
        public DateTime GetLastDayOfMonth(DateTime now)
        {
            int lastDay = DateTime.DaysInMonth(now.Year, now.Month);
            return new DateTime(now.Year, now.Month, lastDay);
        }
        public DateRangeGetter()
        {
            InitializeComponent();
            dateTimePicker1.Value = GetFirstDayOfMonth(DateTime.Now);
            dateTimePicker2.Value = GetLastDayOfMonth(DateTime.Now);
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            RangeStartDate = dtp.Value;
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            RangeEndDate = dtp.Value;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
