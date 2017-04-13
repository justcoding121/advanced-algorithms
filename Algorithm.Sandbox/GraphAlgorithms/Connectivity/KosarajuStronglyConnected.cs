using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;

namespace Algorithm.Sandbox.GraphAlgorithms.Connectivity
{
    /// <summary>
    /// A Kosaraju Strong Connected Component Algorithm Implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KosarajuStronglyConnected<T>
    {
        /// <summary>
        /// Returns all Connected Components using Kosaraju's Algorithm
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public AsArrayList<AsArrayList<T>> 
            FindStronglyConnectedComponents(AsDiGraph<T> graph)
        {
            var visited = new AsHashSet<T>();
            var finishStack = new AsStack<T>();

            //step one - create DFS finish visit stack
            foreach (var vertex in graph.Vertices)
            {
                if(!visited.Contains(vertex.Value.Value))
                {
                    KosarajuStep1(vertex.Value, visited, finishStack);
                }
                
            }

            //reverse edges
            var reverseGraph = ReverseEdges(graph);

            visited.Clear();

            var result = new AsArrayList<AsArrayList<T>>();

            //now pop finish stack and gather the components
            while (finishStack.Count > 0)
            {
                var currentVertex = reverseGraph.FindVertex(finishStack.Pop());

                if (!visited.Contains(currentVertex.Value))
                {
                    result.Add(KosarajuStep2(currentVertex, visited,
                        finishStack, new AsArrayList<T>()));
                }
            }

            return result;
        }

        /// <summary>
        /// Just do a DFS keeping track on finish Stack of Vertices
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="visited"></param>
        /// <param name="finishStack"></param>
        private void KosarajuStep1(AsDiGraphVertex<T> currentVertex,
            AsHashSet<T> visited,
            AsStack<T> finishStack)
        {
            visited.Add(currentVertex.Value);

            foreach(var edge in currentVertex.OutEdges)
            {
                if(!visited.Contains(edge.Value.Value))
                {
                    KosarajuStep1(edge.Value, visited, finishStack);
                }
            }

            //finished visiting, so add to stack
            finishStack.Push(currentVertex.Value);
        }

        /// <summary>
        /// In step two we just add all reachable nodes to result (connected componant)
        /// </summary>
        /// <param name="currentVertex"></param>
        /// <param name="visited"></param>
        /// <param name="finishStack"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private AsArrayList<T> KosarajuStep2(AsDiGraphVertex<T> currentVertex,
            AsHashSet<T> visited, AsStack<T> finishStack,
            AsArrayList<T> result)
        {
            visited.Add(currentVertex.Value);
            result.Add(currentVertex.Value);

            foreach (var edge in currentVertex.OutEdges)
            {
                if (!visited.Contains(edge.Value.Value))
                {
                    KosarajuStep2(edge.Value, visited, finishStack, result);
                }
            }

            return result;
        }

        /// <summary>
        /// create a clone graph with reverse edge directions
        /// </summary>
        /// <param name="workGraph"></param>
        /// <returns></returns>
        private AsDiGraph<T> ReverseEdges(AsDiGraph<T> graph)
        {
            var newGraph = new AsDiGraph<T>();

            foreach (var vertex in graph.Vertices)
            {
                newGraph.AddVertex(vertex.Key);
            }

            foreach (var vertex in graph.Vertices)
            {
                foreach (var edge in vertex.Value.OutEdges)
                {
                    //reverse edge
                    newGraph.AddEdge(edge.Value.Value, vertex.Value.Value);
                }
            }

            return newGraph;
        }


    }
}
