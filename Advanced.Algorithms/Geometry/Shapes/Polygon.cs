using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Polygon object.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        ///     Create a polygon with given edges lines.
        /// </summary>
        public Polygon(List<Line> edges)
        {
            Edges = edges;
        }

        /// <summary>
        ///     Create polygon from the given list of consecutive boundary end points.
        ///     Last and first points will be connected.
        ///     If only one edge point is provided then this polygon will behave like a point,
        ///     a line is created with both ends having same edge point.
        /// </summary>
        public Polygon(List<Point> edgePoints)
        {
            Edges = new List<Line>();

            for (int i = 0; i < edgePoints.Count; i++)
            {
                Edges.Add(new Line(edgePoints[i], edgePoints[(i + 1) % edgePoints.Count]));
            }

        }

        public List<Line> Edges { get; set; }
    }
}
