using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A hash table implementation (value value HashSet) with separate chaining
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    internal class SeparateChainingHashSet<TV> : IHashSet<TV>  
    {
        private const double tolerance = 0.1;
        private DoublyLinkedList<HashSetNode<TV>>[] hashArray;
        private int bucketSize => hashArray.Length;
        private readonly int initialBucketSize;
        private int filledBuckets;

        public int Count { get; private set; }

        //init with an expected size (the larger the size lesser the collission, but memory matters!)
        public SeparateChainingHashSet(int initialBucketSize = 3)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new DoublyLinkedList<HashSetNode<TV>>[initialBucketSize];
        }

        //O(1) time complexity; worst case O(n)
        public bool Contains(TV value)
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
        public void Add(TV value)
        {
            grow();

            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new DoublyLinkedList<HashSetNode<TV>>();
                hashArray[index].InsertFirst(new HashSetNode<TV>(value));
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

                hashArray[index].InsertFirst(new HashSetNode<TV>(value));
            }

            Count++;
        }

        //O(1) time complexity; worst case O(n)
        public void Remove(TV value)
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
                DoublyLinkedListNode<HashSetNode<TV>> item = null;
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

            shrink();

        }

        /// <summary>
        /// clear hash table
        /// </summary>
        public void Clear()
        {
            hashArray = new DoublyLinkedList<HashSetNode<TV>>[initialBucketSize];
            Count = 0;
            filledBuckets = 0;
        }


        private void setValue(TV value)
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

        /// <summary>
        /// Grow array if needed
        /// </summary>
        private void grow()
        {
            if (filledBuckets >= bucketSize * 0.7)
            {
                filledBuckets = 0;
                //increase array size exponentially on demand
                var newBucketSize = bucketSize * 2;

                var biggerArray = new DoublyLinkedList<HashSetNode<TV>>[newBucketSize];

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
                                    biggerArray[newIndex] = new DoublyLinkedList<HashSetNode<TV>>();
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
        private void shrink()
        {
            if (Math.Abs(filledBuckets - bucketSize * 0.3) < tolerance && bucketSize / 2 > initialBucketSize)
            {
                filledBuckets = 0;
                //reduce array by half 
                var newBucketSize = bucketSize / 2;

                var smallerArray = new DoublyLinkedList<HashSetNode<TV>>[newBucketSize];

                for (int i = 0; i < bucketSize; i++)
                {
                    var item = hashArray[i];

                    //hashcode changes when bucket size changes
                    if (item?.Head != null)
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
                                smallerArray[newIndex] = new DoublyLinkedList<HashSetNode<TV>>();
                            }

                            smallerArray[newIndex].InsertFirst(current);

                            current = next;
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

        public IEnumerator<HashSetNode<TV>> GetEnumerator()
        {
            return new SeparateChainingHashSetEnumerator<TV>(hashArray, bucketSize);
        }

    }

    //  implement IEnumerator.
    public class SeparateChainingHashSetEnumerator<TV> : IEnumerator<HashSetNode<TV>> 
    {
        internal DoublyLinkedList<HashSetNode<TV>>[] hashList;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        DoublyLinkedListNode<HashSetNode<TV>> currentNode = null;

        int length;

        internal SeparateChainingHashSetEnumerator(DoublyLinkedList<HashSetNode<TV>>[] hashList, int length)
        {
            this.length = length;
            this.hashList = hashList;
        }

        public bool MoveNext()
        {
            if (currentNode?.Next != null)
            {
                currentNode = currentNode.Next;
                return true;
            }

            while (currentNode?.Next == null)
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

        public HashSetNode<TV> Current
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
            length = 0;
            hashList = null;
        }

    }
}
