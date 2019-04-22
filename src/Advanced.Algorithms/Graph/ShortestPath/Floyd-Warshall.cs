using Advanced.Algorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A floyd-warshall shortest path algorithm implementation.
    /// </summary>
    public class FloydWarshallShortestPath<T, W> where W : IComparable
    {
        readonly IShortestPathOperators<W> @operator;
        public FloydWarshallShortestPath(IShortestPathOperators<W> @operator)
        {
            this.@operator = @operator;
        }

        public List<AllPairShortestPathResult<T, W>> FindAllPairShortestPaths(IGraph<T> graph)
        {
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

            //we need this vertex array index for generics
            //since array indices are int and T is unknown type
            var vertexIndex = new Dictionary<int, T>();
            var reverseVertexIndex = new Dictionary<T, int>();
            var i = 0;
            foreach (var vertex in graph.VerticesAsEnumberable)
            {
                vertexIndex.Add(i, vertex.Key);
                reverseVertexIndex.Add(vertex.Key, i);
                i++;
            }

            //init all distance to default Weight
            var result = new W[graph.VerticesCount, graph.VerticesCount];
            //to trace the path
            var parent = new T[graph.VerticesCount, graph.VerticesCount];
            for (i = 0; i < graph.VerticesCount; i++)
            {
                for (int j = 0; j < graph.VerticesCount; j++)
                {
                    result[i, j] = @operator.MaxValue;
                }
            }

            for (i = 0; i < graph.VerticesCount; i++)
            {
                result[i, i] = @operator.DefaultValue;
            }
            //now set the known edge weights to neighbours
            for (i = 0; i < graph.VerticesCount; i++)
            {
                foreach (var edge in graph.GetVertex(vertexIndex[i]).Edges)
                {
                    result[i, reverseVertexIndex[edge.TargetVertexKey]] = edge.Weight<W>();
                    parent[i, reverseVertexIndex[edge.TargetVertexKey]] = graph.GetVertex(vertexIndex[i]).Key;

                    result[reverseVertexIndex[edge.TargetVertexKey], i] = edge.Weight<W>();
                    parent[reverseVertexIndex[edge.TargetVertexKey], i] = edge.TargetVertexKey;
                }
            }

            //here is the meat of this algorithm
            //if we can reach node i to j via node k and if it is shorter pick that Distance
            for (int k = 0; k < graph.VerticesCount; k++)
            {
                for (i = 0; i < graph.VerticesCount; i++)
                {
                    for (int j = 0; j < graph.VerticesCount; j++)
                    {
                        //no path
                        if (result[i, k].Equals(@operator.MaxValue) 
                            || result[k, j].Equals(@operator.MaxValue))
                        {
                            continue;
                        }

                        var sum = @operator.Sum(result[i, k], result[k, j]);

                        if (sum.CompareTo(result[i, j]) >= 0)
                        {
                            continue;
                        }

                        result[i, j] = sum;
                        parent[i, j] = parent[k, j];
                    }
                }
            }

            //trace path
            var finalResult = new List<AllPairShortestPathResult<T, W>>();
            for (i = 0; i < graph.VerticesCount; i++)
            {
                for (int j = 0; j < graph.VerticesCount; j++)
                {
                    var source = vertexIndex[i];
                    var dest = vertexIndex[j];
                    var distance = result[i, j];
                    var path = tracePath(result, parent, i, j, vertexIndex, reverseVertexIndex);

                    finalResult.Add(new AllPairShortestPathResult<T, W>(source, dest, distance, path));

                }
            }

            return finalResult;
        }

        /// <summary>
        ///Trace path from dest to source.
        /// </summary>
        private List<T> tracePath(W[,] result, T[,] parent, int i, int j,
            Dictionary<int, T> vertexIndex, Dictionary<T, int> reverseVertexIndex)
        {
            var pathStack = new Stack<T>();

            pathStack.Push(vertexIndex[j]);

            var current = parent[i, j];
            while (i != j)
            {
                pathStack.Push(parent[i, j]);
                j = reverseVertexIndex[parent[i, j]];
            }

            var path = new List<T>();

            while (pathStack.Count > 0)
            {
                path.Add(pathStack.Pop());
            }

            return path;
        }

    }

    /// <summary>
    /// All pairs shortest path algorithm result object.
    /// </summary>
    public class AllPairShortestPathResult<T, W> where W : IComparable
    {
        public T Source { get; }
        public T Destination { get; }

        public W Distance { get; }

        public List<T> Path { get; }

        public AllPairShortestPathResult(T source, T destination,
            W distance, List<T> path)
        {
            Source = source;
            Destination = destination;
            Distance = distance;
            Path = path;
        }

    }
}
