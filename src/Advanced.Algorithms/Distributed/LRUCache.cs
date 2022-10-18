using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Distributed;

/// <summary>
///     A least recently used cache implemetation.
/// </summary>
public class LruCache<TK, TV>
{
    private readonly int capacity;

    private readonly DoublyLinkedList<Tuple<TK, TV>> dll = new();

    private readonly Dictionary<TK, DoublyLinkedListNode<Tuple<TK, TV>>> lookUp = new();

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new Exception("Capacity must be a positive integer.");
        this.capacity = capacity;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public TV Get(TK key)
    {
        if (!lookUp.ContainsKey(key))
            return default;

        var node = lookUp[key];

        //move lately used node to beginning of ddl 
        dll.Delete(node);
        var newNode = dll.InsertFirst(node.Data);
        lookUp[key] = newNode;

        return node.Data.Item2;
    }

    /// <summary>
    ///     Time complexity: O(1).
    /// </summary>
    public void Put(TK key, TV value)
    {
        //evict last node of ddl if capacity overflows
        if (lookUp.Count == capacity)
        {
            var nodeToEvict = dll.Last();
            lookUp.Remove(nodeToEvict.Item1);
            dll.DeleteLast();
        }

        //insert
        var newNode = dll.InsertFirst(new Tuple<TK, TV>(key, value));
        lookUp.Add(key, newNode);
    }
}