using System;

namespace Advanced.Algorithms.Geometry
{
    //Only A & D is required to represent a Rectangle
    public class Rectangle
    {
        public Point LeftTopCorner { get; set; }
        public Point RightBottomCorner { get; set; }

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
    }
}
