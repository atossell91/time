using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    public delegate void RowInterfaceEvent(object sender, RowInterfaceEventArgs e);
    public class RowInterfaceEventArgs : EventArgs
    {
        public RowInterfaceEventArgs(work_period p)
        {
            Period = p;
        }
        public work_period Period { get; private set; }
    }
}
