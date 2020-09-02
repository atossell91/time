using System;
using System.Collections.Generic;
using System.Drawing;
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
        
        public virtual void PrepareForPrinting()
        {
        }
        public virtual Bitmap RenderSheet()
        {
            if (this.Image == null)
            {
                return null;
            }

            Bitmap bmp = new Bitmap(this.Image.Width, this.Image.Height);
            Rectangle r = new Rectangle(0, 0, this.Image.Width, this.Image.Height);
            this.DrawToBitmap(bmp, r);

            return bmp;
        }
        public virtual string[] ExportData()
        {
            return null;
        }
    }
}
