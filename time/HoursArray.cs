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
    }
}
