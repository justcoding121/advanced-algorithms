using System;
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
        public static T[] Sort(T[] array, Order order = Order.Ascending)
        {
            //create BST
            var tree = new RedBlackTree<T>();
            foreach (var item in array)
            {
                tree.Insert(item);
            }

            return order == Order.Ascending ? 
                tree.AsEnumerable().ToArray() 
                : tree.AsEnumerableDesc().ToArray();

        }
    }
}
