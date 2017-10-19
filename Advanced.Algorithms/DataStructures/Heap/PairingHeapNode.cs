using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures.Heap
{
    public class PairingHeapNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal PairingHeapNode<T> ChildrenHead { get; set; }
        internal bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

        public PairingHeapNode(T value)
        {
            this.Value = value;
        }

        internal PairingHeapNode<T> Previous;
        internal PairingHeapNode<T> Next;

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as PairingHeapNode<T>).Value);
        }
    }

}
