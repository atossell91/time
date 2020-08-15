using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    static class dimsPAR
    {
        public static readonly Box NumPages = new Box(1365, 66, 1447, 157);
        public static readonly Box Date = new Box(1150, 214, 1525, 246);

        public static readonly Box FirstName = new Box(92, 589, 1102, 651);
        public static readonly Box PRI = new Box(1143, 589, 1536, 651);
        public static readonly Box LastName = new Box(92, 709, 1102, 770);
        public static readonly Box Email = new Box(96, 829, 1110, 890);
        public static readonly Box PhoneNumber = new Box(1143, 829, 1536, 890);
        public static readonly Box Department = new Box(476, 948, 1536, 1010);
        public static readonly Box WorkType = new Box(538, 1075, 1538, 1114);
        public static readonly Box WorkSubtype = new Box(520, 1195, 1321, 1241);

        public static readonly Box RequestorName = new Box(80, 1330, 531, 1375);
        public static readonly Box RequestorEmail = new Box(545, 1329, 1312, 1375);
        public static readonly Box RequestorPhoneNum = new Box(1326, 1329, 1544, 1374);
        public static readonly Box Comments = new Box(78, 1434, 1563, 1525);

    }
}
