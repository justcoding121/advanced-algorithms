using System;
using System.Linq;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures.Heap.Min
{
    public class AsD_aryMinHeap<T> where T : IComparable
    {
        private T[] heapArray;
        private int K;
        public int Count = 0;

        public AsD_aryMinHeap(int k, IEnumerable<T> initial = null)
        {
            K = k;

            if (initial != null)
            {
                var initArray = new T[initial.Count()];

                int i = 0;
                foreach (var item in initial)
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
            var min = parent;

            var minChild = findMinChildIndex(i, initial);
            if (minChild !=-1 
                && initial[minChild].CompareTo(initial[parent]) < 0)
            {
                var temp = initial[minChild];
                initial[minChild] = initial[parent];
                initial[parent] = temp;

                min = minChild;
            }


            //if min is child then drill down child
            if (min != parent)
            {
                BulkInitRecursive(min, initial);
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
                if (heapArray[i].CompareTo(heapArray[(i - 1) / K]) < 0)
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
        public T ExtractMin()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }
            var min = heapArray[0];

            //move last element to top
            heapArray[0] = heapArray[Count - 1];
            Count--;

            int currentParent = 0;
            //now percolate down
            while (true)
            {
                var swapped = false;

                //init to left-most child
                var minChildIndex = findMinChildIndex(currentParent, heapArray);

                if (minChildIndex!=-1 &&
                    heapArray[currentParent].CompareTo(heapArray[minChildIndex]) > 0)
                {
                    var tmp = heapArray[minChildIndex];
                    heapArray[minChildIndex] = heapArray[currentParent];
                    heapArray[currentParent] = tmp;
                    swapped = true;
                }

                if (!swapped)
                {
                    break;
                }

                currentParent = minChildIndex;

            }

            if (heapArray.Length / 2 == Count && heapArray.Length > 2)
            {
                halfArray();
            }

            return min;
        }

        /// <summary>
        /// returns the min Index of child if any 
        /// otherwise returns -1
        /// </summary>
        /// <param name="currentParent"></param>
        /// <param name="heap"></param>
        /// <returns></returns>
        private int findMinChildIndex(int currentParent, T[] heap)
        {
            var currentMin = currentParent * K + 1;

            if (currentMin >= Count)
                return -1;

            for (int i = 2; i <= K; i++)
            {
                if (currentParent * K + i >= Count)
                    break;

                var nextSibling = heap[currentParent * K + i];

                if (heap[currentMin].CompareTo(nextSibling) > 0)
                {
                    currentMin = currentParent * K + i;
                }
            }

            return currentMin;
        }

        //o(1)
        public T PeekMin()
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
