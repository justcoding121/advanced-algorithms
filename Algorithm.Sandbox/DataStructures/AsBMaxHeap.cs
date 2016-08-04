using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMaxHeap<T> where T : IComparable
    {
        
        private T[] heapArray;

        public int Count = 0;

        public AsBMaxHeap()
        {
            this.heapArray = new T[2];
        }

        //o(log(n))
        public void Insert(T newItem)
        {
            if (Count == heapArray.Length)
            {
                doubleArray();
            }

            heapArray[Count] = newItem;

            for (int i = Count; i > 0; i = i / 2)
            {
                if (heapArray[i].CompareTo(heapArray[i / 2]) > 0)
                {
                    var temp = heapArray[i / 2];
                    heapArray[i / 2] = heapArray[i];
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
            var max = heapArray[0];

            heapArray[0] = heapArray[Count - 1];
            Count--;

            int i = 0;

            //percolate down
            while (true)
            {
                var leftIndex = 2 * i + 1;
                var rightIndex = 2 * i + 2;

                var parent = heapArray[i];

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
                        var temp = heapArray[i];
                        heapArray[i] = heapArray[maxChildIndex];
                        heapArray[maxChildIndex] = temp;

                        if (leftIsMax)
                        {
                            i = 2 * i + 1;
                        }
                        else
                        {
                            i = 2 * i + 2;
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                else if (leftIndex < Count && rightIndex > Count)
                {
                    if (heapArray[leftIndex].CompareTo(parent) > 0)
                    {
                        var temp = heapArray[i];
                        heapArray[i] = heapArray[leftIndex];
                        heapArray[leftIndex] = temp;

                        i = 2 * i + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (rightIndex < Count && leftIndex > Count)
                {
                    if (heapArray[rightIndex].CompareTo(parent) > 0)
                    {
                        var temp = heapArray[i];
                        heapArray[i] = heapArray[rightIndex];
                        heapArray[rightIndex] = temp;

                        i = 2 * i + 2;
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

            return max;
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

        private void doubleArray()
        {
            var biggerArray = new T[heapArray.Length * 2];

            for (int i = 0; i < heapArray.Length; i++)
            {
                biggerArray[i] = heapArray[i];
            }

            heapArray = biggerArray;
        }
    }
}
