using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Rectangle object.
    /// </summary>
    public class Rectangle
    {
        public Point LeftTop { get; set; }
        public Point RightBottom { get; set; }

        public Rectangle() { }

        public Rectangle(Point leftTop, Point rightBottom)
        {
            if (rightBottom.Y > leftTop.Y)
            {
                throw new Exception("Top corner should have higher Y value than bottom.");
            }

            if(leftTop.X > rightBottom.X)
            {
                throw new Exception("Right corner should have higher X value than left.");
            }

            LeftTop = leftTop;
            RightBottom = rightBottom;
        }

        internal double Area()
        {
            return Length * Breadth;
        }

        internal double Length => Math.Abs(RightBottom.X - LeftTop.X);
        internal double Breadth => Math.Abs(LeftTop.Y - RightBottom.Y);

        public Polygon ToPolygon()
        {

            var edges = new List<Line>();

            //add all four edge lines of this rectangle
            edges.Add(new Line(LeftTop, new Point(RightBottom.X, LeftTop.Y)));
            edges.Add(new Line(new Point(RightBottom.X, LeftTop.Y), RightBottom));
            edges.Add(new Line(RightBottom, new Point(LeftTop.X, RightBottom.Y)));
            edges.Add(new Line(new Point(LeftTop.X, RightBottom.Y), LeftTop));

            return new Polygon(edges);
        }

    }
}
