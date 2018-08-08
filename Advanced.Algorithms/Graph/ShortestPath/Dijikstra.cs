using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A dijikstra algorithm implementation using Fibornacci Heap.
    /// </summary>
    public class DijikstraShortestPath<T, W> where W : IComparable
    {
        readonly IShortestPathOperators<W> operators;
        public DijikstraShortestPath(IShortestPathOperators<W> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Get shortest distance to target.
        /// </summary>
        public ShortestPathResult<T, W> GetShortestPath(WeightedDiGraph<T, W> graph, T source, T destination)
        {
            //regular argument checks
            if (graph?.FindVertex(source) == null || graph.FindVertex(destination) == null)
            {
                throw new ArgumentException();
            }

            //track progress for distance to each Vertex from source
            var progress = new Dictionary<T, W>();

            //trace our current path by mapping current vertex to its Parent
            var parentMap = new Dictionary<T, T>();

            //min heap to pick next closest vertex 
            var minHeap = new FibornacciMinHeap<MinHeapWrap<T, W>>();
            //keep references of heap Node for decrement key operation
            var heapMapping = new Dictionary<T, MinHeapWrap<T, W>>();

            //add vertices to min heap and progress map
            foreach (var vertex in graph.Vertices)
            {
                //init parent
                parentMap.Add(vertex.Key, default(T));

                //init to max value
                progress.Add(vertex.Key, operators.MaxValue);

                if (vertex.Key.Equals(source))
                {
                    continue;
                }

                //construct heap for all vertices with Max Distance as default
                var wrap = new MinHeapWrap<T, W>()
                {
                    Distance = operators.MaxValue,
                    Target = vertex.Key
                };

                minHeap.Insert(wrap);
                heapMapping.Add(vertex.Key, wrap);
            }

            //start from source vertex as current 
            var current = new MinHeapWrap<T, W>()
            {
                Distance = operators.DefaultValue,
                Target = source
            };

            //until heap is empty
            while (minHeap.Count > 0)
            {
                //no path exists, so return max value
                if(current.Distance.Equals(operators.MaxValue))
                {
                    return new ShortestPathResult<T, W>(null, operators.MaxValue);
                }

                //visit neighbours of current
                foreach (var neighbour in graph.Vertices[current.Target].OutEdges)
                {
                    //new distance to neighbour
                    var newDistance = operators.Sum(current.Distance,
                        graph.Vertices[current.Target].OutEdges[neighbour.Key]);

                    //current distance to neighbour
                    var existingDistance = progress[neighbour.Key.Value];

                    //update distance if new is better
                    if (newDistance.CompareTo(existingDistance) < 0)
                    {
                        progress[neighbour.Key.Value] = newDistance;

                        //decrement distance to neighbour in heap
                        var decremented = new MinHeapWrap<T, W>() { Distance = newDistance, Target = neighbour.Key.Value };
                        minHeap.DecrementKey(heapMapping[neighbour.Key.Value], decremented);
                        heapMapping[neighbour.Key.Value] = decremented;

                        //trace parent
                        parentMap[neighbour.Key.Value] = current.Target;
                    }
                }

                //next min vertex to visit
                current = minHeap.ExtractMin();
            }

            return tracePath(graph, parentMap, source, destination);
         
        }

        /// <summary>
        /// Trace back path from destination to source using parent map.
        /// </summary>
        private ShortestPathResult<T, W> tracePath(WeightedDiGraph<T, W> graph, Dictionary<T, T> parentMap, T source, T destination)
        {
            //trace the path
            var pathStack = new Stack<T>();

            pathStack.Push(destination);

            var currentV = destination;
            while (!Equals(currentV,default(T)) && !Equals(parentMap[currentV], default(T)))
            {
                pathStack.Push(parentMap[currentV]);
                currentV = parentMap[currentV];
            }

            //return result
            var resultPath = new List<T>();
            var resultLength = operators.DefaultValue;
            while (pathStack.Count > 0)
            {
                resultPath.Add(pathStack.Pop());
            }

            for (int i = 0; i < resultPath.Count - 1; i++)
            {
                resultLength = operators.Sum(resultLength,
                    graph.Vertices[resultPath[i]].OutEdges[graph.Vertices[resultPath[i + 1]]]);
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
        internal T Target { get; set; }
        internal W Distance { get; set; }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo((obj as MinHeapWrap<T, W>).Distance);
        }
    }
}
