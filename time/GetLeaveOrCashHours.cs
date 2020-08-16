using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class GetLeaveOrCashHours : Form
    {
        public double AvailableHours { get; set; }

        private bool autoSettingCash = false;
        private bool autoSettingLeave = false;

        private double cashHours;
        public double CashHours
        {
            get
            {
                return this.cashHours;
            }
            set
            {
                this.cashHours = value;

                this.nud_CashHours.Value = new Decimal(this.cashHours);

                if (!this.autoSettingCash)
                {
                    this.autoSettingLeave = true;
                    this.LeaveHours = this.AvailableHours - this.CashHours;
                    this.autoSettingLeave = false;
                }
            }
        }

        private double leaveHours;
        public double LeaveHours
        {
            get
            {
                return this.leaveHours;
            }
            set
            {
                this.leaveHours = value;

                this.nud_LeaveHours.Value = new Decimal(this.leaveHours);

                if (!this.autoSettingLeave)
                {
                    this.autoSettingCash = true;
                    this.CashHours = this.AvailableHours - this.LeaveHours;
                    this.autoSettingCash = false;
                }
            }
        }
        public GetLeaveOrCashHours(double cashHours, double leaveHours)
        {
            cashHours = Math.Round(cashHours, 14);
            leaveHours = Math.Round(leaveHours, 14);

            InitializeComponent();
            this.AvailableHours = cashHours + leaveHours;

            l_AvailableHours.Text = this.AvailableHours.ToString("N3");

            this.CashHours = cashHours;
            this.LeaveHours = leaveHours;

            this.nud_CashHours.Maximum = this.nud_LeaveHours.Maximum = new Decimal(this.AvailableHours);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Nud_CashHours_ValueChanged(object sender, EventArgs e)
        {
            if(!autoSettingCash)
            {
                NumericUpDown nud = (NumericUpDown)sender;
                this.CashHours = (double)nud.Value;
            }
        }

        private void Nud_LeaveHours_ValueChanged(object sender, EventArgs e)
        {
            if (!autoSettingLeave)
            {
                NumericUpDown nud = (NumericUpDown)sender;
                this.LeaveHours = (double)nud.Value;
            }
        }
    }
}
