using System;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Sorting
{
    /// <summary>
    /// A tree sort implementation.
    /// </summary>
    public class TreeSort<T> where T : IComparable
    {
        /// <summary>
        /// Time complexity: O(nlog(n)).
        /// </summary>
        public static T[] Sort(T[] array)
        {
            //create BST
            var tree = new RedBlackTree<T>();
            foreach (var item in array)
            {
                tree.Insert(item);
            }

            //now extract min until empty
            //and return them as sorted array
            var sortedArray = new T[array.Length];
            var j = 0;
            while (tree.Count > 0)
            {
                //can be optimized by consolidating FindMin and Delete!
                var min = tree.Min();
                sortedArray[j] = min;
                tree.Delete(min);
                j++;
            }

            return sortedArray;
        }
    }
}
