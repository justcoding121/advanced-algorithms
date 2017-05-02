using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMaxHeap<T> where T : IComparable
    {
        private T[] heapArray;

        public int Count { get; private set; }

        /// <summary>
        /// Initialize with optional init value
        /// </summary>
        /// <param name="initial"></param>
        public AsBMaxHeap(IEnumerable<T> initial = null)
        {
            if (initial != null)
            {
                var initArray = new T[initial.Count()];

                int i = 0;
                foreach (var item in initial)
                {
                    initArray[i] = item;
                    i++;
                }

                BulkInit(initArray);
                Count = initArray.Length;
            }
            else
            {
                heapArray = new T[2];
            }
        }

        /// <summary>
        /// Initialize with given input 
        /// O(n) time complexity
        /// </summary>
        /// <param name="initial"></param>
        private void BulkInit(T[] initial)
        {
            var i = (initial.Length - 1) / 2;

            while (i >= 0)
            {
                BulkInitRecursive(i, initial);
                i--;
            }

            heapArray = initial;
        }

        /// <summary>
        /// Recursively 
        /// </summary>
        /// <param name="i"></param>
        private void BulkInitRecursive(int i, T[] initial)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;

            var Max = i;
            var parent = i;

            if (left < initial.Length
                && initial[left].CompareTo(initial[parent]) > 0)
            {
                var temp = initial[left];
                initial[left] = initial[parent];
                initial[parent] = temp;
                Max = left;
            }

            if (right < initial.Length
                && initial[right].CompareTo(initial[parent]) > 0)
            {
                var temp = initial[right];
                initial[right] = initial[parent];
                initial[parent] = temp;
                Max = right;
            }

            //if Max is child then drill down child
            if (Max != parent)
            {
                BulkInitRecursive(Max, initial);
            }
        }
        //o(log(n))
        public void Insert(T newItem)
        {
            if (Count == heapArray.Length)
            {
                doubleArray();
            }

            heapArray[Count] = newItem;

            for (int i = Count; i > 0; i = (i - 1) / 2)
            {
                if (heapArray[i].CompareTo(heapArray[(i - 1) / 2]) > 0)
                {
                    var temp = heapArray[(i - 1) / 2];
                    heapArray[(i - 1) / 2] = heapArray[i];
                    heapArray[i] = temp;
                }
                else
                {
                    break;
                }
            }

            Count++;
        }

        public T ExtractMax()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }
            var Max = heapArray[0];

            heapArray[0] = heapArray[Count - 1];
            Count--;

            int parentIndex = 0;

            //percolate down
            while (true)
            {
                var leftIndex = 2 * parentIndex + 1;
                var rightIndex = 2 * parentIndex + 2;

                var parent = heapArray[parentIndex];

                if (leftIndex < Count && rightIndex < Count)
                {
                    var leftChild = heapArray[leftIndex];
                    var rightChild = heapArray[rightIndex];

                    var leftIsMax = false;

                    if (leftChild.CompareTo(rightChild) > 0)
                    {
                        leftIsMax = true;
                    }

                    var maxChildIndex = leftIsMax ? leftIndex : rightIndex;

                    if (heapArray[maxChildIndex].CompareTo(parent) > 0)
                    {
                        var temp = heapArray[parentIndex];
                        heapArray[parentIndex] = heapArray[maxChildIndex];
                        heapArray[maxChildIndex] = temp;

                        if (leftIsMax)
                        {
                            parentIndex = leftIndex;
                        }
                        else
                        {
                            parentIndex = rightIndex;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                else if (leftIndex < Count)
                {
                    if (heapArray[leftIndex].CompareTo(parent) > 0)
                    {
                        var temp = heapArray[parentIndex];
                        heapArray[parentIndex] = heapArray[leftIndex];
                        heapArray[leftIndex] = temp;

                        parentIndex = leftIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (rightIndex < Count)
                {
                    if (heapArray[rightIndex].CompareTo(parent) > 0)
                    {
                        var temp = heapArray[parentIndex];
                        heapArray[parentIndex] = heapArray[rightIndex];
                        heapArray[rightIndex] = temp;

                        parentIndex = rightIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }

            if (heapArray.Length / 2 == Count && heapArray.Length > 2)
            {
                halfArray();
            }

            return Max;
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
