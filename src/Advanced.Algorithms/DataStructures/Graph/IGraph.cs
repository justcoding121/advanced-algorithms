using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.DataStructures.Graph
{
    public interface IGraph<T> : IEnumerable<IGraphVertex<T>>
    {
        int VerticesCount { get; }

        IGraphVertex<T> ReferenceVertex { get; }
        bool ContainsVertex(T value);
        IGraphVertex<T> GetVertex(T value);

        bool HasEdge(T source, T destination);

        IGraph<T> Clone();
    }

    public interface IGraphVertex<T>
    {
        T Value { get; }
        IEnumerable<IEdge<T>> Edges { get; }
    }

    public interface IEdge<T>
    {
        W Weight<W>() where W : IComparable;
        T Value { get; }
        IGraphVertex<T> Target { get; }
    }

    internal class Edge<T, C> : IEdge<T> where C : IComparable
    {
        private IGraphVertex<T> target;
        private object weight;

        internal Edge(IGraphVertex<T> target, C weight)
        {
            this.target = target;
            this.weight = weight;
        }

        public T Value => target.Value;

        public IGraphVertex<T> Target => target;

        public W Weight<W>() where W : IComparable
        {
            return (W)weight;
        }
    }
}
