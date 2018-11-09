using System;
using System.Collections.Generic;
using System.Linq;
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
        public static IEnumerable<T> Sort(IEnumerable<T> enumerable, SortDirection sortDirection = SortDirection.Ascending)
        {
            //create BST
            var tree = new RedBlackTree<T>();
            foreach (var item in enumerable)
            {
                tree.Insert(item);
            }

            return sortDirection == SortDirection.Ascending ?
                                        tree.AsEnumerable() : tree.AsEnumerableDesc();

        }
    }
}
