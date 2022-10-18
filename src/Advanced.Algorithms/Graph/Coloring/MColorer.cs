using System.Collections.Generic;
using Advanced.Algorithms.DataStructures.Graph;

namespace Advanced.Algorithms.Graph;

/// <summary>
///     An m-coloring algorithm implementation.
/// </summary>
public class MColorer<T, TC>
{
    /// <summary>
    ///     Returns true if all vertices can be colored using the given colors
    ///     in such a way so that no neighbours have same color.
    /// </summary>
    public MColorResult<T, TC> Color(IGraph<T> graph, TC[] colors)
    {
        var progress = new Dictionary<IGraphVertex<T>, TC>();

        foreach (var vertex in graph.VerticesAsEnumberable)
            if (!progress.ContainsKey(vertex))
                ColorRecursively(vertex, colors,
                    progress,
                    new HashSet<IGraphVertex<T>>());

        if (progress.Count != graph.VerticesCount) return new MColorResult<T, TC>(false, null);

        var result = new Dictionary<TC, List<T>>();

        foreach (var vertex in progress)
        {
            if (!result.ContainsKey(vertex.Value)) result.Add(vertex.Value, new List<T>());

            result[vertex.Value].Add(vertex.Key.Key);
        }

        return new MColorResult<T, TC>(true, result);
    }

    /// <summary>
    ///     Assign color to each new node.
    /// </summary>
    private Dictionary<IGraphVertex<T>, TC> ColorRecursively(IGraphVertex<T> vertex, TC[] colors,
        Dictionary<IGraphVertex<T>, TC> progress, HashSet<IGraphVertex<T>> visited)
    {
        foreach (var item in colors)
        {
            if (!IsSafe(progress, vertex, item)) continue;

            progress.Add(vertex, item);
            break;
        }

        if (visited.Contains(vertex) == false)
        {
            visited.Add(vertex);

            foreach (var edge in vertex.Edges)
            {
                if (visited.Contains(edge.TargetVertex)) continue;

                ColorRecursively(edge.TargetVertex, colors, progress, visited);
            }
        }

        return progress;
    }

    /// <summary>
    ///     Is it safe to assign this color to this vertex?
    /// </summary>
    private bool IsSafe(Dictionary<IGraphVertex<T>, TC> progress,
        IGraphVertex<T> vertex, TC color)
    {
        foreach (var edge in vertex.Edges)
            if (progress.ContainsKey(edge.TargetVertex)
                && progress[edge.TargetVertex].Equals(color))
                return false;

        return true;
    }
}

/// <summary>
///     M-coloring result object.
/// </summary>
public class MColorResult<T, TC>
{
    public MColorResult(bool canColor, Dictionary<TC, List<T>> partitions)
    {
        CanColor = canColor;
        Partitions = partitions;
    }

    public bool CanColor { get; }
    public Dictionary<TC, List<T>> Partitions { get; }
}