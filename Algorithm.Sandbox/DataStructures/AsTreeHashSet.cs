using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeHashSetNode<K, V> : IComparable 
                                 where K : IComparable
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// A hashSet implementation using binary search tree (log(n) operations in worst case)
    /// This may be better than regular hashSet implementation which can give o(K) in worst case (but O(1) when collisions K is avoided )
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsTreeHashSet<K, V> where K : IComparable
    {
        private AsRedBlackTree<AsTreeHashSetNode<K, V>> binarySearchTree = new AsRedBlackTree<AsTreeHashSetNode<K, V>>();

        //O(log(n) time complexity; 
        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        //O(log(n) time complexity; 
        public V GetValue(K key)
        {
            throw new NotImplementedException();
        }

        //O(log(n) time complexity; 
        //add an item to this hash table
        public void Add(K key, V value)
        {
            throw new NotImplementedException();
        }

        //O(log(n) time complexity; worst case O(n)
        public void Remove(K key)
        {
            throw new NotImplementedException();

        }
    }
}
