using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsRangeTreeNode<T> where T : IComparable
    {
        internal T Value => Values[0];

        /// <summary>
        /// Hold duplicates
        /// </summary>
        internal AsArrayList<T> Values = new AsArrayList<T>();
        internal AsRangeTreeNode<T> Left { get; set; }
        internal AsRangeTreeNode<T> Right { get; set; }

        internal AsRangeTreeNode<T> Parent { get; set; }
        public bool IsLeaf => Left == null && Right == null;


    }

    public class AsDRangeTree<T> where T : IComparable
    {
        private int dimensions;
        public AsDRangeTree(int dimensions)
        {
            this.dimensions = dimensions;
        }

        public void Insert(T[] value)
        {

        }

        public void Delete(T[] value)
        {

        }

        public void GetRange(T[] start, T[] end)
        {

        }

    }

    internal class AsRangeTree<T> where T : IComparable
    {
        internal AsRedBlackTree<T> tree = new AsRedBlackTree<T>();
        internal int Count;

        internal void Insert(T value)
        {
           
        }

        private void Delete(T value)
        {

        }

        private void GetRange(T start, T end)
        {

        }

    }
}
