using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class HoursArray
    {
        public readonly static int x100 = 0;
        public readonly static int x150 = 1;
        public readonly static int x175 = 2;
        public readonly static int x200 = 3;

        private static readonly double[] multiplier = {1.0, 1.5, 1.75, 2.0};

        private double[] hours;
        private const int arrSize = 4;

        private void initHoursArray()
        {
            for (int n =0; n < arrSize; ++n)
            {
                hours[n] = 0.0;
            }
        }
        public HoursArray()
        {
            hours = new double[arrSize];
            initHoursArray();
        }

        public void SetHours(double TotalHours, int Field)
        {
            hours[Field] = TotalHours;
        }
        public void AddHours(double AddedHours, int Field)
        {
            hours[Field] += AddedHours;
        }
        public double GetHours(int Field)
        {
            return hours[Field];
        }
        public double GetTotalActualHours()
        {
            double output = 0.0;
            foreach(double n in hours)
            {
                output += n;
            }
            return output;
        }
        public double GetTotalExtendedHours()
        {
            double output = 0.0;

            output += hours[x100] * multiplier[x100];
            output += hours[x150] * multiplier[x150];
            output += hours[x175] * multiplier[x175];
            output += hours[x200] * multiplier[x200];

            return output;
        }
        public double GetExtendedHours(int field)
        {
            return hours[field] * multiplier[field];
        }
    }
}
