using System;
using System.Linq;
using System.Text;

namespace Advanced.Algorithms.String;

/// <summary>
///     A Manacher's longest palindrome implementation.
/// </summary>
public static class ManachersPalindrome
{
    public static int FindLongestPalindrome(string input)
    {
        if (input.Length <= 1) throw new ArgumentException("Invalid input");

        if (input.Contains("$")) throw new ArgumentException("Input contain sentinel character $.");

        //for even length palindrome
        //we need to do this hack with $
        var array = input.ToCharArray();
        var modifiedInput = new StringBuilder();

        foreach (var item in array)
        {
            modifiedInput.Append("$");
            modifiedInput.Append(item.ToString());
        }

        modifiedInput.Append("$");

        var result = FindLongestPalindromeR(modifiedInput.ToString());

        //remove length of $ sentinel
        return result / 2;
    }

    /// <summary>
    ///     Find the longest palindrome in linear time.
    /// </summary>
    private static int FindLongestPalindromeR(string input)
    {
        var n = input.Length;
        var palindromeLengths = new int[n];
        var center = 0;
        var right = 0;

        for (var i = 0; i < n; i++)
        {
            var mirror = 2 * center - i;

            if (i < right) palindromeLengths[i] = Math.Min(right - i, palindromeLengths[mirror]);

            //expand around center i
            while (i - palindromeLengths[i] - 1 >= 0
                   && i + palindromeLengths[i] + 1 < n
                   && input[i - palindromeLengths[i] - 1] == input[i + palindromeLengths[i] + 1])
                palindromeLengths[i]++;

            //update current palindrome window
            if (i + palindromeLengths[i] > right)
            {
                center = i;
                right = i + palindromeLengths[i];
            }
        }

        //palindromeLengths stores radius; full length in modified string is 2*radius+1
        return FindMax(palindromeLengths.Select(radius => 2 * radius + 1).ToArray());
    }

    /// <summary>
    ///     Returns the max index in given int[] array.
    /// </summary>
    private static int FindMax(int[] palindromeLengths)
    {
        return palindromeLengths.Concat(new[] { int.MinValue }).Max();
    }
}
