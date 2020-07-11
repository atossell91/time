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
    public partial class Number_Getter : Form
    {
        public int Number { get; private set; }
        public Number_Getter(int min, int max, int defaultValue)
        {
            InitializeComponent();
            nud_Number.Value = new Decimal(defaultValue);
            nud_Number.Minimum = min;
            nud_Number.Maximum = max;
        }

        private void Nud_Number_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            Number = (int)nud.Value;
        }

        private void B_Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
