using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public class PersonalInfo
    {
        public string GivenNames { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string PRI { get; set; } = "";
        public string Group { get; set; } = "";
        public string Level { get; set; } = "";
        public string WorkAddress { get; set; } = "";
        public string HomeAddress { get; set; } = "";
        public string WorkPhoneNumber { get; set; } = "";
        public string HomePhoneNumber { get; set; } = "";

        public int GetAge(DateTime date)
        {
            return (int)((date.Subtract(this.DateOfBirth)).Days / 365.25);
        }
    }
}
