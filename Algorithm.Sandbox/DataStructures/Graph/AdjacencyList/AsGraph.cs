using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsGraphVertex<T> : IComparable
        where T : IComparable
    {
        public T Value { get; set; }

        public AsSinglyLinkedList<AsGraphVertex<T>> Vertices { get; set; }

        public AsGraphVertex(T value)
        {
            this.Value = value;

            Vertices = new AsSinglyLinkedList<AsGraphVertex<T>>();
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

    public class AsGraph<T> where T : IComparable
    {
        public AsGraphVertex<T> ReferenceVertex { get; set; }

    }
}
