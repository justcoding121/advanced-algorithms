using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Flow
{
    /// <summary>
    /// Operators to deal with generic Add, Substract etc on edge weights
    /// </summary>
    /// <typeparam name="W"></typeparam>
    public interface IFlowOperators<W> where W : IComparable
    {
        /// <summary>
        /// default value for this type W
        /// </summary>
        /// <returns></returns>
        W defaultWeight { get; }

        /// <summary>
        /// returns the max for this type W
        /// </summary>
        /// <returns></returns>
        W MaxWeight { get; }

        /// <summary>
        /// add two weights
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        W AddWeights(W a, W b);

        /// <summary>
        /// substract b from a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        W SubstractWeights(W a, W b);
    }

    /// <summary>
    /// A ford-fulkerson max flox implementation on weighted directed graph using 
    /// adjacency list representation of graph & residual graph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public class FordFulkersonMaxFlow<T, W> where W : IComparable
    {
        IFlowOperators<W> operators;
        public FordFulkersonMaxFlow(IFlowOperators<W> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Compute max flow by searching a path
        /// And then augmenting the residual graph until
        /// no more path exists in residual graph with possible flow
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="sink"></param>
        /// <returns></returns>
        public W ComputeMaxFlow(AsWeightedDiGraph<T, W> graph,
            T source, T sink)
        {
            var residualGraph = createResidualGraph(graph);

            List<T> path = DFS(residualGraph, source, sink);

            var result = operators.defaultWeight;

            while (path != null)
            {
                result = operators.AddWeights(result, AugmentResidualGraph(graph, residualGraph, path));
                path = DFS(residualGraph, source, sink);
            }

            return result;
        }


        /// <summary>
        /// Return all flow Paths
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="sink"></param>
        /// <returns></returns>
        public List<List<T>> ComputeMaxFlowAndReturnFlowPath(AsWeightedDiGraph<T, W> graph,
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
        /// Augment current Path to residual Graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="residualGraph"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private W AugmentResidualGraph(AsWeightedDiGraph<T, W> graph,
            AsWeightedDiGraph<T, W> residualGraph, List<T> path)
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
        /// depth first search to find a path to sink in residual graph from source
        /// </summary>
        /// <param name="residualGraph"></param>
        /// <param name="source"></param>
        /// <param name="sink"></param>
        /// <returns></returns>
        private List<T> DFS(AsWeightedDiGraph<T, W> residualGraph, T source, T sink)
        {
            //init parent lookup table to trace path
            var parentLookUp = new Dictionary<AsWeightedDiGraphVertex<T, W>, AsWeightedDiGraphVertex<T, W>>();
            foreach (var vertex in residualGraph.Vertices)
            {
                parentLookUp.Add(vertex.Value, null);
            }

            //regular DFS stuff
            var stack = new Stack<AsWeightedDiGraphVertex<T, W>>();
            var visited = new HashSet<AsWeightedDiGraphVertex<T, W>>();
            stack.Push(residualGraph.Vertices[source]);
            visited.Add(residualGraph.Vertices[source]);

            AsWeightedDiGraphVertex<T, W> currentVertex = null;

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
        /// clones this graph and creates a residual graph
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        private AsWeightedDiGraph<T, W> createResidualGraph(AsWeightedDiGraph<T, W> graph)
        {
            var newGraph = new AsWeightedDiGraph<T, W>();

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
}
