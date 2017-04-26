using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Sorting
{

    /// <summary>
    /// A shell sort implementation
    /// </summary>
    public class ShellSort<T> where T : IComparable
    {
        /// <summary>
        /// Sort given integers
        /// </summary>
        /// <param name="array"></param>
        /// <param name="mst"></param>
        /// <returns></returns>
        public static T[] Sort(T[] array)
        {
            var k = array.Length / 2;

            var j = 0;

            while (k >= 1)
            {
                for (int i = k; i < array.Length; i = i + k, j = j + k)
                {
                    if (array[i].CompareTo(array[j]) < 0)
                    {
                        swap(array, i, j);

                        if (i > k)
                        {
                            i -= k * 2;
                            j -= k * 2;
                        }
                    }
                }

                j = 0;
                k /= 2;
            }

            return array;
        }

        private static void swap(T[] array, int i, int j)
        {
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }
}
