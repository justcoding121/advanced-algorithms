using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    public class Polygon
    {
        public Polygon()
        {
            Edges = new List<Line>();
        }

        public List<Line> Edges { get; set; }
    }
}
