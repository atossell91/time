using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    class WeekNumber
    {
        private static readonly int YearStartMonth = 4;
        private static readonly int WeekStartDay = 0;

        private static readonly int weekLen = 7;

        private static int convDayOfWeek(DayOfWeek d)
        {
            switch (d)
            {
                case DayOfWeek.Sunday:
                    {
                        return 0;
                    }
                case DayOfWeek.Monday:
                    {
                        return 1;
                    }
                case DayOfWeek.Tuesday:
                    {
                        return 2;
                    }
                case DayOfWeek.Wednesday:
                    {
                        return 3;
                    }
                case DayOfWeek.Thursday:
                    {
                        return 4;
                    }
                case DayOfWeek.Friday:
                    {
                        return 5;
                    }
                case DayOfWeek.Saturday:
                    {
                        return 6;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        private static DateTime NextSunday(DateTime d)
        {
            int dayOfWeek = convDayOfWeek(d.DayOfWeek);
            int daysToNextWeek = 7 - dayOfWeek;

            int day = d.Day + daysToNextWeek;

            return new DateTime(d.Year, d.Month, day);
        }
        public static DateTime WeekStart(DateTime d)
        {
            int dayOfWeek = convDayOfWeek(d.DayOfWeek);
            int prevSunday = d.Day - dayOfWeek;

            if (prevSunday > 0)
            {
                return new DateTime(d.Year, d.Month, prevSunday);
            }
            else
            {
                int month = d.Month -1;
                int year = d.Year;

                if (month < 1)
                {
                    month = 12;
                    --year;
                }

                int day = DateTime.DaysInMonth(year, month) - prevSunday;

                return new DateTime(year, month, day);
            }
        }
        public static int GetWeekNumber(DateTime date)
        {
            DateTime yrStart;

            yrStart = date.Month < YearStartMonth ?
                new DateTime(date.Year - 1, YearStartMonth, 1) :
                new DateTime(date.Year, YearStartMonth, 1);

            DateTime firstSunday = NextSunday(yrStart);

            if (date < firstSunday)
            {
                return 1;
            }
            else
            {
                TimeSpan timeDiff = date.Subtract(firstSunday);
                double totDays = timeDiff.TotalDays;
                double totWeeks = totDays / weekLen;

                return ((int)totWeeks) + 2;
            }
        }
        public static DateTime GetDateFromWeek(int year, int weekNumber)
        {
            if (weekNumber < 2)
            {
                return new DateTime(year, YearStartMonth, 1);
            }

            int num = weekNumber - 2;

            DateTime prevYearStart = new DateTime(year - 1, YearStartMonth, 1);
            DateTime prevYearEnd = new DateTime(year - 1, 12, 31);
            int weekCutoff = (int)(prevYearEnd.Subtract(prevYearStart).TotalDays / weekLen);

            if (weekNumber > weekCutoff)
            {
                year -= 1;
            }
            DateTime start = NextSunday(new DateTime(year, YearStartMonth, 1));

            DateTime weekStartDate = start.AddDays(num * 7);

            return weekStartDate;
        }
        public static DateTime GetDateFromWeek(int year, int weekNumber, DayOfWeek day)
        {
            DateTime weekStart = GetDateFromWeek(year, weekNumber);
            DateTime outputDate = weekStart.AddDays(
                convDayOfWeek(day));

            return outputDate;
        }
    }
}
