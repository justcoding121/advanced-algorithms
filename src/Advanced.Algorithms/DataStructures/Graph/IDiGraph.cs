using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Graph
{
    /// <summary>
    /// Directed graph. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDiGraph<T> 
    {
        bool IsWeightedGraph { get; }

        bool ContainsVertex(T value);
        IDiGraphVertex<T> GetVertex(T key);
        IDiGraphVertex<T> ReferenceVertex { get; }
        IEnumerable<IDiGraphVertex<T>> VerticesAsEnumberable { get; }
        int VerticesCount { get; }

        bool HasEdge(T source, T destination);

        IDiGraph<T> Clone();      
    }

    public interface IDiGraphVertex<T>
    {
        T Key { get; }
        IEnumerable<IDiEdge<T>> OutEdges { get; }
        IEnumerable<IDiEdge<T>> InEdges { get; }

        IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex);

        int OutEdgeCount { get; }
        int InEdgeCount { get; }
    }

    public interface IDiEdge<T>
    {
        W Weight<W>() where W : IComparable;
        T TargetVertexKey { get; }
        IDiGraphVertex<T> TargetVertex { get; }
    }

    internal class DiEdge<T, C> : IDiEdge<T> where C : IComparable
    {
        private object weight;

        internal DiEdge(IDiGraphVertex<T> target, C weight)
        {
            TargetVertex = target;
            this.weight = weight;
        }

        public T TargetVertexKey => TargetVertex.Key;

        public IDiGraphVertex<T> TargetVertex { get; private set; }

        public W Weight<W>() where W : IComparable
        {
            return (W)weight;
        }
    }

}
