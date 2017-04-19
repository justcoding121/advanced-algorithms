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
    /// A BiDirectional Path Search on DiGraph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BiDirectional<T>
    {
        /// <summary>
        /// Returns true if Path exists from source to destination
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool PathExists(AsDiGraph<T> graph, T source, T destination)
        {
            return BDS(graph, source, destination);
        }

        /// <summary>
        /// Use breadth First Search from Source and Target until they meet
        /// If they could'nt find the element before they meet return false
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private bool BDS(AsDiGraph<T> graph, T source, T destination)
        {
            var visitedA = new AsHashSet<T>();
            var visitedB = new AsHashSet<T>();

            var bfsQueueA = new AsQueue<AsDiGraphVertex<T>>();
            var bfsQueueB = new AsQueue<AsDiGraphVertex<T>>();

            bfsQueueA.Enqueue(graph.Vertices[source]);
            bfsQueueB.Enqueue(graph.Vertices[destination]);

            visitedA.Add(graph.Vertices[source].Value);
            visitedB.Add(graph.Vertices[destination].Value);

            //search from both ends for a Path
            while (true)
            {
                if (bfsQueueA.Count > 0)
                {
                    var current = bfsQueueA.Dequeue();

                    //intersects with search from other end
                    if (visitedB.Contains(current.Value))
                    {
                        return true;
                    }

                    foreach (var edge in current.OutEdges)
                    {
                        if (!visitedA.Contains(edge.Value.Value))
                        {
                            visitedA.Add(edge.Value.Value);
                            bfsQueueA.Enqueue(edge.Value);
                        }
                    }
                }

                if (bfsQueueB.Count > 0)
                {
                    var current = bfsQueueB.Dequeue();

                    //intersects with search from other end
                    if (visitedA.Contains(current.Value))
                    {
                        return true;
                    }

                    foreach (var edge in current.InEdges)
                    {
                        if (!visitedB.Contains(edge.Value.Value))
                        {
                            visitedB.Add(edge.Value.Value);
                            bfsQueueB.Enqueue(edge.Value);
                        }
                    }
                }

                if(bfsQueueA.Count == 0 && bfsQueueB.Count == 0)
                {
                    break;
                }
            }

            return false;
        }
    }
}
