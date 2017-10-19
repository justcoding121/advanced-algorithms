using System;
using System.Linq;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Heap.Max
{
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    public class D_aryMaxHeap<T> where T : IComparable
    {
        private T[] heapArray;
        private int K;
        public int Count = 0;

        public D_aryMaxHeap(int k, IEnumerable<T> initial = null)
        {
            K = k;

            if (initial != null)
            {
                var initArray = new T[initial.Count()];

                int i = 0;
                foreach (var item in initial.OrderByDescending(x=>x))
                {
                    initArray[i] = item;
                    i++;
                }

                Count = initArray.Length;
                BulkInit(initArray);
                heapArray = initArray;
              
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
            var i = (initial.Length - K - 1) / K;

            while (i >= 0)
            {
                BulkInitRecursive(i, initial);
                i--;
            }

            heapArray = initial;
        }

        /// <summary>
        /// Recursively load bulk init values
        /// </summary>
        /// <param name="i"></param>
        private void BulkInitRecursive(int i, T[] initial)
        {
            var parent = i;
            var max = parent;

            var maxChild = findMaxChildIndex(i, initial);
            if (maxChild !=-1 
                && initial[maxChild].CompareTo(initial[parent]) > 0)
            {
                var temp = initial[maxChild];
                initial[maxChild] = initial[parent];
                initial[parent] = temp;

                max = maxChild;
            }


            //if max is child then drill down child
            if (max != parent)
            {
                BulkInitRecursive(max, initial);
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
            for (int i = Count; i > 0; i = (i - 1) / K)
            {
                if (heapArray[i].CompareTo(heapArray[(i - 1) / K]) > 0)
                {
                    var temp = heapArray[(i - 1) / K];
                    heapArray[(i - 1) / K] = heapArray[i];
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
            var currentMax = currentParent * K + 1;

            if (currentMax >= Count)
                return -1;

            for (int i = 2; i <= K; i++)
            {
                if (currentParent * K + i >= Count)
                    break;

                var nextSibling = heap[currentParent * K + i];

                if (heap[currentMax].CompareTo(nextSibling) < 0)
                {
                    currentMax = currentParent * K + i;
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
