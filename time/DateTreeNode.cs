using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class DateTreeNode
    {
        List<DateTreeNode> Nodes;
        double Number = -1.0;

        public DateTreeNode(IEnumerable<double> nums, int p, int q)
        {
            if (p > q)
            {
                return;
            }

            this.Number = nums.ElementAt(p);
            this.Nodes = new List<DateTreeNode>();

            if (p < q)
            {
                this.Nodes.Add(new DateTreeNode(nums, p + 1, q));
            }
        }

        public void Add(IEnumerable<double> nums, int p, int q)
        {
            if (p > q)
            {
                return;
            }

            double element = nums.ElementAt(p);
            int index = Nodes.FindIndex((x) => x.Number == element);

            if(index < 0)
            {
                Nodes.Add(new DateTreeNode(nums,p,q));
            }
            else
            {
                Nodes[index].Add(nums, p + 1, q);
            }
        }
        public void ReadArray()
        {

            Debug.WriteLine(this.Number);
            foreach(DateTreeNode n in Nodes)
            {
                n.ReadArray();
            }
        }
    }
}
