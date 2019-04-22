using Advanced.Algorithms.DataStructures.Graph;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A minimum vertex conver algorithm implementation.
    /// </summary>
    public class MinVertexCover<T>
    {
        public List<IGraphVertex<T>> GetMinVertexCover(IGraph<T> graph)
        {
            return getMinVertexCover(graph.ReferenceVertex, new HashSet<IGraphVertex<T>>(),
                new List<IGraphVertex<T>>());
        }

        /// <summary>
        /// An approximation algorithm for NP complete vertex cover problem.
        /// Add a random edge vertices until done visiting all edges.
        /// </summary>
        private List<IGraphVertex<T>> getMinVertexCover(IGraphVertex<T> vertex,
            HashSet<IGraphVertex<T>> visited, List<IGraphVertex<T>> cover)
        {
            visited.Add(vertex);

            foreach (var edge in vertex.Edges)
            {
                if(!cover.Contains(vertex) && !cover.Contains(edge.TargetVertex))
                {
                    cover.Add(vertex);
                    cover.Add(edge.TargetVertex);
                }

                if(!visited.Contains(edge.TargetVertex))
                {
                    getMinVertexCover(edge.TargetVertex, visited, cover);
                }
            }

            return cover;
        }
    }
}
