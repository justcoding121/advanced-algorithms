namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    ///   Check whether a given point is inside given polygon.
    /// </summary>
    public class PointInsidePolygon
    {
        public static bool IsInside(Polygon polygon, Point point)
        {
            //a imaginary ray line from point to right infinity
            var rayLine = new Line(point, new Point(double.MaxValue, point.Y));

            var intersectionCount = 0;
            for (int i = 0; i < polygon.Edges.Count - 1; i++)
            {
                var edgeLine = polygon.Edges[i];

                if (LineIntersection.Find(rayLine, edgeLine) != null)
                {
                    intersectionCount++;
                }
            }

            //should have odd intersections if point is inside the polygon
            return intersectionCount % 2 != 0;
        }
    }
}
