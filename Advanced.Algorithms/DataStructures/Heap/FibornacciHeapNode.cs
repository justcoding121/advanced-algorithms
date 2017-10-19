using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures.Heap
{
    public class FibornacciHeapNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal int Degree;
        internal FibornacciHeapNode<T> ChildrenHead { get; set; }

        internal FibornacciHeapNode<T> Parent { get; set; }
        internal bool LostChild { get; set; }

        internal FibornacciHeapNode<T> Previous;
        internal FibornacciHeapNode<T> Next;

        public FibornacciHeapNode(T value)
        {
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as FibornacciHeapNode<T>).Value);
        }
    }

}
