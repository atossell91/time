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
        private readonly Font defaultFont = new Font("Arial", 24);

        public Render_4600(PersonalInfo pInfo, data_4600 sheet)
        {
            InitializeComponent();
            //labels = new Dictionary<string, Label>();
            personInfo = pInfo;
            mainSheet = sheet;

            fillPersonalInfo();

            for (int n =0; n < 16; ++n)
            {
                fillOvertimeRow(n);
            }
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

            Size s;
            s = dims4600.OTGridStartDate.CalcSize();
            createFixedLabel("Start_date", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridStartTime.CalcSize();
            createFixedLabel("Start_time", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridEndDate.CalcSize();
            createFixedLabel("End_date", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridStartTime.CalcSize();
            createFixedLabel("End_time", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridMealPeriod.CalcSize();
            createFixedLabel("Meal", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth + dims4600.CodeBorder;

            s = dims4600.OTGridCodes.CalcSize();
            createFixedLabel("Code", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX100.CalcSize();
            createFixedLabel("1.0", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX150.CalcSize();
            createFixedLabel("1.5", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX175.CalcSize();
            createFixedLabel("1.75", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridX200.CalcSize();
            createFixedLabel("2.0", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridExtendedHours.CalcSize();
            createFixedLabel("Ext", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridRecoverableHours.CalcSize();
            createFixedLabel("Rec", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridChargeableCosts.CalcSize();
            createFixedLabel("Charge", currentXpos, yPos, s.Width, s.Height);
            currentXpos += s.Width + dims4600.StandardBorderWidth;

            s = dims4600.OTGridReason.CalcSize();
            createFixedLabel("Insert reason here", currentXpos, yPos, s.Width, s.Height);
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
        private Size getSuggestedSize(string text, Font f)
        {
            Label l = new Label();
            l.Text = text;
            l.Font = f;
            l.AutoSize = true;
            Size s = l.Size;
            l.Dispose();
            return s;
        }
        public void addPersonalInfo()
        {
        }
    }
}
