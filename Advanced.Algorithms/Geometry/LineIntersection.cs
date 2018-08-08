using System;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Line intersection computer.
    /// </summary>
    public class LineIntersection
    {
        /// <summary>
        ///  Returns Point of intersection if do intersect otherwise default Point (null).
        /// </summary>
        /// <param name="precision">precision tolerance.</param>
        /// <returns>The point of intersection.</returns>
        public static Point Find(Line lineA, Line lineB, int precision = 5)
        {
            var tolerance = Math.Round(Math.Pow(0.1, precision), precision);
            return FindIntersection(lineA, lineB, tolerance);
        }

        internal static Point FindIntersection(Line lineA, Line lineB, double tolerance)
        {
            if (lineA == lineB)
            {
                throw new Exception("Both lines are the same.");
            }

            //make lineA as left
            if (lineA.Left.X.CompareTo(lineB.Left.X) > 0)
            {
                var tmp = lineA;
                lineA = lineB;
                lineB = tmp;
            }
            else if (lineA.Left.X.CompareTo(lineB.Left.X) == 0)
            {
                if (lineA.Left.Y.CompareTo(lineB.Left.Y) > 0)
                {
                    var tmp = lineA;
                    lineA = lineB;
                    lineB = tmp;
                }
            }

            double x1 = lineA.Left.X, y1 = lineA.Left.Y;
            double x2 = lineA.Right.X, y2 = lineA.Right.Y;

            double x3 = lineB.Left.X, y3 = lineB.Left.Y;
            double x4 = lineB.Right.X, y4 = lineB.Right.Y;


            //equations of the form x=c (two vertical overlapping lines)
            if (x1 == x2 && x3 == x4 && x1 == x3)
            {
                //get the first intersection in vertical sorted order of lines
                var firstIntersection = new Point(x3, y3);

                //x,y can intersect outside the line segment since line is infinitely long
                //so finally check if x, y is within both the line segments
                if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                    IsInsideLine(lineB, firstIntersection, tolerance))
                {
                    return new Point(x3, y3);
                }
            }

            //equations of the form y=c (two overlapping horizontal lines)
            if (y1 == y2 && y3 == y4 && y1 == y3)
            {
                //get the first intersection in horizontal sorted order of lines
                var firstIntersection = new Point(x3, y3);

                //get the first intersection in sorted order
                //x,y can intersect outside the line segment since line is infinitely long
                //so finally check if x, y is within both the line segments
                if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                    IsInsideLine(lineB, firstIntersection, tolerance))
                {
                    return new Point(x3, y3);
                }
            }

            //equations of the form x=c (two vertical lines)
            if (x1 == x2 && x3 == x4)
            {
                return null;
            }

            //equations of the form y=c (two horizontal lines)
            if (y1 == y2 && y3 == y4)
            {
                return null;
            }

            //general equation of line is y = mx + c where m is the slope
            //assume equation of line 1 as y1 = m1x1 + c1 
            //=> -m1x1 + y1 = c1 ----(1)
            //assume equation of line 2 as y2 = m2x2 + c2
            //=> -m2x2 + y2 = c2 -----(2)
            //if line 1 and 2 intersect then x1=x2=x and y1=y2=y where (x,y) is the intersection point
            //so we will get below two equations 
            //-m1x + y = c1 --------(3)
            //-m2x + y = c2 --------(4)

            double x, y;

            //lineA is vertical x1 = x2
            //slope will be infinity
            //so lets derive another solution
            if (Math.Abs(x1 - x2) < tolerance)
            {
                //compute slope of line 2 (m2) and c2
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                //equation of vertical line is x = c
                //if line 1 and 2 intersect then x1=c1=x
                //subsitute x=x1 in (4) => -m2x1 + y = c2
                // => y = c2 + m2x1 
                x = x1;
                y = c2 + m2 * x1;
            }
            //lineB is vertical x3 = x4
            //slope will be infinity
            //so lets derive another solution
            else if (Math.Abs(x3 - x4) < tolerance)
            {
                //compute slope of line 1 (m1) and c2
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                //equation of vertical line is x = c
                //if line 1 and 2 intersect then x3=c3=x
                //subsitute x=x3 in (3) => -m1x3 + y = c1
                // => y = c1 + m1x3 
                x = x3;
                y = c1 + m1 * x3;
            }
            //lineA and lineB are not vertical 
            //(could be horizontal we can handle it with slope = 0)
            else
            {
                //compute slope of line 1 (m1) and c2
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                //compute slope of line 2 (m2) and c2
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                //solving equations (3) and (4) => x = (c1-c2)/(m2-m1)
                //plugging x value in equation (4) => y = c2 + m2 * x
                x = (c1 - c2) / (m2 - m1);
                y = c2 + m2 * x;

                //verify by plugging intersection point (x, y)
                //in orginal equations (1) and (2) to see if they intersect
                //otherwise x,y values will not be finite and will fail this check
                if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                    && Math.Abs(-m2 * x + y - c2) < tolerance))
                {
                    return null;
                }
            }

            var result = new Point(x, y);

            //x,y can intersect outside the line segment since line is infinitely long
            //so finally check if x, y is within both the line segments
            if (IsInsideLine(lineA, result, tolerance) &&
                IsInsideLine(lineB, result, tolerance))
            {
                return result;
            }

            //return default null (no intersection)
            return null;

        }

        /// <summary>
        /// Returns true if given point(x,y) is inside the given line segment.
        /// </summary>
        private static bool IsInsideLine(Line line, Point p, double tolerance)
        {
            double x = p.X, y = p.Y;

            var leftX = line.Left.X;
            var leftY = line.Left.Y;

            var rightX = line.Right.X;
            var rightY = line.Right.Y;

            return (x.IsGreaterThanOrEqual(leftX, tolerance) && x.IsLessThanOrEqual(rightX, tolerance)
                        || x.IsGreaterThanOrEqual(rightX, tolerance) && x.IsLessThanOrEqual(leftX, tolerance))
                   && (y.IsGreaterThanOrEqual(leftY, tolerance) && y.IsLessThanOrEqual(rightY, tolerance)
                        || y.IsGreaterThanOrEqual(rightY, tolerance) && y.IsLessThanOrEqual(leftY, tolerance));
        }

    }

    /// <summary>
    /// Line extensions.
    /// </summary>
    public static class LineExtensions
    {
        public static bool Intersects(this Line lineA, Line lineB, int precision = 5)
        {
            return LineIntersection.Find(lineA, lineB, precision) != null;
        }

        public static Point Intersection(this Line lineA, Line lineB, int precision = 5)
        {
            return LineIntersection.Find(lineA, lineB, precision);
        }

        internal static bool Intersects(this Line lineA, Line lineB, double tolerance)
        {
            return LineIntersection.FindIntersection(lineA, lineB, tolerance) != null;
        }

        internal static Point Intersection(this Line lineA, Line lineB, double tolerance)
        {
            return LineIntersection.FindIntersection(lineA, lineB, tolerance);
        }
    }
}
