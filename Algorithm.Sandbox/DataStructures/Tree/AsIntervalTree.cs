using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsIntervalTreeNode<T> where T : IComparable
    {
        public T Start { get; set; }
        public T End { get; set; }

        internal T MaxRight { get; set; }

    }

    public class AsIntervalTree<T> where T : IComparable
    {
        internal AsIntervalTreeNode<T> Root;

        public void Insert(AsIntervalTreeNode<T> newInterval)
        {
            if(Root == null)
            {
                Root = newInterval;
                return;
            }

        }

        public void Delete(AsIntervalTreeNode<T> interval)
        {

        }

        public AsArrayList<AsIntervalTreeNode<T>> GetIntersection(AsIntervalTreeNode<T> interval)
        {
            throw new NotImplementedException();
        }
    }
}
