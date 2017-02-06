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
    internal class AsOpenAddressHashSet<K, V> : AsIHashSetAsHashSet<K, V> where K : IComparable
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
            var index = Math.Abs(key.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                return false;
            }
            else
            {
                var current = hashArray[index];

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        return true;
                    }

                    //wrap around
                    if (index == hashArray.Length - 1)
                        index = -1;

                    current = hashArray[++index];
                }
            }

            return false;
        }

        //O(1) time complexity; worst case O(n)
        //add an item to this hash table
        public void Add(K key, V value)
        {
            Grow();

            var index = Math.Abs(key.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new AsHashSetNode<K, V>(key, value);
            }
            else
            {
                var current = hashArray[index];

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        throw new Exception("Duplicate key");
                    }

                    //wrap around
                    if (index == hashArray.Length - 1)
                        index = -1;

                    current = hashArray[++index];
                }

                hashArray[index] = new AsHashSetNode<K, V>(key, value);
            }

            Count++;
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(K key)
        {
            var index = Math.Abs(key.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("No such item for given key");
            }
            else
            {
                var current = hashArray[index];

                AsHashSetNode<K, V> item = null;
                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        item = current;
                        break;
                    }

                    //wrap around
                    if (index == hashArray.Length - 1)
                        index = -1;

                    current = hashArray[++index];
                }

                //remove
                if (item == null)
                {
                    throw new Exception("No such item for given key");
                }
                else
                {

                    hashArray[index] = null;
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
            var index = Math.Abs(key.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index];

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        current.Value = value;
                        return;
                    }

                    //wrap around
                    if (index == hashArray.Length - 1)
                        index = -1;

                    current = hashArray[++index];
                }
            }

            throw new Exception("Item not found");
        }

        private V GetValue(K key)
        {
            var index = Math.Abs(key.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index];

                while (current != null)
                {
                    if (current.Key.CompareTo(key) == 0)
                    {
                        return current.Value;
                    }

                    //wrap around
                    if (index == hashArray.Length - 1)
                        index = -1;

                    current = hashArray[++index];
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
                //increase array size exponentially on demand
                var newBucketSize = bucketSize * 2;

                var biggerArray = new AsHashSetNode<K, V>[newBucketSize];

                for (int i = 0; i < bucketSize; i++)
                {
                    var current = hashArray[i];

                    if (current != null)
                    {
                        var newIndex = Math.Abs(current.Key.GetHashCode()) % newBucketSize;
                        var newLocation = biggerArray[newIndex];

                        while (newLocation != null)
                        {
                            //wrap around
                            if (newIndex == biggerArray.Length - 1)
                                newIndex = -1;

                            newLocation = biggerArray[++newIndex];
                        }

                        biggerArray[newIndex] = current;
                    }
                }

                hashArray = biggerArray;
            }


        }

        /// <summary>
        /// Shrink if needed
        /// </summary>
        private void Shrink()
        {
            if (Count <= bucketSize * 0.3 && bucketSize != initialBucketSize)
            {
                //reduce array by half 
                var newBucketSize = bucketSize / 2;

                var smallerArray = new AsHashSetNode<K, V>[newBucketSize];

                for (int i = 0; i < bucketSize; i++)
                {
                    var current = hashArray[i];

                    if (current != null)
                    {
                        var newIndex = Math.Abs(current.Key.GetHashCode()) % newBucketSize;
                        var newLocation = smallerArray[newIndex];

                        while (newLocation != null)
                        {
                            //wrap around
                            if (newIndex == smallerArray.Length - 1)
                                newIndex = -1;

                            newLocation = smallerArray[++newIndex];
                        }

                        smallerArray[newIndex] = current;
                    }
                }

                hashArray = smallerArray;
            }
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
