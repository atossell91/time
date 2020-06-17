using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class PhoenixOTSheetDims
    {
        public static readonly int BorderWidth = 4;
        public static readonly int RowsPerGrid = 8;
        public static readonly int ColumnsPerGrid = 14;

        public static readonly Size CellSize = new Size(125, 36);

        public static readonly Point TableOneDatesStart = new Point(226, 291);
        public static readonly Point TableTwoDatesStart = new Point(226, 685);

        public static readonly Point TableOneStart = new Point(226, 372);
        public static readonly Point TableTwoStart = new Point(226, 763);

        public static readonly Point WeekInput_TopLeft = new Point(839, 148);
        public static readonly Point WeekInput_BottomRight = new Point(990, 225);

        public static readonly Point GlobalDate_TopLeft = new Point(1091, 148);
        public static readonly Point GlobalDate_BottomRight = new Point(1502, 225);

        public static readonly Point NameInput_TopLeft = new Point(609, 229);
        public static readonly Point NameInput_BottomRight = new Point(1374, 286);

        public static readonly Point SignatureDate_TopLeft = new Point(1540, 1449);
        //public static readonly Point SignatureDate_BottomRight = new Point();

        public static readonly Point SpecificDateStart = new Point(225, 290);
    }
}
