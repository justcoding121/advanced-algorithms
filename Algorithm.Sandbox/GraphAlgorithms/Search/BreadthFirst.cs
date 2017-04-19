using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.GraphAlgorithms.Search
{
    /// <summary>
    /// Bread First Search implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BreadthFirst<T>
    {
        /// <summary>
        /// Returns true if item exists
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool Find(AsGraph<T> graph, T vertex)
        {
            return BFS(graph.ReferenceVertex, new AsHashSet<T>(), vertex);
        }

        /// <summary>
        /// BFS implementation
        /// </summary>
        /// <param name="referenceVertex"></param>
        /// <param name="asHashSet"></param>
        /// <param name="searchVertex"></param>
        /// <returns></returns>
        private bool BFS(AsGraphVertex<T> referenceVertex,
            AsHashSet<T> visited, T searchVertex)
        {
            var bfsQueue = new AsQueue<AsGraphVertex<T>>();

            bfsQueue.Enqueue(referenceVertex);
            visited.Add(referenceVertex.Value);

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Value.Equals(searchVertex))
                {
                    return true;
                }

                foreach (var edge in current.Edges)
                {
                    if (!visited.Contains(edge.Value.Value))
                    {
                        visited.Add(edge.Value.Value);
                        bfsQueue.Enqueue(edge.Value);
                    }
                }
            }

            return false;
        }
    }
}
