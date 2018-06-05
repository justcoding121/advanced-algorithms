using Advanced.Algorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    ///     A custom object representing Line end point.
    /// </summary>
    internal class LineEndPoint : Point, IComparable
    {
        //Is this point the left end point of line.
        internal bool IsLeftEndPoint;
        //The full line.
        internal Line Line;

        /// <param name="p">One end point of the line.</param>
        /// <param name="line">The full line.</param>
        /// <param name="isLeftEndPoint">Is this point the left end point of line.</param>
        internal LineEndPoint(Point p, Line line, bool isLeftEndPoint)
            : base(p.X, p.Y)
        {
            IsLeftEndPoint = isLeftEndPoint;
            Line = line;
        }

        public int CompareTo(object obj)
        {
            return Y.CompareTo((obj as LineEndPoint).Y);
        }
    }

    public class SweepLineIntersection
    {
        public static List<Point> FindIntersections(IEnumerable<Line> lineSegments)
        {
            var lineEndPoints = lineSegments
                .SelectMany(x =>
                {
                    if (x.Start.X < x.End.X)
                    {
                        return new[] {
                            new LineEndPoint(x.Start, x, true),
                            new LineEndPoint(x.End, x, false)
                        };
                    }
                    else
                    {
                        return new[] {
                            new LineEndPoint(x.End, x, true),
                            new LineEndPoint(x.Start, x, false)
                        };
                    }

                })
                .OrderBy(x => x.X)
                .ToList();

            var current = new RedBlackTree<LineEndPoint>();
            var lineRBTNodeMapping = new System.Collections.Generic.Dictionary<Line, RedBlackTreeNode<LineEndPoint>>();
            var result = new List<Point>();

            //start sweeping from left to right
            foreach (var lineEndPoint in lineEndPoints)
            {
                //if this is left end point then check for intersection
                //between closest upper and lower segments with current line.
                if (lineEndPoint.IsLeftEndPoint)
                {
                    var leftNode = current.InsertAndReturnNewNode(lineEndPoint);

                    lineRBTNodeMapping.Add(lineEndPoint.Line, leftNode);

                    //get the closest upper line segment
                    var upper = getClosestUpperEndPoint(leftNode);

                    //get the closest lower line segment
                    var lower = getClosestLowerEndPoint(leftNode);

                    if (upper != null)
                    {
                        var upperIntersection = LineIntersection.FindIntersection(upper.Line, lineEndPoint.Line);
                        if (upperIntersection != null)
                        {
                            result.Add(upperIntersection);
                        }
                    }

                    if (lower != null)
                    {
                        //verify lower is not the same line as upper.
                        if (upper == null || upper.Line != lower.Line)
                        {
                            var lowerIntersection = LineIntersection.FindIntersection(lineEndPoint.Line, lower.Line);
                            if (lowerIntersection != null)
                            {
                                result.Add(lowerIntersection);
                            }
                        }
                    }

                }
                //if this is right end point then check for intersection
                //between closest upper and lower segments.
                else
                {
                    var leftNode = lineRBTNodeMapping[lineEndPoint.Line];
                    var rightNode = current.InsertAndReturnNewNode(lineEndPoint);

                    //get the closest upper line segment
                    var upper = getClosestUpperEndPoint(rightNode);

                    //get the closest lower line segment
                    var lower = getClosestLowerEndPoint(rightNode);

                    if (upper != null && lower != null
                        && upper.Line != lower.Line)
                    {
                        var intersection = LineIntersection.FindIntersection(upper.Line, lower.Line);
                        if (intersection != null)
                        {
                            result.Add(intersection);
                        }
                    }

                    //remove line segment
                    current.Delete(leftNode.Value);
                    current.Delete(rightNode.Value);
                    lineRBTNodeMapping.Remove(lineEndPoint.Line);
                }
            }

            return result;
        }

        private static LineEndPoint getClosestLowerEndPoint(RedBlackTreeNode<LineEndPoint> node)
        {
            return node.Left != null ? node.Left.Value : null;
        }

        private static LineEndPoint getClosestUpperEndPoint(RedBlackTreeNode<LineEndPoint> node)
        {
            return node.Right != null ? node.Right.Value
                                     : node.Parent != null ? node.Parent.Value : null;
        }
    }
}
