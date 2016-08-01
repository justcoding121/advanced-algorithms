using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsDiGraphVertex<T> : IComparable
        where T : IComparable
    {
        public T Value { get; set; }

        public AsSinglyLinkedList<AsDiGraphVertex<T>> OutVertices { get; set; }
        public AsSinglyLinkedList<AsDiGraphVertex<T>> InVertices { get; set; }

        public AsDiGraphVertex(T value)
        {
            this.Value = value;

            OutVertices = new AsSinglyLinkedList<AsDiGraphVertex<T>>();
            InVertices = new AsSinglyLinkedList<AsDiGraphVertex<T>>();
        }

        public int CompareTo(object obj)
        {
           return CompareTo(obj as AsDiGraphVertex<T>);
        }

        private int CompareTo(AsDiGraphVertex<T> vertex)
        {
            return Value.CompareTo(vertex.Value);
        }

    }

    public class AsDiGraph<T> where T : IComparable
    {
        public AsDiGraphVertex<T> ReferenceVertex { get; set; }

    }
}
