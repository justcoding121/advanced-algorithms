using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Compares two points for geometric equality implementing IEqualityComparer.
    /// </summary>
    public class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point x, Point y)
        {
            if(x == null && y == null)
            {
                return true;
            }

            // Check for null values 
            if (x == null || y == null)
            {
                return false;
            }

            if (x == y)
            {
                return true;
            }

            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Point point)
        {
            var hashCode = 33;
            hashCode = hashCode * -21 + point.X.GetHashCode();
            hashCode = hashCode * -21 + point.Y.GetHashCode();
            return hashCode;
        }
    }
}
