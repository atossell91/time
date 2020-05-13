using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public static class ShiftInformation
    {
        public readonly static TimeSpan ShiftLength = new TimeSpan(7, 30, 0);
        public readonly static TimeSpan LunchLength = new TimeSpan(0, 30, 0);
        public readonly static TimeSpan CoffeeLength = new TimeSpan(0, 25, 0);

        public readonly static TimeSpan RegularStartDayShift = new TimeSpan(7, 9, 0);
        public readonly static TimeSpan LateStartDayShift = new TimeSpan(8, 55, 0);
        public readonly static TimeSpan ExportsStartDayShift = new TimeSpan(7, 0, 0);
        public readonly static TimeSpan ExportsEndDayShift = new TimeSpan(15, 0, 0);
        public readonly static TimeSpan ExportsLateEndDayShift = new TimeSpan(17, 0, 0);

        public readonly static TimeSpan RegularStartNightShift = new TimeSpan(16, 49, 0);
        public readonly static TimeSpan LateStartNightShift = new TimeSpan(17, 35, 0);
        public readonly static TimeSpan ExportsStartNightShift = new TimeSpan(19, 0, 0);
        public readonly static TimeSpan ExportsEndNightShift = new TimeSpan(3, 0, 0);

        public readonly static TimeSpan MorningCutoff = new TimeSpan(8, 0, 0);
        public readonly static TimeSpan EveningCutoff = new TimeSpan(20, 0, 0);

        public readonly static TimeSpan WashupTimeAmount = new TimeSpan(0, 10, 0);
        public readonly static TimeSpan NightShiftCutoff = new TimeSpan(16, 30, 0);

        public readonly static TimeSpan HourInterval = new TimeSpan(0, 15, 0);
        public readonly static TimeSpan HourIntervalCutoff = new TimeSpan(0, 7, 0);
    }
}
