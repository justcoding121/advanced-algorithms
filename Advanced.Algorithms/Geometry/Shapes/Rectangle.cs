using System;

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
            var polygon = new Polygon();

            //add all four edge lines of this rectangle
            polygon.Edges.Add(new Line(LeftTopCorner, new Point(RightBottomCorner.X, LeftTopCorner.Y)));
            polygon.Edges.Add(new Line(new Point(RightBottomCorner.X, LeftTopCorner.Y), RightBottomCorner));
            polygon.Edges.Add(new Line(RightBottomCorner, new Point(LeftTopCorner.X, RightBottomCorner.Y)));
            polygon.Edges.Add(new Line(new Point(LeftTopCorner.X, RightBottomCorner.Y), LeftTopCorner));

            return polygon;
        }
    }
}
