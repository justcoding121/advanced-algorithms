using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Compares two rectangles for geometrical equality implementing IEqualityComparer.
    /// </summary>
    public class RectangleComparer : IEqualityComparer<Rectangle>
    {
        public bool Equals(Rectangle x, Rectangle y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            // Check for null values 
            if (x == null || y == null)
            {
                return false;
            }

            return x.LeftTop.X == y.LeftTop.X
                && x.LeftTop.Y == y.LeftTop.Y
                && x.RightBottom.X == y.RightBottom.X
                && x.RightBottom.Y == y.RightBottom.Y;

        }

        public int GetHashCode(Rectangle rectangle)
        {
            var hashCode = 35;
            hashCode = hashCode * -26 + rectangle.LeftTop.GetHashCode();
            hashCode = hashCode * -26 + rectangle.RightBottom.GetHashCode();
            return hashCode;
        }
    }
}
