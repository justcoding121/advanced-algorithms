using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Geometry
{
    /// <summary>
    /// Convex hull using jarvis's algorithm
    /// </summary>
    public class ConvexHull
    {
        private enum Orientation
        {
            ClockWise = 0,
            AntiClockWise = 1,
            Colinear = 2
        }

        public static List<int[]> Find(List<int[]> points)
        {
            var currentPointIndex = findLeftMostPoint(points);
            var startingPointIndex = currentPointIndex;

            var result = new List<int[]>();

            do
            {
                result.Add(points[currentPointIndex]);

                //pick a random point as next Point
                var nextPointIndex = (currentPointIndex + 1) % points.Count;

                for (int i = 0; i < points.Count; i++)
                {
                    if (i == nextPointIndex)
                    {
                        continue;
                    }

                    var orientation = getOrientation(points[currentPointIndex],
                       points[i], points[nextPointIndex]);

                    if (orientation == Orientation.ClockWise)
                    {
                        nextPointIndex = i;
                    }
                }

                currentPointIndex = nextPointIndex;
            }
            while (currentPointIndex != startingPointIndex);

            return result;
        }

        /// <summary>
        /// Compute the orientation of the lines formed by points p, q and r
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static Orientation getOrientation(int[] p, int[] q, int[] r)
        {
            int x1 = p[0], y1 = p[1];
            int x2 = q[0], y2 = q[1];
            int x3 = r[0], y3 = r[1];

            //using slope formula => (y2-y1)/(x2-x1) = (y3-y2)/(x3-x2) (if colinear)
            // derives to (y2-y1)(x3-x2)-(y3-y2)(x2-x1) == 0 
            var result = (y2 - y1) * (x3 - x2) - (y3 - y2) * (x2 - x1);

            //sign will give the direction
            if (result < 0)
            {
                return Orientation.ClockWise;
            }
            else if (result > 0)
            {
                return Orientation.AntiClockWise;
            }

            return Orientation.Colinear;
        }


        private static int findLeftMostPoint(List<int[]> points)
        {
            var left = 0;

            for (int i = 1; i < points.Count; i++)
            {
                if (points[i][0] < points[left][0])
                {
                    left = i;
                }
            }

            return left;
        }
    }
}
