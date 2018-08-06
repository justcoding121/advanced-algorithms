using Advanced.Algorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Distributed
{
    /// <summary>
    /// A least recently used cache implemetation.
    /// </summary>
    public class LRUCache<K, V>
    {
        private readonly int capacity;

        private Dictionary<K, DoublyLinkedListNode<Tuple<K, V>>> lookUp
            = new Dictionary<K, DoublyLinkedListNode<Tuple<K, V>>>();

        private readonly DoublyLinkedList<Tuple<K, V>> dll = new DoublyLinkedList<Tuple<K, V>>();

        public LRUCache(int capacity)
        {
            if (capacity <= 0)
            {
                throw new Exception("Capacity must be a positive integer.");
            }
            this.capacity = capacity;
        }

        /// <summary>
        /// Time complexity: O(1). 
        /// </summary>
        public V Get(K key)
        {
            if (!lookUp.ContainsKey(key))
                return default(V);

            var node = lookUp[key];

            //move lately used node to beginning of ddl 
            dll.Delete(node);
            var newNode = dll.InsertFirst(node.Data);
            lookUp[key] = newNode;

            return node.Data.Item2;
        }

        /// <summary>
        /// Time complexity: O(1). 
        /// </summary>
        public void Put(K key, V value)
        {
            //evict last node of ddl if capacity overflows
            if (lookUp.Count == capacity)
            {
                var nodeToEvict = dll.Last();
                lookUp.Remove(nodeToEvict.Item1);
                dll.DeleteLast();
            }

            //insert
            var newNode = dll.InsertFirst(new Tuple<K, V>(key, value));
            lookUp.Add(key, newNode);
        }
    }
}
