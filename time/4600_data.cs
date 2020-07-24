using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public class data_4600
    {
        public class HoursRow
        {
            public static readonly string[] CodePriority = { "055", "260", "260R", "290" };
            //public static readonly string[] CodePriority = {"290", "260R", "260", "055"};

            private class compareByCodeHelper : IComparer<HoursRow>
            {
                int IComparer<HoursRow>.Compare(HoursRow x, HoursRow y)
                {
                    int xIndex = Array.FindIndex(CodePriority, e => e == x.Code);
                    int yIndex = Array.FindIndex(CodePriority, e => e == y.Code);

                    return xIndex - yIndex;
                }
            }
            public static IComparer<HoursRow> compareByCode()
            {
                return (IComparer<HoursRow>)new compareByCodeHelper();
            }

            public string _code;
            public string Code
            {
                get => this._code;

                set
                {
                    this._code = value;
                    calcExtendedHours();
                }
            }

            public double _X100Hours;
            public double X100Hours
            {
                get => this._X100Hours;

                set
                {
                    this._X100Hours = value;
                    calcExtendedHours();
                    calcActualHours();
                }
            }
            public double _X150Hours;
            public double X150Hours
            {
                get => this._X150Hours;

                set
                {
                    this._X150Hours = value;
                    calcExtendedHours();
                    calcActualHours();
                }
            }
            public double _X175Hours;
            public double X175Hours
            {
                get => this._X175Hours;

                set
                {
                    this._X175Hours = value;
                    calcExtendedHours();
                    calcActualHours();
                }
            }
            public double _X200Hours;
            public double X200Hours
            {
                get => this._X200Hours;

                set
                {
                    this._X200Hours = value;
                    calcExtendedHours();
                    calcActualHours();
                }
            }

            public double ExtendedHours;
            public double ActualHours;

            private void calcExtendedHours()
            {
                string[] validCodes = { "260", "260R", "290" };

                if (Array.Exists(validCodes, element => element == this._code))
                {
                    this.ExtendedHours = _X100Hours +
                        X150Hours * 1.50 +
                        X175Hours * 1.75 +
                        X200Hours * 2.00;
                }
                else
                {
                    this.ExtendedHours = 0.0;
                    /*
                    this.ExtendedHours = X100Hours +
                        X150Hours * +
                        X175Hours * +
                        X200Hours;
                        */
                }
            }
            private void calcActualHours()
            {
                this.ActualHours = X100Hours +
                    X150Hours +
                    X175Hours +
                    X200Hours;
            }

            public void AddHours(double x100Hours, double x150Hours,
                double x175Hours, double x200Hours)
            {
                this.X100Hours += x100Hours;
                this.X150Hours += x150Hours;
                this.X175Hours += x175Hours;
                this.X200Hours += x200Hours;
            }
        }
        public class overtimeRow : HoursRow
        {
            private class compareByStartDateHelper : IComparer<overtimeRow>
            {
                int IComparer<overtimeRow>.Compare(overtimeRow x, overtimeRow y)
                {
                    return x.StartDate.CompareTo(y.StartDate);
                }
            }
            public static IComparer<overtimeRow> compareByStartDate()
            {
                return (IComparer<overtimeRow>) new compareByStartDateHelper();
            }

            public DateTime StartDate;
            public DateTime EndDate;

            public TimeSpan MealPeriod;

            public string Reason;

            public overtimeRow(DateTime start, DateTime end, TimeSpan meal, string code,
                double x100, double x150, double x175, double x200, string reason)
            {
                this.StartDate = start;
                this.EndDate = end;

                this.MealPeriod = meal;

                this._code = code;

                this._X100Hours = x100;
                this.X150Hours = x150;
                this.X175Hours = x175;
                this.X200Hours = x200;

                this.Reason = reason;
            }
        }
        public class codeSummaryRow : HoursRow
        {
            public double LeaveHours;
            public double CashHours;

            public codeSummaryRow(string code)
            {
                this.Code = code;
            }
            public void AddLeaveHours(double hours)
            {
                this.LeaveHours += hours;
            }
            public void AddCashHours(double hours)
            {
                this.CashHours += hours;
            }
        }
        public class CodeSummary
        {
            public List<codeSummaryRow> rows;

            public void AddOvertimeRow(overtimeRow otRow)
            {
                codeSummaryRow csr = new codeSummaryRow(otRow.Code);
                int index = rows.BinarySearch(csr, HoursRow.compareByCode());

                if (index < 0)
                {
                    rows.Add(csr);
                    index = rows.Count - 1;
                }

                rows[index].AddHours(otRow.X100Hours, otRow.X150Hours, otRow.X175Hours, otRow.X200Hours);

                rows.Sort(HoursRow.compareByCode());
            }

            public CodeSummary()
            {
                rows = new List<codeSummaryRow>();
            }
        }

        private List<overtimeRow> OvertimeRows;
        private CodeSummary codeSums;

        public DateTime PeriodStart { get; private set; }
        public DateTime PeriodEnd { get; private set; }

        public data_4600()
        {
            OvertimeRows = new List<overtimeRow>();
            codeSums = new CodeSummary();
        }

        //This function is doing a lot of things - consider redoing
        public void FillNewRow(DateTime start, DateTime end, TimeSpan lunch, string code,
            double x100Hours, double x150Hours, double x175Hours, double x200Hours, string reason)
        {
            overtimeRow newRow = new overtimeRow(start, end, lunch, code, x100Hours, x150Hours,
                x175Hours, x200Hours, reason);

            OvertimeRows.Add(newRow);

            OvertimeRows.Sort(overtimeRow.compareByStartDate());

            //Not sure if this should be here
            codeSums.AddOvertimeRow(newRow);

            this.PeriodStart = OvertimeRows[0].StartDate;
            this.PeriodEnd = OvertimeRows[OvertimeRows.Count - 1].StartDate;
        }
        public int GetNumberOfFilledRows()
        {
            return OvertimeRows.Count;
        }
        public overtimeRow GetOvertimeRow(int index)
        {
            if (OvertimeRows.Count == 0)
            {
                return null;
            }
            return OvertimeRows[index];
        }
        public int GetNumberOfCodes()
        {
            return codeSums.rows.Count;
        }
        public codeSummaryRow GetCodeSummaryRow(int index)
        {
            return codeSums.rows[index];
        }
    }
}
