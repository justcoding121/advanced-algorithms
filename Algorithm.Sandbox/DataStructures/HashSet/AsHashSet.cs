using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// key-value set
    /// </summary>
    public class AsHashSetNode<K, V> : IComparable where K : IComparable
    {
        public K Key;
        public V Value;

        public AsHashSetNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as AsHashSetNode<K, V>);
        }

        private int CompareTo(AsHashSetNode<K, V> node)
        {
            return Key.CompareTo(node.Key);
        }
    }
    /// <summary>
    /// A hash table implementation (key value dictionary)
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsHashSet<K, V> : IEnumerable<AsHashSetNode<K, V>> where K : IComparable
    {

        private AsSinglyLinkedList<AsHashSetNode<K, V>>[] hashArray;
        private int bucketSize => hashArray.Length;
        private int initialBucketSize;

        public int Count { get; private set; }

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsHashSet(int initialBucketSize = 2)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new AsSinglyLinkedList<AsHashSetNode<K, V>>[initialBucketSize];
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
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key) == 0)
                    {
                        return true;
                    }

                    current = current.Next;
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
                hashArray[index] = new AsSinglyLinkedList<AsHashSetNode<K, V>>();
                hashArray[index].InsertFirst(new AsHashSetNode<K, V>(key, value));
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key) == 0)
                    {
                        throw new Exception("Duplicate key");
                    }

                    current = current.Next;
                }

                hashArray[index].InsertFirst(new AsHashSetNode<K, V>(key, value));
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
                var current = hashArray[index].Head;

                //VODO merge both search and remove to a single loop here!
                AsHashSetNode<K, V> item = null;
                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key) == 0)
                    {
                        item = current.Data;
                        break;
                    }

                    current = current.Next;
                }

                //remove
                if (item == null)
                {
                    throw new Exception("No such item for given key");
                }
                else
                {

                    hashArray[index].Delete(item);
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
            hashArray = new AsSinglyLinkedList<AsHashSetNode<K, V>>[initialBucketSize];
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
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key) == 0)
                    {
                        current.Data.Value = value;
                        return;
                    }

                    current = current.Next;
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
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.CompareTo(key) == 0)
                    {
                        return current.Data.Value;
                    }

                    current = current.Next;
                }
            }

            throw new Exception("Item not found");
        }
        /// <summary>
        /// Grow array if needed
        /// </summary>
        private void Grow()
        {
            if (bucketSize == Count)
            {
                //increase array size exponentially on demand
                var newBucketSize = bucketSize * 2;

                var biggerArray = new AsSinglyLinkedList<AsHashSetNode<K, V>>[newBucketSize];

                for (int i = 0; i < bucketSize; i++)
                {
                    var item = hashArray[i];

                    //hashcode changes when bucket size changes
                    if (item != null)
                    {
                        if (item.Head != null)
                        {
                            var current = item.Head;

                            //find new location for each item
                            while (current != null)
                            {
                                var next = current.Next;

                                var newIndex = Math.Abs(current.Data.Key.GetHashCode()) % newBucketSize;

                                if (biggerArray[newIndex] == null)
                                {
                                    biggerArray[newIndex] = new AsSinglyLinkedList<AsHashSetNode<K, V>>();
                                }

                                biggerArray[newIndex].InsertFirst(current);

                                current = next;
                            }

                        }

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
            if (Count == bucketSize / 2 && bucketSize != initialBucketSize)
            {
                //reduce array by half 
                var newBucketSize = bucketSize / 2;

                var smallerArray = new AsSinglyLinkedList<AsHashSetNode<K, V>>[newBucketSize];

                for (int i = 0; i < bucketSize; i++)
                {
                    var item = hashArray[i];

                    //hashcode changes when bucket size changes
                    if (item != null)
                    {
                        if (item.Head != null)
                        {
                            var current = item.Head;

                            //find new location for each item
                            while (current != null)
                            {
                                var next = current.Next;

                                var newIndex = Math.Abs(current.Data.Key.GetHashCode()) % newBucketSize;

                                if (smallerArray[newIndex] == null)
                                {
                                    smallerArray[newIndex] = new AsSinglyLinkedList<AsHashSetNode<K, V>>();
                                }

                                smallerArray[newIndex].InsertFirst(current);

                                current = next;
                            }
                        }

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
            return new HashSetEnumerator<K, V>(hashArray, bucketSize);
        }

    }

    //  implement IEnumerator.
    public class HashSetEnumerator<K, V> : IEnumerator<AsHashSetNode<K, V>> where K : IComparable
    {
        internal AsSinglyLinkedList<AsHashSetNode<K, V>>[] _array;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        AsSinglyLinkedListNode<AsHashSetNode<K, V>> currentNode = null;

        int length;

        internal HashSetEnumerator(AsSinglyLinkedList<AsHashSetNode<K, V>>[] list, int length)
        {
            this.length = length;
            _array = list;
        }

        public bool MoveNext()
        {
            if (currentNode != null && currentNode.Next != null)
            {
                currentNode = currentNode.Next;
                return true;
            }

            while (currentNode == null || currentNode.Next == null)
            {
                position++;

                if (position < length)
                {
                    if (_array[position] == null)
                        continue;

                    currentNode = _array[position].Head;

                    if (currentNode == null)
                        continue;

                    return true;
                }
                else
                {
                    break;
                }
            }

            return false;

        }

        public void Reset()
        {
            position = -1;
            currentNode = null;
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
                    return currentNode.Data;
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
