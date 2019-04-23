using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;


namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A BiDirectional Path Search on DiGraph.
    /// </summary>
    public class BiDirectional<T>
    {
        /// <summary>
        /// Returns true if Path exists from source to destination.
        /// </summary>
        public bool PathExists(IGraph<T> graph, T source, T destination)
        {
            return bfs(graph, source, destination);
        }

        /// <summary>
        /// Use breadth First Search from Source and Target until they meet.
        /// If they could'nt find the element before they meet return false.
        /// </summary>
        private bool bfs(IGraph<T> graph, T source, T destination)
        {
            var visitedA = new HashSet<T>();
            var visitedB = new HashSet<T>();

            var bfsQueueA = new Queue<IGraphVertex<T>>();
            var bfsQueueB = new Queue<IGraphVertex<T>>();

            bfsQueueA.Enqueue(graph.GetVertex(source));
            bfsQueueB.Enqueue(graph.GetVertex(destination));

            visitedA.Add(graph.GetVertex(source).Key);
            visitedB.Add(graph.GetVertex(destination).Key);

            //search from both ends for a Path
            while (true)
            {
                if (bfsQueueA.Count > 0)
                {
                    var current = bfsQueueA.Dequeue();

                    //intersects with search from other end
                    if (visitedB.Contains(current.Key))
                    {
                        return true;
                    }

                    foreach (var edge in current.Edges)
                    {
                        if (visitedA.Contains(edge.TargetVertexKey))
                        {
                            continue;
                        }

                        visitedA.Add(edge.TargetVertexKey);
                        bfsQueueA.Enqueue(edge.TargetVertex);
                    }
                }

                if (bfsQueueB.Count > 0)
                {
                    var current = bfsQueueB.Dequeue();

                    //intersects with search from other end
                    if (visitedA.Contains(current.Key))
                    {
                        return true;
                    }

                    foreach (var edge in current.Edges)
                    {
                        if (visitedB.Contains(edge.TargetVertexKey))
                        {
                            continue;
                        }

                        visitedB.Add(edge.TargetVertexKey);
                        bfsQueueB.Enqueue(edge.TargetVertex);
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
