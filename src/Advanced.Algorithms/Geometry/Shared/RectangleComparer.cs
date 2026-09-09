using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Compares two rectangles for geometrical equality implementing IEqualityComparer.
/// </summary>
public class RectangleComparer : IEqualityComparer<Rectangle>
{
    private static readonly double Tolerance = Math.Round(Math.Pow(0.1, 5), 5);

    public bool Equals(Rectangle x, Rectangle y)
    {
        if (x == null && y == null) return true;

        // Check for null values 
        if (x == null || y == null) return false;

        return x.LeftTop.X.IsEqual(y.LeftTop.X, Tolerance)
               && x.LeftTop.Y.IsEqual(y.LeftTop.Y, Tolerance)
               && x.RightBottom.X.IsEqual(y.RightBottom.X, Tolerance)
               && x.RightBottom.Y.IsEqual(y.RightBottom.Y, Tolerance);
    }

    public int GetHashCode(Rectangle rectangle)
    {
        var hashCode = 35;
        hashCode = hashCode * -26 + rectangle.LeftTop.GetHashCode();
        hashCode = hashCode * -26 + rectangle.RightBottom.GetHashCode();
        return hashCode;
    }
}
