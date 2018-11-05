using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    internal class OpenAddressDictionary<TK, TV> : IDictionary<TK, TV> 
    {
        private DictionaryKeyValuePair<TK, TV>[] hashArray;
        private int bucketSize => hashArray.Length;
        private readonly int initialBucketSize;

        public int Count { get; private set; }

        internal OpenAddressDictionary(int initialBucketSize = 2)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new DictionaryKeyValuePair<TK, TV>[initialBucketSize];
        }

        public TV this[TK key]
        {
            get => getValue(key);
            set => setValue(key, value);
        }


        public bool ContainsKey(TK key)
        {
            var hashCode = getHash(key);
            var index = hashCode % bucketSize;

            if (hashArray[index] == null)
            {
                return false;
            }

            var current = hashArray[index];

            //keep track of this so that we won't circle around infinitely
            var hitKey = current.Key;

            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    return true;
                }

                index++;

                //wrap around
                if (index == bucketSize)
                    index = 0;

                current = hashArray[index];

                //reached original hit again
                if (current != null && current.Key.Equals(hitKey))
                {
                    break;
                }
            }

            return false;
        }

        public void Add(TK key, TV value)
        {
            grow();

            var hashCode = getHash(key);

            var index = hashCode % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new DictionaryKeyValuePair<TK, TV>(key, value);
            }
            else
            {
                var current = hashArray[index];
                //keep track of this so that we won't circle around infinitely
                var hitKey = current.Key;

                while (current != null)
                {
                    if (current.Key.Equals(key))
                    {
                        throw new Exception("Duplicate key");
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    if (current != null && current.Key.Equals(hitKey))
                    {
                        throw new Exception("Dictionary is full");
                    }
                }

                hashArray[index] = new DictionaryKeyValuePair<TK, TV>(key, value);
            }

            Count++;

        }

        public void Remove(TK key)
        {
            var hashCode = getHash(key);
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

                DictionaryKeyValuePair<TK, TV> target = null;

                while (current != null)
                {
                    if (current.Key.Equals(key))
                    {
                        target = current;
                        break;
                    }

                    curIndex++;

                    //wrap around
                    if (curIndex == bucketSize)
                        curIndex = 0;

                    current = hashArray[curIndex];

                    if (current != null && current.Key.Equals(hitKey))
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

            shrink();

        }


        public void Clear()
        {
            hashArray = new DictionaryKeyValuePair<TK, TV>[initialBucketSize];
            Count = 0;
        }


        private void setValue(TK key, TV value)
        {
            var index = getHash(key) % bucketSize;

            if (hashArray[index] == null)
            {
                Add(key, value);
            }
            else
            {
                var current = hashArray[index];
                var hitKey = current.Key;

                while (current != null)
                {
                    if (current.Key.Equals(key))
                    {
                        Remove(key);
                        Add(key, value);
                        return;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Key.Equals(hitKey))
                    {
                        throw new Exception("Item not found");
                    }
                }
            }

            throw new Exception("Item not found");
        }

        private TV getValue(TK key)
        {
            var index = getHash(key) % bucketSize;

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
                    if (current.Key.Equals(key))
                    {
                        return current.Value;
                    }

                    index++;

                    //wrap around
                    if (index == bucketSize)
                        index = 0;

                    current = hashArray[index];

                    //reached original hit again
                    if (current != null && current.Key.Equals(hitKey))
                    {
                        throw new Exception("Item not found");
                    }
                }
            }

            throw new Exception("Item not found");
        }

        private void grow()
        {
            if (bucketSize * 0.7 <= Count)
            {
                var orgBucketSize = bucketSize;
                var currentArray = hashArray;

                //increase array size exponentially on demand
                hashArray = new DictionaryKeyValuePair<TK, TV>[bucketSize * 2];

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


        private void shrink()
        {
            if (Count <= bucketSize * 0.3 && bucketSize / 2 > initialBucketSize)
            {
                var orgBucketSize = bucketSize;

                var currentArray = hashArray;

                //reduce array by half logarithamic
                hashArray = new DictionaryKeyValuePair<TK, TV>[bucketSize / 2];

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

        private int getHash(TK key)
        {
            return Math.Abs(key.GetHashCode());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return new OpenAddressDictionaryEnumerator<TK, TV>(hashArray, hashArray.Length);
        }

    }

    internal class DictionaryKeyValuePair<K, V>
    {
        internal K Key;
        internal V Value;

        internal DictionaryKeyValuePair(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    internal class OpenAddressDictionaryEnumerator<TK, TV> : IEnumerator<KeyValuePair<TK, TV>> 
    {
        internal DictionaryKeyValuePair<TK, TV>[] HashArray;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        private readonly int length;

        internal OpenAddressDictionaryEnumerator(DictionaryKeyValuePair<TK, TV>[] hashArray, int length)
        {
            this.length = length;
            HashArray = hashArray;
        }

        public bool MoveNext()
        {
            position++;

            while (position < length && HashArray[position] == null)
                position++;

            return (position < length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public KeyValuePair<TK, TV> Current
        {
            get
            {

                try
                {
                    return new KeyValuePair<TK, TV>(HashArray[position].Key, HashArray[position].Value);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            HashArray = null;
        }
    }
}
