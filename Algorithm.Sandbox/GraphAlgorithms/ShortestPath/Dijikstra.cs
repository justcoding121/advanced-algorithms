using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;

namespace Algorithm.Sandbox.GraphAlgorithms
{
    /// <summary>
    /// generic operators
    /// </summary>
    /// <typeparam name="W"></typeparam>
    public interface IShortestPathOperators<W> where W : IComparable
    {
        W DefaultValue { get; }
        W MaxValue { get; }
        W Sum(W a, W b);
    }

    /// <summary>
    /// For fibornacci heap node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    internal class MinHeapWrap<T, W> : IComparable where W : IComparable
    {
        internal T Target { get; set; }
        internal W Distance { get; set; }

        public int CompareTo(object obj)
        {
            return Distance.CompareTo((obj as MinHeapWrap<T, W>).Distance);
        }
    }

    /// <summary>
    /// For result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class ShortestPathResult<T, W> where W : IComparable
    {
        public ShortestPathResult(AsArrayList<T> path, W length)
        {
            Length = length;
            Path = path;
        }
        public W Length { get; internal set; }
        public AsArrayList<T> Path { get; private set; }
    }
    /// <summary>
    /// A dijikstra algorithm implementation using Fibornacci Heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DijikstraShortestPath<T, W> where W : IComparable
    {
        IShortestPathOperators<W> operators;
        public DijikstraShortestPath(IShortestPathOperators<W> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Get shortest distance to target
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public ShortestPathResult<T, W> GetShortestPath(AsWeightedDiGraph<T, W> graph, T source, T destination)
        {
            //regular argument checks
            if (graph == null || graph.FindVertex(source) == null
                || graph.FindVertex(destination) == null)
            {
                throw new ArgumentException();
            }

            //track progress for distance to each Vertex from source
            var progress = new AsDictionary<T, W>();

            //trace our current path by mapping current vertex to its Parent
            var parentMap = new AsDictionary<T, T>();

            //min heap to pick next closest vertex 
            var minHeap = new AsFibornacciMinHeap<MinHeapWrap<T, W>>();
            //keep references of heap Node for decrement key operation
            var heapMapping = new AsDictionary<T, AsFibornacciTreeNode<MinHeapWrap<T, W>>>();

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

                var heapNode = minHeap.Insert(wrap);
                heapMapping.Add(vertex.Key, heapNode);
            }

            //start from source vertex as current 
            var sourceVertex = graph.Vertices[source];
            var current = new MinHeapWrap<T, W>()
            {
                Distance = operators.DefaultValue,
                Target = source
            };

            //until heap is empty
            while (minHeap.Count > 0)
            {
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
                        heapMapping[neighbour.Key.Value].Value.Distance = newDistance;

                        //decrement distance to neighbour in heap
                        minHeap.DecrementKey(heapMapping[neighbour.Key.Value]);

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
        /// trace back path from destination to source using parent map
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="parentMap"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private ShortestPathResult<T, W> tracePath(AsWeightedDiGraph<T, W> graph,
            AsDictionary<T, T> parentMap, T source, T destination)
        {
            //trace the path
            var pathStack = new AsStack<T>();

            pathStack.Push(destination);

            var currentV = destination;
            while (!currentV.Equals(default(T)) && !parentMap[currentV].Equals(default(T)))
            {
                pathStack.Push(parentMap[currentV]);
                currentV = parentMap[currentV];
            }

            //return result
            var resultPath = new AsArrayList<T>();
            var resultLength = operators.DefaultValue;
            while (pathStack.Count > 0)
            {
                resultPath.Add(pathStack.Pop());
            }

            for (int i = 0; i < resultPath.Length - 1; i++)
            {
                resultLength = operators.Sum(resultLength,
                    graph.Vertices[resultPath[i]].OutEdges[graph.Vertices[resultPath[i + 1]]]);
            }

            return new ShortestPathResult<T, W>(resultPath, resultLength);
        }
    }
}
