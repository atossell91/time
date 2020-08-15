using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    class Sheet_PAR : Sheet
    {
        private readonly PersonalInfo PersonInfo;

        private readonly Font DEFAULT_FONT = new Font("Arial", 20);

        List<TextBox> textboxes;
        public Sheet_PAR(PersonalInfo pi)
        {
            this.Location = new Point(0, 0);
            this.SizeMode = PictureBoxSizeMode.AutoSize;

            this.PersonInfo = pi;
            textboxes = new List<TextBox>();

            this.Image = time.Properties.Resources.PAR;

            BuildSheet();
        }
        private void BuildSheet()
        {
            createStandardTextBox(dimsPAR.Date, DateTime.Today.ToString("yyyy-MM-dd"));
            createStandardTextBox(dimsPAR.FirstName, PersonInfo.GivenNames);
            createStandardTextBox(dimsPAR.PRI, PersonInfo.PRI);
            createStandardTextBox(dimsPAR.LastName, PersonInfo.Surname);

            createStandardTextBox(dimsPAR.Department, "Canadian Food Inspection Agency");
        }
        private TextBox createStandardTextBox(Box b, string text)
        {
            TextBox tb = new TextBox();
            tb.Location = b.TopLeft;
            tb.Size = b.CalcSize();
            tb.Font = DEFAULT_FONT;
            tb.TextAlign = HorizontalAlignment.Center;

            tb.Text = text;

            this.Controls.Add(tb);
            textboxes.Add(tb);
            return tb;
        }
    }
}
