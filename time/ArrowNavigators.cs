using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace time
{
    public partial class ArrowNavigators : UserControl
    {
        public EventHandler LeftArrowClicked;
        public EventHandler RightArrowClicked;

        public static readonly int DEFAULT_MINMAX_VALUE = 1;

        public new bool Enabled;

        private int navIndex;
        public int NavigationIndex
        {
            get
            {
                return this.navIndex;
            }

            set
            {
                this.navIndex = value;
                this.nud_NavIndex.Value = this.navIndex;
            }
        }

        private int maxNavValue;
        public int MaxNavValue
        {
            get
            {
                return this.maxNavValue;
            }
            set
            {
                this.maxNavValue = value;
                nud_NavIndex.Maximum = this.maxNavValue;
            }
        }

        private int minNavValue;
        public int MinNavValue
        {
            get
            {
                return this.minNavValue;
            }
            set
            {
                this.minNavValue = value;
                nud_NavIndex.Minimum = this.minNavValue;
            }
        }

        public ArrowNavigators()
        {
            InitializeComponent();
            this.nud_NavIndex.Controls[0].Visible = false;

            this.NavigationIndex = DEFAULT_MINMAX_VALUE;
            this.MinNavValue = DEFAULT_MINMAX_VALUE;
            this.MaxNavValue = DEFAULT_MINMAX_VALUE;
        }

        private int calcNavIndex(int amt)
        {
            int sum = this.NavigationIndex + amt;
            
            if (sum < this.MinNavValue)
            {
                return this.MaxNavValue;
            }
            else if (sum > this.MaxNavValue)
            {
                return this.MinNavValue;
            }
            else
            {
                return sum;
            }
        }
        private void B_Left_Click(object sender, EventArgs e)
        {
            this.NavigationIndex = calcNavIndex(-1);
            LeftArrowClicked?.Invoke(this, e);
        }

        private void B_Right_Click(object sender, EventArgs e)
        {
            this.NavigationIndex = calcNavIndex(1);
            RightArrowClicked?.Invoke(this, e);
        }

        private void Tb_NavIndex_TextChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            this.navIndex = (int)nud.Value;
        }
    }
}
