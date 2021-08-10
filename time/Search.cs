using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class Search
    {
        private static int FindIndexOrNextHelper<T>(List<T> arr, T item, IComparer<T> c, int p, int r)
        {
            if (r <= p)
            {
                return p;
            }

            int q = ((r - p) / 2)+1;
            int comp = c.Compare(arr[q], item);
            
            if (comp == 0)
            {
                return q;
            }
            else if (comp < 0)
            {
                return FindIndexOrNextHelper(arr, item , c, p, q - 1);
            }
            else
            {
                return FindIndexOrNextHelper(arr, item, c, q + 1, r);
            }
        }
        public static int FindIndexOrNext<T>(List<T> arr, T item, IComparer<T> c)
        {
            return FindIndexOrNextHelper(arr, item, c, 0, arr.Count-1);
        }
    }
}
