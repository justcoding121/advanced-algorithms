using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A circular linked list implementation.
/// </summary>
public class CircularLinkedList<T> : IEnumerable<T>
{
    public CircularLinkedListNode<T> ReferenceNode { get; set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CircularLinkedListEnumerator<T>(ReferenceNode);
    }

    /// <summary>
    ///     Marks this data as the new reference node after insertion.
    ///     Like insert first assuming that current reference node as head.
    ///     Time complexity: O(1).
    /// </summary>
    public CircularLinkedListNode<T> Insert(T data)
    {
        var newNode = new CircularLinkedListNode<T>(data);

        //if no item exist
        if (ReferenceNode == null)
        {
            //attach the item after reference node
            newNode.Next = newNode;
            newNode.Previous = newNode;
        }
        else
        {
            //attach the item after reference node
            newNode.Previous = ReferenceNode;
            newNode.Next = ReferenceNode.Next;

            ReferenceNode.Next.Previous = newNode;
            ReferenceNode.Next = newNode;
        }

        ReferenceNode = newNode;

        return newNode;
    }

    /// <summary>
    ///     Time complexity: O(1)
    /// </summary>
    public void Delete(CircularLinkedListNode<T> current)
    {
        if (ReferenceNode.Next == ReferenceNode)
        {
            if (ReferenceNode != current) throw new ArgumentException("Not found");

            ReferenceNode = null;
            return;
        }

        current.Previous.Next = current.Next;
        current.Next.Previous = current.Previous;

        //match is a reference node
        if (current == ReferenceNode) ReferenceNode = current.Next;
    }

    /// <summary>
    ///     search and delete.
    ///     Time complexity:O(n).
    /// </summary>
    public void Delete(T data)
    {
        if (ReferenceNode == null) throw new InvalidOperationException("Empty list");

        //only one element on list
        if (ReferenceNode.Next == ReferenceNode)
        {
            if (ReferenceNode.Data.Equals(data))
            {
                ReferenceNode = null;
                return;
            }

            throw new ArgumentException("Not found");
        }

        //atleast two elements from here
        var current = ReferenceNode;
        var found = false;
        while (true)
        {
            if (current.Data.Equals(data))
            {
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;

                //match is a reference node
                if (current == ReferenceNode) ReferenceNode = current.Next;

                found = true;
                break;
            }

            //terminate loop if we are about to cycle
            if (current.Next == ReferenceNode) break;

            current = current.Next;
        }

        if (!found) throw new ArgumentException("Not found");
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public bool IsEmpty()
    {
        return ReferenceNode == null;
    }

    /// <summary>
    ///     Time complexity:  O(1).
    /// </summary>
    public void Clear()
    {
        if (ReferenceNode == null) throw new InvalidOperationException("Empty list");

        ReferenceNode = null;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Union(CircularLinkedList<T> newList)
    {
        if (newList?.ReferenceNode == null)
            return;

        if (ReferenceNode == null)
        {
            ReferenceNode = newList.ReferenceNode;
            return;
        }

        // save predecessors before rewiring (overwriting Previous first corrupts the splice)
        var thisPrevious = ReferenceNode.Previous;
        var otherPrevious = newList.ReferenceNode.Previous;

        thisPrevious.Next = newList.ReferenceNode;
        ReferenceNode.Previous = otherPrevious;

        otherPrevious.Next = ReferenceNode;
        newList.ReferenceNode.Previous = thisPrevious;
    }
}

/// <summary>
///     Circular linked list node.
/// </summary>
public class CircularLinkedListNode<T>
{
    public T Data { get; set; }
    public CircularLinkedListNode<T> Next { get; set; }
    public CircularLinkedListNode<T> Previous { get; set; }

    public CircularLinkedListNode(T data)
    {
        Data = data;
    }
}

internal class CircularLinkedListEnumerator<T> : IEnumerator<T>
{
    private bool disposedValue;
    internal CircularLinkedListNode<T> CurrentNode;
    internal CircularLinkedListNode<T> ReferenceNode;

    internal CircularLinkedListEnumerator(CircularLinkedListNode<T> referenceNode)
    {
        ReferenceNode = referenceNode;
    }

    public bool MoveNext()
    {
        if (ReferenceNode == null)
            return false;

        if (CurrentNode == null)
        {
            CurrentNode = ReferenceNode;
            return true;
        }

        if (CurrentNode.Next != null && CurrentNode.Next != ReferenceNode)
        {
            CurrentNode = CurrentNode.Next;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        CurrentNode = null;
    }


    object IEnumerator.Current => Current;

    public T Current => CurrentNode.Data;

    protected virtual void Dispose(bool disposing)
    {
        if (disposedValue)
        {
            return;
        }

        if (disposing)
        {
            ReferenceNode = null;
            CurrentNode = null;
        }

        disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
