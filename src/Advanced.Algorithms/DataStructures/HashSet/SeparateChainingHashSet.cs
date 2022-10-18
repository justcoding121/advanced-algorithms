using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class SeparateChainingHashSet<T> : IHashSet<T>
{
    private const double Tolerance = 0.1;
    private readonly int initialBucketSize;
    private int filledBuckets;
    private DoublyLinkedList<HashSetNode<T>>[] hashArray;

    internal SeparateChainingHashSet(int initialBucketSize = 3)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new DoublyLinkedList<HashSetNode<T>>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public bool Contains(T value)
    {
        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Value.Equals(value)) return true;

            current = current.Next;
        }

        return false;
    }

    public void Add(T value)
    {
        Grow();

        var index = Math.Abs(value.GetHashCode()) % BucketSize;

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
                if (current.Data.Value.Equals(value)) throw new Exception("Duplicate value");

                current = current.Next;
            }

            hashArray[index].InsertFirst(new HashSetNode<T>(value));
        }

        Count++;
    }

    public void Remove(T value)
    {
        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("No such item for given value");

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

        hashArray[index].Delete(item);

        //if list is empty mark bucket as null
        if (hashArray[index].Head == null)
        {
            hashArray[index] = null;
            filledBuckets--;
        }

        Count--;

        Shrink();
    }

    public void Clear()
    {
        hashArray = new DoublyLinkedList<HashSetNode<T>>[initialBucketSize];
        Count = 0;
        filledBuckets = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new SeparateChainingHashSetEnumerator<T>(hashArray, BucketSize);
    }

    private void SetValue(T value)
    {
        var index = Math.Abs(value.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("Item not found");

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

        throw new Exception("Item not found");
    }

    private void Grow()
    {
        if (filledBuckets >= BucketSize * 0.7)
        {
            filledBuckets = 0;
            //increase array size exponentially on demand
            var newBucketSize = BucketSize * 2;

            var biggerArray = new DoublyLinkedList<HashSetNode<T>>[newBucketSize];

            for (var i = 0; i < BucketSize; i++)
            {
                var item = hashArray[i];

                //hashcode changes when bucket size changes
                if (item != null)
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

            hashArray = biggerArray;
        }
    }

    private void Shrink()
    {
        if (Math.Abs(filledBuckets - BucketSize * 0.3) < Tolerance && BucketSize / 2 > initialBucketSize)
        {
            filledBuckets = 0;
            //reduce array by half 
            var newBucketSize = BucketSize / 2;

            var smallerArray = new DoublyLinkedList<HashSetNode<T>>[newBucketSize];

            for (var i = 0; i < BucketSize; i++)
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
}

internal class SeparateChainingHashSetEnumerator<T> : IEnumerator<T>
{
    private DoublyLinkedListNode<HashSetNode<T>> currentNode;
    internal DoublyLinkedList<HashSetNode<T>>[] HashList;

    private int length;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal SeparateChainingHashSetEnumerator(DoublyLinkedList<HashSetNode<T>>[] hashList, int length)
    {
        this.length = length;
        this.HashList = hashList;
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
                if (HashList[position] == null)
                    continue;

                currentNode = HashList[position].Head;

                if (currentNode == null)
                    continue;

                return true;
            }

            break;
        }

        return false;
    }

    public void Reset()
    {
        position = -1;
        currentNode = null;
    }

    object IEnumerator.Current => Current;

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
        HashList = null;
    }
}