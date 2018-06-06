using System;

namespace Advanced.Algorithms.DataStructures
{
    public class TreeDictionaryNode<TK, TV> : IComparable
                                 where TK : IComparable
    {
        public TK Key { get; }
        public TV Value { get; set; }

        public TreeDictionaryNode(TK key, TV value)
        {
            this.Key = key;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            if (obj is TreeDictionaryNode<TK, TV> itemToComare)
            {
                return Key.CompareTo(itemToComare.Key);
            }

            throw new ArgumentException();
        }
    }

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// <summary>
    /// A Dictionary implementation using balanced binary search tree (log(n) operations in worst case)
    /// This may be better than regular Dictionary implementation which can give o(K) in worst case (but O(1) when collisions K is avoided )
    /// </summary>
    /// <typeparam name="TK"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class TreeDictionary<TK, TV> where TK : IComparable
    {
        //use red-black tree as our balanced BST since it gives good performance for both deletion/insertion
        private readonly RedBlackTree<TreeDictionaryNode<TK, TV>> binarySearchTree;

        public int Count => binarySearchTree.Count;

        public TreeDictionary()
        {
            binarySearchTree = new RedBlackTree<TreeDictionaryNode<TK, TV>>();
        }

        //O(log(n) time complexity; 
        public bool ContainsKey(TK key)
        {
            return binarySearchTree
                .HasItem(new TreeDictionaryNode<TK, TV>(key, default(TV)));
        }

        //O(log(n) time complexity; 
        public TV GetValue(TK key)
        {
            return binarySearchTree
                .FindNode(new TreeDictionaryNode<TK, TV>(key, default(TV)))
                .Value
                .Value;
        }

        //O(log(n) time complexity; 
        //add an item to this hash table
        public void Add(TK key, TV value)
        {
            binarySearchTree.Insert(new TreeDictionaryNode<TK, TV>(key, value));
        }

        //O(log(n) time complexity
        public void Remove(TK key)
        {
            binarySearchTree.Delete(new TreeDictionaryNode<TK, TV>(key, default(TV)));
        }

        //O(n) time complexity
        public ArrayList<TreeDictionaryNode<TK, TV>> GetAll()
        {
            var nodes = binarySearchTree.GetAllNodes();

            var allNodeValues = new ArrayList<TreeDictionaryNode<TK, TV>>();

            foreach (var node in nodes)
            {
                allNodeValues.Add(node);
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
