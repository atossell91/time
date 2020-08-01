using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    class Sheet_4600 : Sheet
    {
        private readonly PersonalInfo personInfo;
        private readonly data_4600 mainSheet;

        //private Dictionary<string, Label> labels;
        private readonly Font defaultFont = new Font("Arial", 16);

        public Sheet_4600(PersonalInfo pInfo, data_4600 sheet)
        {
            this.IsLandscape = false;
            this.PaperType = System.Drawing.Printing.PaperKind.Legal;

            //InitializeComponent();
            //labels = new Dictionary<string, Label>();
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Location = new Point(0, 0);

            this.Image = time.Properties.Resources.blank4600;

            personInfo = pInfo;
            mainSheet = sheet;

            fillPersonalInfo();
            fillPeriodCovered();
            fillMultipleOvertimeRows();
            fillCodeSummary();
            fillTotalHours();
            fillSignatureDate();
        }
        private void createFixedLabel(string text, int topLeftX, int topLeftY, int width, int height)
        {
            int marginWith = 3;
            Label l = new Label();
            l.Location = new Point(topLeftX + marginWith, topLeftY + marginWith);
            l.Text = text;
            l.Font = this.defaultFont;
            l.BackColor = Color.Transparent;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Size = new Size(width - marginWith * 2, height - marginWith * 2);
            //PageTools.CentreTextBox(l, dims4600.Surname);
            this.Controls.Add(l);
        }
        private void createFixedLabel(string text, Box position)
        {
            Size s = position.CalcSize();
            createFixedLabel(text, position.TopLeft.X, position.TopLeft.Y, s.Width, s.Height);
        }
        private int drawHorizontalGridCell(string text, int xPos, int yPos, Box refBox)
        {
            int currentXpos = xPos;

            Size s = refBox.CalcSize();

            createFixedLabel(text, xPos, yPos, s.Width, s.Height);

            currentXpos += s.Width + dims4600.StandardBorderWidth;

            return currentXpos;
        }
        private string ToString(double d)
        {
            if (d == 0.0)
            {
                return "";
            }
            return d.ToString("0.###");
        }
        private void fillOvertimeRow(int rowNum)
        {
            int gridStart = dims4600.OTGridCodes.TopLeft.Y;
            int yPos = gridStart + rowNum * (dims4600.OTGridCodes.CalcSize().Height + dims4600.StandardBorderWidth - 1) + 3;

            int currentXpos = dims4600.OTGridStartDate.TopLeft.X;

            string f_date = "yyyy-MM-dd";
            string f_time = "HH:mm";
            string f_num = "######";
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
            createFixedLabel(ToString(mainSheet.GetOvertimeRow(rowNum)._X100Hours), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX150.CalcSize();
            createFixedLabel(ToString(mainSheet.GetOvertimeRow(rowNum).X150Hours), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX175.CalcSize();
            createFixedLabel(ToString(mainSheet.GetOvertimeRow(rowNum).X175Hours), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX200.CalcSize();
            createFixedLabel(ToString(mainSheet.GetOvertimeRow(rowNum).X200Hours), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridExtendedHours.CalcSize();
            createFixedLabel(ToString(mainSheet.GetOvertimeRow(rowNum).ExtendedHours), currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridRecoverableHours.CalcSize();
            createFixedLabel("", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridChargeableCosts.CalcSize();
            createFixedLabel(/*mainSheet.GetOvertimeRow(rowNum).ChargeableCosts.ToString()*/"", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridReason.CalcSize();
            createFixedLabel(mainSheet.GetOvertimeRow(rowNum).Reason, currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;
        }
        private void fillMultipleOvertimeRows()
        {
            for (int n = 0; n < mainSheet.GetNumberOfFilledRows(); ++n)
            {
                fillOvertimeRow(n);
            }
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

            int gridStart = dims4600.OTSummaryCodeColumn.TopLeft.Y;
            int yPos = gridStart + (dims4600.OTSummaryRowHeight * rowNum);

            int currentXpos = dims4600.OTSummaryCodeColumn.TopLeft.X;
            currentXpos = drawHorizontalGridCell(mainSheet.GetCodeSummaryRow(rowNum).Code,
                currentXpos, yPos, dims4600.OTSummaryCodeColumn);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).X100Hours),
                currentXpos, yPos, dims4600.OTSummaryX100Column);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).X150Hours),
                currentXpos, yPos, dims4600.OTSummaryX150Column);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).X175Hours),
                currentXpos, yPos, dims4600.OTSummaryX175Column);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).X200Hours),
                currentXpos, yPos, dims4600.OTSummaryX200Column);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).ActualHours),
                currentXpos, yPos, dims4600.OTSummaryActualHours);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).ExtendedHours),
                currentXpos, yPos, dims4600.OTSummaryExtendedHours);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).LeaveHours),
                currentXpos, yPos, dims4600.OTSummaryLeaveHours);

            currentXpos = drawHorizontalGridCell(ToString(mainSheet.GetCodeSummaryRow(rowNum).CashHours),
                currentXpos, yPos, dims4600.OTSummaryCashHours);
        }
        private void fillTotalHours()
        {
            createFixedLabel(ToString(mainSheet.GetLeaveHours()),
                dims4600.TotalLeave);
            createFixedLabel(ToString(mainSheet.GetCashHours()),
                dims4600.TotalCash);
        }
        private void fillCodeSummary()
        {
            Debug.WriteLine("Number of codes: " + mainSheet.GetNumberOfCodes());
            for (int n = 0; n < mainSheet.GetNumberOfCodes(); ++n)
            {
                fillSummaryCodeRow(n);
            }
        }
        private void fillSignatureDate()
        {
            createFixedLabel(DateTime.Now.ToString("yyyy-MM-dd"), dims4600.SignatureDate);
        }
        public void addPersonalInfo()
        {
        }
    }
}
