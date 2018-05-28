using System;

namespace Advanced.Algorithms.Geometry
{

    public class RectangleIntersection
    {
        /// <summary>
        /// Returns the rectangle formed by the intersection if do intersect
        /// Otherwise default value of Rectangle struct
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rectangle FindIntersection(Rectangle a, Rectangle b)
        {
            //check for intersection
            if (a.LeftTopCorner.X > b.RightBottomCorner.X // A is right of B   
             || a.RightBottomCorner.X < b.LeftTopCorner.X // A is left of B
             || a.RightBottomCorner.Y > b.LeftTopCorner.Y //A is above B
             || a.LeftTopCorner.Y < b.RightBottomCorner.Y)//A is below B
            {
                //no intersection
                return default(Rectangle);
            }

            var leftTopCorner = new Point
            (
                 Math.Max(a.LeftTopCorner.X, b.LeftTopCorner.X),
                 Math.Min(a.LeftTopCorner.Y, b.LeftTopCorner.Y)
            );


            var rightBottomCorner = new Point
            (
                Math.Min(a.RightBottomCorner.X, b.RightBottomCorner.X),
                Math.Max(a.RightBottomCorner.Y, b.RightBottomCorner.Y)
            );


            return new Rectangle()
            {
                LeftTopCorner = leftTopCorner,
                RightBottomCorner = rightBottomCorner
            };
        }
    }
}
