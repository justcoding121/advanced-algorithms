using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures.Graph
{
    public interface IDiGraph<T> : IEnumerable<IDiGraphVertex<T>>
    {
        bool ContainsVertex(T value);
        IDiGraphVertex<T> GetVertex(T value);
        IDiGraphVertex<T> ReferenceVertex { get; }

        int VerticesCount { get; }

        bool HasEdge(T source, T destination);

        IDiGraph<T> Clone();
        
    }

    public interface IDiGraphVertex<T>
    {
        T Value { get; }
        IEnumerable<IDiEdge<T>> OutEdges { get; }
        IEnumerable<IDiEdge<T>> InEdges { get; }

        IDiEdge<T> GetOutEdge(IDiGraphVertex<T> targetVertex);

        int OutEdgeCount { get; }
        int InEdgeCount { get; }

      
    }

    public interface IDiEdge<T>
    {
        W Weight<W>() where W : IComparable;
        T Value { get; }
        IDiGraphVertex<T> Target { get; }
    }

    internal class DiEdge<T, C> : IDiEdge<T> where C : IComparable
    {
        private IDiGraphVertex<T> target;
        private object weight;

        internal DiEdge(IDiGraphVertex<T> target, C weight)
        {
            this.target = target;
            this.weight = weight;
        }

        public T Value => target.Value;

        public IDiGraphVertex<T> Target => target;

        public W Weight<W>() where W : IComparable
        {
            return (W)weight;
        }
    }

}
