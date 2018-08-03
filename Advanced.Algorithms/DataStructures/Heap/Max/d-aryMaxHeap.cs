using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A D-ary max heap implementation.
    /// </summary>
    public class DaryMaxHeap<T> : IEnumerable<T> where T : IComparable
    {
        private T[] heapArray;
        private int k;
        public int Count = 0;

        /// <summary>
        /// Time complexity: O(n) when initial is provided otherwise O(1).
        /// </summary>
        /// <param name="k">The number of children per heap node.</param>
        /// <param name="initial">The initial items if any.</param>
        public DaryMaxHeap(int k, IEnumerable<T> initial = null)
        {

            if(k <= 2)
            {
                throw new Exception("Number of nodes k must be greater than 2.");
            }

            this.k = k;

            if (initial != null)
            {
                var items = initial as T[] ?? initial.ToArray();
                var initArray = new T[items.Count()];

                int i = 0;
                foreach (var item in items)
                {
                    initArray[i] = item;
                    i++;
                }

                Count = initArray.Length;
                BulkInit(initArray);
              
            }
            else
            {
                this.heapArray = new T[k];
            }
        }

        /// <summary>
        /// Initialize with given input.
        /// Time complexity: O(n).
        /// </summary>
        private void BulkInit(T[] initial)
        {
            var i = (initial.Length - 1) / k;

            while (i >= 0)
            {
                bulkInitRecursive(i, initial);
                i--;
            }

            heapArray = initial;
        }

        /// <summary>
        /// Recursively load bulk init values.
        /// </summary>
        private void bulkInitRecursive(int i, T[] initial)
        {
            while (true)
            {
                var parent = i;
                var max = findMaxChildIndex(i, initial);

                if (max != -1 && initial[max].CompareTo(initial[parent]) > 0)
                {
                    var temp = initial[max];
                    initial[max] = initial[parent];
                    initial[parent] = temp;

                    i = max;
                    continue;
                }

                break;
            }
        }


        /// <summary>
        /// Time complexity: O(log(n) base K).
        /// </summary>
        public void Insert(T newItem)
        {
            if (Count == heapArray.Length)
            {
                doubleArray();
            }

            heapArray[Count] = newItem;

            //percolate up
            for (int i = Count; i > 0; i = (i - 1) / k)
            {
                if (heapArray[i].CompareTo(heapArray[(i - 1) / k]) > 0)
                {
                    var temp = heapArray[(i - 1) / k];
                    heapArray[(i - 1) / k] = heapArray[i];
                    heapArray[i] = temp;
                }
                else
                {
                    break;
                }
            }

            Count++;
        }

        /// <summary>
        /// Time complexity: O(log(n) base K).
        /// </summary>
        public T ExtractMax()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }
            var max = heapArray[0];

            //move last element to top
            heapArray[0] = heapArray[Count - 1];
            Count--;

            int currentParent = 0;
            //now percolate down
            while (true)
            {
                var swapped = false;

                //init to left-most child
                var maxChildIndex = findMaxChildIndex(currentParent, heapArray);

                if (maxChildIndex!=-1 &&
                    heapArray[currentParent].CompareTo(heapArray[maxChildIndex]) < 0)
                {
                    var tmp = heapArray[maxChildIndex];
                    heapArray[maxChildIndex] = heapArray[currentParent];
                    heapArray[currentParent] = tmp;
                    swapped = true;
                }

                if (!swapped)
                {
                    break;
                }

                currentParent = maxChildIndex;

            }

            if (heapArray.Length / 2 == Count && heapArray.Length > 2)
            {
                halfArray();
            }

            return max;
        }

        /// <summary>
        /// Returns the max Index of child if any. 
        /// Otherwise returns -1.
        /// </summary>
        private int findMaxChildIndex(int currentParent, T[] heap)
        {
            var currentMax = currentParent * k + 1;

            if (currentMax >= Count)
                return -1;

            for (int i = 2; i <= k; i++)
            {
                if (currentParent * k + i >= Count)
                    break;

                var nextSibling = heap[currentParent * k + i];

                if (heap[currentMax].CompareTo(nextSibling) < 0)
                {
                    currentMax = currentParent * k + i;
                }
            }

            return currentMax;
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public T PeekMax()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }

            return heapArray[0];
        }

        private void halfArray()
        {
            var smallerArray = new T[heapArray.Length / 2];

            for (int i = 0; i < Count; i++)
            {
                smallerArray[i] = heapArray[i];
            }

            heapArray = smallerArray;
        }

        private void doubleArray()
        {
            var biggerArray = new T[heapArray.Length * 2];

            for (int i = 0; i < Count; i++)
            {
                biggerArray[i] = heapArray[i];
            }

            heapArray = biggerArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return heapArray.Take(Count).GetEnumerator();
        }
    }
}
