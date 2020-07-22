using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public class data_4600
    {
        public class overtimeRow
        {
            public DateTime StartDate;
            public DateTime EndDate;

            public TimeSpan MealPeriod;

            public string Code;

            public double X100Hours;
            public double X150Hours;
            public double X175Hours;
            public double X200Hours;

            public double ExtendedHours;
            public bool Recoverable;
            public double ChargeableCosts;

            public string Reason;

            public overtimeRow(DateTime start, DateTime end, TimeSpan meal, string code,
                double x100, double x150, double x175, double x200)
            {
                this.StartDate = start;
                this.EndDate = end;

                this.MealPeriod = meal;

                this.Code = code;

                this.X100Hours = x100;
                this.X150Hours = x150;
                this.X175Hours = x175;
                this.X200Hours = x200;
            }
        }

        public List<overtimeRow> OvertimeRows;

        public data_4600()
        {
        }
        public void FillNewRow(DateTime start, DateTime end, TimeSpan lunch, string code,
            double x100Hours, double x150Hours, double x175Hours, double x200Hours)
        {
            OvertimeRows.Add(new overtimeRow(start, end, lunch, code, x100Hours, x150Hours,
                x175Hours, x200Hours));
        }
        public overtimeRow GetOvertimeRow(int index)
        {
            return OvertimeRows[index];
        }
    }
}
