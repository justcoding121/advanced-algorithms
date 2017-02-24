using System;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    internal class AsBTreeNode<T> where T : IComparable
    {
        public T[] Values { get; set; }

        public AsBTreeNode<T> Parent { get; set; }
        public AsBTreeNode<T>[] Children { get; set; }
        public int Count { get; internal set; }

        public AsBTreeNode(int maxChildren)
        {
            Values = new T[maxChildren];
            Children = new AsBTreeNode<T>[maxChildren + 1];
        }
    }

    public class AsBTree<T> where T : IComparable
    {
        public int Count { get; private set; }

        internal AsBTreeNode<T> Root;
        private int maxChildren;

        public AsBTree(int maxChildren)
        {
            this.maxChildren = maxChildren;
        }


        public void Insert(T value)
        {
            var nodeToInsert = FindInsertionNode(ref Root, value);

            if (nodeToInsert.Count == nodeToInsert.Values.Length)
            {

            }
        }

        private AsBTreeNode<T> FindInsertionNode(ref AsBTreeNode<T> node, T value)
        {
            if (node == null)
            {
                node = new AsBTreeNode<T>(maxChildren);
                return node;
            }

            throw new NotImplementedException();
        }

        public void Delete(T value)
        {

        }

        public bool Exists(T value)
        {
            throw new NotImplementedException();
        }
    }
}