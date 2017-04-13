using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;

namespace Algorithm.Sandbox.GraphAlgorithms.Coloring
{
    public class MColorResult<T, C>
    {
        public bool CanColor { get; }
        public AsDictionary<C, AsArrayList<T>> Partitions {get;}

        public MColorResult(bool canColor, AsDictionary<C, AsArrayList<T>> partitions)
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

            AsGraphVertex<T> first = graph.ReferenceVertex;

            var progress = CanColor(first, colors, 
                new AsDictionary<AsGraphVertex<T>, C>(),
                new AsHashSet<AsGraphVertex<T>>());

            if (progress.Count == graph.VerticesCount)
            {
                var result = new AsDictionary<C, AsArrayList<T>>();

                foreach(var vertex in progress)
                {
                    if(!result.ContainsKey(vertex.Value))
                    {
                        result.Add(vertex.Value, new AsArrayList<T>());
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
        private AsDictionary<AsGraphVertex<T>, C> CanColor(AsGraphVertex<T> vertex, C[] colors, 
             AsDictionary<AsGraphVertex<T>, C> progress, AsHashSet<AsGraphVertex<T>> visited)
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
                    if (visited.Contains(edge.Value))
                    {
                        continue;
                    }

                    CanColor(edge.Value, colors, progress, visited);
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
        private bool isSafe(AsDictionary<AsGraphVertex<T>, C> progress,
            AsGraphVertex<T> vertex, C color)
        {
           foreach(var edge in vertex.Edges)
            {
                if(progress.ContainsKey(edge.Value)
                    && progress[edge.Value].Equals(color))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
