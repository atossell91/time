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
    public partial class row_interface : UserControl
    {
        private event KeyEventHandler HorizontalArrowDown;
        public event KeyEventHandler VerticalArrowDown;
        public event EventHandler InputRowSelected;
        public event EventHandler NewEntryAdded;
        public event MouseEventHandler MouseWheelScroll;

        public delegate void RowInterfaceEvent(object sender, RowInterfaceEventArgs e);
        public event RowInterfaceEvent LeaveTimeBoxEvent;

        public const int rowHeight = 50;
        public const int rowWidth = 1200;

        private readonly work_period period;

        public readonly DateTime Date;

        public List<Control> currentControl;
        public int selectedControl;
        public static int RowID { private set; get; }

        private Point cursorPos;

        private bool keyPressed = false;

        private readonly Color weekDayColor = Color.Transparent;
        private readonly Color sundayColor = Color.LightGray;

        private void initControlsList()
        {
            currentControl.Add(mtb_Start);
            currentControl.Add(mtb_End);
            //currentControl.Add(nud_overtime);
            //currentControl.Add(nud_premiums);
            //currentControl.Add(cb_Washup);
            currentControl.Add(rtb_Comment);
        }

        public row_interface() : this(new work_period(DateTime.Now))
        {
        }
        public row_interface(work_period p)
        {
            ++RowID;
            this.Date = p.Date;
            InitializeComponent();
            this.MouseWheel += row_interface_MouseWheel;
            nud_overtime.MouseWheel += nud_MouseWheel;
            nud_premiums.MouseWheel += nud_MouseWheel;
            this.HorizontalArrowDown += Parent_KeyDown;
            currentControl = new List<Control>();
            initControlsList();

            period = p;
            DateTime date = period.Date;
            if (period.StartTime > DateTime.MinValue && period.EndTime > DateTime.MinValue)
            {
                changeStartTime(period.StartTime);
                changeEndTime(period.EndTime);
            }

            l_DayOfWeek.Text = date.DayOfWeek.ToString();

            DateTime min = DateTime.MinValue;
            if (p.StartTime == min || p.EndTime == min)
            {
                showNuds(false);
            }
            else
            {
                showNuds(true);
            }
            applyWeekNum();

            this.nud_overtime.Value = new decimal(p.Overtime.TotalHours);
            this.nud_premiums.Value = new decimal(p.ShiftPremiums.TotalHours);
            this.cb_Washup.Checked = p.WashupTime > TimeSpan.Zero;
            this.rtb_Comment.Text = p.Comment;

            if (p.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                this.BackColor = this.sundayColor;
            }
            else
            {
                this.BackColor = this.weekDayColor;
            }
        }
        private void showNuds(bool flag)
        {
            nud_overtime.Enabled = flag;
            nud_premiums.Enabled = flag;
        }
        private void applyWeekNum()
        {
            DateTime date = period.Date;
            int weekNum = WeekNumber.GetWeekNumber(date);
            l_dayOfMonth.Text = date.Day.ToString();

            if (date.Day == 1 || date.DayOfWeek == 0 || (string)this.Tag == "debug")
            {
                l_weekNum.Text = weekNum.ToString();
            }
            else
            {
                l_weekNum.Text = "";
            }
        }

        private bool cursorIsInbounds(Point p)
        {
            int b = 4; // Buffer zone for the ends to ensure that the cursor going out of bounds is detected

            int x = p.X;
            int y = p.Y;

            return (x-b > 0 && x+b < this.Width && y-b > 0 && y+b < this.Height);
        }
        private void changeBackgroundColour(bool isHighlighted)
        {
            if (isHighlighted)
            {
                this.BackColor = Color.LightBlue;
            }
            else if (this.period.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                this.BackColor = this.sundayColor;
            }
            else
            {
                this.BackColor = this.weekDayColor;
            }
        }
        private void row_interface_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            cursorPos = e.Location;
            if (cursorIsInbounds(e.Location))
            {
                changeBackgroundColour(true);
            }
            else
            {
                changeBackgroundColour(false);
            }
            //*/
        }

        private void changeStartTime(DateTime t)
        {
            period.StartTime = t;
            if (t != DateTime.MinValue)
            {
                mtb_Start.Text = period.StartTime.ToString("HH:mm");
            }
            else
            {
                showNuds(false);
                mtb_Start.Text = "";
            }
        }
        private void changeEndTime(DateTime t)
        {
            period.EndTime = t;
            if (t != DateTime.MinValue)
            {
                mtb_End.Text = period.EndTime.ToString("HH:mm");
            }
            else
            {
                showNuds(false);
                mtb_End.Text = "";
            }
        }
        private void changeOvertime(TimeSpan t)
        {
            period.Overtime = t;
            nud_overtime.Value = new decimal(period.Overtime.TotalHours);
        }
        private void changePremiums(TimeSpan t)
        {
            period.ShiftPremiums = t;
            nud_premiums.Value = new decimal(period.ShiftPremiums.Hours);
        }
        private void changeWashupTime(TimeSpan t)
        {
            period.WashupTime = t;
            cb_Washup.Checked = period.WashupTime > TimeSpan.Zero;
        }
        private void changeComment(string txt)
        {
            period.Comment = txt;
            rtb_Comment.Text = period.Comment;
        }
        public void clearRowInterface()
        {
            changeStartTime(DateTime.MinValue);
            changeEndTime(DateTime.MinValue);
            changeOvertime(TimeSpan.Zero);
            changePremiums(TimeSpan.Zero);
            changeWashupTime(TimeSpan.Zero);
            changeComment("");
        }
        private TimeSpan decimalToTimeSpan(double num)
        {
            int hours = (int)num;
            int minutes = (int)((num - hours)*60);

            return new TimeSpan(hours, minutes, 0);
        }
        private TimeSpan calcOvertime()
        {
            return ShiftInformation.LockTimeToInterval(
                ShiftInformation.CalcOvertime(period.StartTime, period.EndTime));
        }
        private TimeSpan calcShiftPremium()
        {
            return ShiftInformation.LockTimeToInterval(
                ShiftInformation.CalcShiftPremium(period.StartTime, period.EndTime));
        }
        private void nud_overtime_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            if (period.EndTime == DateTimePicker.MinimumDateTime && period.StartTime == DateTimePicker.MinimumDateTime)
            {
                nud.Value = decimal.Zero;
                return;
            }
            changeOvertime(decimalToTimeSpan((double)nud.Value));
        }

        private void nud_premiums_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            if (period.EndTime == DateTimePicker.MinimumDateTime && period.StartTime == DateTimePicker.MinimumDateTime)
            {
                nud.Value = decimal.Zero;
                return;
            }
            period.ShiftPremiums = decimalToTimeSpan((double)nud.Value);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            //period.WashupTime = cb.Checked;

            if (cb.Checked)
            {
                changeWashupTime(ShiftInformation.CalcWashupTime(this.period.StartTime, this.period.EndTime, ShiftInformation.LunchLength));
                cb.BackColor = SystemColors.ControlDark;
            }
            else
            {
                changeWashupTime(TimeSpan.Zero);
                cb.BackColor = Control.DefaultBackColor;
            }
        }

        public int Location_DayOfMonth
        {
            get
            {
                return l_dayOfMonth.Location.X;
            }
        }
        public int Location_WeekNum
        {
            get
            {
                return l_weekNum.Location.X;
            }
        }
        public int Location_StartTime
        {
            get
            {
                return mtb_Start.Location.X;
            }
        }
        public int Location_EndTime
        {
            get
            {
                return mtb_End.Location.X;
            }
        }
        public int Location_Overtime
        {
            get
            {
                return nud_overtime.Location.X;
            }
        }
        public int Location_ShiftPremium
        {
            get
            {
                return nud_premiums.Location.X;
            }
        }
        public int Location_WashupTime
        {
            get
            {
                return cb_Washup.Location.X;
            }
        }

        private void row_interface_MouseWheel(object sender, MouseEventArgs e)
        {
            changeBackgroundColour(false);
        }

        private void row_interface_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                MessageBox.Show("Down");
            }
        }
        private int parseInputHours(string time)
        {
            string currentString = "";
            for (int n = 0; n < time.Length && time[n] != ':'; ++n)
            {
                char c = time[n];
                if (Char.IsDigit(c))
                {
                    currentString += c;
                }
            }

            int hours;
            if (int.TryParse(currentString, out hours))
            {
                return hours;
            }
            else
            {
                return -1;
            }
        }
        private int parseInputMinutes(string time)
        {
            string currentString = "";

            int index = 0;
            for (; index < time.Length && time[index] != ':'; ++index) ;

            if (index >= time.Length)
            {
                return 0;
            }
            ++index;

            for (; index < time.Length; ++index)
            {
                char c = time[index];
                if (Char.IsDigit(c))
                {
                    currentString += c;
                }
            }

            currentString = currentString.Length == 1 ? currentString + "0" : currentString;

            int outputMinutes;
            if (int.TryParse(currentString, out outputMinutes))
            {
                return outputMinutes;
            }
            else
            {
                return -1;
            }
        }
        private bool parseInputDate(MaskedTextBox mtb, out TimeSpan result)
        {
            if (mtb.Mask != "00:00" || mtb.ValidatingType != typeof(DateTime))
            {
                result = TimeSpan.Zero;
                return false;
            }
            string inputTime = mtb.Text;

            int hours = parseInputHours(inputTime);
            int minutes = parseInputMinutes(inputTime);

            if (hours < 0 || minutes < 0 )
            {
                result = TimeSpan.Zero;
                return false;
            }

            result = new TimeSpan(hours, minutes, 0);
            return true;
        }
        private void RaiseNewEntryAddedEvent(object sender, EventArgs e)
        {
            NewEntryAdded?.Invoke(this, EventArgs.Empty);
        }
        private void checkTimes()
        {
            if (period.StartTime > DateTime.MinValue && period.EndTime > DateTime.MinValue)
            {
                showNuds(true);
            }
        }
        private TimeSpan guessStartTime(DateTime end)
        {
            TimeSpan t = end.TimeOfDay;

            if (t == ShiftInformation.ExportsEndDayShift || t == ShiftInformation.ExportsLateEndDayShift)
            {
                return ShiftInformation.ExportsStartDayShift;
            }
            else if(t >= ShiftInformation.MorningCutoff && t < ShiftInformation.EveningCutoff)
            {
                return ShiftInformation.RegularStartDayShift;
            }
            else if (t == ShiftInformation.ExportsEndNightShift)
            {
                return ShiftInformation.ExportsStartNightShift;
            }
            else
            {
                return ShiftInformation.RegularStartNightShift;
            }
        }
        private bool tryAutoSetStartTime()
        {
            if (period.StartTime != DateTime.MinValue)
            {
                return false;
            }
            changeStartTime(ShiftInformation.CombineDateAndTime(period.Date, guessStartTime(period.EndTime)));
            return true;
        }
        private void checkEndTime()
        {
            if (period.EndTime <= period.StartTime)
            {
                changeEndTime(period.EndTime.AddDays(1));
            }
        }
        private string checkMinuteValue(MaskedTextBox mtb)
        {
            string txt = mtb.Text;
            int len = txt.Length;

            if (len == 3 &&
                (Char.IsDigit(txt[0]) ||
                Char.IsDigit(txt[1])))
            {
                return txt + "00";
            }
            else if (len == 4)
            {
                return txt + "0";
            }
            else
            {
                return txt;
            }
        }
        private void updateOvertime()
        {
            nud_overtime.Value = new Decimal(calcOvertime().TotalHours);
        }
        private void updateShiftPremiums()
        {
            nud_premiums.Value = new Decimal(calcShiftPremium().TotalHours);
        }
        private void updateWashup()
        {
            period.WashupTime = ShiftInformation.CalcWashupTime(period.StartTime, period.EndTime, ShiftInformation.LunchLength);
            cb_Washup.Checked = period.WashupTime > TimeSpan.Zero;
        }
        private void mtb_Start_Validating(object sender, CancelEventArgs e)
        {
            Debug.WriteLine("Validating start input");
            MaskedTextBox mtb = (MaskedTextBox)sender;

            mtb.Text = checkMinuteValue(mtb);

            TimeSpan ts;
            if (parseInputDate(mtb, out ts))
            {
                if (!keyPressed)
                {
                    return;
                }
                Debug.WriteLine("TS:" + ts.ToString());
                mtb.Text = ts.ToString();
                changeStartTime(ShiftInformation.CombineDateAndTime(Date, ts));
            }
            else
            {
                Debug.WriteLine("Validation fail");
            }
            checkTimes();
            updateOvertime();
            updateShiftPremiums();
            updateWashup();

            RowInterfaceEventArgs ev = new RowInterfaceEventArgs(period);
            LeaveTimeBoxEvent?.Invoke(this, ev);
        }
        private void mtb_End_Validating(object sender, CancelEventArgs e) //NEED TO ENSURE CHANGES TO OT, PREMIUMS AND WASHUP ARE REFLECTED IN DATASOURCE!!!
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;

            mtb.Text = checkMinuteValue(mtb);

            TimeSpan ts;
            if (parseInputDate(mtb, out ts))
            {
                if (!keyPressed)
                {
                    return;
                }
                changeEndTime(ShiftInformation.CombineDateAndTime(Date, ts));
                tryAutoSetStartTime();
                checkEndTime();
                checkTimes();
                updateOvertime();
                updateShiftPremiums();
                updateWashup();

                RowInterfaceEventArgs ev = new RowInterfaceEventArgs(period);
                LeaveTimeBoxEvent?.Invoke(this, ev);
            }
        }
        private void MaskedTextBox_Enter(object sender, EventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;
            
            selectedControl = currentControl.IndexOf(mtb);
            RaiseInputRowSelectedEvent();

            BeginInvoke((MethodInvoker)(() =>
            {
                mtb.Select(0, 100);
            }
            ));
            changeBackgroundColour(true);
        }
        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;

            selectedControl = currentControl.IndexOf(nud);
            RaiseInputRowSelectedEvent();

            BeginInvoke((MethodInvoker)(() =>
            {
                nud.Select(0, 100);
            }
            ));
        }
        private void CheckBox_Enter(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            selectedControl = currentControl.IndexOf(cb);
            RaiseInputRowSelectedEvent();
        }
        private void RichTextBox_Enter(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;

            selectedControl = currentControl.IndexOf(rtb);
            RaiseInputRowSelectedEvent();

            BeginInvoke((MethodInvoker)(() =>
            {
                rtb.Select(0, 100);
            }
            ));
        }
        private void changeCurrentSelectedControl(int dir)
        {
            int num = dir / Math.Abs(dir);

            int tempIndex = selectedControl + dir;

            tempIndex = tempIndex < 0 ? currentControl.Count - 1 : tempIndex;

            int newIndex = tempIndex % currentControl.Count;

            selectedControl = newIndex;
            currentControl[selectedControl].Focus();
        }
        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                changeCurrentSelectedControl(-1);
            }
            else if (e.KeyCode == Keys.Right)
            {
                changeCurrentSelectedControl(1);
            }
        }
        private void raiseHorizontalArrowEvent(KeyEventArgs e)
        {
            HorizontalArrowDown?.Invoke(this, e);
        }
        private void raiseVerticalArrowEvent(KeyEventArgs e)
        {
            VerticalArrowDown?.Invoke(this, e);
        }
        private void Child_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                raiseHorizontalArrowEvent(e);
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down/* || e.KeyCode == Keys.Enter*/)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (!cursorIsInbounds(cursorPos))
                {
                    changeBackgroundColour(false);
                }
                raiseVerticalArrowEvent(e);
            }
            else
            {
                keyPressed = true;
            }
        }
        private string removeMask(string s)
        {
            string str = "";
            for (int n =0; n < s.Length; ++n)
            {
                if (Char.IsDigit(s[n]))
                {
                    str += s[n];
                }
            }
            return str;
        }
        private string checkHourValue(string str)
        {
            string s = removeMask(str);
            if (s.Length <= 0)
            {
                return "00:";
            }
            else if (s.Length <= 1)
            {
                return "0" + s + ":";
            }
            else if (s.Length == 2)
            {
                return "" + s + ":";
            }
            else
            {
                return str;
            }
        }
        private void validateHours(MaskedTextBox hrs)
        {
            string s = checkHourValue(hrs.Text);

            if (hrs.Text == s)
            {
                hrs.Text = "";
                hrs.Text = s;
            }

            hrs.Text = s;
        }
        private void mtb_Times_KeyDown(object sender, KeyEventArgs e)
        {
            Keys k = e.KeyCode;
            if(k == Keys.OemPeriod ||
                k == Keys.Decimal)
            {
                e.Handled = true;
                validateHours((MaskedTextBox)sender);
            }
            else
            {
                Child_KeyDown(sender, e);
            }
        }
        private void RaiseInputRowSelectedEvent()
        {
            InputRowSelected?.Invoke(this, EventArgs.Empty);
        }

        private void Rtb_Comment_TextChanged(object sender, EventArgs e)
        {
            RichTextBox tb = (RichTextBox)sender;
            period.Comment = tb.Text;
        }

        private void control_Leave(object sender, EventArgs e)
        {
            //RowInterfaceEventArgs ev = new RowInterfaceEventArgs(period);
            //LeaveTimeBoxEvent?.Invoke(this, ev);
        }

        private void row_interface_Validated(object sender, EventArgs e)
        {
            keyPressed = false;
        }
        private void nud_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void MaskedTextBox_Click(object sender, EventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;
            string blankTimeMask = "  :";
            if (mtb.Text == blankTimeMask)
            {
                mtb.Select(0, 1000);
            }
        }
    }
}
