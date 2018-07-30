using System;
using System.Linq;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    public class D_aryMaxHeap<T> where T : IComparable
    {
        private T[] heapArray;
        private int k;
        public int Count = 0;

        public D_aryMaxHeap(int k, IEnumerable<T> initial = null)
        {
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
        /// Initialize with given input 
        /// O(n) time complexity
        /// </summary>
        /// <param name="initial"></param>
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
        /// Recursively load bulk init values
        /// </summary>
        /// <param name="i"></param>
        /// <param name="initial"></param>
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


        //O(log(n) base K)
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
        //O(log(n) base K)
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
        /// returns the max Index of child if any 
        /// otherwise returns -1
        /// </summary>
        /// <param name="currentParent"></param>
        /// <param name="heap"></param>
        /// <returns></returns>
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

        //o(1)
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
    }
}
