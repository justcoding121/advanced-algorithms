using System;
using System.Linq;

namespace Algorithm.Sandbox.DataStructures.Set
{
    public class SparseSet
    {
        private int[] sparse;
        private int[] dense;

        public int Length { get; private set; }

        public SparseSet(int maxVal, int capacity)
        {
            sparse = Enumerable.Repeat(-1, maxVal + 1).ToArray(); 
            dense = Enumerable.Repeat(-1, capacity).ToArray();
        }

        public void Add(int value)
        {
            if (value < 0)
            {
                throw new Exception("Negative values not supported.");
            }

            if (value >= sparse.Length)
            {
                throw new Exception("Item is greater than max value.");
            }

            if (Length >= dense.Length)
            {
                throw new Exception("Set reached its capacity.");
            }

            sparse[value] = Length;
            dense[Length] = value;
            Length++;
        }

        public void Remove(int value)
        {
            if (value < 0)
            {
                throw new Exception("Negative values not supported.");
            }

            if (value >= sparse.Length)
            {
                throw new Exception("Item is greater than max value.");
            }

            if (HasItem(value) == false)
            {
                throw new Exception("Item do not exist.");
            }

            //find element
            var index = sparse[value];
            sparse[value] = -1;

            //replace index with last value of dense
            var lastVal = dense[Length - 1];
            dense[index] = lastVal;
            dense[Length - 1] = -1;

            //update sparse for lastVal
            sparse[lastVal] = index;

            Length--;
        }

        public bool HasItem(int value)
        {
            var index = sparse[value];

            if(index == -1 || dense[index] != value)
            {
                return false;
            }

            return true;
        }

        public void Clear()
        {
            Length = 0;
        }
    }
}
