using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Search;

/// <summary>
///     A boyer-moore majority finder algorithm implementation.
/// </summary>
public class BoyerMoore<T> where T : IComparable
{
    public static T FindMajority(IEnumerable<T> input)
    {
        var candidate = FindMajorityCandidate(input, input.Count());

        if (Verify(input, input.Count(), candidate)) return candidate;

        return default;
    }

    //Find majority candidate
    private static T FindMajorityCandidate(IEnumerable<T> input, int length)
    {
        var count = 1;
        var candidate = input.First();

        foreach (var element in input.Skip(1))
        {
            if (candidate.Equals(element))
                count++;
            else
                count--;

            if (count == 0)
            {
                candidate = element;
                count = 1;
            }
        }

        return candidate;
    }

    //verify that candidate is indeed the majority
    private static bool Verify(IEnumerable<T> input, int size, T candidate)
    {
        return input.Count(x => x.Equals(candidate)) > size / 2;
    }
}