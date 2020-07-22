using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class Box
    {
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }

        public Size CalcSize()
        {
            int width = BottomRight.X - TopLeft.X;
            int height = BottomRight.Y - TopLeft.Y;

            return new Size(width, height);
        }
        public Box(Point topLeft, Point bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }
        public Box(int topLeftX, int topLeftY, int bottomRightX, int bottomRightY)
        {
            Point topLeft = new Point(topLeftX, topLeftY);
            Point bottomRight = new Point(bottomRightX, bottomRightY);

            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }
    }
}
