using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures.Graph
{
    /// <summary>
    /// UnDirected graph. (When implemented on a directed graphs only outgoing edges are considered as Edges).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGraph<T>
    {
        bool IsWeightedGraph { get; }

        int VerticesCount { get; }
        IGraphVertex<T> ReferenceVertex { get; }
        bool ContainsVertex(T key);
        IGraphVertex<T> GetVertex(T key);
        IEnumerable<IGraphVertex<T>> VerticesAsEnumberable { get; }

        bool HasEdge(T source, T destination);

        IGraph<T> Clone();
    }

    public interface IGraphVertex<T>
    {
        T Key { get; }
        IEnumerable<IEdge<T>> Edges { get; }

        IEdge<T> GetEdge(IGraphVertex<T> targetVertex);
    }

    public interface IEdge<T>
    {
        W Weight<W>() where W : IComparable;
        T TargetVertexKey { get; }
        IGraphVertex<T> TargetVertex { get; }
    }

    internal class Edge<T, C> : IEdge<T> where C : IComparable
    {
        private object weight;

        internal Edge(IGraphVertex<T> target, C weight)
        {
            this.TargetVertex = target;
            this.weight = weight;
        }

        public T TargetVertexKey => TargetVertex.Key;

        public IGraphVertex<T> TargetVertex { get; private set; }

        public W Weight<W>() where W : IComparable
        {
            return (W)weight;
        }
    }
}
