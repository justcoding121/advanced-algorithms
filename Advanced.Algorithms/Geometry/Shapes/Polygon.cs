using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    public class Polygon
    {
        public Polygon()
        {
            Edges = new List<Line>();
        }

        /// <summary>
        ///     Create polygon from the given list of consecutive boundary end points.
        ///     Last and first points will be connected.
        /// </summary>
        /// <param name="edges"></param>
        public Polygon(List<Point> edges) : this()
        {
            for (int i = 0; i < edges.Count; i++)
            {
                Edges.Add(new Line(edges[i], edges[(i + 1) % edges.Count]));
            }

        }

        public List<Line> Edges { get; set; }
    }
}
