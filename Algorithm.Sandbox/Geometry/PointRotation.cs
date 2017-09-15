using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Geometry
{
    public class PointRotation
    {

        public static Point Rotate(Point center, Point point, int angle)
        {
            double angleInRadians = angle * (Math.PI / 180);

            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            var x = cosTheta * (point.x - center.x) -
                    sinTheta * (point.y - center.y) + center.x;

            var y = sinTheta * (point.x - center.x) +
                    cosTheta * (point.y - center.y) + center.y;

            return new Point() { x = x, y = y };
        }
    }
}
