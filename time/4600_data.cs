using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class data_4600
    {
        class overtimeRow
        {
            public DateTime Start;
            public DateTime End;
            public TimeSpan MealPeriod;
            public string Code;
            private HoursArray Hours;

            private double ExtendedHours;
            private bool Recoverable;
            private double ChargeableCosts;

            private string Reason;
            private overtimeRow()
            {
                Hours = new HoursArray();
            }
            public overtimeRow(DateTime start, DateTime end, TimeSpan mealPeriod,
                string code, int[] hours)
            {
                Start = start;
                End = end;
                MealPeriod = mealPeriod;
                Code = code;

            }

            private double calcExtendedHours()
            {
                double ext = 0.0;
                ext += Hours.GetHours(HoursArray.x100);
                ext += Hours.GetHours(HoursArray.x150);
                ext += Hours.GetHours(HoursArray.x175);
                ext += Hours.GetHours(HoursArray.x200);

                return ext;
            }
            private void setHours(int index, double hours)
            {
                Hours.SetHours(hours, index);
            }
        }
        class codeSummary
        {
            public string code;
            public double cashHours;
            public double leaveHours;
            public double premiums;
        }

        List<overtimeRow> rows;

        public data_4600()
        {
            rows = new List<overtimeRow>();
        }
    }
}
