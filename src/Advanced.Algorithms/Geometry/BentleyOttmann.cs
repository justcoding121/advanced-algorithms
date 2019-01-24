using System;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    ///     Bentley-Ottmann sweep line algorithm to find line intersections.
    /// </summary>
    public class BentleyOttmann
    {
        private readonly int precision;
        private readonly PointComparer pointComparer;

        private HashSet<Event> verticalHorizontalLines;
        private HashSet<Event> normalLines;

        private BHeap<Event> eventQueue;
        private HashSet<Event> eventQueueLookUp;

        private Dictionary<Event, Event> rightLeftEventLookUp;
        private RedBlackTree<Event> currentlyTrackedLines;

        private Dictionary<Point, HashSet<Tuple<Event, Event>>> intersectionEvents;

        internal readonly double Tolerance;

        internal Line SweepLine;

        public BentleyOttmann(int precision = 5)
        {
            this.precision = precision;
            pointComparer = new PointComparer();

            Tolerance = Math.Round(Math.Pow(0.1, precision), precision);
        }

        private void initialize(IEnumerable<Line> lineSegments)
        {
            SweepLine = new Line(new Point(0, 0), new Point(0, int.MaxValue), Tolerance);

            currentlyTrackedLines = new RedBlackTree<Event>(true, pointComparer);
            intersectionEvents = new Dictionary<Point, HashSet<Tuple<Event, Event>>>(pointComparer);

            verticalHorizontalLines = new HashSet<Event>();
            normalLines = new HashSet<Event>();

            rightLeftEventLookUp = lineSegments
                                   .Select(x =>
                                   {
                                       if (x.Left.X < 0 || x.Left.Y < 0 || x.Right.X < 0 || x.Right.Y < 0)
                                       {
                                           throw new Exception("Negative coordinates are not supported.");
                                       }

                                       return new KeyValuePair<Event, Event>(
                                          new Event(x.Left, pointComparer, EventType.Start, x, this),
                                          new Event(x.Right, pointComparer, EventType.End, x, this)
                                       );

                                   }).ToDictionary(x => x.Value, x => x.Key);

            eventQueueLookUp = new HashSet<Event>(rightLeftEventLookUp.SelectMany(x => new[] {
                                    x.Key,
                                    x.Value
                                }), new PointComparer());

            eventQueue = new BHeap<Event>(SortDirection.Ascending, eventQueueLookUp, new EventQueueComparer());
        }

        public Dictionary<Point, List<Line>> FindIntersections(IEnumerable<Line> lineSegments)
        {
            initialize(lineSegments);

            while (eventQueue.Count > 0)
            {
                var currentEvent = eventQueue.Extract();
                eventQueueLookUp.Remove(currentEvent);
                sweepTo(currentEvent);

                switch (currentEvent.Type)
                {
                    case EventType.Start:

                        if (verticalHorizontalLines.Count > 0)
                        {
                            foreach (var line in verticalHorizontalLines)
                            {
                                var intersection = findIntersection(currentEvent, line);
                                recordIntersection(currentEvent, line, intersection);
                            }
                        }

                        if (currentEvent.Segment.IsVertical || currentEvent.Segment.IsHorizontal)
                        {
                            verticalHorizontalLines.Add(currentEvent);

                            foreach (var line in normalLines)
                            {
                                var intersection = findIntersection(currentEvent, line);
                                recordIntersection(currentEvent, line, intersection);
                            }

                            break;
                        }

                        normalLines.Add(currentEvent);

                        currentlyTrackedLines.Insert(currentEvent);

                        var lower = currentlyTrackedLines.NextLower(currentEvent);
                        var upper = currentlyTrackedLines.NextHigher(currentEvent);

                        var lowerIntersection = findIntersection(currentEvent, lower);
                        recordIntersection(currentEvent, lower, lowerIntersection);
                        enqueueIntersectionEvent(currentEvent, lowerIntersection);

                        var upperIntersection = findIntersection(currentEvent, upper);
                        recordIntersection(currentEvent, upper, upperIntersection);
                        enqueueIntersectionEvent(currentEvent, upperIntersection);

                        break;

                    case EventType.End:

                        currentEvent = rightLeftEventLookUp[currentEvent];

                        if (currentEvent.Segment.IsVertical || currentEvent.Segment.IsHorizontal)
                        {
                            verticalHorizontalLines.Remove(currentEvent);
                            break;
                        }

                        normalLines.Remove(currentEvent);

                        lower = currentlyTrackedLines.NextLower(currentEvent);
                        upper = currentlyTrackedLines.NextHigher(currentEvent);

                        currentlyTrackedLines.Delete(currentEvent);

                        var upperLowerIntersection = findIntersection(lower, upper);
                        recordIntersection(lower, upper, upperLowerIntersection);
                        enqueueIntersectionEvent(currentEvent, upperLowerIntersection);

                        break;

                    case EventType.Intersection:

                        var intersectionLines = intersectionEvents[currentEvent as Point];

                        foreach (var item in intersectionLines)
                        {
                            swapBstNodes(currentlyTrackedLines, item.Item1, item.Item2);

                            var upperLine = item.Item1;
                            var upperUpper = currentlyTrackedLines.NextHigher(upperLine);

                            var newUpperIntersection = findIntersection(upperLine, upperUpper);
                            recordIntersection(upperLine, upperUpper, newUpperIntersection);
                            enqueueIntersectionEvent(currentEvent, newUpperIntersection);

                            var lowerLine = item.Item2;
                            var lowerLower = currentlyTrackedLines.NextLower(lowerLine);

                            var newLowerIntersection = findIntersection(lowerLine, lowerLower);
                            recordIntersection(lowerLine, lowerLower, newLowerIntersection);
                            enqueueIntersectionEvent(currentEvent, newLowerIntersection);
                        }

                        break;
                }

            }

            return intersectionEvents.ToDictionary(x => x.Key,
                                                   x => x.Value.SelectMany(y => new[] { y.Item1.Segment, y.Item2.Segment })
                                                       .Distinct().ToList());
        }

        private void sweepTo(Event currentEvent)
        {
            SweepLine = new Line(new Point(currentEvent.X, 0), new Point(currentEvent.X, int.MaxValue), Tolerance);
        }

        internal void swapBstNodes(RedBlackTree<Event> currentlyTrackedLines, Event value1, Event value2)
        {
            var node1 = currentlyTrackedLines.Find(value1).Item1;
            var node2 = currentlyTrackedLines.Find(value2).Item1;

            if (node1 == null || node2 == null)
            {
                throw new Exception("Value1, Value2 or both was not found in this BST.");
            }

            var tmp = node1.Value;
            node1.Value = node2.Value;
            node2.Value = tmp;

            currentlyTrackedLines.NodeLookUp[node1.Value] = node1;
            currentlyTrackedLines.NodeLookUp[node2.Value] = node2;
        }

        private void enqueueIntersectionEvent(Event currentEvent, Point intersection)
        {
            if (intersection == null)
            {
                return;
            }

            var intersectionEvent = new Event(intersection, pointComparer, EventType.Intersection, null, this);

            if (intersectionEvent.X > SweepLine.Left.X
                || (intersectionEvent.X == SweepLine.Left.X
                   && intersectionEvent.Y > currentEvent.Y))
            {
                if (!eventQueueLookUp.Contains(intersectionEvent))
                {
                    eventQueue.Insert(intersectionEvent);
                    eventQueueLookUp.Add(intersectionEvent);
                }
            }

        }

        private Point findIntersection(Event a, Event b)
        {
            if (a == null || b == null
                || a.Type == EventType.Intersection
                || b.Type == EventType.Intersection)
            {
                return null;
            }

            return a.Segment.Intersection(b.Segment, Tolerance);
        }

        private void recordIntersection(Event line1, Event line2, Point intersection)
        {
            if (intersection == null)
            {
                return;
            }

            var existing = intersectionEvents.ContainsKey(intersection) ?
                intersectionEvents[intersection] : new HashSet<Tuple<Event, Event>>();

            if (line1.Segment.Slope.CompareTo(line2.Segment.Slope) > 0)
            {
                existing.Add(new Tuple<Event, Event>(line1, line2));
            }
            else
            {
                existing.Add(new Tuple<Event, Event>(line2, line1));
            }

            intersectionEvents[intersection] = existing;
        }
    }

    //point type
    internal enum EventType
    {
        Start = 0,
        Intersection = 1,
        End = 2
    }

    /// <summary>
    ///     A custom object representing start/end/intersection point.
    /// </summary>
    internal class Event : Point, IComparable
    {
        private readonly double tolerance;
        private readonly PointComparer pointComparer;

        internal EventType Type;

        //The full line only if not an intersection event
        internal Line Segment;

        internal BentleyOttmann Algorithm;

        internal Line LastSweepLine;
        internal Point LastIntersection;

        internal Event(Point eventPoint, PointComparer pointComparer, EventType eventType,
            Line lineSegment, BentleyOttmann algorithm)
            : base(eventPoint.X, eventPoint.Y)
        {
            tolerance = algorithm.Tolerance;
            this.pointComparer = pointComparer;

            Type = eventType;
            Segment = lineSegment;
            Algorithm = algorithm;
        }

        public int CompareTo(object that)
        {
            if (Equals(that))
            {
                return 0;
            }

            var thatEvent = that as Event;

            var line1 = Segment;
            var line2 = thatEvent.Segment;

            Point intersectionA;
            if (Type == EventType.Intersection)
            {
                intersectionA = this as Point;
            }
            else
            {
                if (LastSweepLine == Algorithm.SweepLine)
                {
                    intersectionA = LastIntersection;
                }
                else
                {
                    intersectionA = LineIntersection.FindIntersection(line1, Algorithm.SweepLine, tolerance);
                    LastSweepLine = Algorithm.SweepLine;
                    LastIntersection = intersectionA;
                }
            }

            Point intersectionB;
            if (Type == EventType.Intersection)
            {
                intersectionB = thatEvent as Point;
            }
            else
            {
                if (thatEvent.LastSweepLine == thatEvent.Algorithm.SweepLine)
                {
                    intersectionB = thatEvent.LastIntersection;
                }
                else
                {
                    intersectionB = LineIntersection.FindIntersection(line2, thatEvent.Algorithm.SweepLine, tolerance);
                    thatEvent.LastSweepLine = thatEvent.Algorithm.SweepLine;
                    thatEvent.LastIntersection = intersectionB;
                }
            }

            var result = intersectionA.Y.CompareTo(intersectionB.Y);
            if (result != 0)
            {
                return result;
            }

            //if Y is same use slope as comparison
            double slope1 = line1.Slope;

            //if Y is same use slope as comparison
            double slope2 = line2.Slope;

            result = slope1.CompareTo(slope2);
            if (result != 0)
            {
                return result;
            }

            //if slope is the same use diff of X co-ordinate
            result = line1.Left.X.CompareTo(line2.Left.X);
            if (result != 0)
            {
                return result;
            }

            //if diff of X co-ordinate is same use diff of Y co-ordinate
            result = line1.Left.Y.CompareTo(line2.Left.Y);

            //at this point this is guaranteed to be not same.
            //since we don't let duplicate lines with input HashSet of lines.
            //see line equals override in Line class.
            return result;
        }

        public override bool Equals(object that)
        {
            if (that == this)
            {
                return true;
            }

            var thatEvent = that as Event;

            if ((Type != EventType.Intersection && thatEvent.Type == EventType.Intersection)
                || (Type == EventType.Intersection && thatEvent.Type != EventType.Intersection))
            {
                return false;
            }

            if (Type == EventType.Intersection && thatEvent.Type == EventType.Intersection)
            {
                return pointComparer.Equals(this as Point, thatEvent);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    //Used to override event comparison when using BMinHeap for Event queue.
    internal class EventQueueComparer : Comparer<Event>
    {
        public override int Compare(Event a, Event b)
        {
            //same object
            if (a == b)
            {
                return 0;
            }

            //compare X
            var result = a.X.CompareTo(b.X);

            if (result != 0)
            {
                return result;
            }

            //Left event first, then intersection and finally right.
            result = a.Type.CompareTo(b.Type);

            if (result != 0)
            {
                return result;
            }

            return a.Y.CompareTo(b.Y);
        }
    }

}
