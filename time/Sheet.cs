using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class Sheet : PictureBox
    {
        public PaperKind PaperType = PaperKind.Letter;
        public bool IsLandscape = false;
    }
}
