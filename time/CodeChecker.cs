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
            new List<Code_Interface>{new Code260(), new Code055(), new Code290(), new Extra260()};
        public static List<PremiumCode> CheckCodes(List<work_period> inputPeriods, ref Settings settings)
        {
            List<PremiumCode> outputCodes = new List<PremiumCode>();

            foreach (Code_Interface code in CodeChecker.ComputableCodes)
            {
                outputCodes.AddRange(code.GenerateCodes(inputPeriods, settings));
            }
            return outputCodes;
        }

        interface Code_Interface
        {
            List<PremiumCode> GenerateCodes(List<work_period> wp, Settings settings);
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
            public List<PremiumCode> GenerateCodes(List<work_period> wp, Settings settings)
            {
                List<PremiumCode> outputCodes = new List<PremiumCode>();
                for (int n = 0; n < wp.Count; ++n)
                {
                    work_period p = wp[n];

                    DateTime nextDay = p.Date.AddDays(1.0);
                    nextDay = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, 0, 0, 0);

                    PremiumCode pc = new PremiumCode(DEFAULT_CODE, p.Date);

                    if (p.Overtime <= TimeSpan.Zero)
                    {
                        continue;
                    }
                    else if ((nextDay.DayOfWeek == DayOfWeek.Sunday) &&
                        p.EndTime.Date == nextDay.Date)
                    {
                        pc.Hours = ShiftInformation.LockTimeToInterval(
                            ShiftInformation.CalcOvertime(p.StartTime,
                            nextDay), settings.RoundOT);

                        PremiumCode holidayOT = new PremiumCode(DEFAULT_CODE, nextDay);
                        holidayOT.Hours = ShiftInformation.LockTimeToInterval(
                            ShiftInformation.CalcHoursWorked(p.StartTime, p.EndTime,
                            ShiftInformation.LunchLength).Subtract(pc.Hours), settings.RoundOT);
                        findAndAddHours(holidayOT, outputCodes);
                    }
                    else
                    {
                        pc.Hours = p.Overtime;
                    }

                    if (pc.Hours > TimeSpan.Zero)
                    {
                        findAndAddHours(pc, outputCodes);
                    }
                }

                return outputCodes;
            }
        }
        public class Code055 : Code_Interface
        {
            public readonly static string DEFAULT_CODE = "055";
            public List<PremiumCode> GenerateCodes(List<work_period> wp, Settings settings)
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
            public List<PremiumCode> GenerateCodes(List<work_period> wp, Settings settings)
            {
                List<PremiumCode> outputcodes = new List<PremiumCode>();

                foreach (work_period p in wp)
                {
                    if (p.WashupTime <= TimeSpan.Zero ||
                        p.StartTime == DateTime.MinValue ||
                        p.EndTime == DateTime.MinValue)
                    {
                        continue;
                    }

                    TimeSpan washup = ShiftInformation.CalcWashupTime(p.StartTime, p.EndTime, ShiftInformation.LunchLength);
                    DateTime washupEnd = p.EndTime + washup;
                    TimeSpan ot = ShiftInformation.CalcOvertime(p.StartTime, p.EndTime);

                    if (ot >= ShiftInformation.ShiftLength) // 7.5 Hrs of OT or more
                    {
                        PremiumCode pc = new PremiumCode(DEFAULT_CODE, p.EndTime);
                        pc.EndDate = washupEnd;
                        pc.SetArrayHours(p.WashupTime, PremiumCode.HoursMultiplier.X200);

                        outputcodes.Add(pc);
                    }
                    else if (p.EndTime.DayOfWeek == DayOfWeek.Sunday &&
                        washupEnd.DayOfWeek == DayOfWeek.Sunday &&
                        settings.SplitSaturday) // Shift ends on a sunday
                    {
                        PremiumCode x20 = new PremiumCode(DEFAULT_CODE, p.EndTime);
                        x20.EndDate = washupEnd;
                        x20.SetArrayHours(p.WashupTime, PremiumCode.HoursMultiplier.X200);
                        outputcodes.Add(x20);
                    }
                    else if (washupEnd.DayOfWeek == DayOfWeek.Sunday)
                    {
                        DateTime cutoff = new DateTime(washupEnd.Year, washupEnd.Month, washupEnd.Day,
                            0, 0, 0);

                        PremiumCode x15 = new PremiumCode(DEFAULT_CODE, p.EndTime);
                        x15.EndDate = cutoff;
                        x15.SetArrayHours(cutoff - p.EndTime, PremiumCode.HoursMultiplier.X150);
                        outputcodes.Add(x15);

                        PremiumCode x20 = new PremiumCode(DEFAULT_CODE, cutoff);
                        x20.EndDate = washupEnd;
                        x20.SetArrayHours(washupEnd - cutoff, PremiumCode.HoursMultiplier.X200);
                        outputcodes.Add(x20);
                    }
                    else //Usual case
                    {
                        PremiumCode pc = new PremiumCode(DEFAULT_CODE, p.EndTime);
                        pc.EndDate = washupEnd;
                        pc.SetArrayHours(p.WashupTime, PremiumCode.HoursMultiplier.X150);

                        outputcodes.Add(pc);
                    }
                }

                return outputcodes;
            }
        }
        public class Extra260 : Code_Interface
        {
            public readonly static string DEFAULT_CODE = "260*";
            public List<PremiumCode> GenerateCodes(List<work_period> wp, Settings settings)
            {
                List<PremiumCode> li = new List<PremiumCode>();
                foreach (work_period p in wp)
                {
                    if (p.AddCumulativeOT)
                    {
                        PremiumCode pc = new PremiumCode(DEFAULT_CODE, p.Date);
                        pc.Hours = new TimeSpan(0, 15, 0);
                        li.Add(pc);
                    }
                }
                return li;
            }
        }
    }
}
