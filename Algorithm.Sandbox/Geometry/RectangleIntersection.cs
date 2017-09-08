using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Geometry
{
    //Only A & D is required to represent a Rectangle
    public struct Rectangle
    {
        public Point leftTopCorner { get; set; }
        public Point rightBottomCorner { get; set; }
    }
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-two-rectangles-overlap/
    /// </summary>
    public class RectangleIntersection
    {
        /// <summary>
        /// Returns the rectangle formed by the intersection if do intersect
        /// Otherwise default value of Rectangle struct
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Rectangle FindIntersection(Rectangle A, Rectangle B)
        {
            //check for intersection
            if (A.leftTopCorner.x > B.rightBottomCorner.x // A is right of B   
             || A.rightBottomCorner.x < B.leftTopCorner.x // A is left of B
             || A.rightBottomCorner.y > B.leftTopCorner.y //A is above B
             || A.leftTopCorner.y < B.rightBottomCorner.y)//A is below B
            {
                //no intersection
                return default(Rectangle);
            }

            var leftTopCorner = new Point();

            leftTopCorner.x = Math.Max(A.leftTopCorner.x, B.leftTopCorner.x);
            leftTopCorner.y = Math.Min(A.leftTopCorner.y, B.leftTopCorner.y);

            var rightBottomCorner = new Point();

            rightBottomCorner.x = Math.Min(A.rightBottomCorner.x, B.rightBottomCorner.x);
            rightBottomCorner.y = Math.Max(A.rightBottomCorner.y, B.rightBottomCorner.y);

            return new Rectangle()
            {
                leftTopCorner = leftTopCorner,
                rightBottomCorner = rightBottomCorner
            };
        }
    }
}
