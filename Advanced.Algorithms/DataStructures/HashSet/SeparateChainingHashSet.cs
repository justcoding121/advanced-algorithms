using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    internal class SeparateChainingHashSet<T> : IHashSet<T>  
    {
        private const double tolerance = 0.1;
        private DoublyLinkedList<HashSetNode<T>>[] hashArray;
        private int bucketSize => hashArray.Length;
        private readonly int initialBucketSize;
        private int filledBuckets;

        public int Count { get; private set; }

        internal SeparateChainingHashSet(int initialBucketSize = 3)
        {
            this.initialBucketSize = initialBucketSize;
            hashArray = new DoublyLinkedList<HashSetNode<T>>[initialBucketSize];
        }

        public bool Contains(T value)
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

        public void Add(T value)
        {
            grow();

            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                hashArray[index] = new DoublyLinkedList<HashSetNode<T>>();
                hashArray[index].InsertFirst(new HashSetNode<T>(value));
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

                hashArray[index].InsertFirst(new HashSetNode<T>(value));
            }

            Count++;
        }

        public void Remove(T value)
        {
            var index = Math.Abs(value.GetHashCode()) % bucketSize;

            if (hashArray[index] == null)
            {
                throw new Exception("No such item for given value");
            }
            else
            {
                var current = hashArray[index].Head;

                //TODO merge both search and remove to a single loop here!
                DoublyLinkedListNode<HashSetNode<T>> item = null;
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

        public void Clear()
        {
            hashArray = new DoublyLinkedList<HashSetNode<T>>[initialBucketSize];
            Count = 0;
            filledBuckets = 0;
        }

        private void setValue(T value)
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

        private void grow()
        {
            if (filledBuckets >= bucketSize * 0.7)
            {
                filledBuckets = 0;
                //increase array size exponentially on demand
                var newBucketSize = bucketSize * 2;

                var biggerArray = new DoublyLinkedList<HashSetNode<T>>[newBucketSize];

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
                                    biggerArray[newIndex] = new DoublyLinkedList<HashSetNode<T>>();
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

        private void shrink()
        {
            if (Math.Abs(filledBuckets - bucketSize * 0.3) < tolerance && bucketSize / 2 > initialBucketSize)
            {
                filledBuckets = 0;
                //reduce array by half 
                var newBucketSize = bucketSize / 2;

                var smallerArray = new DoublyLinkedList<HashSetNode<T>>[newBucketSize];

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
                                smallerArray[newIndex] = new DoublyLinkedList<HashSetNode<T>>();
                            }

                            smallerArray[newIndex].InsertFirst(current);

                            current = next;
                        }
                    }
                }

                hashArray = smallerArray;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SeparateChainingHashSetEnumerator<T>(hashArray, bucketSize);
        }

    }

    internal class SeparateChainingHashSetEnumerator<T> : IEnumerator<T> 
    {
        internal DoublyLinkedList<HashSetNode<T>>[] hashList;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;
        DoublyLinkedListNode<HashSetNode<T>> currentNode = null;

        int length;

        internal SeparateChainingHashSetEnumerator(DoublyLinkedList<HashSetNode<T>>[] hashList, int length)
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

        public T Current
        {
            get
            {
                try
                {
                    return currentNode.Data.Value;
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
