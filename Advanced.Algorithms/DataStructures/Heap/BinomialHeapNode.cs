using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures.Heap
{
    public class BinomialHeapNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree => Children.Count;

        internal BinomialHeapNode<T> Parent { get; set; }
        internal List<BinomialHeapNode<T>> Children { get; set; }

        public BinomialHeapNode(T value)
        {
            this.Value = value;

            Children = new List<BinomialHeapNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as BinomialHeapNode<T>).Value);
        }
    }

}
