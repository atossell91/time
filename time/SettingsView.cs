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
    public partial class SettingsView : Form
    {
        public bool SplitSaturday = true;
        public bool RoundOT = true;
        public void InitControls()
        {
            if (SplitSaturday)
            {
                rb_Split_True.Checked = true;
            }
            else
            {
                rb_Split_False.Checked = true;
            }

            if (RoundOT)
            {
                rb_Round_True.Checked = true;
            }
            else
            {
                rb_Round_False.Checked = true;
            }
        }
        public SettingsView()
        {
            InitializeComponent();
            InitControls();
        }
        public SettingsView(Settings s)
        {
            InitializeComponent();

            this.SplitSaturday = s.SplitSaturday;
            this.RoundOT = s.RoundOT;

            InitControls();
        }
        public Settings getSettings()
        {
            Settings s = new Settings();
            s.SplitSaturday = SplitSaturday;
            s.RoundOT = RoundOT;
            return s;
        }

        private void Rb_Split_True_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            SplitSaturday = rb.Checked;
        }

        private void Rb_Round_True_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            RoundOT = rb.Checked;
        }
    }
}
