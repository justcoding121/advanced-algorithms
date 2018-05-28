namespace Advanced.Algorithms.Geometry
{

    public class PointInsidePolygon
    {
        /// <summary>
        ///     Checks whether the given point is inside given polygon.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="point">The point to test.</param>
        /// <returns></returns>
        public static bool IsInside(Polygon polygon, Point point)
        {
            //a imaginary ray line from point to right infinity
            var rayLine = new Line(point, new Point(double.MaxValue, point.Y));

            var intersectionCount = 0;
            for (int i = 0; i < polygon.Edges.Count - 1; i++)
            {
                var edgeLine = polygon.Edges[i];

                if (LineIntersection.FindIntersection(rayLine, edgeLine) != null)
                {
                    intersectionCount++;
                }
            }

            //should have odd intersections if point is inside the polygon
            return intersectionCount % 2 != 0;
        }
    }
}
