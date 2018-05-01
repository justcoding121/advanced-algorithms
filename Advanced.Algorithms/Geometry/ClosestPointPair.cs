using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Geometry
{

    public class ClosestPointPair
    {
        public static double Find(List<int[]> points)
        {
            var xSorted = points
                .Select(z => new Point() { x = z[0], y = z[1] })
                .OrderBy(p => p.x).ToList();

            return Find(xSorted, 0, points.Count - 1);
        }

        public static double Find(List<Point> points, int left, int right)
        {
            if (right - left <= 3)
            {
                return bruteForce(points, left, right);
            }

            var mid = (left + right) / 2;

            var leftMin = Find(points, 0, mid);
            var rightMin = Find(points, mid + 1, right);

            var min = Math.Min(leftMin, rightMin);
            var midX = points[mid].x;

            var strips = new List<Point>();

            for (var i = left; i <= right; i++)
            {
                if (Math.Abs(points[i].x - midX) < min)
                {
                    strips.Add(points[i]);
                }
            }

            //vertical strips within the radius of min
            strips = strips.OrderBy(p => p.y).ToList();

            for (var i = 0; i < strips.Count; i++)
            {
                for (var j = i + 1; j < strips.Count && Math.Abs(strips[i].y - strips[j].y) < min; j++)
                {
                    //check for radius 
                    min = Math.Min(min, getDistance(strips[i], strips[j]));
                    
                }
            }

            return min;

        }

        private static double bruteForce(IList<Point> points, int left, int right)
        {
            var min = double.MaxValue;
            for (var i = left; i < right; i++)
            {
                for (var j = left + 1; j <= right; j++)
                {
                    min = Math.Min(min, getDistance(points[i], points[j]));
                }
            }
            return min;
        }

        /// <summary>
        /// Eucledian distance
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        private static double getDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(point1.x - point2.x), 2)
                     + Math.Pow(Math.Abs(point1.y - point2.y), 2));
        }
    }
}
