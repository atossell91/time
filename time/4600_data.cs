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
            public event EventHandler DataChanged;

            public DateTime Start;
            public DateTime End;
            public TimeSpan MealPeriod;
            public string Code;
            public HoursArray Hours;

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

                this.DataChanged += UpdateFields;

                ExtendedHours = this.Hours.GetTotalActualHours();
            }
            private void TriggerDataChanged()
            {
                DataChanged?.Invoke(this, EventArgs.Empty);
            }
            private void UpdateFields(Object sender, EventArgs e)
            {
                this.Hours.GetTotalExtendedHours();
            }
            private void setHours(int index, double hours)
            {
                Hours.SetHours(hours, index);
            }
            public double getHours(int index)
            {
                return Hours.GetHours(index);
            }
        }
        class CodeTotal
        {
            public string code;
            public HoursArray hours;

            public double actualHours;
            public double extendedHours;

            public double cashHours;
            public double leaveHours;
            public double premiums;
        }
        class CodesList
        {
            private List<CodeTotal> list;

            public void Add(overtimeRow r)
            {
                foreach(CodeTotal c in list)
                {
                    if (c.code == r.Code)
                    {
                        c.hours.AddHours(r.getHours(HoursArray.x100), HoursArray.x100);
                        c.hours.AddHours(r.getHours(HoursArray.x150), HoursArray.x150);
                        c.hours.AddHours(r.getHours(HoursArray.x175), HoursArray.x175);
                        c.hours.AddHours(r.getHours(HoursArray.x200), HoursArray.x200);

                        c.actualHours = r.Hours.GetTotalActualHours();
                        c.extendedHours = r.Hours.GetTotalExtendedHours();

                        break;
                    }

                }
            }
        }

        List<overtimeRow> rows;
        CodesList codes;

        public data_4600()
        {
            rows = new List<overtimeRow>();
        }

        public void AddRow(DateTime start, DateTime end, TimeSpan mealPeriod, string code,
            int[] hours)
        {
            rows.Add(new overtimeRow(start, end, mealPeriod, code, hours));
            codes.Add(rows[rows.Count - 1]);
            //rows[rows.Count - 1].DataChanged += 
        }
    }
}
