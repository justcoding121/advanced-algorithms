using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A HashSet implementation using balanced binary search tree (log(n) operations in worst case)
    /// This may be better than regular HashSet implementation which can give o(K) in worst case (but O(1) when collisions K is avoided )
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class TreeHashSet<V> where V : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private RedBlackTree<V> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public TreeHashSet()
        {
            binarySearchTree = new RedBlackTree<V>();
        }

        //O(log(n) time complexity; 
        public bool Contains(V value)
        {
            return binarySearchTree
                .HasItem(value);
        }

        //O(log(n) time complexity; 
        //add an item to this hash table
        public void Add(V value)
        {
            binarySearchTree.Insert(value);
        }

        //O(log(n) time complexity
        public void Remove(V value)
        {
            binarySearchTree.Delete(value);
        }

        //O(n) time complexity
        public ArrayList<V> GetAll()
        {
            var nodes = binarySearchTree.GetAllNodes();

            var allNodeValues = new ArrayList<V>();

            for (int i = 0; i < nodes.Count; i++)
            {
                allNodeValues.Add(nodes[i]);
            }

            nodes.Clear();

            return allNodeValues;
        }

        internal void Clear()
        {
            binarySearchTree.Clear();
        }
    }
}
