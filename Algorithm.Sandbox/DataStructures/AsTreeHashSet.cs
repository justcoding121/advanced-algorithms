using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A hashSet implementation using binary search tree (log(n) operations in worst case)
    /// This may be better than regular hashSet implementation which can give o(K) in worst case (but O(1) when collisions K is avoided )
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsTreeHashSet<K,V>
    {
        private AsBST<K, V> binarySearchTree = new AsBST<K, V>();

        //O(logn) time complexity; 
        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        //O(logn) time complexity; 
        public V GetValue(K key)
        {
            throw new NotImplementedException();
        }

        //O(logn) time complexity; 
        //add an item to this hash table
        public void Add(K key, V value)
        {
            throw new NotImplementedException();
        }

        //O(logn) time complexity; worst case O(n)
        public void Remove(K key)
        {
            throw new NotImplementedException();

        }
    }
}
