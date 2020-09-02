using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    class Sheet_OT : Sheet
    {
        Sheet_OT()
        {
            this.Image = time.Properties.Resources.Phoenix_Inspectors_Weekly_OT_Template_With_Blank_Form;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
