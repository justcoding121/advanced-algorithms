using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Sorting;

namespace Advanced.Algorithms.Search;

/// <summary>
///     A quick select for Kth smallest algorithm implementation.
/// </summary>
public class QuickSelect<T> where T : IComparable
{
    public static T FindSmallest(IEnumerable<T> input, int k)
    {
        var inputArray = input.ToArray();

        var left = 0;
        var right = inputArray.Length - 1;

        var rnd = new Random();

        while (left <= right)
        {
            var median = MedianOfMedian(inputArray, left, right);

            var pivot = 0;

            for (var i = left; i <= right; i++)
                if (inputArray[i].Equals(median))
                {
                    pivot = i;
                    break;
                }

            var newPivot = Partition(inputArray, left, right, pivot);

            if (newPivot == k - 1)
                return inputArray[newPivot];
            if (newPivot > k - 1)
                right = newPivot - 1;
            else
                left = newPivot + 1;
        }

        return default;
    }

    private static T MedianOfMedian(T[] input, int left, int right)
    {
        if (left.CompareTo(right) == 0) return input[left];

        var comparer = new CustomComparer<T>(SortDirection.Ascending, Comparer<T>.Default);

        var size = 5;
        var currentLeft = left;

        var medians = new T[(right - left) / size + 1];
        var medianIndex = -1;
        while (currentLeft <= right)
        {
            var currentRight = currentLeft + size - 1;

            if (currentRight <= right)
            {
                Sort(input, currentLeft, currentRight, comparer);
                medians[++medianIndex] = Median(input, currentLeft, currentRight);
            }
            else
            {
                Sort(input, currentLeft, right, comparer);
                medians[++medianIndex] = Median(input, currentLeft, right);
            }

            currentLeft = currentRight + 1;
        }

        if (medians.Length == 1) return medians[0];

        return MedianOfMedian(medians, 0, medians.Length - 1);
    }

    //partition using pivot
    private static int Partition(T[] input, int left, int right, int pivot)
    {
        var pivotValue = input[pivot];
        var newPivot = left;

        //prevent comparing pivot against itself
        Swap(input, pivot, right);

        for (var i = left; i < right; i++)
            if (input[i].CompareTo(pivotValue) < 0)
            {
                Swap(input, i, newPivot);
                newPivot++;
            }

        //move pivot back to middle
        Swap(input, newPivot, right);

        return newPivot;
    }

    private static void Sort(T[] input, int left, int right, CustomComparer<T> comparer)
    {
        MergeSort<T>.PartitionMerge(input, left, right, comparer);
    }

    private static T Median(T[] input, int left, int right)
    {
        return input[left + (right - left) / 2];
    }

    private static void Swap(T[] input, int i, int j)
    {
        if (i != j)
        {
            var tmp = input[i];
            input[i] = input[j];
            input[j] = tmp;
        }
    }
}