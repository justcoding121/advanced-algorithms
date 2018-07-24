using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A self expanding array implementation.
    /// </summary>
    /// <typeparam name="T">The datatype of this ArrayList.</typeparam>
    public class ArrayList<T> : IEnumerable<T>
    {
        private readonly int initialArraySize;
        private int arraySize;
        private T[] array;

        public int Length { get; private set; }

        /// <summary>
        /// Constructor.
        /// TimeComplexity: O(1) if initial is empty otherwise O(n).
        /// </summary>
        /// <param name="initalArraySize">The initial array size.</param>
        /// <param name="initial">Initial values if any.</param>
        public ArrayList(int initalArraySize = 2, IEnumerable<T> initial = null)
        {
            if (initalArraySize < 2)
            {
                throw new Exception("Initial array size must be greater than 1");
            }

            initialArraySize = initalArraySize;
            arraySize = initalArraySize;
            array = new T[arraySize];

            if (initial == null)
            {
                return;
            }

            foreach(var item in initial)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Constructor.
        /// TimeComplexity: O(1) if initial is empty otherwise O(n).
        /// </summary>
        /// <param name="initial">Initial values if any.</param>
        public ArrayList(IEnumerable<T> initial) 
            : this (2, initial){ }

        /// <summary>
        /// Indexed access to array.
        /// Time Complexity: O(1).
        /// </summary>
        /// <param name="index">The index to write or read.</param>
        /// <returns></returns>
        public T this[int index]
        {
            get => itemAt(index);
            set => setItem(index, value);
        }

        private T itemAt(int i)
        {
            if (i >= Length)
                throw new System.Exception("Index exeeds array size");

            return array[i];
        }

        /// <summary>
        /// Add a new item to this array list.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            grow();

            array[Length] = item;
            Length++;
        }

        /// <summary>
        /// Insert element at specified index
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="index">The index to insert at.<param>
        /// <param name="item">The item to insert.</param>
        public void InsertAt(int index, T item)
        {
            grow();

            shift(index);

            array[index] = item;
            Length++;
        }

        /// <summary>
        /// Shift the position of elements right by one starting at this index.
        /// Creates a blank field at index.
        /// </summary>
        private void shift(int index)
        {
            Array.Copy(array, index, array, index + 1, Length - index);
        }

        internal void Clear()
        {
            arraySize = initialArraySize;
            array = new T[arraySize];
            Length = 0;
        }

        private void setItem(int i, T item)
        {
            if (i >= Length)
                throw new System.Exception("Index exeeds array size");

            array[i] = item;
        }

        /// <summary>
        /// Remove the item at given index.
        /// Time complexity: O(1) amortized.
        /// </summary>
        /// <param name="i">The index to remove at.</param>
        public void RemoveAt(int i)
        {
            if (i >= Length)
                throw new System.Exception("Index exeeds array size");

            //shift elements
            for (var j = i; j < arraySize - 1; j++)
            {
                array[j] = array[j + 1];
            }

            Length--;

            shrink();
        }

        private void grow()
        {
            if (Length != arraySize)
            {
                return;
            }

            //increase array size exponentially on demand
            arraySize *= 2;

            var biggerArray = new T[arraySize];
            Array.Copy(array, 0, biggerArray, 0, Length);
            array = biggerArray;
        }

        private void shrink()
        {
            if (Length != arraySize / 2 || arraySize == initialArraySize)
            {
                return;
            }

            //reduce array by half 
            arraySize /= 2;

            var smallerArray = new T[arraySize];
            Array.Copy(array, 0, smallerArray, 0, Length);
            array = smallerArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListEnumerator<T>(array, Length);
        }
    }

    internal class ArrayListEnumerator<T> : IEnumerator<T>
    {
        private T[] array;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int position = -1;
        private int length;

        internal ArrayListEnumerator(T[] list, int length)
        {
            this.length = length;
            array = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                try
                {
                    return array[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            array = null;
        }
    }

}
