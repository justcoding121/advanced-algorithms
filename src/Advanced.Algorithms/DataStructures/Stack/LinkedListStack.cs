using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation;

internal class LinkedListStack<T> : IStack<T>
{
    private readonly SinglyLinkedList<T> list = new();
    public int Count { get; private set; }

    public T Pop()
    {
        if (Count == 0) throw new Exception("Empty stack");

        var result = list.DeleteFirst();
        Count--;
        return result;
    }

    public void Push(T item)
    {
        list.InsertFirst(item);
        Count++;
    }

    public T Peek()
    {
        return Count == 0 ? default : list.Head.Data;
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