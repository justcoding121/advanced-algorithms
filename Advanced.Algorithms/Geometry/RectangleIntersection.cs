using System;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Rectangle intersection finder.
    /// </summary>
    public class RectangleIntersection
    {
        /// <summary>
        /// Returns the rectangle formed by the intersection if do intersect.
        /// Otherwise default value of Rectangle struct.
        /// </summary>
        public static Rectangle FindIntersection(Rectangle a, Rectangle b)
        {
            //check for intersection
            if (!DoIntersect(a, b))
            {
                //no intersection
                return null;
            }

            var leftTopCorner = new Point
            (
                 Math.Max(a.LeftTop.X, b.LeftTop.X),
                 Math.Min(a.LeftTop.Y, b.LeftTop.Y)
            );


            var rightBottomCorner = new Point
            (
                Math.Min(a.RightBottom.X, b.RightBottom.X),
                Math.Max(a.RightBottom.Y, b.RightBottom.Y)
            );


            return new Rectangle()
            {
                LeftTop = leftTopCorner,
                RightBottom = rightBottomCorner
            };
        }

        public static bool DoIntersect(Rectangle a, Rectangle b)
        {
            //check for intersection
            if (a.LeftTop.X > b.RightBottom.X // A is right of B   
             || a.RightBottom.X < b.LeftTop.X // A is left of B
             || a.RightBottom.Y > b.LeftTop.Y //A is above B
             || a.LeftTop.Y < b.RightBottom.Y)//A is below B
            {
                //no intersection
                return false;
            }

            return true;
        }
    }
}
