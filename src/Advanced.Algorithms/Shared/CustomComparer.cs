using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms
{
    internal class CustomComparer<T> : IComparer<T> where T : IComparable
    {
        private readonly bool isMax;
        private readonly IComparer<T> comparer;

        internal CustomComparer(SortDirection sortDirection, IComparer<T> comparer)
        {
            this.isMax = sortDirection == SortDirection.Descending;
            this.comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            return !isMax ? compare(x, y) : compare(y, x);
        }

        private int compare(T x, T y)
        {
            return comparer.Compare(x, y);
        }
    }
}
