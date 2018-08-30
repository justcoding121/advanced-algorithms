using System;

namespace Advanced.Algorithms.Geometry
{
    internal static class DoubleExtensions
    {
        internal static bool IsLessThan(this double a, double b, double tolerance)
        {
            return (a - b) < -tolerance;
        }

        internal static bool IsLessThanOrEqual(this double a, double b, double tolerance)
        {
            var result = a - b;

            return result < -tolerance || Math.Abs(result) < tolerance;
        }

        internal static bool IsGreaterThan(this double a, double b, double tolerance)
        {
            return (a - b) > tolerance;
        }

        internal static bool IsGreaterThanOrEqual(this double a, double b, double tolerance)
        {
            var result = a - b;
            return result > tolerance || Math.Abs(result) < tolerance;
        }
    }
}
