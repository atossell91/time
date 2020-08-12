using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    static class StatHoliday
    {
        public static DateTime NewYearsDay(int year)
        {
            return new DateTime(year, 1, 1);
        }
        public static DateTime VictoriaDay(int year)
        {
            DateTime day = new DateTime(year, 5, 24);
            for (;
                day.DayOfWeek != DayOfWeek.Monday; day.AddDays(-1.0));

            return day;
        }
        public static DateTime CanadaDay(int year)
        {
            return new DateTime(year, 7, 1);
        }
        public static DateTime HeritageDay(int year) //Provincial
        {
            return new DateTime(year, 8, 1);
        }
        public static DateTime LabourDay(int year)
        {
            DateTime d = new DateTime(year, 09, 01);

            for (; d.DayOfWeek != DayOfWeek.Monday; d.AddDays(1.0)) ;

            return d;
        }
        public static DateTime ThanksgivingDay(int year)
        {
            DateTime day = new DateTime(year, 10, 1);

            int monCount = 0;
            for (; monCount < 2 && day.Day < DateTime.DaysInMonth(year, 10); day.AddDays(1.0))
            {
                if (day.DayOfWeek == DayOfWeek.Monday)
                {
                    ++monCount;
                }
            }
            return day;
        }
        public static DateTime RemembranceDay(int year)
        {
            return new DateTime(year, 11, 11);
        }
        public static DateTime ChristmasDay(int year)
        {
            return new DateTime(year, 12, 25);
        }
        public static DateTime BoxingDay(int year)
        {
            return new DateTime(year, 12, 26);
        }

        private struct monthDay
        {
            public int day;
            public int month;

            public monthDay(int d, int m)
            {
                day = d;
                month = m;
            }
        }
        static readonly monthDay[] paschalTable =
        {
            new monthDay(4, 14), //1
            new monthDay(4, 3),
            new monthDay(3, 23),
            new monthDay(4, 11),
            new monthDay(3, 31), //5
            new monthDay(4, 18),
            new monthDay(4, 8),
            new monthDay(3, 28),
            new monthDay(4, 16),
            new monthDay(4, 5), //10
            new monthDay(3, 25),
            new monthDay(4, 13),
            new monthDay(4, 2),
            new monthDay(3, 22),
            new monthDay(4, 10), //15
            new monthDay(3, 30),
            new monthDay(4, 17),
            new monthDay(4, 7),
            new monthDay(3, 27),
        };

        private static DateTime Easter(int year)
        {
            int index = year % 19;

            int month = paschalTable[index].month;
            int day = paschalTable[index].day;

            return new DateTime(year, month, day);
        }
        private static DateTime EasterSunday(int year)
        {
            DateTime easter = Easter(year);

            DateTime date = easter.AddDays(1.0);
            for (; date.DayOfWeek != DayOfWeek.Sunday; date.AddDays(1.0)) ;

            return date;
        }
        private static DateTime GoodFriday(int year)
        {
            return EasterSunday(year).AddDays(-2.0);
        }

        public static DateTime AdjustHolidayDate(DateTime hol)
        {

            DateTime adjustedDate = hol;

            if (hol.DayOfWeek == DayOfWeek.Saturday)
            {
                adjustedDate = hol.AddDays(2.0);
            }
            else if (hol.DayOfWeek == DayOfWeek.Sunday)
            {
                adjustedDate = hol.AddDays(1.0);
            }

            if (hol.Date == BoxingDay(hol.Year) &&
                adjustedDate == AdjustHolidayDate(ChristmasDay(hol.Year)))
            {
                return adjustedDate.AddDays(1.0);
            }

            return adjustedDate;
        }

        public static List<DateTime> GetAllHolidays(int yr)
        {
            List<DateTime> hols = new List<DateTime>();

            hols.Add(AdjustHolidayDate(NewYearsDay(yr)));
            hols.Add(AdjustHolidayDate(GoodFriday(yr)));
            hols.Add(AdjustHolidayDate(EasterSunday(yr)));
            hols.Add(AdjustHolidayDate(VictoriaDay(yr)));
            hols.Add(AdjustHolidayDate(CanadaDay(yr)));
            hols.Add(AdjustHolidayDate(HeritageDay(yr)));
            hols.Add(AdjustHolidayDate(LabourDay(yr)));
            hols.Add(AdjustHolidayDate(ThanksgivingDay(yr)));
            hols.Add(AdjustHolidayDate(RemembranceDay(yr)));
            hols.Add(AdjustHolidayDate(ChristmasDay(yr)));
            hols.Add(AdjustHolidayDate(BoxingDay(yr)));

            return hols;
        }
        public static bool IsStatDay(DateTime d)
        {
            List<DateTime> stats = GetAllHolidays(d.Year);

            return stats.Exists((x) => d.Date == x.Date);
        }
    }
}
