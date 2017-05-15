using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Coloring
{
    public class MColorResult<T, C>
    {
        public bool CanColor { get; }
        public Dictionary<C, List<T>> Partitions {get;}

        public MColorResult(bool canColor, Dictionary<C, List<T>> partitions)
        {
            CanColor = canColor;
            Partitions = partitions;
        }

    }

    public class MColorer<T, C>
    {
        /// <summary>
        /// returns true if all vertices can be colored using the given colors 
        /// in such a way so that no neighbours have same color
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        public MColorResult<T, C> Color(AsGraph<T> graph, C[] colors)
        {

            GraphVertex<T> first = graph.ReferenceVertex;

            var progress = CanColor(first, colors, 
                new Dictionary<GraphVertex<T>, C>(),
                new HashSet<GraphVertex<T>>());

            if (progress.Count == graph.VerticesCount)
            {
                var result = new Dictionary<C, List<T>>();

                foreach(var vertex in progress)
                {
                    if(!result.ContainsKey(vertex.Value))
                    {
                        result.Add(vertex.Value, new List<T>());
                    }

                    result[vertex.Value].Add(vertex.Key.Value);
                }

                return new MColorResult<T, C>(true, result);
            }

            return new MColorResult<T, C>(false, null);
        }

        /// <summary>
        /// assign color to each new node
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="colors"></param>
        /// <param name="progress"></param>
        /// <param name="visited"></param>
        /// <returns></returns>
        private Dictionary<GraphVertex<T>, C> CanColor(GraphVertex<T> vertex, C[] colors, 
             Dictionary<GraphVertex<T>, C> progress, HashSet<GraphVertex<T>> visited)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (isSafe(progress, vertex, colors[i]))
                {
                    progress.Add(vertex, colors[i]);
                    break;
                }
            }

            if (visited.Contains(vertex) == false)
            {
                visited.Add(vertex);

                foreach (var edge in vertex.Edges)
                {
                    if (visited.Contains(edge))
                    {
                        continue;
                    }

                    CanColor(edge, colors, progress, visited);
                }
            }

            return progress;
        }

        /// <summary>
        /// Is it safe to assign this color to this vertex?
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="vertex"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool isSafe(Dictionary<GraphVertex<T>, C> progress,
            GraphVertex<T> vertex, C color)
        {
           foreach(var edge in vertex.Edges)
            {
                if(progress.ContainsKey(edge)
                    && progress[edge].Equals(color))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
