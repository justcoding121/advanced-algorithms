using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// Compares two arrays.
    /// </summary>
    internal class ArrayComparer<T> : IEqualityComparer<T[]>
    {
        public bool Equals(T[] x, T[] y)
        {
            if (x == y)
            {
                return true;
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (!x[i].Equals(y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(T[] x)
        {
            unchecked
            {
                if (x == null)
                {
                    return 0;
                }

                int hash = 17;

                foreach (var element in x)
                {
                    hash = hash * 31 + element.GetHashCode();
                }

                return hash;
            }
        }
    }
}
