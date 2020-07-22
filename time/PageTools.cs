using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    class PageTools
    {
        public static void CentreTextBox(Control tb, Box box)
        {
            Size s = box.CalcSize();

            tb.Size = new Size(s.Width, tb.Size.Height);

            int yStart = box.TopLeft.Y;
            if (tb.Height < s.Height)
            {
                int ySpace = s.Height - tb.Height;
                yStart += ySpace / 2;
            }
            tb.Location = new Point(box.TopLeft.X, yStart);

            if (tb is TextBox)
            {
                ((TextBox)tb).TextAlign = HorizontalAlignment.Center;
            }
        }
    }
}
