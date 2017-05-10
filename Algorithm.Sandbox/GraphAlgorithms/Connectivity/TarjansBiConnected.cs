using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.ArticulationPoint;

namespace Algorithm.Sandbox.GraphAlgorithms.Connectivity
{
    /// <summary>
    /// Finds if a graph is BiConnected
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TarjansBiConnected<T>
    {
        /// <summary>
        /// This is using ariticulation alogrithm based on the observation that
        /// a graph is BiConnected if and only if there is no articulation Points
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public bool IsBiConnected(AsGraph<T> graph)
        {
            var articulationAlgo = new TarjansArticulationFinder<T>();
            return articulationAlgo.FindArticulationPoints(graph).Count == 0;
        }
    }
}
