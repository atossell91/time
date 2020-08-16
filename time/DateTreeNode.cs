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
        int Number = -1;

        public IEnumerable<object> Objects;

        public DateTreeNode()
        {
            this.Number = 0;
            this.Nodes = new List<DateTreeNode>();
        }
        private DateTreeNode(IEnumerable<int> nums, int p, int q, IEnumerable<Object> obj)
        {
            if (p > q)
            {
                return;
            }

            this.Number = nums.ElementAt(p);
            this.Nodes = new List<DateTreeNode>();

            if (p == q && this.Number == nums.ElementAt(q))
            {
                this.Objects = obj;
            }

            if (p < q)
            {
                this.Nodes.Add(new DateTreeNode(nums, p+1, q, obj));
            }
        }

        public void Add(IEnumerable<int> nums, int p, int q, IEnumerable<object> obj)
        {
            if (p > q)
            {
                return;
            }

            double element = nums.ElementAt(p);
            int index = Nodes.FindIndex((x) => x.Number == element);

            if (p == q && this.Number == nums.ElementAt(q))
            {
                this.Objects = obj;
            }


            if (index < 0)
            {
                Nodes.Add(new DateTreeNode(nums,p,q,obj));
            }
            else
            {
                Nodes[index].Add(nums, p + 1, q,obj);
            }
        }
        public string OutputTree()
        {
            string output = "";

            output += " " + this.Number;
            if (this.Objects != null)
            {
                foreach (Object o in Objects)
                {
                    output += " " + o.ToString();
                }
            }

            if (Nodes.Count > 0)
            {
                output += "!";
            }

            foreach (DateTreeNode n in Nodes)
            {
                output += n.OutputTree();
            }

            if (Nodes.Count > 0)
            {
                output += "i";
            }

            return output;
        }
    }
}
