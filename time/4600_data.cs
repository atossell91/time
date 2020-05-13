using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class data_4600
    {
        class HourList
        {

        }
        class overtimeRow
        {
            public DateTime Start;
            public DateTime End;
            public TimeSpan MealPeriod;
            public string code;

            public int x100 = 0;
            public int x150 = 1;
            public int x175 = 2;
            public int x200 = 3;
            private List<double> Hours;

            private double ExtendedHours;
            private bool Recoverable;
            private double ChargeableCosts;

            private string Reason;

            private void initHoursList()
            {
                Hours = new List<double>();
                for (int n =0; n < 4; ++n)
                {
                    Hours.Add(0.0);
                }
            }
            overtimeRow()
            {
                initHoursList();
            }

            private double calcExtendedHours()
            {
                double ext = 0.0;
                ext += Hours[x100];
                ext += Hours[x150] * 1.5;
                ext += Hours[x175] * 1.75;
                ext += Hours[x200] * 2;

                return ext;
            }
            private void setHours(int index, double hours)
            {
                Hours[index] = hours;
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
