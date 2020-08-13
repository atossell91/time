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
    public partial class Yearly_Holiday_Viewer : Form
    {
        public Yearly_Holiday_Viewer(int year)
        {
            InitializeComponent();
            List<DateTime> dates = StatHoliday.GetAllHolidays(year);

            string f = "dddd, MMMM dd";
            l_NewYear.Text = dates[0].ToString(f);
            l_GoodFriday.Text = dates[1].ToString(f);
            l_Easter.Text = dates[2].ToString(f);
            l_VictoriaDay.Text = dates[3].ToString(f);
            l_CanadaDay.Text = dates[4].ToString(f);
            l_HeritageDay.Text = dates[5].ToString(f);
            l_LabourDay.Text = dates[6].ToString(f);
            l_Thanksgiving.Text = dates[7].ToString(f);
            l_RemembranceDay.Text = dates[8].ToString(f);
            l_Christmas.Text = dates[9].ToString(f);
            l_BoxingDay.Text = dates[10].ToString(f);
        }
    }
}
