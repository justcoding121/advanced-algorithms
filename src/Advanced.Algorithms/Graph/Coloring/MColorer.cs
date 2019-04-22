using Advanced.Algorithms.DataStructures.Graph;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{
    /// <summary>
    /// An m-coloring algorithm implementation.
    /// </summary>
    public class MColorer<T, C>
    {
        /// <summary>
        /// Returns true if all vertices can be colored using the given colors 
        /// in such a way so that no neighbours have same color.
        /// </summary>
        public MColorResult<T, C> Color(IGraph<T> graph, C[] colors)
        {

            var first = graph.ReferenceVertex;

            var progress = canColor(first, colors, 
                new Dictionary<IGraphVertex<T>, C>(),
                new HashSet<IGraphVertex<T>>());

            if (progress.Count != graph.VerticesCount)
            {
                return new MColorResult<T, C>(false, null);
            }

            var result = new Dictionary<C, List<T>>();

            foreach(var vertex in progress)
            {
                if(!result.ContainsKey(vertex.Value))
                {
                    result.Add(vertex.Value, new List<T>());
                }

                result[vertex.Value].Add(vertex.Key.Key);
            }

            return new MColorResult<T, C>(true, result);

        }

        /// <summary>
        /// Assign color to each new node.
        /// </summary>
        private Dictionary<IGraphVertex<T>, C> canColor(IGraphVertex<T> vertex, C[] colors, 
             Dictionary<IGraphVertex<T>, C> progress, HashSet<IGraphVertex<T>> visited)
        {
            foreach (var item in colors)
            {
                if (!isSafe(progress, vertex, item))
                {
                    continue;
                }

                progress.Add(vertex, item);
                break;
            }

            if (visited.Contains(vertex) == false)
            {
                visited.Add(vertex);

                foreach (var edge in vertex.Edges)
                {
                    if (visited.Contains(edge.TargetVertex))
                    {
                        continue;
                    }

                    canColor(edge.TargetVertex, colors, progress, visited);
                }
            }

            return progress;
        }

        /// <summary>
        /// Is it safe to assign this color to this vertex?
        /// </summary>
        private bool isSafe(Dictionary<IGraphVertex<T>, C> progress,
            IGraphVertex<T> vertex, C color)
        {
           foreach(var edge in vertex.Edges)
            {
                if(progress.ContainsKey(edge.TargetVertex)
                    && progress[edge.TargetVertex].Equals(color))
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// M-coloring result object.
    /// </summary>
    public class MColorResult<T, C>
    {
        public bool CanColor { get; }
        public Dictionary<C, List<T>> Partitions { get; }

        public MColorResult(bool canColor, Dictionary<C, List<T>> partitions)
        {
            CanColor = canColor;
            Partitions = partitions;
        }

    }
}
