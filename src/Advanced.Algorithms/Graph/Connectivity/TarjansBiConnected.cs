using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// Finds if a graph is BiConnected.
    /// </summary>
    public class TarjansBiConnected<T>
    {
        /// <summary>
        /// This is using ariticulation alogrithm based on the observation that
        /// a graph is BiConnected if and only if there is no articulation Points.
        /// </summary>
        public bool IsBiConnected(Graph<T> graph)
        {
            var articulationAlgo = new TarjansArticulationFinder<T>();
            return articulationAlgo.FindArticulationPoints(graph).Count == 0;
        }
    }
}
