using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
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

            return x.LeftTopCorner.X == y.LeftTopCorner.X
                && x.LeftTopCorner.Y == y.LeftTopCorner.Y
                && x.RightBottomCorner.X == y.RightBottomCorner.X
                && x.RightBottomCorner.Y == y.RightBottomCorner.Y;

        }

        public int GetHashCode(Rectangle rectangle)
        {
            var hashCode = 35;
            hashCode = hashCode * -26 + rectangle.LeftTopCorner.GetHashCode();
            hashCode = hashCode * -26 + rectangle.RightBottomCorner.GetHashCode();
            return hashCode;
        }
    }
}
