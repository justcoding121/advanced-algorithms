using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    /// <summary>
    /// A hash table implementation (key value dictionary) with Open Addressing
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    internal class AsOpenAddressHashSet<K, V> : AsIHashSet<K, V> where K : IComparable
    {

        private AsHashSetNode<K, V>[] hashArray;
        private int bucketSize => hashArray.Length;
        private int initialBucketSize;
      

        public int Count { get; private set; }

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsOpenAddressHashSet(int initialBucketSize = 2)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new AsHashSetNode<K, V>[initialBucketSize];
        }

        public V this[K key]
        {
            get { return GetValue(key); }
            set { SetValue(key, value); }

        }
        //O(1) time complexity; worst case O(n)
        public bool ContainsKey(K key)
        {
            var hashCode = SaltHash(key.GetHashCode());
            var index = hashCode % bucketSize;

            if (hashArray[index] == null)
            {
                return false;
            }
            else
            {
                var current = hashArray[index];

                //keep track of this so that we won't circle around infinitely
                var hitKey = current.Key;

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        return true;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Key.CompareTo(hitKey) == 0)
                    {
                        break;
                    }
                }
            }

            return false;
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(K key, V value)
        {

            Grow();

            var hashCode = SaltHash(key.GetHashCode());

            var index = hashCode % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new AsHashSetNode<K, V>(key, value);
            }
            else
            {
                var current = hashArray[index];
                //keep track of this so that we won't circle around infinitely
                var hitKey = current.Key;

                while (current != null)
                {

                    if (current.Key.CompareTo(key) == 0)
                    {
                        throw new Exception("Duplicate key");
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    if (current != null && current.Key.CompareTo(hitKey) == 0)
                    {
                        throw new Exception("HashSet is full");
                    }
                }

                hashArray[index] = new AsHashSetNode<K, V>(key, value);
            }

            Count++;

        }

        //O(1) time complexity; worst case O(n)
        public void Remove(K key)
        {
            var hashCode = SaltHash(key.GetHashCode());
            var curIndex = hashCode % bucketSize;

            if (hashArray[curIndex] == null)
            {
                throw new Exception("No such item for given key");
            }
            else
            {
                var current = hashArray[curIndex];

                //prevent circling around infinitely
                var hitKey = current.Key;

                AsHashSetNode<K, V> target = null;

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        target = current;
                        break;
                    }

                    curIndex++;

                    //wrap around
                    if (curIndex == bucketSize)
                        curIndex = 0;

                    current = hashArray[curIndex];

                    if (current != null && current.Key.CompareTo(hitKey) == 0)
                    {
                        throw new Exception("No such item for given key");
                    }
                }

                //remove
                if (target == null)
                {
                    throw new Exception("No such item for given key");
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
                        Add(current.Key, current.Value);
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
            hashArray = new AsHashSetNode<K, V>[initialBucketSize];
            Count = 0;
        }


        private void SetValue(K key, V value)
        {
            var index = (SaltHash(key.GetHashCode())) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index];
                var hitKey = current.Key;

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        current.Value = value;
                        return;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Key.CompareTo(hitKey) == 0)
                    {
                        throw new Exception("Item not found");
                    }
                }
            }

            throw new Exception("Item not found");
        }

        private V GetValue(K key)
        {
            var index = (SaltHash(key.GetHashCode())) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index];
                var hitKey = current.Key;

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        return current.Value;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Key.CompareTo(hitKey) == 0)
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
                hashArray = new AsHashSetNode<K, V>[bucketSize * 2];

                for (int i = 0; i < orgBucketSize; i++)
                {
                    var current = currentArray[i];

                    if (current != null)
                    {
                        Add(current.Key, current.Value);
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
                hashArray = new AsHashSetNode<K, V>[bucketSize / 2];

                for (int i = 0; i < orgBucketSize; i++)
                {
                    var current = currentArray[i];

                    if (current != null)
                    {
                        Add(current.Key, current.Value);
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
        private int SaltHash(int key)
        {
            return key * 3;
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AsHashSetNode<K, V>> GetEnumerator()
        {
            return new OpenAddressHashSetEnumerator<K, V>(hashArray, hashArray.Length);
        }

    }

    //  implement IEnumerator.
    public class OpenAddressHashSetEnumerator<K, V> : IEnumerator<AsHashSetNode<K, V>> where K : IComparable
    {
        internal AsHashSetNode<K, V>[] _array;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        int length;

        public OpenAddressHashSetEnumerator(AsHashSetNode<K, V>[] list, int length)
        {
            this.length = length;
            _array = list;
        }

        public bool MoveNext()
        {
            position++;

            while (position < length && _array[position] == null)
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

        public AsHashSetNode<K, V> Current
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
