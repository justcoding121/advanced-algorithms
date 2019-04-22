using Advanced.Algorithms.DataStructures.Graph;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// A Kosaraju Strong Connected Component Algorithm Implementation.
    /// </summary>
    public class KosarajuStronglyConnected<T>
    {
        /// <summary>
        /// Returns all Connected Components using Kosaraju's Algorithm.
        /// </summary>
        public List<List<T>> 
            FindStronglyConnectedComponents(IDiGraph<T> graph)
        {
            var visited = new HashSet<T>();
            var finishStack = new Stack<T>();

            //step one - create DFS finish visit stack
            foreach (var vertex in graph)
            {
                if(!visited.Contains(vertex.Value))
                {
                    kosarajuStep1(vertex, visited, finishStack);
                }           
            }

            //reverse edges
            var reverseGraph = reverseEdges(graph);

            visited.Clear();

            var result = new List<List<T>>();

            //now pop finish stack and gather the components
            while (finishStack.Count > 0)
            {
                var currentVertex = reverseGraph.GetVertex(finishStack.Pop());

                if (!visited.Contains(currentVertex.Value))
                {
                    result.Add(kosarajuStep2(currentVertex, visited,
                        finishStack, new List<T>()));
                }
            }

            return result;
        }

        /// <summary>
        /// Just do a DFS keeping track on finish Stack of Vertices.
        /// </summary>
        private void kosarajuStep1(IDiGraphVertex<T> currentVertex,
            HashSet<T> visited,
            Stack<T> finishStack)
        {
            visited.Add(currentVertex.Value);

            foreach(var edge in currentVertex.OutEdges)
            {
                if(!visited.Contains(edge.Value))
                {
                    kosarajuStep1(edge.Target, visited, finishStack);
                }
            }

            //finished visiting, so add to stack
            finishStack.Push(currentVertex.Value);
        }

        /// <summary>
        /// In step two we just add all reachable nodes to result (connected componant).
        /// </summary>
        private List<T> kosarajuStep2(IDiGraphVertex<T> currentVertex,
            HashSet<T> visited, Stack<T> finishStack,
            List<T> result)
        {
            visited.Add(currentVertex.Value);
            result.Add(currentVertex.Value);

            foreach (var edge in currentVertex.OutEdges)
            {
                if (!visited.Contains(edge.Value))
                {
                    kosarajuStep2(edge.Target, visited, finishStack, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Create a clone graph with reverse edge directions.
        /// </summary>
        private IDiGraph<T> reverseEdges(IDiGraph<T> graph)
        {
            var newGraph = new DiGraph<T>();

            foreach (var vertex in graph)
            {
                newGraph.AddVertex(vertex.Value);
            }

            foreach (var vertex in graph)
            {
                foreach (var edge in vertex.OutEdges)
                {
                    //reverse edge
                    newGraph.AddEdge(edge.Value, vertex.Value);
                }
            }

            return newGraph;
        }
    }
}
