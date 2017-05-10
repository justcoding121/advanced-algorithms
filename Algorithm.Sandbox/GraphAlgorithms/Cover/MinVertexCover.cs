using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Cover
{
    public class MinVertexCover<T>
    {
        public List<AsGraphVertex<T>> GetMinVertexCover(AsGraph<T> graph)
        {
            return GetMinVertexCover(graph.ReferenceVertex, new HashSet<AsGraphVertex<T>>(),
                new List<AsGraphVertex<T>>());
        }

        /// <summary>
        /// An approximation algorithm for NP complete vertex cover problem
        /// Add a random edge vertices until done visiting all edges
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        /// <param name="cover"></param>
        /// <returns></returns>
        private List<AsGraphVertex<T>> GetMinVertexCover(AsGraphVertex<T> vertex,
            HashSet<AsGraphVertex<T>> visited, List<AsGraphVertex<T>> cover)
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
