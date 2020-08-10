using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class CodeChecker
    {
        private static List<Code_Interface> ComputableCodes = 
            new List<Code_Interface>{new Code260(), new Code055()};
        public static List<PremiumCode> CheckCodes(List<work_period> inputPeriods)
        {
            List<PremiumCode> outputCodes = new List<PremiumCode>();
            foreach (Code_Interface code in CodeChecker.ComputableCodes)
            {
                outputCodes.AddRange(code.GenerateCodes(inputPeriods));
            }
            return outputCodes;
        }

        interface Code_Interface
        {
            List<PremiumCode> GenerateCodes(List<work_period> wp);
        }

        public class Code260 : Code_Interface
        {
            public readonly static string DEFAULT_CODE = "260";

            private List<PremiumCode> outputCodes = new List<PremiumCode>();

            public Code260()
            {
                this.outputCodes = new List<PremiumCode>();
            }

            private void findAndAddHours(PremiumCode pc)
            {
                //outputCodes.Sort(PremiumCode.compareByStartDate());
                int index = outputCodes.BinarySearch(pc, PremiumCode.compareByStartDate());
                
                if (index >= 0)
                {
                    outputCodes[index].Hours.Add(pc.Hours);
                }
                else
                {
                    outputCodes.Add(pc);
                    outputCodes.Sort(PremiumCode.compareByStartDate());
                }
            }
            public List<PremiumCode> GenerateCodes(List<work_period> wp)
            {

                for (int n = 0; n < wp.Count; ++n)
                {
                    work_period p = wp[n];

                    DateTime nextDay = p.Date.AddDays(1.0);

                    PremiumCode pc = new PremiumCode(DEFAULT_CODE, p.Date);

                    if (p.Overtime <= TimeSpan.Zero)
                    {
                        continue;
                    }
                    else if (ShiftInformation.IsDayOff(nextDay) &&
                        p.EndTime.Date == nextDay.Date)
                    {
                        pc.Hours = p.Overtime - p.EndTime.TimeOfDay;
                        PremiumCode holidayOT = new PremiumCode(DEFAULT_CODE, nextDay);
                        holidayOT.Hours = p.EndTime.TimeOfDay;
                        findAndAddHours(holidayOT);
                    }
                    else
                    {
                        pc.Hours = p.Overtime;
                    }
                    findAndAddHours(pc);
                }

                return outputCodes;
            }
        }
        public class Code055 : Code_Interface
        {
            public readonly static string DEFAULT_CODE = "055";
            public List<PremiumCode> GenerateCodes(List<work_period> wp)
            {
                List<PremiumCode> outputCodes = new List<PremiumCode>();

                foreach (work_period p in wp)
                {
                    if (p.ShiftPremiums > TimeSpan.Zero)
                    {
                        PremiumCode pCode = new PremiumCode(DEFAULT_CODE, p.Date);
                        pCode.Hours = p.ShiftPremiums;
                        outputCodes.Add(pCode);
                    }
                }

                return outputCodes;
            }
        }
        public class Code290 : Code_Interface
        {
            public List<PremiumCode> GenerateCodes(List<work_period> wp)
            {
                return new List<PremiumCode>();
            }
        }
    }
}
