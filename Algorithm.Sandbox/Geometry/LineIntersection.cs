using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Geometry
{
    public struct Line
    {
        public double x1 { get; set; }
        public double y1 { get; set; }

        public double x2 { get; set; }
        public double y2 { get; set; }
    }

    public struct Point
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class LineIntersection
    {
        /// <summary>
        ///  Returns true if do intersect otherwise false
        /// </summary>
        /// <param name="lineA"></param>
        /// <param name="lineB"></param>
        /// <returns></returns>
        public static Point DoIntersect(Line lineA, Line lineB)
        {
            double x1 = lineA.x1, y1 = lineA.y1;
            double x2 = lineA.x2, y2 = lineA.y2;

            double x3 = lineB.x1, y3 = lineB.y1;
            double x4 = lineB.x2, y4 = lineB.y2;

            //general equation of line is y = mx + c where m is the slope
            //assume equation of line 1 as y1 = m1x1 + c1 ----(1)
            //=> -m1x1 + y1 = c1
            //assume equation of line 2 as y2 = m2x2 + c2-----(2)
            //=> -m2x2 + y2 = c2
            //if line 1 and 2 intersect then x1=x2=x & y1=y2=y where (x,y) is the intersection point
            //so we will get below two equations 
            //-m1x + y = c1 --------(3)
            //-m2x + y = c2 --------(4)
            //solving equations (3) & (4) => x = (c1-c2)/(m2-m1)
            //plugging x value in equation (1) => y = c2 + m2 * x

            //compute slope of line 1 (m1) and c2
            var m1 = (y2 - y1) / (x2 - x1);
            var c1 = -m1 * x1 + y1;

            //compute slope of line 2 (m2) and c2
            var m2 = (y4 - y3) / (x4 - x3);
            var c2 = -m2 * x3 + y3;

            //solve for x & y the intersection points
            var x = (c1 - c2) / (m2 - m1);
            var y = c2 + m2 * x;

            //verify by plugging intersection point (x, y)
            //in orginal equations (1) & (2) to see if they intersect
            //otherwise x,y values will not be finite and will fail this check
            if (-m1 * x + y == c1
                && -m2 * x + y == c2)
            {
                return new Point() { x = x, y = y };
            }

            //null
            return default(Point);

        }
    }
}
