using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A dijikstra algorithm implementation using Fibonacci Heap.
    /// </summary>
    public class DijikstraShortestPath<T, W> where W : IComparable
    {
        readonly IShortestPathOperators<W> @operator;
        public DijikstraShortestPath(IShortestPathOperators<W> @operator)
        {
            this.@operator = @operator;
        }

        /// <summary>
        /// Get shortest distance to target.
        /// </summary>
        public ShortestPathResult<T, W> FindShortestPath(IGraph<T> graph, T source, T destination)
        {
            //regular argument checks
            if (graph?.GetVertex(source) == null || graph.GetVertex(destination) == null)
            {
                throw new ArgumentException();
            }

            if (this.@operator == null)
            {
                throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");
            }

            if (!graph.IsWeightedGraph)
            {
                if (this.@operator.DefaultValue.GetType() != typeof(int))
                {
                    throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                        "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");
                }
            }

            //track progress for distance to each Vertex from source
            var progress = new Dictionary<T, W>();

            //trace our current path by mapping current vertex to its Parent
            var parentMap = new Dictionary<T, T>();

            //min heap to pick next closest vertex 
            var minHeap = new FibonacciHeap<MinHeapWrap<T, W>>();
            //keep references of heap Node for decrement key operation
            var heapMapping = new Dictionary<T, MinHeapWrap<T, W>>();

            //add vertices to min heap and progress map
            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                //init parent
                parentMap.Add(vertex.Key, default(T));

                //init to max value
                progress.Add(vertex.Key, @operator.MaxValue);

                if (vertex.Key.Equals(source))
                {
                    continue;
                }
            }

            //start from source vertex as current 
            var current = new MinHeapWrap<T, W>()
            {
                Distance = @operator.DefaultValue,
                Vertex = source
            };

            minHeap.Insert(current);
            heapMapping.Add(current.Vertex, current);

            //until heap is empty
            while (minHeap.Count > 0)
            {
                //next min vertex to visit
                current = minHeap.Extract();
                heapMapping.Remove(current.Vertex);

                //no path exists, so return max value
                if (current.Distance.Equals(@operator.MaxValue))
                {
                    return new ShortestPathResult<T, W>(null, @operator.MaxValue);
                }

                //visit neighbours of current
                foreach (var neighbour in graph.GetVertex(current.Vertex).Edges.Where(x => !x.TargetVertexKey.Equals(source)))
                {
                    //new distance to neighbour
                    var newDistance = @operator.Sum(current.Distance,
                        graph.GetVertex(current.Vertex).GetEdge(neighbour.TargetVertex).Weight<W>());

                    //current distance to neighbour
                    var existingDistance = progress[neighbour.TargetVertexKey];

                    //update distance if new is better
                    if (newDistance.CompareTo(existingDistance) < 0)
                    {
                        progress[neighbour.TargetVertexKey] = newDistance;

                        if (!heapMapping.ContainsKey(neighbour.TargetVertexKey))
                        {
                            var wrap = new MinHeapWrap<T, W>() { Distance = newDistance, Vertex = neighbour.TargetVertexKey };
                            minHeap.Insert(wrap);
                            heapMapping.Add(neighbour.TargetVertexKey, wrap);
                        }
                        else
                        {
                            //decrement distance to neighbour in heap
                            var decremented = new MinHeapWrap<T, W>() { Distance = newDistance, Vertex = neighbour.TargetVertexKey };
                            minHeap.UpdateKey(heapMapping[neighbour.TargetVertexKey], decremented);
                            heapMapping[neighbour.TargetVertexKey] = decremented;
                        }

                        //trace parent
                        parentMap[neighbour.TargetVertexKey] = current.Vertex;
                    }
                }

            }

            return tracePath(graph, parentMap, source, destination);

        }

        /// <summary>
        /// Trace back path from destination to source using parent map.
        /// </summary>
        private ShortestPathResult<T, W> tracePath(IGraph<T> graph, Dictionary<T, T> parentMap, T source, T destination)
        {
            //trace the path
            var pathStack = new Stack<T>();

            pathStack.Push(destination);

            var currentV = destination;
            while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV], default(T)))
            {
                pathStack.Push(parentMap[currentV]);
                currentV = parentMap[currentV];
            }

            //return result
            var resultPath = new List<T>();
            var resultLength = @operator.DefaultValue;
            while (pathStack.Count > 0)
            {
                resultPath.Add(pathStack.Pop());
            }

            for (int i = 0; i < resultPath.Count - 1; i++)
            {
                resultLength = @operator.Sum(resultLength,
                    graph.GetVertex(resultPath[i]).GetEdge(graph.GetVertex(resultPath[i + 1])).Weight<W>());
            }

            return new ShortestPathResult<T, W>(resultPath, resultLength);
        }
    }

    /// <summary>
    /// Generic operators interface required by shorted path algorithms.
    /// </summary>
    public interface IShortestPathOperators<W> where W : IComparable
    {
        W DefaultValue { get; }
        W MaxValue { get; }
        W Sum(W a, W b);
    }

    /// <summary>
    /// Shortest path result object.
    /// </summary>
    public class ShortestPathResult<T, W> where W : IComparable
    {
        public ShortestPathResult(List<T> path, W length)
        {
            Length = length;
            Path = path;
        }
        public W Length { get; internal set; }
        public List<T> Path { get; private set; }
    }

    /// <summary>
    /// For fibornacci heap node.
    /// </summary>
    internal class MinHeapWrap<T, W> : IComparable where W : IComparable
    {
        internal T Vertex { get; set; }
        internal W Distance { get; set; }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo((obj as MinHeapWrap<T, W>).Distance);
        }
    }
}
