using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class OvertimeInformation
    {
        public class codeRow
        {
            public string code = "";
            public string hours = "";
        }
        private class day
        {
            public DateTime date = DateTime.MinValue;
            public const int ROWS_PER_DAY = 8;
            private int rowCount = 0;

            public codeRow[] codes = new codeRow[ROWS_PER_DAY];

            private void initCodes()
            {
                for (int n =0; n < ROWS_PER_DAY; ++n)
                {
                    this.codes[n] = new codeRow();
                }
            }
            public day(DateTime date)
            {
                this.date = date;
                initCodes();
            }
            public void SetCode(int index, string code, string hours)
            {
                if (index >= ROWS_PER_DAY)
                {
                    return;
                }

                codeRow c = codes[index];

                c.code = code;
                c.hours = hours;
            }
            public codeRow[] GetAllCodes()
            {
                return this.codes;
            }
        }
        private class week
        {
            public const int DAYS_PER_WEEK = 7;
            public day[] days = new day[DAYS_PER_WEEK];

            public week(DateTime start)
            {
                for (int n =0; n < DAYS_PER_WEEK; ++n)
                {
                    days[n] = new day(start.AddDays(n));
                }
            }

            public day GetDay(DayOfWeek day)
            {
                return this.days[(int)day];
            }
        }

        private const int MAX_WEEKS = 2;
        week[] weeks = new week[MAX_WEEKS];

        private void initWeeks(DateTime start)
        {
            for (int n = 0; n < MAX_WEEKS; ++n)
            {
                weeks[n] = new week(start.AddDays(week.DAYS_PER_WEEK));
            }
        }
        public OvertimeInformation()
        {
            DateTime start = new DateTime(2020, 9, 6);
            initWeeks(start);
        }

        public void SetCode(string code, string hours, int weekIndex, int dayIndex, int codeRowIndex)
        {
            codeRow c = weeks[weekIndex].days[dayIndex].codes[codeRowIndex];
            c.code = code;
            c.hours = hours;
        }
        public codeRow GetCodeRow(string code, string hours, int weekIndex, int dayIndex, int codeRowIndex)
        {
            return weeks[weekIndex].days[dayIndex].codes[codeRowIndex];
        }
    }
}
