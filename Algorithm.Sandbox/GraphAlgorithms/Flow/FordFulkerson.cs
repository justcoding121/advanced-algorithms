using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System;

namespace Algorithm.Sandbox.GraphAlgorithms.Flow
{
    public interface IFordFulkersonOperators<W> where W : IComparable
    {
        /// <summary>
        /// default value for this type W
        /// </summary>
        /// <returns></returns>
        W defaultWeight();

        /// <summary>
        /// returns the max for this type W
        /// </summary>
        /// <returns></returns>
        W MaxWeight();

        W Add(W a, W b);
        W Substract(W a, W b);
    }
    public class FordFulkersonMaxFlow<T, W> where W : IComparable
    {
        IFordFulkersonOperators<W> operators;
        public FordFulkersonMaxFlow(IFordFulkersonOperators<W> operators)
        {
            this.operators = operators;
        }

        public W ComputeMaxFlow(AsWeightedDiGraph<T, W> graph,
            T source, T sink)
        {
            var residualGraph = createResidualGraph(graph);

            AsArrayList<T> path = DFS(residualGraph, source, sink);

            var result = operators.defaultWeight();

            while (path != null)
            {
                result = operators.Add(result, AugmentResidualGraph(graph, residualGraph, path));
                path = DFS(residualGraph, source, sink);
            }

            return result;
        }

        private W AugmentResidualGraph(AsWeightedDiGraph<T, W> graph,
            AsWeightedDiGraph<T, W> residualGraph, AsArrayList<T> path)
        {
            var min = operators.MaxWeight();

            for (int i = 0; i < path.Length - 1; i++)
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
            for (int i = 0; i < path.Length - 1; i++)
            {
                var vertex_1 = residualGraph.FindVertex(path[i]);
                var vertex_2 = residualGraph.FindVertex(path[i + 1]);

                vertex_1.OutEdges[vertex_2] = operators.Add(vertex_1.OutEdges[vertex_2], min);
                vertex_2.OutEdges[vertex_1] = operators.Substract(vertex_2.OutEdges[vertex_1], min);

            }

            return min;
        }

        private AsArrayList<T> DFS(AsWeightedDiGraph<T, W> residualGraph, T source, T sink)
        {
            //init parent lookup table to trace path
            var parentLookUp = new AsDictionary<AsWeightedDiGraphVertex<T, W>, AsWeightedDiGraphVertex<T, W>>();
            foreach (var vertex in residualGraph.Vertices)
            {
                parentLookUp.Add(vertex.Value, null);
            }

            //regular DFS stuff
            var stack = new AsStack<AsWeightedDiGraphVertex<T, W>>();
            var visited = new AsHashSet<AsWeightedDiGraphVertex<T, W>>();
            stack.Push(residualGraph.Vertices[source]);

            AsWeightedDiGraphVertex<T, W> currentVertex = null;

            while (stack.Count() > 0)
            {
                currentVertex = stack.Pop();
                visited.Add(currentVertex);

                if (!currentVertex.Value.Equals(sink))
                {
                    foreach (var edge in currentVertex.OutEdges)
                    {

                        //visit only if edge have available flow
                        if (!visited.Contains(edge.Key)
                            && edge.Value.CompareTo(operators.defaultWeight()) > 0)
                        {
                            stack.Push(edge.Key);
                            parentLookUp[edge.Key] = currentVertex;
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
            var path = new AsStack<T>();

            while (currentVertex != null)
            {
                path.Push(parentLookUp[currentVertex].Value);
                currentVertex = parentLookUp[currentVertex];
            }

            //now reverse the stack to get the path from source to sink
            var result = new AsArrayList<T>();

            while (path.Count() > 0)
            {
                result.Add(path.Pop());
            }

            return result;
        }

        /// <summary>
        /// clones this graph and creates a residual graph
        /// </summary>
        /// <param name="residualGraph"></param>
        /// <returns></returns>
        private AsWeightedDiGraph<T, W> createResidualGraph(AsWeightedDiGraph<T, W> residualGraph)
        {
            var newGraph = new AsWeightedDiGraph<T, W>();

            //clone graph vertices
            foreach (var vertex in residualGraph.Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            //clone edges
            foreach (var vertex in residualGraph.Vertices)
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
