using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsKDTreeNode<T> where T : IComparable
    {
        public T[] Points { get; set; }

        public AsKDTreeNode(int dimensions)
        {
            Points = new T[dimensions];
        }

        AsKDTreeNode<T> Left { get; set; }
        AsKDTreeNode<T> Right { get; set; }
    }
    public class AsKDTree<T> where T : IComparable
    {
        private int dimensions;
        internal AsKDTreeNode<T> Root;

        public AsKDTree(int dimensions)
        {
            this.dimensions = dimensions;
        }

        public void Insert(T[] point)
        {
            throw new NotImplementedException();
        }

        public void Delete(T[] point)
        {
            throw new NotImplementedException();
        }

        public T[] FindNearestNeighbour(T[] point)
        {
            throw new NotImplementedException();
        }

        public AsArrayList<T[]> FindRange(T[] start, T[] end)
        {
            throw new NotImplementedException();
        } 
    }
}
