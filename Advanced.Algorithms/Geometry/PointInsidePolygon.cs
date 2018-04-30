using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{

    public class PointInsidePolygon
    {
        public static bool IsInside(List<int[]> polygonEdgePoints, int[] testPoint)
        {
            //a imaginary ray line from point to right infinity
            var rayLine = new Line()
            {
                x1 = testPoint[0],
                y1 = testPoint[1],
                x2 = double.MaxValue,
                y2 = testPoint[1]
            };

            var intersectionCount = 0;
            for (int i = 0; i < polygonEdgePoints.Count - 1; i++)
            {
                var edgeLine = new Line()
                {
                    x1 = polygonEdgePoints[i][0],
                    y1 = polygonEdgePoints[i][1],
                    x2 = polygonEdgePoints[i + 1][0],
                    y2 = polygonEdgePoints[i + 1][1]
                };

                if (!LineIntersection.FindIntersection(rayLine, edgeLine).Equals(default(Point)))
                {
                    intersectionCount++;
                }
            }

            //should have odd intersections if point is inside the polygon
            return intersectionCount % 2 != 0;
        }
    }
}
