using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A pairing minMax heap implementation.
/// </summary>
public class PairingHeap<T> : IEnumerable<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMaxHeap;

    private readonly Dictionary<T, List<PairingHeapNode<T>>> heapMapping = new();

    private PairingHeapNode<T> root;

    public PairingHeap(SortDirection sortDirection = SortDirection.Ascending)
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
    ///     Insert a new Node.
    ///     Time complexity: O(1).
    /// </summary>
    public void Insert(T newItem)
    {
        var newNode = new PairingHeapNode<T>(newItem);
        root = Meld(root, newNode);
        AddMapping(newItem, newNode);
        Count++;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public T Extract()
    {
        var minMax = root;
        RemoveMapping(minMax.Value, minMax);
        Meld(root.ChildrenHead);
        Count--;
        return minMax.Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void UpdateKey(T currentValue, T newValue)
    {
        var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

        if (node == null) throw new Exception("Current value is not present in this heap.");

        if (comparer.Compare(newValue, node.Value) > 0)
            throw new Exception($"New value is not {(!isMaxHeap ? "less" : "greater")} than old value.");

        UpdateNodeValue(currentValue, newValue, node);

        if (node == root) return;

        DeleteChild(node);

        root = Meld(root, node);
    }

    /// <summary>
    ///     Merge another heap with this heap.
    ///     Time complexity: O(1).
    /// </summary>
    public void Merge(PairingHeap<T> pairingHeap)
    {
        root = Meld(root, pairingHeap.root);
        Count = Count + pairingHeap.Count;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public T Peek()
    {
        if (root == null)
            throw new Exception("Empty heap");

        return root.Value;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    private void Meld(PairingHeapNode<T> headNode)
    {
        if (headNode == null)
            return;

        var passOneResult = new List<PairingHeapNode<T>>();

        var current = headNode;

        if (current.Next == null)
        {
            headNode.Next = null;
            headNode.Previous = null;
            passOneResult.Add(headNode);
        }
        else
        {
            while (true)
            {
                if (current == null) break;

                if (current.Next != null)
                {
                    var next = current.Next;
                    var nextNext = next.Next;
                    passOneResult.Add(Meld(current, next));
                    current = nextNext;
                }
                else
                {
                    var lastInserted = passOneResult[passOneResult.Count - 1];
                    passOneResult[passOneResult.Count - 1] = Meld(lastInserted, current);
                    break;
                }
            }
        }

        var passTwoResult = passOneResult[passOneResult.Count - 1];

        if (passOneResult.Count == 1)
        {
            root = passTwoResult;
            return;
        }


        for (var i = passOneResult.Count - 2; i >= 0; i--)
        {
            current = passOneResult[i];
            passTwoResult = Meld(passTwoResult, current);
        }

        root = passTwoResult;
    }

    /// <summary>
    ///     makes the smaller node parent of other and returns the Parent
    /// </summary>
    private PairingHeapNode<T> Meld(PairingHeapNode<T> node1,
        PairingHeapNode<T> node2)
    {
        if (node2 != null)
        {
            node2.Previous = null;
            node2.Next = null;
        }

        if (node1 == null) return node2;

        node1.Previous = null;
        node1.Next = null;

        if (node2 != null && comparer.Compare(node1.Value, node2.Value) <= 0)
        {
            AddChild(ref node1, node2);
            return node1;
        }

        AddChild(ref node2, node1);
        return node2;
    }

    /// <summary>
    ///     Add new child to parent node
    /// </summary>
    private void AddChild(ref PairingHeapNode<T> parent, PairingHeapNode<T> child)
    {
        if (parent.ChildrenHead == null)
        {
            parent.ChildrenHead = child;
            child.Previous = parent;
            return;
        }

        var head = parent.ChildrenHead;

        child.Previous = head;
        child.Next = head.Next;

        if (head.Next != null) head.Next.Previous = child;

        head.Next = child;
    }

    /// <summary>
    ///     delete node from parent
    /// </summary>
    private void DeleteChild(PairingHeapNode<T> node)
    {
        //if this node is the child head pointer of parent
        if (node.IsHeadChild)
        {
            var parent = node.Previous;

            //use close sibling as new parent child pointer
            if (node.Next != null) node.Next.Previous = parent;

            parent.ChildrenHead = node.Next;
        }
        else
        {
            //just do regular deletion from linked list
            node.Previous.Next = node.Next;

            if (node.Next != null) node.Next.Previous = node.Previous;
        }
    }

    private void AddMapping(T newItem, PairingHeapNode<T> newNode)
    {
        if (heapMapping.ContainsKey(newItem))
            heapMapping[newItem].Add(newNode);
        else
            heapMapping[newItem] = new List<PairingHeapNode<T>>(new[] { newNode });
    }

    private void UpdateNodeValue(T currentValue, T newValue, PairingHeapNode<T> node)
    {
        RemoveMapping(currentValue, node);
        node.Value = newValue;
        AddMapping(newValue, node);
    }

    private void RemoveMapping(T currentValue, PairingHeapNode<T> node)
    {
        heapMapping[currentValue].Remove(node);
        if (heapMapping[currentValue].Count == 0) heapMapping.Remove(currentValue);
    }
}