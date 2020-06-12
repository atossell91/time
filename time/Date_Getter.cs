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
    public partial class Date_Getter : Form
    {
        public int Year { get; private set; }
        private int getYear()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;

            if (now < new DateTime(year, 4, 1))
            {
                --year;
            }
            return year;
        }
        public Date_Getter()
        {
            InitializeComponent();
            nud_Year.Value = new Decimal(getYear());
        }

        private void Nud_Year_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            Year = (int)nud.Value;
        }

        private void B_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
