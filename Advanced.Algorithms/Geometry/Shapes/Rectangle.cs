using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Geometry
{
    //Only A & D is required to represent a Rectangle
    public class Rectangle
    {
        public Point LeftTopCorner { get; set; }
        public Point RightBottomCorner { get; set; }

        public Rectangle() { }

        public Rectangle(Point leftTopCorner, Point rightBottomCorner)
        {
            LeftTopCorner = leftTopCorner;
            RightBottomCorner = rightBottomCorner;
        }

        internal double Area()
        {
            return Length * Breadth;
        }

        internal double Length => Math.Abs(RightBottomCorner.X - LeftTopCorner.X);
        internal double Breadth => Math.Abs(LeftTopCorner.Y - RightBottomCorner.Y);

        public override bool Equals(object obj)
        {
            var tgt = obj as Rectangle;

            return tgt.LeftTopCorner.X == LeftTopCorner.X
                && tgt.LeftTopCorner.Y == LeftTopCorner.Y
                && tgt.RightBottomCorner.X == RightBottomCorner.X
                && tgt.RightBottomCorner.Y == RightBottomCorner.Y;
        }

        public Polygon ToPolygon()
        {

            var edges = new List<Line>();

            //add all four edge lines of this rectangle
            edges.Add(new Line(LeftTopCorner, new Point(RightBottomCorner.X, LeftTopCorner.Y)));
            edges.Add(new Line(new Point(RightBottomCorner.X, LeftTopCorner.Y), RightBottomCorner));
            edges.Add(new Line(RightBottomCorner, new Point(LeftTopCorner.X, RightBottomCorner.Y)));
            edges.Add(new Line(new Point(LeftTopCorner.X, RightBottomCorner.Y), LeftTopCorner));

            return new Polygon(edges);
        }

    }
}
