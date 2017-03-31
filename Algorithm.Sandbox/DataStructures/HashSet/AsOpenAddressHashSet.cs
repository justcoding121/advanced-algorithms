using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    /// <summary>
    /// A hash table implementation (value value HashSet) with Open Addressing
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    internal class AsOpenAddressHashSet<V> : AsIHashSet<V> 
    {

        private AsHashSetNode<V>[] hashArray;
        private int bucketSize => hashArray.Length;
        private int initialBucketSize;


        public int Count { get; private set; }

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsOpenAddressHashSet(int initialBucketSize = 2)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new AsHashSetNode<V>[initialBucketSize];
        }

        //O(1) time complexity; worst case O(n)
        public bool Contains(V value)
        {
            var hashCode = SaltHash(Math.Abs(value.GetHashCode()));
            var index = hashCode % bucketSize;

            if (hashArray[index] == null)
            {
                return false;
            }
            else
            {
                var current = hashArray[index];

                //keep track of this so that we won't circle around infinitely
                var hitKey = current.Value;

                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        return true;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Value.Equals(hitKey))
                    {
                        break;
                    }
                }
            }

            return false;
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(V value)
        {

            Grow();

            var hashCode = SaltHash(Math.Abs(value.GetHashCode()));

            var index = hashCode % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new AsHashSetNode<V>(value);
            }
            else
            {
                var current = hashArray[index];
                //keep track of this so that we won't circle around infinitely
                var hitKey = current.Value;

                while (current != null)
                {

                    if (current.Value.Equals(value))
                    {
                        throw new Exception("Duplicate value");
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    if (current != null && current.Value.Equals(hitKey))
                    {
                        throw new Exception("HashSet is full");
                    }
                }

                hashArray[index] = new AsHashSetNode<V>(value);
            }

            Count++;

        }

        //O(1) time complexity; worst case O(n)
        public void Remove(V value)
        {
            var hashCode = SaltHash(Math.Abs(value.GetHashCode()));
            var curIndex = hashCode % bucketSize;

            if (hashArray[curIndex] == null)
            {
                throw new Exception("No such item for given value");
            }
            else
            {
                var current = hashArray[curIndex];

                //prevent circling around infinitely
                var hitKey = current.Value;

                AsHashSetNode<V> target = null;

                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        target = current;
                        break;
                    }

                    curIndex++;

                    //wrap around
                    if (curIndex == bucketSize)
                        curIndex = 0;

                    current = hashArray[curIndex];

                    if (current != null && current.Value.Equals(hitKey))
                    {
                        throw new Exception("No such item for given value");
                    }
                }

                //remove
                if (target == null)
                {
                    throw new Exception("No such item for given value");
                }
                else
                {
                    //delete this element
                    hashArray[curIndex] = null;

                    //now time to cleanup subsequent broken hash elements due to this emptied cell
                    curIndex++;

                    //wrap around
                    if (curIndex == bucketSize)
                        curIndex = 0;

                    current = hashArray[curIndex];

                    //until an empty cell
                    while (current != null)
                    {
                        //delete current
                        hashArray[curIndex] = null;

                        //add current back to table
                        Add(current.Value);
                        Count--;

                        curIndex++;

                        //wrap around
                        if (curIndex == bucketSize)
                            curIndex = 0;

                        current = hashArray[curIndex];
                    }

                }

            }

            Count--;

            Shrink();

        }

        /// <summary>
        /// clear hash table
        /// </summary>
        public void Clear()
        {
            hashArray = new AsHashSetNode<V>[initialBucketSize];
            Count = 0;
        }


        private void SetValue(V value)
        {
            var index = (SaltHash(Math.Abs(value.GetHashCode()))) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index];
                var hitKey = current.Value;

                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        Remove(value);
                        Add(value);
                        return;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Value.Equals(hitKey))
                    {
                        throw new Exception("Item not found");
                    }
                }
            }

            throw new Exception("Item not found");
        }

        private V GetValue(V value)
        {
            var index = (SaltHash(Math.Abs(value.GetHashCode()))) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index];
                var hitKey = current.Value;

                while (current != null)
                {
                    if (current.Value.Equals(value))
                    {
                        return current.Value;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Value.Equals(hitKey))
                    {
                        throw new Exception("Item not found");
                    }
                }
            }

            throw new Exception("Item not found");
        }
        /// <summary>
        /// Grow array if needed
        /// </summary>
        private void Grow()
        {
            if (bucketSize * 0.7 <= Count)
            {
                var orgBucketSize = bucketSize;
                var currentArray = hashArray;

                //increase array size exponentially on demand
                hashArray = new AsHashSetNode<V>[bucketSize * 2];

                for (int i = 0; i < orgBucketSize; i++)
                {
                    var current = currentArray[i];

                    if (current != null)
                    {
                        Add(current.Value);
                        Count--;
                    }
                }

                currentArray = null;
            }
        }

        /// <summary>
        /// Shrink if needed
        /// </summary>
        private void Shrink()
        {
            if (Count <= bucketSize * 0.3 && bucketSize / 2 > initialBucketSize)
            {
                var orgBucketSize = bucketSize;

                var currentArray = hashArray;

                //reduce array by half logarithamic
                hashArray = new AsHashSetNode<V>[bucketSize / 2];

                for (int i = 0; i < orgBucketSize; i++)
                {
                    var current = currentArray[i];

                    if (current != null)
                    {
                        Add(current.Value);
                        Count--;
                    }
                }

                currentArray = null;
            }
        }

        /// <summary>
        /// salt the hash with a random number
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private int SaltHash(int value)
        {
            return value * 3;
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AsHashSetNode<V>> GetEnumerator()
        {
            return new AsOpenAddressHashSetEnumerator<V>(hashArray, hashArray.Length);
        }

    }

    //  implement IEnumerator.
    public class AsOpenAddressHashSetEnumerator<V> : IEnumerator<AsHashSetNode<V>> 
    {
        internal AsHashSetNode<V>[] hashArray;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        int length;

        public AsOpenAddressHashSetEnumerator(AsHashSetNode<V>[] hashArray, int length)
        {
            this.length = length;
            this.hashArray = hashArray;
        }

        public bool MoveNext()
        {
            position++;

            while (position < length && hashArray[position] == null)
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

        public AsHashSetNode<V> Current
        {
            get
            {

                try
                {
                    return hashArray[position];
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
