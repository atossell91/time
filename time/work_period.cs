using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace time
{
    public class work_period
    {
        private class sortByDateHelper : IComparer<work_period>
        {
            int IComparer<work_period>.Compare(work_period x, work_period y)
            {
                return x.Date.CompareTo(y.Date);
            }
        }
        public static IComparer<work_period> CompareByDate()
        {
            return (IComparer<work_period>)new sortByDateHelper();
        }
        public readonly DateTime Date;

        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }
        
        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        private TimeSpan overtime;
        public TimeSpan Overtime
        {
            get
            {
                return this.overtime;
            }
            set
            {
                this.overtime = value;
            }
        }
        
        private TimeSpan shiftPremiums;
        public TimeSpan ShiftPremiums
        {
            get
            {
                return this.shiftPremiums;
            }
            set
            {
                this.shiftPremiums = value;
            }
        }

        private TimeSpan washupTime;
        
        public TimeSpan WashupTime
        {
            get
            {
                return this.washupTime;
            }
            set
            {
                this.washupTime = value;
            }
        }
        private TimeSpan cumulativeMins;
        public TimeSpan CumulativeMins
        {
            get
            {
                return this.cumulativeMins;
            }
            set
            {
                this.cumulativeMins = value;
            }
        }
        private bool addCumulativeOT;
        public bool AddCumulativeOT
        {
            get
            {
                return this.addCumulativeOT;
            }
            set
            {
                this.addCumulativeOT = value;
            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        /*
        //Booleans determining whether a field is modifiable or not. If true, the field
        // can be modified with it's setter.
        private bool b_startTime;
        private bool b_endTime;
        private bool b_overtime;
        private bool b_shiftPremiums;
        private bool b_washupTime;

        private void initBools(bool value)
        {
            b_startTime = b_endTime =
                b_overtime = b_shiftPremiums =
                b_washupTime = value;
        }
        */
        public work_period(DateTime date)
        {
            Date = date;
            startTime = date;
            endTime = date;
            //initBools(true);

            this.startTime = DateTime.MinValue;
            this.endTime = DateTime.MinValue;
            this.overtime = TimeSpan.Zero;
            this.shiftPremiums = TimeSpan.Zero;
            this.WashupTime = TimeSpan.Zero;
            this.cumulativeMins = TimeSpan.Zero;
            this.addCumulativeOT = false;
        }
        private work_period(DateTime d, DateTime s, DateTime e,
            TimeSpan o, TimeSpan sp, TimeSpan w, TimeSpan cm, bool cot, string com)
        {
            this.Date = d;
            this.StartTime = s;
            this.EndTime = e;
            this.Overtime = o;
            this.ShiftPremiums = sp;
            this.WashupTime = w;
            this.CumulativeMins = cm;
            this.AddCumulativeOT = cot;
            this.Comment = com;
        }
        public static bool TryParse(string s, out work_period result)
        {
            WordParser scanner = new WordParser(s, ',');

            DateTime fixedDate;
            DateTime start;
            DateTime end;
            TimeSpan ot;
            TimeSpan premium;
            TimeSpan wash;
            TimeSpan cmMins;
            bool cmOT;
            string comment;

            bool check = true;

            check = DateTime.TryParse(scanner.NextWord(), out fixedDate) ? check : false;
            check = DateTime.TryParse(scanner.NextWord(), out start) ? check : false;
            check = DateTime.TryParse(scanner.NextWord(), out end) ? check : false;
            check = TimeSpan.TryParse(scanner.NextWord(), out ot) ? check : false;
            check = TimeSpan.TryParse(scanner.NextWord(), out premium) ? check : false;
            check = TimeSpan.TryParse(scanner.NextWord(), out wash) ? check : false;

            string str = scanner.NextWord();

            if(TimeSpan.TryParse(str, out cmMins) && bool.TryParse(scanner.NextWord(), out cmOT))
            {
                comment = scanner.NextWord();
            }
            else
            {
                comment = str;
                cmMins = TimeSpan.Zero;
                cmOT = false;
            }

            if (!check)
            {
                Debug.WriteLine("Parse Fail");
                result = null;
                return false;
            }

            //Need another constructor to handle extra mins
            work_period period = new work_period(fixedDate, start, end, ot, premium, wash, cmMins, cmOT, comment);

            result = period;
            return true;
        }
        public override string ToString()
        {
            char s = ',';
            string dateOnlyFormat = "yyyy-MM-dd";
            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            return Date.ToString(dateOnlyFormat) + s +
                StartTime.ToString(dateTimeFormat) + s +
                EndTime.ToString(dateTimeFormat) + s +
                Overtime.ToString() + s +
                ShiftPremiums.ToString() + s +
                WashupTime.ToString() + s +
                CumulativeMins.ToString() + s +
                AddCumulativeOT.ToString() + s +
                Comment;
        }
        public bool IsValid()
        {
            return this.StartTime != DateTime.MinValue &&
                this.EndTime != DateTime.MinValue;
        }
    }
}
