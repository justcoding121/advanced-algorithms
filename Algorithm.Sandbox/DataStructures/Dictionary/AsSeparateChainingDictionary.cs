using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    /// <summary>
    /// A hash table implementation (key value dictionary) with separate chaining
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    internal class AsSeparateChainingDictionary<K, V> : AsIDictionary<K, V>  
    {

        private AsDoublyLinkedList<AsDictionaryNode<K, V>>[] hashArray;
        private int bucketSize => hashArray.Length;
        private int initialBucketSize;
        private int filledBuckets;

        public int Count { get; private set; }

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsSeparateChainingDictionary(int initialBucketSize = 3)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new AsDoublyLinkedList<AsDictionaryNode<K, V>>[initialBucketSize];
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
                    if (current.Data.Key.Equals(key))
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
                hashArray[index] = new AsDoublyLinkedList<AsDictionaryNode<K, V>>();
                hashArray[index].InsertFirst(new AsDictionaryNode<K, V>(key, value));
                filledBuckets++;
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Key.Equals(key))
                    {
                        throw new Exception("Duplicate key");
                    }

                    current = current.Next;
                }

                hashArray[index].InsertFirst(new AsDictionaryNode<K, V>(key, value));
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
                AsDoublyLinkedListNode<AsDictionaryNode<K, V>> item = null;
                while (current != null)
                {
                    if (current.Data.Key.Equals(key))
                    {
                        item = current;
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

                    //if list is empty mark bucket as null
                    if (hashArray[index].Head == null)
                    {
                        hashArray[index] = null;
                        filledBuckets--;
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
            hashArray = new AsDoublyLinkedList<AsDictionaryNode<K, V>>[initialBucketSize];
            Count = 0;
            filledBuckets = 0;
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
                    if (current.Data.Key.Equals(key))
                    {
                        Remove(key);
                        Add(key, value);
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
                    if (current.Data.Key.Equals(key))
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
            if (filledBuckets >= bucketSize * 0.7)
            {
                filledBuckets = 0;
                //increase array size exponentially on demand
                var newBucketSize = bucketSize * 2;

                var biggerArray = new AsDoublyLinkedList<AsDictionaryNode<K, V>>[newBucketSize];

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
                                    filledBuckets++;
                                    biggerArray[newIndex] = new AsDoublyLinkedList<AsDictionaryNode<K, V>>();
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
            if (filledBuckets == bucketSize * 0.3 && bucketSize / 2 > initialBucketSize)
            {
                filledBuckets = 0;
                //reduce array by half 
                var newBucketSize = bucketSize / 2;

                var smallerArray = new AsDoublyLinkedList<AsDictionaryNode<K, V>>[newBucketSize];

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
                                    filledBuckets++;
                                    smallerArray[newIndex] = new AsDoublyLinkedList<AsDictionaryNode<K, V>>();
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

        public IEnumerator<AsDictionaryNode<K, V>> GetEnumerator()
        {
            return new AsSeparateChainingDictionaryEnumerator<K, V>(hashArray, bucketSize);
        }

    }

    //  implement IEnumerator.
    public class AsSeparateChainingDictionaryEnumerator<K, V> : IEnumerator<AsDictionaryNode<K, V>> 
    {
        internal AsDoublyLinkedList<AsDictionaryNode<K, V>>[] hashList;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        AsDoublyLinkedListNode<AsDictionaryNode<K, V>> currentNode = null;

        int length;

        internal AsSeparateChainingDictionaryEnumerator(AsDoublyLinkedList<AsDictionaryNode<K, V>>[] hashList, int length)
        {
            this.length = length;
            this.hashList = hashList;
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
                    if (hashList[position] == null)
                        continue;

                    currentNode = hashList[position].Head;

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

        public AsDictionaryNode<K, V> Current
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
