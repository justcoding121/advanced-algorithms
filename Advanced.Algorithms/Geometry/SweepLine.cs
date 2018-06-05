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

        public override bool Equals(object obj)
        {
            var tgt = obj as LineEndPoint;
            return this == tgt;
        }

        public int CompareTo(object obj)
        {
            return Y.CompareTo((obj as LineEndPoint).Y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class SweepLineIntersection
    {
        public static List<Point> FindIntersections(IEnumerable<Line> lineSegments)
        {
            var lineLeftRightMap = lineSegments
                .Select(x =>
                {
                    if (x.Start.X < x.End.X)
                    {
                        return new KeyValuePair<LineEndPoint, LineEndPoint>(
                            new LineEndPoint(x.Start, x, true),
                            new LineEndPoint(x.End, x, false)
                        );
                    }
                    else
                    {
                        return new KeyValuePair<LineEndPoint, LineEndPoint>(
                             new LineEndPoint(x.End, x, true),
                             new LineEndPoint(x.Start, x, false)
                         );
                    }

                })
                .ToDictionary(x => x.Key, x => x.Value);

            var lineRightLeftMap = lineLeftRightMap.ToDictionary(x => x.Value, x => x.Key);

            var currentlyTracked = new RedBlackTree<LineEndPoint>();

            var result = new List<Point>();

            //start sweeping from left to right
            foreach (var lineEndPoint in lineLeftRightMap
                                        .SelectMany(x => new[] { x.Key, x.Value })
                                        .OrderBy(x => x.X)
                                        .ThenByDescending(x => x.IsLeftEndPoint)
                                        .ThenBy(x => x.Y))
            {
                //if this is left end point then check for intersection
                //between closest upper and lower segments with current line.
                if (lineEndPoint.IsLeftEndPoint)
                {
                    //left end
                    var leftNode = currentlyTracked.InsertAndReturnNewNode(lineEndPoint);

                    //get the closest upper line segment
                    var upperLines = getClosestUpperEndPoint(leftNode, lineEndPoint);

                    //get the closest lower line segment
                    var lowerLines = getClosestLowerEndPoint(leftNode, lineEndPoint);

                    foreach (var upperLine in upperLines)
                    {
                        var upperIntersection = LineIntersection.FindIntersection(upperLine, lineEndPoint.Line);
                        if (upperIntersection != null)
                        {
                            result.Add(upperIntersection);
                        }
                    }

                    foreach (var lowerLine in lowerLines)
                    {
                        //verify lower is not the same line as upper.
                        if (!upperLines.Any(x => x == lowerLine))
                        {
                            var lowerIntersection = LineIntersection.FindIntersection(lineEndPoint.Line, lowerLine);
                            if (lowerIntersection != null)
                            {
                                result.Add(lowerIntersection);
                            }
                        }
                    }

                    //right end
                    currentlyTracked.Insert(lineLeftRightMap[lineEndPoint]);
                }
                //if this is right end point then check for intersection
                //between closest upper and lower segments.
                else
                {
                    //remove left
                    currentlyTracked.Delete(lineRightLeftMap[lineEndPoint]);

                    var rightNode = currentlyTracked.Find(lineEndPoint);

                    //get the closest upper line segment
                    var upper = getClosestUpperEndPoint(rightNode, lineEndPoint);

                    //get the closest lower line segment
                    var lower = getClosestLowerEndPoint(rightNode, lineEndPoint);

                    //remove right
                    currentlyTracked.Delete(lineEndPoint);

                    if (upper.Count > 0
                        && lower.Count > 0)
                    {
                        for (int i = 0; i < upper.Count; i++)
                            for (int j = 0; j < lower.Count; j++)
                            {
                                var upperLine = upper[i];
                                var lowerLine = lower[j];

                                if (upperLine != lowerLine)
                                {
                                    var intersection = LineIntersection.FindIntersection(upperLine, lowerLine);
                                    if (intersection != null)
                                    {
                                        result.Add(intersection);
                                    }
                                }

                            }
                    }

                }
            }

            return result;
        }

        private static List<Line> getClosestLowerEndPoint(RedBlackTreeNode<LineEndPoint> node, LineEndPoint currentLine)
        {
            var result = node.Value.ToList();

            //root or left child
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Left != null)
                {
                    result.AddRange(node.Left.Value);
                }
            }
            //right child
            else
            {
                if (node.Left != null)
                {
                    result.AddRange(node.Left.Value);
                }
                else
                {
                    result.AddRange(node.Parent.Value);
                }
            }

            return result.Where(x => x.Line != currentLine.Line)
                         .Where(x => x.Y <= currentLine.Y)
                         .OrderByDescending(x => x.Y)
                         .Take(1)
                         .Select(x => x.Line)
                         .ToList();
        }

        private static List<Line> getClosestUpperEndPoint(RedBlackTreeNode<LineEndPoint> node, LineEndPoint currentLine)
        {
            var result = node.Values;

            //root or left child
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Right != null)
                {
                    result.AddRange(node.Right.Values);
                }
                else
                {
                    //not root
                    if (node.Parent != null)
                    {
                        result.AddRange(node.Parent.Values);
                    }
                }
            }
            //right child
            else
            {
                if (node.Right != null)
                {
                    result.AddRange(node.Right.Values);
                }
            }

            return result.Where(x => x.Line != currentLine.Line)
                      .Where(x => x.Y >= currentLine.Y)
                      .OrderBy(x => x.Y)
                      .Take(1)
                      .Select(x => x.Line)
                      .ToList();
        }
    }
}
