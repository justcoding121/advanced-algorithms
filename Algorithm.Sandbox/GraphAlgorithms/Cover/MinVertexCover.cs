using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Linq;

namespace Algorithm.Sandbox.GraphAlgorithms.Cover
{
    public class MinVertexCover<T>
    {
        public AsArrayList<AsGraphVertex<T>> GetMinVertexCover(AsGraph<T> graph)
        {
            return GetMinVertexCover(graph.ReferenceVertex, new AsHashSet<AsGraphVertex<T>>(),
                new AsArrayList<AsGraphVertex<T>>());
        }

        /// <summary>
        /// An approximation algorithm for NP complete vertex cover problem
        /// Add a random edge vertices until done visiting all edges
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="visited"></param>
        /// <param name="cover"></param>
        /// <returns></returns>
        private AsArrayList<AsGraphVertex<T>> GetMinVertexCover(AsGraphVertex<T> vertex,
            AsHashSet<AsGraphVertex<T>> visited, AsArrayList<AsGraphVertex<T>> cover)
        {
            visited.Add(vertex);

            foreach (var edge in vertex.Edges)
            {
                if(!cover.Contains(vertex) && !cover.Contains(edge.Value))
                {
                    cover.Add(vertex);
                    cover.Add(edge.Value);
                }

                if(!visited.Contains(edge.Value))
                {
                    GetMinVertexCover(edge.Value, visited, cover);
                }
            }

            return cover;
        }
    }
}
