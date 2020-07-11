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
    public partial class date_name_getter : Form
    {
        public int Number { private set; get; }
        public string Name { private set; get; }

        public date_name_getter(int min, int max, int year)
        {
            InitializeComponent();
            button1.Enabled = false;
            nud_Date.Value = year;
            nud_Date.Minimum = min;
            nud_Date.Maximum = max;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Nud_Date_ValueChanged(object sender, EventArgs e)
        {
            this.Number = (int)((NumericUpDown)sender).Value;
        }

        private void Tb_Name_TextChanged(object sender, EventArgs e)
        {
            this.Name = ((TextBox)sender).Text;
            if(!String.IsNullOrEmpty(this.Name) && !String.IsNullOrWhiteSpace(this.Name))
            {
                button1.Enabled = true;
            } 
        }
    }
}
