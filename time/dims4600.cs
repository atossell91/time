using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class dims4600
    {
        public class Box
        {
            Point TopLeft;
            Point BottomRight;

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
                Point topLeft = new Point(topLeftX, topLeftX);
                Point bottomRight = new Point(bottomRightX, bottomRightY);

                this.TopLeft = topLeft;
                this.BottomRight = bottomRight;
            }
        }

        //Personal Info
        public readonly static Box Surname = new Box(582, 204, 1111, 271);
        public readonly static Box GivenName = new Box(582, 204, 1111, 271);
        public readonly static Box PRI = new Box(1451,205,1647,270);
        public readonly static Box Group = new Box(50, 319, 148, 360);
        public readonly static Box Level = new Box(151, 320, 248, 360);
        public readonly static Box Period = new Box(439, 295, 808, 360);
        public readonly static Box Establishment = new Box(811, 294, 1647, 360);

        //OT Grid
        public readonly static Box OTGridStartDate = new Box(50, 757, 181, 804);
        public readonly static Box OTGridStartTime = new Box(185, 757, 257, 804);
        public readonly static Box OTGridEndDate = new Box(260, 757, 391, 804);
        public readonly static Box OTGridEndTime = new Box(394, 757, 466, 804);
        public readonly static Box OTGridMealPeriod = new Box(470, 757, 556, 804);
        public readonly static Box OTGridCodes = new Box(563, 757, 659, 804);
        public readonly static Box OTGridX100 = new Box(663, 757, 763, 804);
        public readonly static Box OTGridX150 = new Box(766, 757, 864, 804);
        public readonly static Box OTGridX175 = new Box(867, 757, 967, 804);
        public readonly static Box OTGridX200 = new Box(970, 757, 1068, 804);
        public readonly static Box OTGridExtendedHours = new Box(1071, 757, 1182, 804);
        public readonly static Box OTGridRecoverableHours = new Box(1186, 757, 1264, 804);
        public readonly static Box OTGridChargeableCosts = new Box(1267, 757, 1367, 804);
        public readonly static Box OTGridReason = new Box(1370, 757, 1647, 804);

        //Overtime cells
        //public readonly static Box OTCellHeight = new Box(47
        public readonly static Box OvertimeGrid = new Box(50, 757, 1647, 1558);

        //OT Summary
        public readonly static Box OTSummaryCodeColumn = new Box(579, 1699, 665, 2179);
        public readonly static Box OTSummaryX100Column = new Box(668, 1699, 754, 2179);
        public readonly static Box OTSummaryX150Column = new Box(758, 1699, 844, 2179);
        public readonly static Box OTSummaryX175Column = new Box(847, 1699, 933, 2179);
        public readonly static Box OTSummaryX200Column = new Box(937, 1699, 1023, 2179);
        public readonly static Box OTSummaryActualHours = new Box(1026, 1699, 1143, 2179);
        public readonly static Box OTSummaryExtendedHours = new Box(1147, 1699, 1263, 2179);
        public readonly static Box OTSummaryLeaveHours = new Box(1267, 1699, 1381, 2179);
        public readonly static Box OTSummaryCash = new Box(1384, 1699, 1498, 2179);
        public readonly static Box OTSummaryRecoverable = new Box(1501, 1699, 1647, 2179);

        //Totals
        public readonly static Box TotalCash = new Box(780, 2183, 933, 2269);
        public readonly static Box TotalLeave = new Box(1147, 2183, 1263, 2269);
        public readonly static Box TotalPremiums = new Box(1502, 2183, 1646, 2269);
        public readonly static Box EmployeeSignature = new Box(646, 2273, 1279, 2330);
        public readonly static Box SignatureDate = new Box(1334, 2273, 1564, 2330);
    }
}
