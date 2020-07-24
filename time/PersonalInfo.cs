using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public class PersonalInfo
    {
        public static readonly char SepChar = '|';

        public string GivenNames { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string PRI { get; set; } = "";
        public string Group { get; set; } = "";
        public string Level { get; set; } = "";
        public string WorkAddress { get; set; } = "";
        public string HomeAddress { get; set; } = "";
        public string WorkPhoneNumber { get; set; } = "";
        public string HomePhoneNumber { get; set; } = "";

        public PersonalInfo()
        {
            this.DateOfBirth = DateTime.Now;
        }
        public int GetAge(DateTime date)
        {
            return (int)((date.Subtract(this.DateOfBirth)).Days / 365.25);
        }

        public string printInfo()
        {
            string output = "";

            output += this.GivenNames + SepChar;
            output += this.Surname + SepChar;
            output += this.DateOfBirth.ToString() + SepChar;
            output += this.PRI + SepChar;
            output += this.Group + SepChar;
            output += this.Level + SepChar;
            output += this.WorkAddress + SepChar;
            output += this.HomeAddress + SepChar;
            output += this.WorkPhoneNumber + SepChar;
            output += this.HomePhoneNumber + SepChar;

            return output;
        }
        public static bool TryParse(string s, out PersonalInfo info)
        {
            info = new PersonalInfo();

            WordParser parser = new WordParser(s, SepChar);

            info.GivenNames = parser.NextWord();
            info.Surname = parser.NextWord();

            DateTime dob;
            if (!DateTime.TryParse(parser.NextWord(), out dob))
            {
                info = null;
                return false;
            }

            info.DateOfBirth = dob;
            info.PRI = parser.NextWord();
            info.Group = parser.NextWord();
            info.Level = parser.NextWord();
            info.WorkAddress = parser.NextWord();
            info.HomeAddress = parser.NextWord();
            info.WorkPhoneNumber = parser.NextWord();
            info.HomePhoneNumber = parser.NextWord();

            return true;
        }
    }
}
