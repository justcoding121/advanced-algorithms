using System;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Rotates given point by given angle about given center.
    /// </summary>
    public class PointRotation
    {
        public static Point Rotate(Point center, Point point, int angle)
        {
            double angleInRadians = angle * (Math.PI / 180);

            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            var x = cosTheta * (point.X - center.X) -
                    sinTheta * (point.Y - center.Y) + center.X;

            var y = sinTheta * (point.X - center.X) +
                    cosTheta * (point.Y - center.Y) + center.Y;

            return new Point(x, y);
        }
    }
}
