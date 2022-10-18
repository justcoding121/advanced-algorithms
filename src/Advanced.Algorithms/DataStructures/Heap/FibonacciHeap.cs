using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A fibornacci minMax heap implementation.
/// </summary>
public class FibonacciHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;

    private FibonacciHeapNode<T> heapForestHead;

    private readonly Dictionary<T, List<FibonacciHeapNode<T>>> heapMapping = new();

    //holds the min/max node at any given time
    private FibonacciHeapNode<T> minMaxNode;

    public FibonacciHeap(SortDirection sortDirection = SortDirection.Ascending)
    {
        isMaxHeap = sortDirection == SortDirection.Descending;
        comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return heapMapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Insert(T newItem)
    {
        var newNode = new FibonacciHeapNode<T>(newItem);

        //return pointer to new Node
        MergeForests(newNode);

        if (minMaxNode == null)
        {
            minMaxNode = newNode;
        }
        else
        {
            if (comparer.Compare(minMaxNode.Value, newNode.Value) > 0) minMaxNode = newNode;
        }

        AddMapping(newItem, newNode);

        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Extract()
    {
        if (heapForestHead == null) throw new Exception("Empty heap");

        var minMaxValue = minMaxNode.Value;

        RemoveMapping(minMaxValue, minMaxNode);

        //remove tree root
        DeleteNode(ref heapForestHead, minMaxNode);

        MergeForests(minMaxNode.ChildrenHead);
        Meld();

        Count--;

        return minMaxValue;
    }


    /// <summary>
    ///     Update the Heap with new value for this node pointer.
    ///     Time complexity: O(1).
    /// </summary>
    public void UpdateKey(T currentValue, T newValue)
    {
        var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

        if (node == null) throw new Exception("Current value is not present in this heap.");

        if (comparer.Compare(newValue, node.Value) > 0)
            throw new Exception($"New value is not {(!isMaxHeap ? "less" : "greater")} than old value.");

        UpdateNodeValue(currentValue, newValue, node);

        if (node.Parent == null
            && comparer.Compare(minMaxNode.Value, node.Value) > 0)
            minMaxNode = node;

        var current = node;

        if (current.Parent == null || comparer.Compare(current.Value, current.Parent.Value) >= 0) return;

        var parent = current.Parent;

        //if parent already lost one child
        //then cut current and parent
        if (parent.LostChild)
        {
            parent.LostChild = false;

            var grandParent = parent.Parent;

            //mark grand parent
            if (grandParent == null) return;

            Cut(parent);
            Cut(current);
        }
        else
        {
            Cut(current);
        }
    }

    /// <summary>
    ///     Unions this heap with another.
    ///     Time complexity: O(1).
    /// </summary>
    public void Merge(FibonacciHeap<T> fibonacciHeap)
    {
        MergeForests(fibonacciHeap.heapForestHead);
        Count = Count + fibonacciHeap.Count;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (heapForestHead == null) throw new Exception("Empty heap");

        return minMaxNode.Value;
    }

    /// <summary>
    ///     Merge roots with same degrees in Forest.
    /// </summary>
    private void Meld()
    {
        if (heapForestHead == null)
        {
            minMaxNode = null;
            return;
        }

        //degree - node dictionary
        var mergeDictionary = new Dictionary<int, FibonacciHeapNode<T>>();

        var current = heapForestHead;
        minMaxNode = current;
        while (current != null)
        {
            current.Parent = null;
            var next = current.Next;
            //no same degree already in merge dictionary
            //add to hash table
            if (!mergeDictionary.ContainsKey(current.Degree))
            {
                mergeDictionary.Add(current.Degree, current);

                if (minMaxNode == current) minMaxNode = null;

                DeleteNode(ref heapForestHead, current);

                current = next;
            }
            //insert back to forest by merging current tree 
            //with existing tree in merge dictionary
            else
            {
                var currentDegree = current.Degree;
                var existing = mergeDictionary[currentDegree];

                if (comparer.Compare(existing.Value, current.Value) < 0)
                {
                    current.Parent = existing;

                    DeleteNode(ref heapForestHead, current);

                    var childHead = existing.ChildrenHead;
                    InsertNode(ref childHead, current);
                    existing.ChildrenHead = childHead;

                    existing.Degree++;

                    InsertNode(ref heapForestHead, existing);
                    current = existing;
                    current.Next = next;
                }
                else
                {
                    existing.Parent = current;

                    var childHead = current.ChildrenHead;
                    InsertNode(ref childHead, existing);
                    current.ChildrenHead = childHead;

                    current.Degree++;
                }


                if (minMaxNode == null
                    || comparer.Compare(minMaxNode.Value, current.Value) > 0)
                    minMaxNode = current;

                mergeDictionary.Remove(currentDegree);
            }
        }

        //insert back trees with unique degrees to forest
        if (mergeDictionary.Count > 0)
        {
            foreach (var node in mergeDictionary)
            {
                InsertNode(ref heapForestHead, node.Value);

                if (minMaxNode == null
                    || comparer.Compare(minMaxNode.Value, node.Value.Value) > 0)
                    minMaxNode = node.Value;
            }

            mergeDictionary.Clear();
        }
    }

    /// <summary>
    ///     Delete this node from Heap Tree and adds it to forest as a new tree
    /// </summary>
    private void Cut(FibonacciHeapNode<T> node)
    {
        var parent = node.Parent;

        //cut child and attach to heap Forest
        //and mark parent for lost child
        var childHead = node.Parent.ChildrenHead;
        DeleteNode(ref childHead, node);
        node.Parent.ChildrenHead = childHead;

        node.Parent.Degree--;
        if (parent.Parent != null) parent.LostChild = true;
        node.LostChild = false;
        node.Parent = null;

        InsertNode(ref heapForestHead, node);

        //update 
        if (comparer.Compare(minMaxNode.Value, node.Value) > 0) minMaxNode = node;
    }

    /// <summary>
    ///     Merges the given fibornacci node list to current Forest
    /// </summary>
    private void MergeForests(FibonacciHeapNode<T> headPointer)
    {
        var current = headPointer;
        while (current != null)
        {
            var next = current.Next;
            InsertNode(ref heapForestHead, current);
            current = next;
        }
    }

    private void InsertNode(ref FibonacciHeapNode<T> head, FibonacciHeapNode<T> newNode)
    {
        newNode.Next = newNode.Previous = null;

        if (head == null)
        {
            head = newNode;
            return;
        }

        head.Previous = newNode;
        newNode.Next = head;

        head = newNode;
    }

    private void DeleteNode(ref FibonacciHeapNode<T> heapForestHead, FibonacciHeapNode<T> deletionNode)
    {
        if (deletionNode == heapForestHead)
        {
            if (deletionNode.Next != null) deletionNode.Next.Previous = null;

            heapForestHead = deletionNode.Next;
            deletionNode.Next = null;
            deletionNode.Previous = null;
            return;
        }

        deletionNode.Previous.Next = deletionNode.Next;

        if (deletionNode.Next != null) deletionNode.Next.Previous = deletionNode.Previous;

        deletionNode.Next = null;
        deletionNode.Previous = null;
    }

    private void AddMapping(T newItem, FibonacciHeapNode<T> newNode)
    {
        if (heapMapping.ContainsKey(newItem))
            heapMapping[newItem].Add(newNode);
        else
            heapMapping[newItem] = new List<FibonacciHeapNode<T>>(new[] { newNode });
    }

    private void UpdateNodeValue(T currentValue, T newValue, FibonacciHeapNode<T> node)
    {
        RemoveMapping(currentValue, node);
        node.Value = newValue;
        AddMapping(newValue, node);
    }

    private void RemoveMapping(T currentValue, FibonacciHeapNode<T> node)
    {
        heapMapping[currentValue].Remove(node);
        if (heapMapping[currentValue].Count == 0) heapMapping.Remove(currentValue);
    }
}