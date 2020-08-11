using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public class PremiumCode
    {
        private class compareByStartDateHelper : IComparer<PremiumCode>
        {
            int IComparer<PremiumCode>.Compare(PremiumCode x, PremiumCode y)
            {
                return x.StartDate.CompareTo(y.StartDate);
            }
        }
        public static IComparer<PremiumCode> compareByStartDate()
        {
            return (IComparer<PremiumCode>)new compareByStartDateHelper();
        }
        private class compareByCodeHelper : IComparer<PremiumCode>
        {
            int IComparer<PremiumCode>.Compare(PremiumCode x, PremiumCode y)
            {
                return x.Code.CompareTo(y.Code);
            }
        }
        public static IComparer<PremiumCode> compareByCode()
        {
            return (IComparer<PremiumCode>)new compareByCodeHelper();
        }
        private class compareByDateAndCodeHelper : IComparer<PremiumCode>
        {
            int IComparer<PremiumCode>.Compare(PremiumCode x, PremiumCode y)
            {
                if (x.StartDate != y.StartDate)
                {
                    return compareByStartDate().Compare(x, y);
                }
                else if (x.Code != y.Code)
                {
                    return compareByCode().Compare(x, y);
                }
                else
                {
                    return 0;
                }
            }
        }
        public static IComparer<PremiumCode> compareByStartDateAndCode()
        {
            return (IComparer<PremiumCode>)new compareByDateAndCodeHelper();
        }
        public string Code { get; private set; }
        public DateTime StartDate = DateTime.MinValue;
        public DateTime EndDate = DateTime.MinValue;

        public TimeSpan[] hours;
        public const int HOURS_ARR_SIZE = 4;

        enum HoursMultiplier
        {
            X100, X150, X175, X200
        }
        void SetArrayHours(TimeSpan hrs, HoursMultiplier index)
        {
            hours[(int)index] = hrs;
        }

        public TimeSpan Hours
        {
            get
            {
                return hours.Aggregate((x, y) => x + y);
            }
            set
            {
                hours[(int)HoursMultiplier.X150] = value;
            }
        }

        public PremiumCode(string code, DateTime start)
        {
            this.hours = new TimeSpan[HOURS_ARR_SIZE];
            this.Code = code;
            this.StartDate = start;
        }

        public override string ToString()
        {
            string s = " ";
            return this.Code + s + this.StartDate.ToString() + s + this.EndDate.ToString() + s +
                this.Hours.ToString();
        }
    }
}
