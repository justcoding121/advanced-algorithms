using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A ford-fulkerson max flox implementation on weighted directed graph using 
    /// adjacency list representation of graph and residual graph.
    /// </summary>
    public class FordFulkersonMaxFlow<T, W> where W : IComparable
    {
        readonly IFlowOperators<W> operators;
        public FordFulkersonMaxFlow(IFlowOperators<W> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Compute max flow by searching a path
        /// and then augmenting the residual graph until
        /// no more path exists in residual graph with possible flow.
        /// </summary>

        public W ComputeMaxFlow(WeightedDiGraph<T, W> graph,
            T source, T sink)
        {
            var residualGraph = createResidualGraph(graph);

            var path = DFS(residualGraph, source, sink);

            var result = operators.defaultWeight;

            while (path != null)
            {
                result = operators.AddWeights(result, AugmentResidualGraph(graph, residualGraph, path));
                path = DFS(residualGraph, source, sink);
            }

            return result;
        }


        /// <summary>
        /// Return all flow Paths.
        /// </summary>
        public List<List<T>> ComputeMaxFlowAndReturnFlowPath(WeightedDiGraph<T, W> graph,
            T source, T sink)
        {
            var residualGraph = createResidualGraph(graph);

            List<T> path = DFS(residualGraph, source, sink);

            var flow = operators.defaultWeight;

            var result = new List<List<T>>();
            while (path != null)
            {
                result.Add(path);
                flow = operators.AddWeights(flow, AugmentResidualGraph(graph, residualGraph, path));
                path = DFS(residualGraph, source, sink);
            }

            return result;
        }

        /// <summary>
        /// Augment current Path to residual Graph.
        /// </summary>
        private W AugmentResidualGraph(WeightedDiGraph<T, W> graph,
            WeightedDiGraph<T, W> residualGraph, List<T> path)
        {
            var min = operators.MaxWeight;

            for (int i = 0; i < path.Count - 1; i++)
            {
                var vertex_1 = residualGraph.FindVertex(path[i]);
                var vertex_2 = residualGraph.FindVertex(path[i + 1]);

                var edgeValue = vertex_1.OutEdges[vertex_2];

                if (min.CompareTo(edgeValue) > 0)
                {
                    min = edgeValue;
                }

            }

            //augment path
            for (int i = 0; i < path.Count - 1; i++)
            {
                var vertex_1 = residualGraph.FindVertex(path[i]);
                var vertex_2 = residualGraph.FindVertex(path[i + 1]);

                //substract from forward paths
                vertex_1.OutEdges[vertex_2] = operators.SubstractWeights(vertex_1.OutEdges[vertex_2], min);

                //add for backward paths
                vertex_2.OutEdges[vertex_1] = operators.AddWeights(vertex_2.OutEdges[vertex_1], min);

            }

            return min;
        }

        /// <summary>
        /// Depth first search to find a path to sink in residual graph from source.
        /// </summary>
        private List<T> DFS(WeightedDiGraph<T, W> residualGraph, T source, T sink)
        {
            //init parent lookup table to trace path
            var parentLookUp = new Dictionary<WeightedDiGraphVertex<T, W>, WeightedDiGraphVertex<T, W>>();
            foreach (var vertex in residualGraph.Vertices)
            {
                parentLookUp.Add(vertex.Value, null);
            }

            //regular DFS stuff
            var stack = new Stack<WeightedDiGraphVertex<T, W>>();
            var visited = new HashSet<WeightedDiGraphVertex<T, W>>();
            stack.Push(residualGraph.Vertices[source]);
            visited.Add(residualGraph.Vertices[source]);

            WeightedDiGraphVertex<T, W> currentVertex = null;

            while (stack.Count > 0)
            {
                currentVertex = stack.Pop();

                //reached sink? then break otherwise dig in
                if (currentVertex.Value.Equals(sink))
                {
                    break;
                }
                else
                {
                    foreach (var edge in currentVertex.OutEdges)
                    {

                        //visit only if edge have available flow
                        if (!visited.Contains(edge.Key)
                            && edge.Value.CompareTo(operators.defaultWeight) > 0)
                        {
                            //keep track of this to trace out path once sink is found
                            parentLookUp[edge.Key] = currentVertex;
                            stack.Push(edge.Key);
                            visited.Add(edge.Key);
                        }
                    }
                }
            }

            //could'nt find a path
            if (currentVertex == null || !currentVertex.Value.Equals(sink))
            {
                return null;
            }

            //traverse back from sink to find path to source
            var path = new Stack<T>();

            path.Push(sink);

            while (currentVertex != null && !currentVertex.Value.Equals(source))
            {
                path.Push(parentLookUp[currentVertex].Value);
                currentVertex = parentLookUp[currentVertex];
            }

            //now reverse the stack to get the path from source to sink
            var result = new List<T>();

            while (path.Count > 0)
            {
                result.Add(path.Pop());
            }

            return result;
        }

        /// <summary>
        /// Clones this graph and creates a residual graph.
        /// </summary>
        private WeightedDiGraph<T, W> createResidualGraph(WeightedDiGraph<T, W> graph)
        {
            var newGraph = new WeightedDiGraph<T, W>();

            //clone graph vertices
            foreach (var vertex in graph.Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            //clone edges
            foreach (var vertex in graph.Vertices)
            {
                //Use either OutEdges or InEdges for cloning
                //here we use OutEdges
                foreach (var edge in vertex.Value.OutEdges)
                {
                    //original edge
                    newGraph.AddEdge(vertex.Key, edge.Key.Value, edge.Value);
                    //add a backward edge for residual graph with edge value as default(W)
                    newGraph.AddEdge(edge.Key.Value, vertex.Key, default(W));
                }
            }

            return newGraph;
        }
    }

    /// <summary>
    /// Operators to deal with generic Add, Substract etc on edge weights for flow algorithms such as ford-fulkerson algorithm.
    /// </summary>
    public interface IFlowOperators<W> where W : IComparable
    {
        /// <summary>
        /// default value for this type W.
        /// </summary>
        W defaultWeight { get; }

        /// <summary>
        /// returns the max for this type W.
        /// </summary>
        W MaxWeight { get; }

        /// <summary>
        /// add two weights.
        /// </summary>
        W AddWeights(W a, W b);

        /// <summary>
        /// substract b from a.
        /// </summary>
        W SubstractWeights(W a, W b);
    }
}
