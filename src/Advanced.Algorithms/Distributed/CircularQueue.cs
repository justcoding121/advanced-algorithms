using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     Cicular queue aka Ring Buffer using fixed size array.
/// </summary>
public class CircularQueue<T>
{
    //points to the index new element should be inserted
    private int end;
    private readonly T[] queue;

    //points to the index of next element to be deleted
    private int start;

    public CircularQueue(int size)
    {
        queue = new T[size];
    }

    public int Count { get; private set; }

    /// <summary>
    ///     Note: When buffer overflows oldest data will be erased.
    ///     Time complexity: O(1)
    /// </summary>
    public T Enqueue(T data)
    {
        var deleted = default(T);

        //wrap around removing oldest element
        if (Count == queue.Length)
        {
            deleted = queue[start];
            start = (start + 1) % queue.Length;
            Count--;
        }

        queue[end] = data;
        end = (end + 1) % queue.Length;
        Count++;

        return deleted;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    /// <returns>Deleted items.</returns>
    public IEnumerable<T> Enqueue(T[] bulk)
    {
        var deletedList = new List<T>();

        foreach (var item in bulk)
        {
            var wasFull = Count == queue.Length;
            var deleted = Enqueue(item);

            //include default(T) when it was a real overwrite
            if (wasFull) deletedList.Add(deleted);
        }

        return deletedList;
    }

    /// <summary>
    ///     O(1) Time complexity.
    /// </summary>
    public T Dequeue()
    {
        if (Count == 0) throw new InvalidOperationException("Empty queue.");

        var element = queue[start];
        start = (start + 1) % queue.Length;
        Count--;

        //reset
        if (Count == 0) start = end = 0;

        return element;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public IEnumerable<T> Dequeue(int bulkNumber)
    {
        var deletedList = new List<T>();
        while (bulkNumber > 0 && Count > 0)
        {
            deletedList.Add(Dequeue());
            bulkNumber--;
        }

        return deletedList;
    }
}