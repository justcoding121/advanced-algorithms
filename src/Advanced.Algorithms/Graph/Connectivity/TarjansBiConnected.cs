using Advanced.Algorithms.DataStructures.Graph;

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
        public bool IsBiConnected(IGraph<T> graph)
        {
            var algorithm = new TarjansArticulationFinder<T>();
            return algorithm.FindArticulationPoints(graph).Count == 0;
        }
    }
}
