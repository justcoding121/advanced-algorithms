using Advanced.Algorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

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

    internal class LineEndPointNode : IComparable
    {
        internal LineEndPointNode(LineEndPoint endPoint)
        {
            EndPoints.Add(endPoint);
        }

        internal List<LineEndPoint> EndPoints { get; set; } = new List<LineEndPoint>();

        public int CompareTo(object obj)
        {
            return EndPoints[0].Y.CompareTo((obj as LineEndPointNode).EndPoints[0].Y);
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

            var currentlyTracked = new BST<LineEndPointNode>();

            var result = new List<Point>();

            throw new NotImplementedException();

        }


        private static BSTNode<LineEndPointNode> getNextLower(BSTNode<LineEndPointNode> node)
        {
            //root or left child
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Left != null)
                {
                    return node.Left;
                }
                else
                {
                    while (node.Parent != null && node.IsLeftChild)
                    {
                        node = node.Parent;
                    }

                    return node?.Parent;
                }
            }
            //right child
            else
            {
                if (node.Left != null)
                {
                    return node.Left;
                }
                else
                {
                    return node.Parent;
                }
            }

        }

        private static List<Line> getClosestUpperEndPoints(BSTNode<LineEndPointNode> node, LineEndPoint currentLine)
        {
            var result = new List<LineEndPoint>();
            result.AddRange(node.Value.EndPoints);

            var nextUpper = getNextUpper(node);

            if (nextUpper != null)
            {
                result.AddRange(nextUpper.Value.EndPoints);
            }

            return result.Where(x => x.Line != currentLine.Line)
                      .OrderBy(x => x.Y)
                      .Select(x => x.Line)
                      .ToList();
        }

        private static BSTNode<LineEndPointNode> getNextUpper(BSTNode<LineEndPointNode> node)
        {
            //root or left child
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Right != null)
                {
                    return node.Right;
                }
                else
                {
                    return node?.Parent;
                }
            }
            //right child
            else
            {
                if (node.Right != null)
                {
                    return node.Right;
                }
                else
                {
                    while (node.Parent != null && node.IsRightChild)
                    {
                        node = node.Parent;
                    }

                    return node?.Parent;
                }
            }
        }
    }
}
