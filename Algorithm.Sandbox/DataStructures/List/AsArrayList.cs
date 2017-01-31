using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A self expanding array (dynamic array) aka array vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsArrayList<T> where T : IComparable
    {
        private int arraySize;
        private int currentEndPosition;
        private T[] array;

        public int Length => currentEndPosition;
        //constructor init 
        public AsArrayList()
        {
            arraySize = 2;
            array = new T[arraySize];
        }

        public T[] ToArray()
        {
            var newArray = new T[currentEndPosition];

            for (int i = 0; i < currentEndPosition; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }

        //O(1)
        public T ItemAt(int i)
        {
            if (i > array.Length)
                throw new System.Exception("Index exeeds array size");

            return array[i];
        }

        //O(1) amortized 
        public void AddItem(T item)
        {
            if (currentEndPosition == arraySize)
            {
                //increase array size exponentially on demand
                arraySize *= 2;

                var biggerArray = new T[arraySize];

                for (int i = 0; i < currentEndPosition; i++)
                {
                    biggerArray[i] = array[i];
                }

                array = biggerArray;
            }

            array[currentEndPosition] = item;
            currentEndPosition++;
        }

        //O(1)
        public void SetItem(int i, T item)
        {
            if (i > array.Length)
                throw new System.Exception("Index exeeds array size");

            array[i] = item;
        }

        //O(n) 
        public void RemoveItem(int i)
        {
            if (i > array.Length)
                throw new System.Exception("Index exeeds array size");

            //shift elements
            for (int j = i; j < arraySize - 1; j++)
            {
                array[j] = array[j + 1];
            }

            currentEndPosition--;

            if (currentEndPosition == arraySize / 2 && arraySize/2 !=0)
            {
                //reduce array by half 
                arraySize /= 2;

                var smallerArray = new T[arraySize];

                for (int j = 0; j < currentEndPosition; j++)
                {
                    smallerArray[j] = array[j];
                }

                array = smallerArray;
            }
        }

        //O(n) 
        public int Find(T item)
        {
            //shift elements
            for (int i = 0; i < arraySize; i++)
            {
                if (array[i].CompareTo(item) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        //O(n) 
        public void RemoveItem(T item)
        {
            //shift elements
            for (int i = 0; i < arraySize; i++)
            {
                if (array[i].CompareTo(item) == 0)
                {
                    RemoveItem(i);
                    return;
                }
            }

        }

    }
}
