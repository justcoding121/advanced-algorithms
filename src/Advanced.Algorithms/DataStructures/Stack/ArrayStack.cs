using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class ArrayStack<T> : IStack<T>
{
    private readonly List<T> list = new();
    public int Count { get; private set; }

    public T Pop()
    {
        if (Count == 0) throw new Exception("Empty stack");

        var result = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        Count--;
        return result;
    }

    public void Push(T item)
    {
        list.Add(item);
        Count++;
    }

    public T Peek()
    {
        if (Count == 0) return default;

        return list[list.Count - 1];
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}