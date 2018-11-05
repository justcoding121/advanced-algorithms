using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A* algorithm implementation using Fibornacci Heap.
    /// </summary>
    public class AStarShortestPath<T, W> where W : IComparable
    {
        readonly IShortestPathOperators<W> operators;
        readonly IAStarHeuristic<T, W> heuristic;

        public AStarShortestPath(IShortestPathOperators<W> operators, IAStarHeuristic<T, W> heuristic)
        {
            this.operators = operators;
            this.heuristic = heuristic;
        }

        /// <summary>
        /// Search path to target using the heuristic.
        /// </summary>
        public ShortestPathResult<T, W> FindShortestPath(WeightedDiGraph<T, W> graph, T source, T destination)
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
            var minHeap = new FibornacciHeap<AStarWrap<T, W>>();
            //keep references of heap Node for decrement key operation
            var heapMapping = new Dictionary<T, AStarWrap<T, W>>();

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
            }

            //start from source vertex as current 
            var current = new AStarWrap<T, W>(heuristic, destination)
            {
                Distance = operators.DefaultValue,
                Vertex = source
            };

            //insert neighbour in heap
            minHeap.Insert(current);
            heapMapping[source] = current;

            //until heap is empty
            while (minHeap.Count > 0)
            {
                //next min vertex to visit
                current = minHeap.Extract();
                heapMapping.Remove(current.Vertex);

                //no path exists, so return max value
                if (current.Distance.Equals(operators.MaxValue))
                {
                    return new ShortestPathResult<T, W>(null, operators.MaxValue);
                }

                //visit neighbours of current
                foreach (var neighbour in graph.Vertices[current.Vertex].OutEdges.Where(x => !x.Key.Value.Equals(source)))
                {
                    //new distance to neighbour
                    var newDistance = operators.Sum(current.Distance,
                        graph.Vertices[current.Vertex].OutEdges[neighbour.Key]);

                    //current distance to neighbour
                    var existingDistance = progress[neighbour.Key.Value];

                    //update distance if new is better
                    if (newDistance.CompareTo(existingDistance) < 0)
                    {
                        progress[neighbour.Key.Value] = newDistance;

                        if (heapMapping.ContainsKey(neighbour.Key.Value))
                        {
                            //decrement distance to neighbour in heap
                            var decremented = new AStarWrap<T, W>(heuristic, destination) { Distance = newDistance, Vertex = neighbour.Key.Value };
                            minHeap.UpdateKey(heapMapping[neighbour.Key.Value], decremented);
                            heapMapping[neighbour.Key.Value] = decremented;

                        }
                        else
                        {
                            //insert neighbour in heap
                            var discovered = new AStarWrap<T, W>(heuristic, destination) { Distance = newDistance, Vertex = neighbour.Key.Value };
                            minHeap.Insert(discovered);
                            heapMapping[neighbour.Key.Value] = discovered;
                        }

                        //trace parent
                        parentMap[neighbour.Key.Value] = current.Vertex;
                    }
                }
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
            while (!Equals(currentV, default(T)) && !Equals(parentMap[currentV], default(T)))
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
    /// Search heuristic used by A* search algorithm.
    /// </summary>
    public interface IAStarHeuristic<T, W> where W : IComparable
    {
        /// <summary>
        /// Return the distance to target for given sourcevertex as computed by the hueristic used for A* search.
        /// </summary>
        W HueristicDistanceToTarget(T sourceVertex, T targetVertex);
    }

    //Node for our Fibornacci heap
    internal class AStarWrap<T, W> : IComparable where W : IComparable
    {
        private IAStarHeuristic<T, W> heuristic;
        private T destinationVertex;
        internal AStarWrap(IAStarHeuristic<T, W> heuristic, T destinationVertex)
        {
            this.heuristic = heuristic;
            this.destinationVertex = destinationVertex;
        }

        internal T Vertex { get; set; }
        internal W Distance { get; set; }

        //compare distance to target using the heuristic provided
        public int CompareTo(object obj)
        {
            if (this == obj)
            {
                return 0;
            }

            var result1 = heuristic.HueristicDistanceToTarget(Vertex, destinationVertex);
            var result2 = heuristic.HueristicDistanceToTarget((obj as AStarWrap<T, W>).Vertex, destinationVertex);

            return result1.CompareTo(result2);
        }
    }
}
