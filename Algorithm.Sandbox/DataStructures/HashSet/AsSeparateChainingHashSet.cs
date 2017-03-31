using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{

    /// <summary>
    /// A hash table implementation (value value HashSet) with separate chaining
    /// TODO improve performance by using a Prime number greater than total elements as Bucket Size
    /// </summary>
    /// <typeparam name="V"></typeparam>
    internal class AsSeparateChainingHashSet<V> : AsIHashSet<V>  
    {

        private AsDoublyLinkedList<AsHashSetNode<V>>[] hashArray;
        private int bucketSize => hashArray.Length;
        private int initialBucketSize;
        private int filledBuckets;

        public int Count { get; private set; }

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public AsSeparateChainingHashSet(int initialBucketSize = 3)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new AsDoublyLinkedList<AsHashSetNode<V>>[initialBucketSize];
        }

        //O(1) time complexity; worst case O(n)
        public bool Contains(V value)
        {
            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                return false;
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Value.Equals(value))
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
        public void Add(V value)
        {
            Grow();

            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new AsDoublyLinkedList<AsHashSetNode<V>>();
                hashArray[index].InsertFirst(new AsHashSetNode<V>(value));
                filledBuckets++;
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Value.Equals(value))
                    {
                        throw new Exception("Duplicate value");
                    }

                    current = current.Next;
                }

                hashArray[index].InsertFirst(new AsHashSetNode<V>(value));
            }

            Count++;
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(V value)
        {
            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("No such item for given value");
            }
            else
            {
                var current = hashArray[index].Head;

                //VODO merge both search and remove to a single loop here!
                AsDoublyLinkedListNode<AsHashSetNode<V>> item = null;
                while (current != null)
                {
                    if (current.Data.Value.Equals(value))
                    {
                        item = current;
                        break;
                    }

                    current = current.Next;
                }

                //remove
                if (item == null)
                {
                    throw new Exception("No such item for given value");
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
            hashArray = new AsDoublyLinkedList<AsHashSetNode<V>>[initialBucketSize];
            Count = 0;
            filledBuckets = 0;
        }


        private void SetValue(V value)
        {
            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Value.Equals(value))
                    {
                        Remove(value);
                        Add(value);
                        return;
                    }

                    current = current.Next;
                }
            }

            throw new Exception("Item not found");
        }

        private V GetValue(V value)
        {
            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("Item not found");
            }
            else
            {
                var current = hashArray[index].Head;

                while (current != null)
                {
                    if (current.Data.Value.Equals(value))
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

                var biggerArray = new AsDoublyLinkedList<AsHashSetNode<V>>[newBucketSize];

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

                                var newIndex = Math.Abs(current.Data.Value.GetHashCode()) % newBucketSize;

                                if (biggerArray[newIndex] == null)
                                {
                                    filledBuckets++;
                                    biggerArray[newIndex] = new AsDoublyLinkedList<AsHashSetNode<V>>();
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

                var smallerArray = new AsDoublyLinkedList<AsHashSetNode<V>>[newBucketSize];

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

                                var newIndex = Math.Abs(current.Data.Value.GetHashCode()) % newBucketSize;

                                if (smallerArray[newIndex] == null)
                                {
                                    filledBuckets++;
                                    smallerArray[newIndex] = new AsDoublyLinkedList<AsHashSetNode<V>>();
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

        public IEnumerator<AsHashSetNode<V>> GetEnumerator()
        {
            return new AsSeparateChainingHashSetEnumerator<V>(hashArray, bucketSize);
        }

    }

    //  implement IEnumerator.
    public class AsSeparateChainingHashSetEnumerator<V> : IEnumerator<AsHashSetNode<V>> 
    {
        internal AsDoublyLinkedList<AsHashSetNode<V>>[] hashList;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        AsDoublyLinkedListNode<AsHashSetNode<V>> currentNode = null;

        int length;

        internal AsSeparateChainingHashSetEnumerator(AsDoublyLinkedList<AsHashSetNode<V>>[] hashList, int length)
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

        public AsHashSetNode<V> Current
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
