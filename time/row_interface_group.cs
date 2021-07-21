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
    public partial class row_interface_group : UserControl
    {
        public event RowInterfaceEvent LeaveTimeBox;
        private int year;
        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
                l_Year.Text = this.year.ToString();
            }
        }
        private int month;
        public int Month
        {
            get
            {
                return this.month;
            }
            set
            {
                this.month = value;
                cb_Month.SelectedIndex = this.month-1;
            }
        }

        private List<work_period> periods;
        public List<work_period> WorkPeriodData
        {
            set
            {
                this.periods = value;
                selectMonth(this.month);
            }
        }
        private List<row_interface> rowList;

        private int maxHeaderHeight;

        private int selectedRow;

        public event KeyEventHandler KeyDownEvent;
        private void initRows()


        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            //Debug.WriteLine("ACTUAL DATE: " + periods[periods.Count - 1]);
            for (int n =0; n < daysInMonth; ++n)
            {
                DateTime day = new DateTime(year, month, n + 1);

                //Check if work period exists already
                //  Init row with period if it exists already, or create a new period
                int periodIndex = periods.BinarySearch(new work_period(day), work_period.CompareByDate());

                if (periodIndex >= 0)
                {
                    work_period p = periods[periodIndex];
                    //periods.Add(p);
                    rowList.Add(new row_interface(p));
                }
                else
                {
                    periods.Add(new work_period(day));
                    rowList.Add(new row_interface(periods[periods.Count-1]));
                }
                periods.Sort(work_period.CompareByDate());
                //Set row properties and events
                //rowList[n].KeyDownEvent += row_interface_group_KeyDown;
                rowList[n].Location = new Point(0, n * row_interface.rowHeight);
                rowList[n].VerticalArrowDown += ControlVerticalArrowPressed;
                rowList[n].InputRowSelected += inputRowSelected;
                rowList[n].LeaveTimeBoxEvent += onLeaveTimeBox;
                panel1.Controls.Add(rowList[n]);
            }
        }
        private void selectFirstRow()
        {
            if (rowList == null || rowList.Count < 1)
            {
                return;
            }
            selectedRow = 0;
            rowList[selectedRow].selectedControl = 1;

            rowList[selectedRow].currentControl[rowList[selectedRow].selectedControl].Focus();
        }
        private void checkHeaderHeight(Label l)
        {
            if (l.Height > maxHeaderHeight)
            {
                maxHeaderHeight = l.Height;
            }
        }
        private void createHeaders()
        {
            row_interface dummy = new row_interface(new work_period(DateTime.Now));
            maxHeaderHeight = 0;

            int maxHeaderWidth = 80;
            int fontSize = 10;
            string font = "Arial";
            int yoffset = 0;
            int xoffset = 0;

            Label lh_dayOfMonth = new Label();
            lh_dayOfMonth.AutoSize = true;
            lh_dayOfMonth.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_dayOfMonth.Text = "Day of Month";
            lh_dayOfMonth.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_dayOfMonth.Location = new Point(dummy.Location_DayOfMonth + xoffset, yoffset);
            Controls.Add(lh_dayOfMonth);
            checkHeaderHeight(lh_dayOfMonth);

            Label lh_WeekNumber = new Label();
            lh_WeekNumber.AutoSize = true;
            lh_WeekNumber.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_WeekNumber.Text = "Week Number";
            lh_WeekNumber.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_WeekNumber.Location = new Point(dummy.Location_WeekNum + xoffset, yoffset);
            Controls.Add(lh_WeekNumber);
            checkHeaderHeight(lh_WeekNumber);

            Label lh_StartTime = new Label();
            lh_StartTime.AutoSize = true;
            lh_StartTime.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_StartTime.Text = "Start Time";
            lh_StartTime.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_StartTime.Location = new Point(dummy.Location_StartTime + xoffset, yoffset);
            Controls.Add(lh_StartTime);
            checkHeaderHeight(lh_StartTime);

            Label lh_EndTime = new Label();
            lh_EndTime.AutoSize = true;
            lh_EndTime.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_EndTime.Text = "End Time";
            lh_EndTime.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_EndTime.Location = new Point(dummy.Location_EndTime + xoffset, yoffset);
            Controls.Add(lh_EndTime);
            checkHeaderHeight(lh_EndTime);

            Label lh_Overtime = new Label();
            lh_Overtime.AutoSize = true;
            lh_Overtime.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_Overtime.Text = "Overtime";
            lh_Overtime.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_Overtime.Location = new Point(dummy.Location_Overtime + xoffset, yoffset);
            Controls.Add(lh_Overtime);
            checkHeaderHeight(lh_Overtime);

            Label lh_ShiftPremium = new Label();
            lh_ShiftPremium.AutoSize = true;
            lh_ShiftPremium.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_ShiftPremium.Text = "Shift Premiums";
            lh_ShiftPremium.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_ShiftPremium.Location = new Point(dummy.Location_ShiftPremium + xoffset, yoffset);
            Controls.Add(lh_ShiftPremium);
            checkHeaderHeight(lh_ShiftPremium);

            Label lh_Washup = new Label();
            lh_Washup.AutoSize = true;
            lh_Washup.MaximumSize = new Size(maxHeaderWidth, 3000);
            lh_Washup.Text = "Washup Time";
            lh_Washup.Font = new Font(font, fontSize, FontStyle.Regular);
            lh_Washup.Location = new Point(dummy.Location_WashupTime + xoffset -10, yoffset);
            Controls.Add(lh_Washup);
            checkHeaderHeight(lh_Washup);
        }
        private void autoCreateHeaders()
        {
            row_interface dummy = new row_interface(new work_period(DateTime.Now));

            foreach (Control c in dummy.Controls)
            {
                Label l = new Label();
                l.Location = new Point(c.Location.X, 0);
                l.Font = new Font("Arial", 12);
                l.Text = (string)c.Tag;
                int height = l.Height;
                l.AutoSize = false;
                l.Size = new Size(c.Size.Width, height);
                l.TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(l);
                checkHeaderHeight(l);
            }
        }
        public row_interface_group()
        {
            InitializeComponent();

            periods = new List<work_period>();

            rowList = new List<row_interface>();
            rowList.Add(sample_row_inteface);
        }

        public void selectMonth(int m)
        {
            Debug.WriteLine("Selecting the month");
            month = m;
            foreach(row_interface r in rowList)
            {
                Controls.Remove(r);
                r.Dispose();
            }

            rowList.Clear();

            Debug.WriteLine("initializing new rows");
            initRows();

            selectFirstRow();
            Debug.WriteLine("Done selecting the month");
        }
        /*public row_interface_group(List<work_period> days)
        {
            InitializeComponent();
            workRecords = days;
            rowList = new List<row_interface>();

            createHeaders();
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Size = new Size(row_interface.rowWidth+20, 500);
            panel1.Location = new Point(0, maxHeaderHeight);
            initRows();
            //this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;// | AnchorStyles.Left | AnchorStyles.Right
            //panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;// | AnchorStyles.Left | AnchorStyles.Right;
        }*/
        private void forwardKeyEvent(KeyEventArgs e)
        {
            KeyEventHandler handler = KeyDownEvent;
            if (handler != null)
            {
                KeyDownEvent(this, e);
            }
        }
        private void row_interface_group_KeyDown(object sender, KeyEventArgs e)
        {
            //forwardKeyEvent(e);
        }
        private void changeSelectedRow(int direction)
        {
            int control = rowList[selectedRow].selectedControl;
            control = control >= 0 ? control : 0;

            int dir = direction / Math.Abs(direction);
            int selection = selectedRow + dir;

            selection = selection < 0 ? rowList.Count - 1 : selection;

            selectedRow = selection % rowList.Count;

            rowList[selectedRow].selectedControl = control;
            rowList[selectedRow].currentControl[control].Focus();
        }
        private void ControlVerticalArrowPressed(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Row interface group, vertical arrow");
            if (e.KeyCode == Keys.Up)
            {
                changeSelectedRow(-1);
            }
            else if (e.KeyCode == Keys.Down/* || e.KeyCode == Keys.Enter*/)
            {
                changeSelectedRow(1);
            }
        }
        private void inputRowSelected(object sender, EventArgs e)
        {
            selectedRow = rowList.IndexOf((row_interface)sender);
        }
        public void ClearRow()
        {
            rowList[selectedRow].clearRowInterface();
        }
        private void cb_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Changing selected index");
            ComboBox cmb = (ComboBox)cb_Month;
            selectMonth(cmb.SelectedIndex + 1);
            //selectFirstRow();
        }
        private void onLeaveTimeBox(object sender, RowInterfaceEventArgs e)
        {
            LeaveTimeBox?.Invoke(sender, e);
        }
    }
}
