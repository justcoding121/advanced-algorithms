using System;

namespace Advanced.Algorithms.Geometry
{
    //Only A & D is required to represent a Rectangle
    public struct Rectangle
    {
        public Point LeftTopCorner { get; set; }
        public Point RightBottomCorner { get; set; }
    }

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
            if (a.LeftTopCorner.x > b.RightBottomCorner.x // A is right of B   
             || a.RightBottomCorner.x < b.LeftTopCorner.x // A is left of B
             || a.RightBottomCorner.y > b.LeftTopCorner.y //A is above B
             || a.LeftTopCorner.y < b.RightBottomCorner.y)//A is below B
            {
                //no intersection
                return default(Rectangle);
            }

            var leftTopCorner = new Point
            {
                x = Math.Max(a.LeftTopCorner.x, b.LeftTopCorner.x),
                y = Math.Min(a.LeftTopCorner.y, b.LeftTopCorner.y)
            };


            var rightBottomCorner = new Point
            {
                x = Math.Min(a.RightBottomCorner.x, b.RightBottomCorner.x),
                y = Math.Max(a.RightBottomCorner.y, b.RightBottomCorner.y)
            };


            return new Rectangle()
            {
                LeftTopCorner = leftTopCorner,
                RightBottomCorner = rightBottomCorner
            };
        }
    }
}
