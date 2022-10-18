using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     A dijikstra algorithm implementation using Fibonacci Heap.
/// </summary>
public class DijikstraShortestPath<T, TW> where TW : IComparable
{
    private readonly IShortestPathOperators<TW> @operator;

    public DijikstraShortestPath(IShortestPathOperators<TW> @operator)
    {
        this.@operator = @operator;
    }

    /// <summary>
    ///     Get shortest distance to target.
    /// </summary>
    public ShortestPathResult<T, TW> FindShortestPath(IGraph<T> graph, T source, T destination)
    {
        //regular argument checks
        if (graph?.GetVertex(source) == null || graph.GetVertex(destination) == null) throw new ArgumentException();

        if (@operator == null)
            throw new ArgumentException("Provide an operator implementation for generic type W during initialization.");

        if (!graph.IsWeightedGraph)
            if (@operator.DefaultValue.GetType() != typeof(int))
                throw new ArgumentException("Edges of unweighted graphs are assigned an imaginary weight of one (1)." +
                                            "Provide an appropriate IShortestPathOperators<int> operator implementation during initialization.");

        //track progress for distance to each Vertex from source
        var progress = new Dictionary<T, TW>();

        //trace our current path by mapping current vertex to its Parent
        var parentMap = new Dictionary<T, T>();

        //min heap to pick next closest vertex 
        var minHeap = new FibonacciHeap<MinHeapWrap<T, TW>>();
        //keep references of heap Node for decrement key operation
        var heapMapping = new Dictionary<T, MinHeapWrap<T, TW>>();

        //add vertices to min heap and progress map
        foreach (var vertex in graph.VerticesAsEnumberable)
        {
            //init parent
            parentMap.Add(vertex.Key, default);

            //init to max value
            progress.Add(vertex.Key, @operator.MaxValue);

            if (vertex.Key.Equals(source)) continue;
        }

        //start from source vertex as current 
        var current = new MinHeapWrap<T, TW>
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
                return new ShortestPathResult<T, TW>(null, @operator.MaxValue);

            //visit neighbours of current
            foreach (var neighbour in graph.GetVertex(current.Vertex).Edges
                         .Where(x => !x.TargetVertexKey.Equals(source)))
            {
                //new distance to neighbour
                var newDistance = @operator.Sum(current.Distance,
                    graph.GetVertex(current.Vertex).GetEdge(neighbour.TargetVertex).Weight<TW>());

                //current distance to neighbour
                var existingDistance = progress[neighbour.TargetVertexKey];

                //update distance if new is better
                if (newDistance.CompareTo(existingDistance) < 0)
                {
                    progress[neighbour.TargetVertexKey] = newDistance;

                    if (!heapMapping.ContainsKey(neighbour.TargetVertexKey))
                    {
                        var wrap = new MinHeapWrap<T, TW> { Distance = newDistance, Vertex = neighbour.TargetVertexKey };
                        minHeap.Insert(wrap);
                        heapMapping.Add(neighbour.TargetVertexKey, wrap);
                    }
                    else
                    {
                        //decrement distance to neighbour in heap
                        var decremented = new MinHeapWrap<T, TW>
                            { Distance = newDistance, Vertex = neighbour.TargetVertexKey };
                        minHeap.UpdateKey(heapMapping[neighbour.TargetVertexKey], decremented);
                        heapMapping[neighbour.TargetVertexKey] = decremented;
                    }

                    //trace parent
                    parentMap[neighbour.TargetVertexKey] = current.Vertex;
                }
            }
        }

        return TracePath(graph, parentMap, source, destination);
    }

    /// <summary>
    ///     Trace back path from destination to source using parent map.
    /// </summary>
    private ShortestPathResult<T, TW> TracePath(IGraph<T> graph, Dictionary<T, T> parentMap, T source, T destination)
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
        while (pathStack.Count > 0) resultPath.Add(pathStack.Pop());

        for (var i = 0; i < resultPath.Count - 1; i++)
            resultLength = @operator.Sum(resultLength,
                graph.GetVertex(resultPath[i]).GetEdge(graph.GetVertex(resultPath[i + 1])).Weight<TW>());

        return new ShortestPathResult<T, TW>(resultPath, resultLength);
    }
}

/// <summary>
///     Generic operators interface required by shorted path algorithms.
/// </summary>
public interface IShortestPathOperators<TW> where TW : IComparable
{
    TW DefaultValue { get; }
    TW MaxValue { get; }
    TW Sum(TW a, TW b);
}

/// <summary>
///     Shortest path result object.
/// </summary>
public class ShortestPathResult<T, TW> where TW : IComparable
{
    public ShortestPathResult(List<T> path, TW length)
    {
        Length = length;
        Path = path;
    }

    public TW Length { get; internal set; }
    public List<T> Path { get; }
}

/// <summary>
///     For fibornacci heap node.
/// </summary>
internal class MinHeapWrap<T, TW> : IComparable where TW : IComparable
{
    internal T Vertex { get; set; }
    internal TW Distance { get; set; }

    public int CompareTo(object obj)
    {
        return Distance.CompareTo((obj as MinHeapWrap<T, TW>).Distance);
    }
}