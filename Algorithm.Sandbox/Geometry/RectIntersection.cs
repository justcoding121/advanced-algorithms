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
        public Point leftCorner { get; set; }
        public Point rightCorner { get; set; }
    }
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-two-rectangles-overlap/
    /// </summary>
    public class RectIntersection
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
            if (A.leftCorner.x > B.rightCorner.x // A is right of B   
             || A.rightCorner.x < B.leftCorner.x // A is left of B
             || A.rightCorner.y > B.leftCorner.y //A is above B
             || A.leftCorner.y < B.rightCorner.y)//A is below B
            {
                //no intersection
                return default(Rectangle);
            }

            var leftCorner = new Point();

            leftCorner.x = Math.Max(A.leftCorner.x, B.leftCorner.x);
            leftCorner.y = Math.Min(A.leftCorner.y, B.leftCorner.y);

            var rightCorner = new Point();

            rightCorner.x = Math.Min(A.rightCorner.x, B.rightCorner.x);
            rightCorner.y = Math.Max(A.rightCorner.y, B.rightCorner.y);

            //swap if in reverse order
            if (leftCorner.x > rightCorner.x || leftCorner.y < rightCorner.y)
            {
                var tmp = leftCorner;
                leftCorner = rightCorner;
                rightCorner = tmp;
            }

            return new Rectangle()
            {
                leftCorner = leftCorner,
                rightCorner = rightCorner
            };
        }
    }
}
