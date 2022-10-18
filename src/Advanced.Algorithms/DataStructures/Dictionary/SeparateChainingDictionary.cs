using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class SeparateChainingDictionary<TK, TV> : IDictionary<TK, TV>
{
    private const double Tolerance = 0.1;
    private readonly int initialBucketSize;
    private int filledBuckets;

    private DoublyLinkedList<KeyValuePair<TK, TV>>[] hashArray;


    public SeparateChainingDictionary(int initialBucketSize = 3)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public TV this[TK key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }

    public bool ContainsKey(TK key)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Key.Equals(key)) return true;

            current = current.Next;
        }

        return false;
    }

    public void Add(TK key, TV value)
    {
        Grow();

        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null)
        {
            hashArray[index] = new DoublyLinkedList<KeyValuePair<TK, TV>>();
            hashArray[index].InsertFirst(new KeyValuePair<TK, TV>(key, value));
            filledBuckets++;
        }
        else
        {
            var current = hashArray[index].Head;

            while (current != null)
            {
                if (current.Data.Key.Equals(key)) throw new Exception("Duplicate key");

                current = current.Next;
            }

            hashArray[index].InsertFirst(new KeyValuePair<TK, TV>(key, value));
        }

        Count++;
    }

    public void Remove(TK key)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("No such item for given key");

        var current = hashArray[index].Head;

        //TODO merge both search and remove to a single loop here!
        DoublyLinkedListNode<KeyValuePair<TK, TV>> item = null;
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
        hashArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[initialBucketSize];
        Count = 0;
        filledBuckets = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return new SeparateChainingDictionaryEnumerator<TK, TV>(hashArray, BucketSize);
    }

    private void SetValue(TK key, TV value)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null)
        {
            Add(key, value);
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

    private TV GetValue(TK key)
    {
        var index = Math.Abs(key.GetHashCode()) % BucketSize;

        if (hashArray[index] == null) throw new Exception("Item not found");

        var current = hashArray[index].Head;

        while (current != null)
        {
            if (current.Data.Key.Equals(key)) return current.Data.Value;

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

            var biggerArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[newBucketSize];

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

                            var newIndex = Math.Abs(current.Data.Key.GetHashCode()) % newBucketSize;

                            if (biggerArray[newIndex] == null)
                            {
                                filledBuckets++;
                                biggerArray[newIndex] = new DoublyLinkedList<KeyValuePair<TK, TV>>();
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

            var smallerArray = new DoublyLinkedList<KeyValuePair<TK, TV>>[newBucketSize];

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

                        var newIndex = Math.Abs(current.Data.Key.GetHashCode()) % newBucketSize;

                        if (smallerArray[newIndex] == null)
                        {
                            filledBuckets++;
                            smallerArray[newIndex] = new DoublyLinkedList<KeyValuePair<TK, TV>>();
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

internal class SeparateChainingDictionaryEnumerator<TK, TV> : IEnumerator<KeyValuePair<TK, TV>>
{
    private DoublyLinkedListNode<KeyValuePair<TK, TV>> currentNode;
    internal DoublyLinkedList<KeyValuePair<TK, TV>>[] HashList;

    private readonly int length;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal SeparateChainingDictionaryEnumerator(DoublyLinkedList<KeyValuePair<TK, TV>>[] hashList, int length)
    {
        this.length = length;
        HashList = hashList;
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

    public KeyValuePair<TK, TV> Current
    {
        get
        {
            try
            {
                return new KeyValuePair<TK, TV>(currentNode.Data.Key, currentNode.Data.Value);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    public void Dispose()
    {
        HashList = null;
    }
}