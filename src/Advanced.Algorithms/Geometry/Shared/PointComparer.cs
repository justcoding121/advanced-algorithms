using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Compares two points for geometric equality implementing IEqualityComparer.
/// </summary>
public class PointComparer : IEqualityComparer<Point>
{
    private static readonly double Tolerance = Math.Round(Math.Pow(0.1, 5), 5);

    public bool Equals(Point x, Point y)
    {
        if (x == null && y == null) return true;

        // Check for null values 
        if (x == null || y == null) return false;

        if (x == y) return true;

        return x.X.IsEqual(y.X, Tolerance) && x.Y.IsEqual(y.Y, Tolerance);
    }

    public int GetHashCode(Point point)
    {
        var hashCode = 33;
        hashCode = hashCode * -21 + point.X.GetHashCode();
        hashCode = hashCode * -21 + point.Y.GetHashCode();
        return hashCode;
    }
}
