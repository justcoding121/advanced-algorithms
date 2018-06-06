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

            //start sweeping from left to right
            foreach (var lineEndPoint in lineLeftRightMap
                                        .SelectMany(x => new[] { x.Key, x.Value })
                                        .OrderBy(x => x.X)
                                        .ThenByDescending(x => x.IsLeftEndPoint)
                                        .ThenBy(x=>x.Y))
            {
                //if this is left end point then check for intersection
                //between closest upper and lower segments with current line.
                if (lineEndPoint.IsLeftEndPoint)
                {
                    //left end
                    var leftNode = insert(currentlyTracked, lineEndPoint);

                    //get the closest upper line segment
                    var upperLines = getClosestUpperEndPoints(leftNode, lineEndPoint);

                    //get the closest lower line segment
                    var lowerLines = getClosestLowerEndPoints(leftNode, lineEndPoint);

                    //right end
                    var rightNode = insert(currentlyTracked, lineLeftRightMap[lineEndPoint]);

                    upperLines.AddRange(getClosestUpperEndPoints(rightNode, lineLeftRightMap[lineEndPoint]));
                    lowerLines.AddRange(getClosestLowerEndPoints(rightNode, lineLeftRightMap[lineEndPoint]));

                    if (upperLines.Count > 0)
                    {
                        foreach (var upperLine in upperLines)
                        {
                            var upperIntersection = LineIntersection.FindIntersection(upperLine, lineEndPoint.Line);
                            if (upperIntersection != null)
                            {
                                result.Add(upperIntersection);
                            }
                        }
                    }

                    //also verify lower is not the same line as upper.
                    if (lowerLines.Count > 0)
                    {
                        foreach (var lowerLine in lowerLines)
                        {
                            if (!upperLines.Any(x => x == lowerLine))
                            {
                                var lowerIntersection = LineIntersection.FindIntersection(lineEndPoint.Line, lowerLine);
                                if (lowerIntersection != null)
                                {
                                    result.Add(lowerIntersection);
                                }
                            }
                        }
                    }

                    var linesInBetween = getLinesBetween(currentlyTracked, lineEndPoint, lineLeftRightMap[lineEndPoint]);

                    foreach (var line in linesInBetween)
                    {
                        if (!upperLines.Any(x => x == line)
                            && !lowerLines.Any(x => x == line))
                        {
                            var intersection = LineIntersection.FindIntersection(lineEndPoint.Line, line);
                            if (intersection != null)
                            {
                                result.Add(intersection);
                            }
                        }
                    }

                  
                }
                //if this is right end point then check for intersection
                //between closest upper and lower segments.
                else
                {
                    var leftNode = currentlyTracked.FindNode(new LineEndPointNode(lineRightLeftMap[lineEndPoint]));
                    
                    //get the closest upper line segment
                    var upperLines = getClosestUpperEndPoints(leftNode, lineRightLeftMap[lineEndPoint]);

                    //get the closest lower line segment
                    var lowerLines = getClosestLowerEndPoints(leftNode, lineRightLeftMap[lineEndPoint]);
           
                    var rightNode = currentlyTracked.FindNode(new LineEndPointNode(lineEndPoint));

                    //get the closest upper line segment
                    upperLines.AddRange(getClosestUpperEndPoints(rightNode, lineEndPoint));

                    //get the closest lower line segment
                    lowerLines.AddRange(getClosestLowerEndPoints(rightNode, lineEndPoint));

                    //remove left
                    delete(currentlyTracked, lineRightLeftMap[lineEndPoint]);

                    //remove right
                    delete(currentlyTracked, lineEndPoint);

                    foreach (var upperLine in upperLines)
                    {
                        foreach (var lowerLine in lowerLines)
                        {
                            if (upperLine != null && lowerLine != null
                                && upperLine != lowerLine)
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

            return result.Distinct().ToList();
        }

        /// <summary>
        ///     Get the list of lines that is between the given line in currently tracked
        /// </summary>
        /// <param name="currentlyTracked"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private static List<Line> getLinesBetween(BST<LineEndPointNode> currentlyTracked,
            LineEndPoint left, LineEndPoint right)
        {
            var lower = left.Y < right.Y ? left : right;
            var upper = left.Y > right.Y ? left : right;

            var result = new List<Line>();

            var next = currentlyTracked.FindNode(new LineEndPointNode(lower));
            var last = currentlyTracked.FindNode(new LineEndPointNode(upper));

            if (next != null)
            {
                result.AddRange(next.Value.EndPoints.Select(x => x.Line));
            }

            if (last != null && last != next)
            {
                result.AddRange(last.Value.EndPoints.Select(x => x.Line));
            }

            if (next != null)
            {
                next = getNextUpper(next);
                while (next != null && next != last)
                {
                    result.AddRange(next.Value.EndPoints.Select(x => x.Line));
                    next = getNextUpper(next);
                }
            }

            return result.Where(x => x != left.Line).Distinct().ToList();
        }

        private static void delete(BST<LineEndPointNode> currentlyTracked, LineEndPoint lineEndPoint)
        {
            var existing = currentlyTracked.FindNode(new LineEndPointNode(lineEndPoint));

            if (existing.Value.EndPoints.Count > 1)
            {
                existing.Value.EndPoints.Remove(lineEndPoint);
            }
            else
            {
                currentlyTracked.Delete(existing.Value);
            }
        }

        private static BSTNode<LineEndPointNode> insert(BST<LineEndPointNode> currentlyTracked, LineEndPoint lineEndPoint)
        {
            var newNode = new LineEndPointNode(lineEndPoint);
            var existing = currentlyTracked.FindNode(newNode);

            if (existing != null)
            {
                existing.Value.EndPoints.Add(lineEndPoint);
                return existing;
            }

            currentlyTracked.Insert(newNode);

            return currentlyTracked.FindNode(newNode);
        }

        private static List<Line> getClosestLowerEndPoints(BSTNode<LineEndPointNode> node, LineEndPoint currentLine)
        {
            var result = new List<LineEndPoint>();
            result.AddRange(node.Value.EndPoints);

            var nextLower = getNextLower(node);

            if (nextLower != null)
            {
                result.AddRange(nextLower.Value.EndPoints);
            }

            return result.Where(x => x.Line != currentLine.Line)
                         .OrderByDescending(x => x.Y)
                         .Select(x => x.Line)
                         .Distinct()
                         .ToList();
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
