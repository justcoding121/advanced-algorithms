using System;
using System.Collections.Generic;

namespace Advanced.Algorithms;

internal class CustomComparer<T> : IComparer<T> where T : IComparable
{
    private readonly IComparer<T> comparer;
    private readonly bool isMax;

    internal CustomComparer(SortDirection sortDirection, IComparer<T> comparer)
    {
        isMax = sortDirection == SortDirection.Descending;
        this.comparer = comparer;
    }

    public int Compare(T x, T y)
    {
        return !isMax ? compare(x, y) : compare(y, x);
    }

    private int compare(T x, T y)
    {
        return comparer.Compare(x, y);
    }
}