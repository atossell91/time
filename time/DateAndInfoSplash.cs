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
    public partial class DateAndInfoSplash : Form
    {
        public int Year;
        public PersonalInfo PersonalInfo
        {
            get
            {
                return personalnfoGetter1.Info;
            }
        }
        public DateAndInfoSplash()
        {
            InitializeComponent();

            this.Year = DateTime.Now.Year;
            numericUpDown1.Value = this.Year;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            this.Year = (int)nud.Value;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            personalnfoGetter1.SavePersonalInfoToFile();
            this.Close();
        }
    }
}
