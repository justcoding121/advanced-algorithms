using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A minimum vertex conver algorithm implementation.
    /// </summary>
    public class MinVertexCover<T>
    {
        public List<GraphVertex<T>> GetMinVertexCover(Graph<T> graph)
        {
            return getMinVertexCover(graph.ReferenceVertex, new HashSet<GraphVertex<T>>(),
                new List<GraphVertex<T>>());
        }

        /// <summary>
        /// An approximation algorithm for NP complete vertex cover problem.
        /// Add a random edge vertices until done visiting all edges.
        /// </summary>
        private List<GraphVertex<T>> getMinVertexCover(GraphVertex<T> vertex,
            HashSet<GraphVertex<T>> visited, List<GraphVertex<T>> cover)
        {
            visited.Add(vertex);

            foreach (var edge in vertex.Edges)
            {
                if(!cover.Contains(vertex) && !cover.Contains(edge))
                {
                    cover.Add(vertex);
                    cover.Add(edge);
                }

                if(!visited.Contains(edge))
                {
                    getMinVertexCover(edge, visited, cover);
                }
            }

            return cover;
        }
    }
}
