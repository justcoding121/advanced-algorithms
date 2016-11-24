using Algorithm.Sandbox.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Sorting
{
    public class TreeSort<T> where T : IComparable
    {
        //O(nlog(n))
        public static T[] Sort(T[] array)
        {
            //create BST
            var tree = new AsRedBlackTree<T>();
            for (int i = 0; i < array.Length; i++)
            {
                tree.Insert(array[i]);
            }

            //now extract min until empty
            //and return them as sorted array
            var sortedArray = new T[array.Length];
            int j = 0;
            while (tree.Count > 0)
            {
                //can be optimized by consolidating FindMin & Delete!
                var min = tree.FindMin();
                sortedArray[j] = min;
                tree.Delete(min);
                j++;
            }

            return sortedArray;
        }
    }
}
