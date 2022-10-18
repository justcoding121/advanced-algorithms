using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class OpenAddressHashSet<T> : IHashSet<T>
{
    private readonly int initialBucketSize;
    private HashSetNode<T>[] hashArray;

    internal OpenAddressHashSet(int initialBucketSize = 2)
    {
        this.initialBucketSize = initialBucketSize;
        hashArray = new HashSetNode<T>[initialBucketSize];
    }

    private int BucketSize => hashArray.Length;

    public int Count { get; private set; }

    public bool Contains(T value)
    {
        var hashCode = GetHash(value);
        var index = hashCode % BucketSize;

        if (hashArray[index] == null) return false;

        var current = hashArray[index];

        //keep track of this so that we won't circle around infinitely
        var hitKey = current.Value;

        while (current != null)
        {
            if (current.Value.Equals(value)) return true;

            index++;

            //wrap around
            if (index == BucketSize)
                index = 0;

            current = hashArray[index];

            //reached original hit again
            if (current != null && current.Value.Equals(hitKey)) break;
        }

        return false;
    }

    public void Add(T value)
    {
        Grow();

        var hashCode = GetHash(value);

        var index = hashCode % BucketSize;

        if (hashArray[index] == null)
        {
            hashArray[index] = new HashSetNode<T>(value);
        }
        else
        {
            var current = hashArray[index];
            //keep track of this so that we won't circle around infinitely
            var hitKey = current.Value;

            while (current != null)
            {
                if (current.Value.Equals(value)) throw new Exception("Duplicate value");

                index++;

                //wrap around
                if (index == BucketSize)
                    index = 0;

                current = hashArray[index];

                if (current != null && current.Value.Equals(hitKey)) throw new Exception("HashSet is full");
            }

            hashArray[index] = new HashSetNode<T>(value);
        }

        Count++;
    }

    public void Remove(T value)
    {
        var hashCode = GetHash(value);
        var curIndex = hashCode % BucketSize;

        if (hashArray[curIndex] == null) throw new Exception("No such item for given value");

        var current = hashArray[curIndex];

        //prevent circling around infinitely
        var hitKey = current.Value;

        HashSetNode<T> target = null;

        while (current != null)
        {
            if (current.Value.Equals(value))
            {
                target = current;
                break;
            }

            curIndex++;

            //wrap around
            if (curIndex == BucketSize)
                curIndex = 0;

            current = hashArray[curIndex];

            if (current != null && current.Value.Equals(hitKey)) throw new Exception("No such item for given value");
        }

        //remove
        if (target == null)
        {
            throw new Exception("No such item for given value");
        }

        //delete this element
        hashArray[curIndex] = null;

        //now time to cleanup subsequent broken hash elements due to this emptied cell
        curIndex++;

        //wrap around
        if (curIndex == BucketSize)
            curIndex = 0;

        current = hashArray[curIndex];

        //until an empty cell
        while (current != null)
        {
            //delete current
            hashArray[curIndex] = null;

            //add current back to table
            Add(current.Value);
            Count--;

            curIndex++;

            //wrap around
            if (curIndex == BucketSize)
                curIndex = 0;

            current = hashArray[curIndex];
        }

        Count--;

        Shrink();
    }

    public void Clear()
    {
        hashArray = new HashSetNode<T>[initialBucketSize];
        Count = 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new OpenAddressHashSetEnumerator<T>(hashArray, hashArray.Length);
    }

    private void Grow()
    {
        if (!(BucketSize * 0.7 <= Count)) return;

        var orgBucketSize = BucketSize;
        var currentArray = hashArray;

        //increase array size exponentially on demand
        hashArray = new HashSetNode<T>[BucketSize * 2];

        for (var i = 0; i < orgBucketSize; i++)
        {
            var current = currentArray[i];

            if (current != null)
            {
                Add(current.Value);
                Count--;
            }
        }

        currentArray = null;
    }


    private void Shrink()
    {
        if (Count <= BucketSize * 0.3 && BucketSize / 2 > initialBucketSize)
        {
            var orgBucketSize = BucketSize;

            var currentArray = hashArray;

            //reduce array by half logarithamic
            hashArray = new HashSetNode<T>[BucketSize / 2];

            for (var i = 0; i < orgBucketSize; i++)
            {
                var current = currentArray[i];

                if (current != null)
                {
                    Add(current.Value);
                    Count--;
                }
            }

            currentArray = null;
        }
    }

    private int GetHash(T value)
    {
        return Math.Abs(value.GetHashCode());
    }
}

internal class HashSetNode<T>
{
    internal T Value;

    internal HashSetNode(T value)
    {
        Value = value;
    }
}

internal class OpenAddressHashSetEnumerator<TV> : IEnumerator<TV>
{
    internal HashSetNode<TV>[] HashArray;
    private int length;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int position = -1;

    internal OpenAddressHashSetEnumerator(HashSetNode<TV>[] hashArray, int length)
    {
        this.length = length;
        this.HashArray = hashArray;
    }

    public bool MoveNext()
    {
        position++;

        while (position < length && HashArray[position] == null)
            position++;

        return position < length;
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current => Current;

    public TV Current
    {
        get
        {
            try
            {
                return HashArray[position].Value;
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
        HashArray = null;
    }
}