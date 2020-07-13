using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time.Resources
{
    public class PersonalInfo
    {
        public string GivenNames { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PRI { get; set; }
        public string Group { get; set; }
        public int Level { get; set; }
        public string WorkAddress { get; set; }
        public string HomeAddress { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }

        public int GetAge(DateTime date)
        {
            return (int)((date.Subtract(this.DateOfBirth)).Days / 365.25);
        }
    }
}
