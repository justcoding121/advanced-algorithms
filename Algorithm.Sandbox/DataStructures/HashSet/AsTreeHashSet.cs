using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeHashSetNode<K, V> : IComparable
                                 where K : IComparable
    {
        public K Key { get; private set; }
        public V Value { get; set; }

        public AsTreeHashSetNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            var itemToComare = obj as AsTreeHashSetNode<K, V>;
            return Key.CompareTo(itemToComare.Key);
        }
    }
    /// <summary>
    /// A hashSet implementation using balanced binary search tree (log(n) operations in worst case)
    /// This may be better than regular hashSet implementation which can give o(K) in worst case (but O(1) when collisions K is avoided )
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class AsTreeHashSet<K, V> where K : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private AsRedBlackTree<AsTreeHashSetNode<K, V>> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public AsTreeHashSet()
        {
            binarySearchTree = new AsRedBlackTree<AsTreeHashSetNode<K, V>>();
        }

        //O(log(n) time complexity; 
        public bool ContainsKey(K key)
        {
            var keyItem = new AsTreeHashSetNode<K, V>(key, default(V));
            return binarySearchTree.HasItem(keyItem);
        }

        //O(log(n) time complexity; 
        public V GetValue(K key)
        {
            var keyItem = new AsTreeHashSetNode<K, V>(key, default(V));
            return binarySearchTree.Find(keyItem).Value.Value;
        }

        //O(log(n) time complexity; 
        //add an item to this hash table
        public void Add(K key, V value)
        {
            var keyItem = new AsTreeHashSetNode<K, V>(key, value);
            binarySearchTree.Insert(keyItem);
        }

        //O(log(n) time complexity
        public void Remove(K key)
        {
            var keyItem = new AsTreeHashSetNode<K, V>(key, default(V));
            binarySearchTree.Delete(keyItem);
        }

        public AsArrayList<AsTreeHashSetNode<K, V>> GetAll()
        {
            var nodes = binarySearchTree.GetAllNodes();

            var allNodeValues = new AsArrayList<AsTreeHashSetNode<K, V>>();

            for (int i = 0; i < nodes.Length; i++)
            {
                allNodeValues.AddItem(nodes.ItemAt(i));
            }

            nodes.Clear();

            return allNodeValues;
        }
    }
}
