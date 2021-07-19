using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class dimsPhoenixOT
    {
        public class Week
        {
            public const int DayBorderThickness = 5;
            public const int DaysInWeek = 7;

            public readonly Size Size = new Size(
                (Day.Size.Width * DaysInWeek) + DayBorderThickness * (DaysInWeek +1),
                Day.Size.Height + DayBorderThickness *2);
            public class Day
            {
                public static readonly Size DateBoxSize = new Size(251, 37);
                public static readonly Size TimeBoxSize = new Size(124, 35);
                public static readonly Size CodeBoxSize = TimeBoxSize;

                public const int MaxCodeRows = 8;

                public static readonly Size Size = new Size(
                    DateBoxSize.Width,
                    DateBoxSize.Height + ((TimeBoxSize.Height + BorderThickness) * MaxCodeRows) + TimeCodeGridYOffset - BorderThickness + (DayBorderThickness *2)
                    );

                public const int TimeCodeGridYOffset = 81;
                public const int BorderThickness = 3;

                public readonly Box DateBox;
                public readonly Point TimeColumnStart;
                public readonly Point CodeColumnStart;

                public Day(int DateTopLeftX, int DateTopLeftY)
                {
                    DateBox = new Box(DateTopLeftX, DateTopLeftY,
                        DateTopLeftX + DateBoxSize.Width, DateTopLeftY + DateBoxSize.Height);

                    int GridYPos = DateBox.TopLeft.Y + TimeCodeGridYOffset;
                    TimeColumnStart = new Point(DateBox.TopLeft.X, GridYPos);
                    int TimeColXPos = DateBox.TopLeft.X + TimeBoxSize.Width + 3;
                    CodeColumnStart = new Point(TimeColXPos, GridYPos);
                }
            }

            public readonly Point TopLeft;
            public readonly Point[] DayStartPositions;

            public Week(int x, int y)
            {
                this.TopLeft = new Point(x, y);

                DayStartPositions = new Point[DaysInWeek];
                for (int n =0; n < DaysInWeek; ++n)
                {
                    DayStartPositions[n] = new Point(n * (Day.Size.Width + DayBorderThickness), y);
                }
            }
        }
    }
}
