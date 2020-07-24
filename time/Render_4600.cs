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
    public partial class Render_4600 : Form
    {
        private readonly PersonalInfo personInfo;
        private readonly data_4600 mainSheet;

        //private Dictionary<string, Label> labels;
        private readonly Font defaultFont = new Font("Arial", 16);

        public Render_4600(PersonalInfo pInfo, data_4600 sheet)
        {
            InitializeComponent();
            //labels = new Dictionary<string, Label>();
            personInfo = pInfo;
            mainSheet = sheet;

            fillPersonalInfo();
            fillPeriodCovered();

            for (int n =0; n < sheet.GetNumberOfFilledRows(); ++n)
            {
                fillOvertimeRow(n);
            }

            fillCodeSummary();
        }
        private void createFixedLabel(string text, int topLeftX, int topLeftY, int width, int height)
        {
            int marginWith = 3;
            Label l = new Label();
            l.Location = new Point(topLeftX+marginWith, topLeftY+marginWith);
            l.Text = text;
            l.Font = this.defaultFont;
            l.BackColor = Color.HotPink;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Size = new Size(width-marginWith*2, height-marginWith*2);
            //PageTools.CentreTextBox(l, dims4600.Surname);
            pictureBox1.Controls.Add(l);
        }
        private void createFixedLabel(string text, Box position)
        {
            Size s = position.CalcSize();
            createFixedLabel(text, position.TopLeft.X, position.TopLeft.Y, s.Width, s.Height);
        }
        private void fillOvertimeRow(int rowNum)
        {
            int gridStart = dims4600.OTGridCodes.TopLeft.Y;
            int yPos = gridStart + rowNum * (dims4600.OTGridCodes.CalcSize().Height + dims4600.StandardBorderWidth-1)+3;

            int currentXpos = dims4600.OTGridStartDate.TopLeft.X;

            string f_date = "yyyy-MM-dd";
            string f_time = "HH:mm";
            Size s;
            s = dims4600.OTGridStartDate.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).StartDate.ToString(f_date), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridStartTime.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).StartDate.ToString(f_time), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridEndDate.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).EndDate.ToString(f_date), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridStartTime.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).EndDate.ToString(f_time), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridMealPeriod.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).MealPeriod.ToString("mm"), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth + dims4600.CodeBorder;

            s = dims4600.OTGridCodes.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum)._code, currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX100.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum)._X100Hours.ToString(), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX150.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).X150Hours.ToString(), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX175.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).X175Hours.ToString(), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX200.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).X200Hours.ToString(), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridExtendedHours.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).ExtendedHours.ToString(), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridRecoverableHours.CalcSize();
            createFixedLabel("Rec", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridChargeableCosts.CalcSize();
            createFixedLabel(/*mainSheet.GetOvertimeRow(rowNum).ChargeableCosts.ToString()*/"", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridReason.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).Reason, currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;
        }
        private void fillPersonalInfo()
        {
            createFixedLabel(this.personInfo.Surname, dims4600.Surname);
            createFixedLabel(this.personInfo.GivenNames, dims4600.GivenName);
            createFixedLabel(this.personInfo.PRI, dims4600.PRI);
            createFixedLabel(this.personInfo.Group, dims4600.Group);
            createFixedLabel(this.personInfo.Level, dims4600.Level);
            createFixedLabel(this.personInfo.WorkAddress, dims4600.Establishment);
        }
        private void fillPeriodCovered()
        {
            string f_date = "yyyy-MM-dd";
            string value = "";
            if (mainSheet.PeriodStart != mainSheet.PeriodEnd)
            {
                value = mainSheet.PeriodStart.ToString(f_date) + " - " +
                    mainSheet.PeriodEnd.ToString(f_date);
            }
            else
            {
                value = mainSheet.PeriodStart.ToString(f_date);
            }
            createFixedLabel(value, dims4600.Period);
        }
        private void fillSummaryCodeRow(int rowNum)
        {
            int rowHeight = 10;

            int gridStart = dims4600.OTSummaryCodeColumn.TopLeft.Y;
            int yPos = gridStart + (rowHeight * rowNum);

            int currentXpos = dims4600.OTSummaryCodeColumn.TopLeft.X;

            Size s;

            s = dims4600.OTSummaryCodeColumn.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).Code, dims4600.OTSummaryCodeColumn);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTSummaryX100Column.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).X100Hours.ToString(), dims4600.OTSummaryX100Column);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTSummaryX150Column.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).X150Hours.ToString(), dims4600.OTSummaryX150Column);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTSummaryX175Column.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).X175Hours.ToString(), dims4600.OTSummaryX175Column);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTSummaryX200Column.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).X200Hours.ToString(), dims4600.OTSummaryX200Column);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTSummaryActualHours.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).ActualHours.ToString(), dims4600.OTSummaryActualHours);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTSummaryExtendedHours.CalcSize();
            createFixedLabel(mainSheet.GetCodeSummaryRow(rowNum).ExtendedHours.ToString(), dims4600.OTSummaryExtendedHours);
            currentXpos += s.Width + dims4600.StandardBorderWidth;
        }
        private void fillCodeSummary()
        {
            for (int n =0; n < mainSheet.GetNumberOfCodes(); ++n)
            {
                fillSummaryCodeRow(n);
            }
        }
        public void addPersonalInfo()
        {
        }
    }
}
