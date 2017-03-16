using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsRangeTreeNode<T> where T : IComparable
    {
        internal T Value => Values[0];

        /// <summary>
        /// Hold duplicates
        /// </summary>
        internal AsArrayList<T> Values { get; set; }
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
        internal AsRangeTreeNode<T> Root;
        internal int Count;

        private void Insert(T value)
        {
            if (Root == null)
            {
                Root = new AsRangeTreeNode<T>();
                Root.Values.Add(value);
                Count++;
                return;
            }

            var insertionNode = FindInsertionLeaf(Root, value);

            //duplicate
            if (value.CompareTo(insertionNode.Value) == 0)
            {
                insertionNode.Values.Add(value);
                Count++;
                return;
            }

            SplitInsert(ref insertionNode, value, null, null);
        }

        private void SplitInsert(ref AsRangeTreeNode<T> insertionNode, T value,
            AsRangeTreeNode<T> left, AsRangeTreeNode<T> right)
        {
            throw new NotImplementedException();
        }

        private AsRangeTreeNode<T> FindInsertionLeaf(AsRangeTreeNode<T> node, T value)
        {
            //if leaf and match
            if (value.CompareTo(node.Value) == 0 && node.IsLeaf)
            {
                return node;
            }
            else if (value.CompareTo(node.Value) < 0
                && node.Left != null)
            {
                return FindInsertionLeaf(node.Left, value);
            }
            else if (node.Right != null)
            {
                return FindInsertionLeaf(node.Right, value);
            }

            return node;
        }

        private void Delete(T value)
        {

        }

        private void GetRange(T start, T end)
        {

        }


    }
}
