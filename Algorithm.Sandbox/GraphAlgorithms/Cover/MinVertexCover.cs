using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Cover
{
    public class MinVertexCover<T>
    {
        public List<GraphVertex<T>> GetMinVertexCover(AsGraph<T> graph)
        {
            return GetMinVertexCover(graph.ReferenceVertex, new HashSet<GraphVertex<T>>(),
                new List<GraphVertex<T>>());
        }

        /// <summary>
        /// An approximation algorithm for NP complete vertex cover problem
        /// Add a random edge vertices until done visiting all edges
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        /// <param name="cover"></param>
        /// <returns></returns>
        private List<GraphVertex<T>> GetMinVertexCover(GraphVertex<T> vertex,
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
                    GetMinVertexCover(edge, visited, cover);
                }
            }

            return cover;
        }
    }
}
