using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A singly linked list implementation.
/// </summary>
public class SinglyLinkedList<T> : IEnumerable<T>
{
    public SinglyLinkedListNode<T> Head;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new SinglyLinkedListEnumerator<T>(ref Head);
    }

    /// <summary>
    ///     Insert first. Time complexity: O(1).
    /// </summary>
    public void InsertFirst(T data)
    {
        var newNode = new SinglyLinkedListNode<T>(data);

        newNode.Next = Head;

        Head = newNode;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void InsertLast(T data)
    {
        var newNode = new SinglyLinkedListNode<T>(data);

        if (Head == null)
        {
            Head = new SinglyLinkedListNode<T>(data);
        }
        else
        {
            var current = Head;

            while (current.Next != null) current = current.Next;

            current.Next = newNode;
        }
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T DeleteFirst()
    {
        if (Head == null) throw new Exception("Nothing to remove");

        var firstData = Head.Data;

        Head = Head.Next;

        return firstData;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public T DeleteLast()
    {
        if (Head == null) throw new Exception("Nothing to remove");

        var current = Head;
        SinglyLinkedListNode<T> prev = null;
        while (current.Next != null)
        {
            prev = current;
            current = current.Next;
        }

        var lastData = prev.Next.Data;
        prev.Next = null;
        return lastData;
    }

    /// <summary>
    ///     Delete given element.
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T element)
    {
        if (Head == null) throw new Exception("Empty list");

        var current = Head;
        SinglyLinkedListNode<T> prev = null;

        do
        {
            if (current.Data.Equals(element))
            {
                //last element
                if (current.Next == null)
                {
                    //head is the only node
                    if (prev == null)
                        Head = null;
                    else
                        //last element
                        prev.Next = null;
                }
                else
                {
                    //current is head
                    if (prev == null)
                        Head = current.Next;
                    else
                        //delete
                        prev.Next = current.Next;
                }

                break;
            }

            prev = current;
            current = current.Next;
        } while (current != null);
    }

    // Time complexity: O(1).
    public bool IsEmpty()
    {
        return Head == null;
    }

    // Time complexity: O(1).
    public void Clear()
    {
        if (Head == null) throw new Exception("Empty list");

        Head = null;
    }

    /// <summary>
    ///     Inserts this element to the begining.
    ///     Time complexity: O(1).
    /// </summary>
    public void InsertFirst(SinglyLinkedListNode<T> current)
    {
        current.Next = Head;
        Head = current;
    }
}

/// <summary>
///     Singly linked list node.
/// </summary>
public class SinglyLinkedListNode<T>
{
    public T Data;
    public SinglyLinkedListNode<T> Next;

    public SinglyLinkedListNode(T data)
    {
        Data = data;
    }
}

internal class SinglyLinkedListEnumerator<T> : IEnumerator<T>
{
    internal SinglyLinkedListNode<T> CurrentNode;
    internal SinglyLinkedListNode<T> HeadNode;

    internal SinglyLinkedListEnumerator(ref SinglyLinkedListNode<T> headNode)
    {
        this.HeadNode = headNode;
    }

    public bool MoveNext()
    {
        if (HeadNode == null)
            return false;

        if (CurrentNode == null)
        {
            CurrentNode = HeadNode;
            return true;
        }

        if (CurrentNode.Next != null)
        {
            CurrentNode = CurrentNode.Next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        CurrentNode = HeadNode;
    }


    object IEnumerator.Current => Current;

    public T Current => CurrentNode.Data;

    public void Dispose()
    {
        HeadNode = null;
        CurrentNode = null;
    }
}