﻿using System;
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
        }
        private work_period(DateTime d, DateTime s, DateTime e,
            TimeSpan o, TimeSpan sp, TimeSpan w, string com)
        {
            this.Date = d;
            this.StartTime = s;
            this.EndTime = e;
            this.Overtime = o;
            this.ShiftPremiums = sp;
            this.WashupTime = w;
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
            TimeSpan wash = TimeSpan.Zero; //Leave unitialized after removing fixer function
            string comment;

            if (DateTime.TryParse(scanner.NextWord(), out fixedDate) &&
                DateTime.TryParse(scanner.NextWord(), out start) &&
                DateTime.TryParse(scanner.NextWord(), out end) &&
                TimeSpan.TryParse(scanner.NextWord(), out ot) &&
                TimeSpan.TryParse(scanner.NextWord(), out premium) 
                //&& TimeSpan.TryParse((str = scanner.NextWord()), out wash) //Uncomment after deleting fixer function
                )
            {

                Fixer_Functions._CORRECT_WASHUP(start, end, ref wash, scanner.NextWord()); //Fixer function. Delete once all save data is up-to-date

                comment = scanner.NextWord();
                result = new work_period(fixedDate, start, end, ot, premium, wash, comment);
                return true;
            }
            else
            {
                Debug.WriteLine("Parse Fail");
                result = null;
                return false;
            }
        }
        public override string ToString()
        {
            char s = ',';
            return Date.ToString() + s +
                StartTime.ToString() + s +
                EndTime.ToString() + s +
                Overtime.ToString() + s +
                ShiftPremiums.ToString() + s +
                WashupTime.ToString() + s +
                Comment;
        }
    }
}
