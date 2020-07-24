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
        //Personal Info
        public readonly static Box Surname = new Box(51, 206, 579, 271);
        public readonly static Box GivenName = new Box(582, 206, 1111, 271);
        public readonly static Box PRI = new Box(1452,206,1648,271);
        public readonly static Box Group = new Box(51, 320, 149, 361);
        public readonly static Box Level = new Box(152, 320, 249, 361);
        public readonly static Box Period = new Box(439, 295, 808, 360);
        public readonly static Box Establishment = new Box(811, 294, 1647, 360);

        public readonly static int StandardBorderWidth = 4;
        public readonly static int CodeBorder = 4;
        //OT Grid
        private readonly static int otCellStartY = 758;
        private readonly static int otCellEndY = 805;

        public readonly static Box OTGridStartDate = new Box(51, otCellStartY, 181, otCellEndY);
        public readonly static Box OTGridStartTime = new Box(185, otCellStartY, 257, otCellEndY);
        public readonly static Box OTGridEndDate = new Box(261, otCellStartY, 391, otCellEndY);
        public readonly static Box OTGridEndTime = new Box(395, otCellStartY, 466, otCellEndY);
        public readonly static Box OTGridMealPeriod = new Box(470, otCellStartY, 556, otCellEndY);
        public readonly static Box OTGridCodes = new Box(564, otCellStartY, 659, otCellEndY);
        public readonly static Box OTGridX100 = new Box(663, otCellStartY, 763, otCellEndY);
        public readonly static Box OTGridX150 = new Box(767, otCellStartY, 864, otCellEndY);
        public readonly static Box OTGridX175 = new Box(868, otCellStartY, 967, otCellEndY);
        public readonly static Box OTGridX200 = new Box(971, otCellStartY, 1068, otCellEndY);
        public readonly static Box OTGridExtendedHours = new Box(1072, otCellStartY, 1182, otCellEndY);
        public readonly static Box OTGridRecoverableHours = new Box(1187, otCellStartY, 1264, otCellEndY);
        public readonly static Box OTGridChargeableCosts = new Box(1268, otCellStartY, 1367, otCellEndY);
        public readonly static Box OTGridReason = new Box(1371, otCellStartY, 1647, otCellEndY);

        //Overtime cells
        //public readonly static Box OTCellHeight = new Box(47
        public readonly static Box OvertimeGrid = new Box(50, 757, 1647, 1558);

        //OT Summary
        public const int OTSummaryRowHeight = 40;
        private const int OTSummaryStartY = 1699;
        private const int OTSummaryFirstRowEndY = OTSummaryStartY + OTSummaryRowHeight;
        public readonly static Box OTSummaryCodeColumn = new Box(579, OTSummaryStartY, 665, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryX100Column = new Box(668, OTSummaryStartY, 754, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryX150Column = new Box(758, OTSummaryStartY, 844, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryX175Column = new Box(847, OTSummaryStartY, 933, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryX200Column = new Box(937, OTSummaryStartY, 1023, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryActualHours = new Box(1026, OTSummaryStartY, 1143, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryExtendedHours = new Box(1147, OTSummaryStartY, 1263, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryLeaveHours = new Box(1267, OTSummaryStartY, 1381, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryCashHours = new Box(1384, OTSummaryStartY, 1498, OTSummaryFirstRowEndY);
        public readonly static Box OTSummaryRecoverable = new Box(1501, OTSummaryStartY, 1647, OTSummaryFirstRowEndY);

        //Totals
        public readonly static Box TotalCash = new Box(780, 2183, 933, 2269);
        public readonly static Box TotalLeave = new Box(1147, 2183, 1263, 2269);
        public readonly static Box TotalPremiums = new Box(1502, 2183, 1646, 2269);
        public readonly static Box EmployeeSignature = new Box(646, 2273, 1279, 2330);
        public readonly static Box SignatureDate = new Box(1334, 2273, 1564, 2330);
    }
}
