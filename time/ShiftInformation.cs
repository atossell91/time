using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public readonly static TimeSpan RegularStartNightShift = new TimeSpan(16, 50, 0);
        public readonly static TimeSpan LateStartNightShift = new TimeSpan(17, 35, 0);
        public readonly static TimeSpan ExportsStartNightShift = new TimeSpan(19, 0, 0);
        public readonly static TimeSpan ExportsEndNightShift = new TimeSpan(3, 0, 0);

        public readonly static TimeSpan MorningCutoff = new TimeSpan(8, 0, 0);
        public readonly static TimeSpan EveningCutoff = new TimeSpan(20, 0, 0);

        public readonly static TimeSpan WashupTimeAmount = new TimeSpan(0, 10, 0);
        public readonly static TimeSpan NightShiftCutoff = new TimeSpan(16, 30, 0);

        public readonly static TimeSpan HourInterval = new TimeSpan(0, 15, 0);
        public readonly static TimeSpan HourIntervalCutoff = new TimeSpan(0, 7, 0);

        public static DateTime CombineDateAndTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }
        public static TimeSpan LockTimeToInterval(TimeSpan time)
        {
            long totalTime = time.Ticks;
            long increment = HourInterval.Ticks;
            long cut = HourIntervalCutoff.Ticks;

            long correctedTime = ((long)((totalTime + cut) / increment)) * increment;
            return new TimeSpan(correctedTime);
        }
        public static bool IsRestDay(DateTime d)
        {
            return (d.DayOfWeek == DayOfWeek.Saturday ||
                d.DayOfWeek == DayOfWeek.Sunday);
        }
        public static TimeSpan CalcHoursWorked(DateTime start, DateTime end, TimeSpan lunch)
        {
            return (end.Subtract(start)).Subtract(lunch);
        }
        public static TimeSpan CalcShiftPremium(DateTime start, DateTime end)
        {
            if (start.Date == DateTime.MinValue ||
                end.Date == DateTime.MinValue ||
                IsRestDay(start) ||
                StatHoliday.IsStatDay(start))
            {
                return TimeSpan.Zero;
            }

            TimeSpan hoursWorked = CalcHoursWorked(start, end, LunchLength);
            TimeSpan halfPoint = new TimeSpan(hoursWorked.Ticks / 2);

            if (start.TimeOfDay >= NightShiftCutoff)
            {
                return CalcHoursWorked(start, end, LunchLength);
            }
            else if (start.TimeOfDay.Add(halfPoint) > NightShiftCutoff)
            {
                return CalcHoursWorked(CombineDateAndTime(start, NightShiftCutoff),
                    end, LunchLength);
            }

            return TimeSpan.Zero;
        }
        public static TimeSpan CalcWashupTime(DateTime start, DateTime end, TimeSpan lunch)
        {
            if (ShiftInformation.IsRestDay(start) ||
                StatHoliday.IsStatDay(start))
            {
                return ShiftInformation.WashupTimeAmount;
            }
            TimeSpan washup = CalcHoursWorked(start, end.AddMinutes(WashupTimeAmount.TotalMinutes), lunch).Subtract(ShiftLength);

            if (washup < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }
            else if (washup > WashupTimeAmount)
            {
                return WashupTimeAmount;
            }
            else
            {
                return washup;
            }
        }
        public static TimeSpan CalcOvertime(DateTime start, DateTime end)
        {

            if (start.Date == DateTime.MinValue.Date || end.Date == DateTime.MinValue.Date)
            {
                return TimeSpan.Zero;
            }

            TimeSpan hoursWorked = CalcHoursWorked(start, end, LunchLength);

            if (hoursWorked > ShiftLength && (!IsRestDay(start) || !StatHoliday.IsStatDay(start)))
            {
                return hoursWorked.Subtract(ShiftLength);
            }
            else if (IsRestDay(start) || StatHoliday.IsStatDay(start))
            {
                return hoursWorked;
            }
            else
            {
                return TimeSpan.Zero;
            }
        }
    }
}
