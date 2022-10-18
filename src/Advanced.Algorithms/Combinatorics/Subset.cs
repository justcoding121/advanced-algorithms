using System.Collections.Generic;

namespace Advanced.Algorithms.Combinatorics;

/// <summary>
///     Subset generator.
/// </summary>
public class Subset
{
    public static List<List<T>> Find<T>(List<T> input)
    {
        var result = new List<List<T>>();

        Recurse(input, 0, new List<T>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<T>(List<T> input,
        int k, List<T> prefix, HashSet<int> prefixIndices,
        List<List<T>> result)
    {
        result.Add(new List<T>(prefix));

        for (var j = k; j < input.Count; j++)
        {
            if (prefixIndices.Contains(j)) continue;

            prefix.Add(input[j]);
            prefixIndices.Add(j);

            Recurse(input, j + 1, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }
}