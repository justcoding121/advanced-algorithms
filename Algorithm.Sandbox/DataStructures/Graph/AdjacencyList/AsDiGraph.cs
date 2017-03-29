using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsDiGraphVertex<T> 
        
    {
        public T Value { get; set; }

        public AsArrayList<AsDiGraphVertex<T>> OutEdges { get; set; }
        public AsArrayList<AsDiGraphVertex<T>> InEdges { get; set; }

        public AsDiGraphVertex(T value)
        {
            this.Value = value;

            OutEdges = new AsArrayList<AsDiGraphVertex<T>>();
            InEdges = new AsArrayList<AsDiGraphVertex<T>>();
        }

        public int CompareTo(object obj)
        {
           return CompareTo(obj as AsDiGraphVertex<T>);
        }

    }

    public class AsDiGraph<T> where T : IComparable
    {
        public AsDiGraphVertex<T> ReferenceVertex { get; set; }

        public AsDiGraphVertex<T> AddVertex(T value,
            AsArrayList<T> inEdges, AsArrayList<T> outEdges)
        {
            if (inEdges == null || outEdges == null || value == null)
            {
                throw new ArgumentNullException();
            }

            if(ReferenceVertex != null && inEdges.Length == 0 && outEdges.Length == 0)
            {
                throw new ArgumentException();
            }

            throw new NotImplementedException();
        }
        public void RemoveVertex(AsDiGraphVertex<T> value)
        {
            if(value == null)
            {
                throw new ArgumentNullException();
            }

            throw new NotImplementedException();
        }

        public void AddEdge(AsDiGraphVertex<T> source, AsDiGraphVertex<T> dest)
        {

        }

        public void RemoveEdge(AsDiGraphVertex<T> source, AsDiGraphVertex<T> dest)
        {

        }
    }
}
