using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A binary max heap implementation.
    /// </summary>
    public class BMaxHeap<T> : IEnumerable<T> where T : IComparable
    {
        private T[] heapArray;
        private readonly IComparer<T> comparer;

        public int Count { get; private set; }

        public BMaxHeap()
            : this(null, null) { }

        public BMaxHeap(IEnumerable<T> initial)
            : this(initial, null) { }

        public BMaxHeap(IComparer<T> comparer)
            : this(null, comparer) { }

        /// <summary>
        /// Time complexity: O(n) if initial is provided. Otherwise O(1).
        /// </summary>
        /// <param name="initial">The initial items in the heap.</param>
        public BMaxHeap(IEnumerable<T> initial, IComparer<T> comparer)
        {
            if (comparer != null)
            {
                this.comparer = comparer;
            }
            else
            {
                this.comparer = Comparer<T>.Default;
            }

            if (initial != null)
            {
                var items = initial as T[] ?? initial.ToArray();
                var initArray = new T[items.Count()];

                var i = 0;
                foreach (var item in items)
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

        private void BulkInit(T[] initial)
        {
            var i = (initial.Length - 1) / 2;

            while (i >= 0)
            {
                bulkInitRecursive(i, initial);
                i--;
            }

            heapArray = initial;
        }

        private void bulkInitRecursive(int i, T[] initial)
        {
            while (true)
            {
                var parent = i;

                var left = 2 * i + 1;
                var right = 2 * i + 2;

                var max = left < initial.Length && right < initial.Length ? comparer.Compare(initial[left], initial[right]) > 0 ? left : right
                    : left < initial.Length ? left
                    : right < initial.Length ? right : -1;

                if (max != -1 && comparer.Compare(initial[max], initial[parent]) > 0)
                {
                    var temp = initial[max];
                    initial[max] = initial[parent];
                    initial[parent] = temp;

                    //if max is child then drill down child
                    i = max;
                    continue;
                }

                break;
            }
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Insert(T newItem)
        {
            if (Count == heapArray.Length)
            {
                doubleArray();
            }

            heapArray[Count] = newItem;

            for (int i = Count; i > 0; i = (i - 1) / 2)
            {
                if (comparer.Compare(heapArray[i], heapArray[(i - 1) / 2]) > 0)
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

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T ExtractMax()
        {
            if (Count == 0)
            {
                throw new Exception("Empty heap");
            }

            var max = heapArray[0];

            delete(0);

            return max;
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

        /// <summary>
        /// Time complexity: O(n).
        /// </summary>
        public void Delete(T value)
        {
            var index = findIndex(value);

            if (index != -1)
            {
                delete(index);
                return;
            }

            throw new Exception("Item not found.");

        }

        /// <summary>
        /// Time complexity: O(n).
        /// </summary>
        public bool Exists(T value)
        {
            return findIndex(value) != -1;
        }

        private void delete(int parentIndex)
        {
            heapArray[parentIndex] = heapArray[Count - 1];
            Count--;

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

                    if (comparer.Compare(leftChild, rightChild) > 0)
                    {
                        leftIsMax = true;
                    }

                    var maxChildIndex = leftIsMax ? leftIndex : rightIndex;

                    if (comparer.Compare(heapArray[maxChildIndex], parent) > 0)
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
                    if (comparer.Compare(heapArray[leftIndex], parent) > 0)
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
                    if (comparer.Compare(heapArray[rightIndex], parent) > 0)
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
        }


        private int findIndex(T value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (heapArray[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
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