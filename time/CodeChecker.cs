using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //private List<PremiumCode> outputCodes = new List<PremiumCode>();

            private void findAndAddHours(PremiumCode pc, List<PremiumCode> outputCodes)
            {
                outputCodes.Sort(PremiumCode.compareByStartDate());
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
                List<PremiumCode> outputCodes = new List<PremiumCode>();
                for (int n = 0; n < wp.Count; ++n)
                {
                    work_period p = wp[n];

                    DateTime nextDay = p.Date.AddDays(1.0);

                    PremiumCode pc = new PremiumCode(DEFAULT_CODE, p.Date);

                    if (p.Overtime <= TimeSpan.Zero)
                    {
                        continue;
                    }
                    else if ((nextDay.DayOfWeek == DayOfWeek.Sunday || StatHoliday.IsStatDay(nextDay)) &&
                        p.EndTime.Date == nextDay.Date)
                    {
                        pc.Hours = p.Overtime - p.EndTime.TimeOfDay;
                        Debug.WriteLine("SAT HOURS: " + pc.Hours.TotalHours);
                        PremiumCode holidayOT = new PremiumCode(DEFAULT_CODE, nextDay);
                        holidayOT.Hours = p.EndTime.TimeOfDay;
                        Debug.WriteLine("SUN HOURS: " + holidayOT.Hours.TotalHours);
                        findAndAddHours(holidayOT, outputCodes);
                    }
                    else
                    {
                        pc.Hours = p.Overtime;
                    }
                    findAndAddHours(pc, outputCodes);
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
            public readonly static string DEFAULT_CODE = "290";
            public List<PremiumCode> GenerateCodes(List<work_period> wp)
            {
                List<PremiumCode> outputCodes = new List<PremiumCode>();

                for (int n =0; n < wp.Count; ++n)
                {
                    work_period p = wp[n];
                    
                    if (p.WashupTime <= TimeSpan.Zero)
                    {
                        continue;
                    }

                    DateTime nextDay = p.Date.AddDays(1.0);


                    DateTime washupEnd = p.EndTime.Add(ShiftInformation.WashupTimeAmount);

                    if((nextDay.DayOfWeek == DayOfWeek.Sunday || StatHoliday.IsStatDay(nextDay)) && 
                        washupEnd.Date == nextDay.Date)
                    {
                        DateTime start = new DateTime(washupEnd.Year, washupEnd.Month, washupEnd.Day,
                            0, 0, 0);

                        PremiumCode doubleWash = new PremiumCode(DEFAULT_CODE, start);
                        doubleWash.EndDate = washupEnd;
                        doubleWash.SetArrayHours(washupEnd.TimeOfDay, PremiumCode.HoursMultiplier.X200);
                    }

                }
                return outputCodes;
            }
        }
    }
}
