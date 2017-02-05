using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A self expanding array (dynamic array) aka array vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsArrayList<T> : IEnumerable<T>
    {

        private int currentEndPosition;
        private T[] array;
        private int initArraySize;

        public int ArraySize { get; private set; }
        public int Length => currentEndPosition;
        //constructor init 
        public AsArrayList(int initalArraySize = 2)
        {
            if (initalArraySize < 2)
            {
                throw new Exception("Initial array size must be greater than 1");
            }

            initArraySize = initalArraySize;
            ArraySize = initalArraySize;
            array = new T[ArraySize];
        }

        //public T[] ToArray()
        //{
        //    var newArray = new T[Length];

        //    for (int i = 0; i < Length; i++)
        //    {
        //        newArray[i] = array[i];
        //    }

        //    return newArray;
        //}

        /// <summary>
        /// Expose indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return ItemAt(index); }
            set { SetItem(index, value); }
        }

        //O(1)
        private T ItemAt(int i)
        {
            if (i >= Length)
                throw new System.Exception("Index exeeds array size");

            return array[i];
        }

        //O(1) amortized 
        public void AddItem(T item)
        {
            Grow();

            array[currentEndPosition] = item;
            currentEndPosition++;
        }


        /// <summary>
        /// empty the list
        /// </summary>
        internal void Clear()
        {
            ArraySize = initArraySize;
            array = new T[ArraySize];
            currentEndPosition = 0;
        }

        //O(1)
        private void SetItem(int i, T item)
        {
            if (i >= Length)
                throw new System.Exception("Index exeeds array size");

            array[i] = item;
        }

        //O(n) 
        public void RemoveItem(int i)
        {
            if (i >= Length)
                throw new System.Exception("Index exeeds array size");

            //shift elements
            for (int j = i; j < ArraySize - 1; j++)
            {
                array[j] = array[j + 1];
            }

            currentEndPosition--;

            Shrink();
        }


        /// <summary>
        /// Grow array if needed
        /// </summary>
        private void Grow()
        {
            if (currentEndPosition == ArraySize)
            {
                //increase array size exponentially on demand
                ArraySize *= 2;

                var biggerArray = new T[ArraySize];

                for (int i = 0; i < currentEndPosition; i++)
                {
                    biggerArray[i] = array[i];
                }

                array = biggerArray;
            }
        }

        /// <summary>
        /// Shrink array if needed
        /// </summary>
        private void Shrink()
        {
            if (currentEndPosition == ArraySize / 2 && ArraySize != initArraySize)
            {
                //reduce array by half 
                ArraySize /= 2;

                var smallerArray = new T[ArraySize];

                for (int j = 0; j < currentEndPosition; j++)
                {
                    smallerArray[j] = array[j];
                }

                array = smallerArray;
            }
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListEnumerator<T>(array, Length);
        }

    }

    //  implement IEnumerator.
    public class ArrayListEnumerator<T> : IEnumerator<T>
    {
        private T[] _array;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        int length;

        public ArrayListEnumerator(T[] list, int length)
        {
            this.length = length;
            _array = list;
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

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
            
                try
                {
                    return _array[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {

        }
    }

}
