using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsRangeTreeNode<T> where T:IComparable
    {
        internal AsRangeTreeNode<T> Left { get; set; }
        internal AsRangeTreeNode<T> Right { get; set; }

        internal AsRangeTreeNode<T> Parent { get; set; }
    }

    public class AsRangeTree<T> where T : IComparable
    {
        private int dimensions;
        public AsRangeTree(int dimensions)
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
}
