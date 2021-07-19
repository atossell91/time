using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    class Sheet_OT : Sheet
    {
        private readonly OvertimeInformation Data;
        private readonly PersonalInfo PersonInfo;

        private List<TextBox> textboxes;

        Sheet_OT(PersonalInfo pi, OvertimeInformation data)
        {
            this.Image = time.Properties.Resources.Phoenix_Inspectors_Weekly_OT_Template_With_Blank_Form;
            this.SizeMode = PictureBoxSizeMode.AutoSize;

            this.textboxes = new List<TextBox>();

            this.Data = data;
            this.PersonInfo = pi;
        }

        private TextBox createStandardTextBox(Point location, int fontSize)
        {// 18 and 26
            TextBox tb = new TextBox();

            tb.Location = location;
            tb.Font = new Font("Arial", fontSize);

            textboxes.Add(tb);
            this.Controls.Add(tb);

            return tb;
        }
        private void FillDay(DateTime date)
        {
            dimsPhoenixOT.Week w = new dimsPhoenixOT.Week(226, 291);
        }
    }
}
