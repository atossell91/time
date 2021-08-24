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
        public bool SplitSaturday;
        public bool RoundOT;
        public SettingsView()
        {
            InitializeComponent();
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
            CheckBox cb = (CheckBox)sender;
            SplitSaturday = cb.Checked;
        }

        private void Rb_Round_True_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            RoundOT = cb.Checked;
        }
    }
}
