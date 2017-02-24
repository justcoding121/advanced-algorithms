using System;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    internal class AsBTreeNode<T> where T : IComparable
    {
        public AsArrayList<T> Value { get; set; }

        public AsBTreeNode<T> Parent { get; set; }
        public AsArrayList<AsBTreeNode<T>> Children { get; set; }

    }

    public class AsBTree<T> where T : IComparable
    {
        public int Count { get; private set; }

        internal AsBTreeNode<T> Root;

        public void Insert(T value)
        {

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