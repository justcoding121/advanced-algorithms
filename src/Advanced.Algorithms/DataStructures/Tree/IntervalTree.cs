using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A multi-dimensional interval tree implementation.
    /// </summary>
    public class IntervalTree<T> : IEnumerable<Tuple<T[], T[]>> where T : IComparable
    {
        private readonly int dimensions;
        private readonly OneDimentionalIntervalTree<T> tree;
        private HashSet<Tuple<T[], T[]>> items = new HashSet<Tuple<T[], T[]>>(new IntervalComparer<T>());

        public int Count { get; private set; }


        public IntervalTree(int dimension)
        {
            if (dimension <= 0)
            {
                throw new Exception("Dimension should be greater than 0.");
            }

            this.dimensions = dimension;
            tree = new OneDimentionalIntervalTree<T>(defaultValue);
        }

        /// <summary>
        /// Add a new interval to this interval tree.
        /// Time complexity : O(d(log(n) + m)) where d is dimensions and
        /// m is the number of intervals that overlaps with this inserted interval.
        /// </summary>
        public void Insert(T[] start, T[] end)
        {
            validateDimensions(start, end);

            if(items.Contains(new Tuple<T[], T[]>(start, end)))
            {
                throw new Exception("Inteval exists.");
            }

            var currentTrees = new List<OneDimentionalIntervalTree<T>> { tree };

            for (var i = 0; i < dimensions; i++)
            {
                var allOverlaps = new List<OneDimentionalIntervalTree<T>>();

                foreach (var tree in currentTrees)
                {
                    //insert in current dimension
                    tree.Insert(new OneDimentionalInterval<T>(start[i], end[i], defaultValue));

                    //get all overlaps
                    //and insert next dimension value to each overlapping node
                    var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[i], end[i], defaultValue));
                    foreach (var overlap in overlaps)
                    {
                        allOverlaps.Add(overlap.NextDimensionIntervals);
                    }
                }

                currentTrees = allOverlaps;
            }

            items.Add(new Tuple<T[], T[]> (start, end));
           
            Count++;

        }

        /// <summary>
        /// Delete this interval from this interval tree.
        /// Time complexity :  O(d(log(n) + m)) where d is dimensions and
        /// m is the number of intervals that overlap with this deleted interval.
        /// </summary>
        public void Delete(T[] start, T[] end)
        {
            if (!items.Contains(new Tuple<T[], T[]>(start, end)))
            {
                throw new Exception("Inteval does'nt exist.");
            }

            validateDimensions(start, end);

            var allOverlaps = new List<OneDimentionalIntervalTree<T>>();
            var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[0], end[0], defaultValue));

            foreach (var overlap in overlaps)
            {
                allOverlaps.Add(overlap.NextDimensionIntervals);
            }

            deleteOverlaps(allOverlaps, start, end, 1);
            tree.Delete(new OneDimentionalInterval<T>(start[0], end[0], defaultValue));

            items.Remove(new Tuple<T[], T[]>(start, end));
            Count--;
        }

        /// <summary>
        /// Recursively delete values from overlaps in next dimension.
        /// </summary>
        private void deleteOverlaps(List<OneDimentionalIntervalTree<T>> currentTrees, T[] start, T[] end, int index)
        {
            //base case
            if (index == start.Length)
                return;

            var allOverlaps = new List<OneDimentionalIntervalTree<T>>();

            foreach (var tree in currentTrees)
            {
                var overlaps = tree.GetOverlaps(new OneDimentionalInterval<T>(start[index], end[index], defaultValue));

                foreach (var overlap in overlaps)
                {
                    allOverlaps.Add(overlap.NextDimensionIntervals);
                }
            }

            //dig in to next dimension to 
            deleteOverlaps(allOverlaps, start, end, ++index);

            index--;

            //now delete
            foreach (var tree in allOverlaps)
            {
                if (tree.Count > 0)
                {
                    tree.Delete(new OneDimentionalInterval<T>(start[index], end[index], defaultValue));
                }

            }
        }

        /// <summary>
        /// Does this interval overlap with any interval in this interval tree?
        /// </summary>
        public bool DoOverlap(T[] start, T[] end)
        {
            validateDimensions(start, end);

            var allOverlaps = getOverlaps(tree, start, end, 0);

            return allOverlaps.Count > 0;
        }

        /// <summary>
        /// returns a list of matching intervals.
        /// Time complexity : O(d(log(n) + m)) where d is dimensions and
        /// m is the number of overlaps.
        /// </summary>
        public List<Tuple<T[], T[]>> GetOverlaps(T[] start, T[] end)
        {
            return getOverlaps(tree, start, end, 0);
        }

        /// <summary>
        /// Does this interval overlap with any interval in this interval tree?
        /// </summary>
        private List<Tuple<T[], T[]>> getOverlaps(OneDimentionalIntervalTree<T> currentTree,
            T[] start, T[] end, int dimension)
        {
            var nodes = currentTree.GetOverlaps(new OneDimentionalInterval<T>(start[dimension], end[dimension], defaultValue));

            if (dimension + 1 == dimensions)
            {
                var result = new List<Tuple<T[], T[]>>();

                foreach (var node in nodes)
                {
                    var fStart = new T[dimensions];
                    var fEnd = new T[dimensions];

                    fStart[dimension] = node.Start;
                    fEnd[dimension] = node.End[node.MatchingEndIndex];

                    var thisDimResult = new Tuple<T[], T[]>(fStart, fEnd);

                    result.Add(thisDimResult);
                }

                return result;
            }
            else
            {
                var result = new List<Tuple<T[], T[]>>();

                foreach (var node in nodes)
                {
                    var nextDimResult = getOverlaps(node.NextDimensionIntervals, start, end, dimension + 1);

                    foreach (var nextResult in nextDimResult)
                    {
                        nextResult.Item1[dimension] = node.Start;
                        nextResult.Item2[dimension] = node.End[node.MatchingEndIndex];

                        result.Add(nextResult);
                    }
                }

                return result;
            }

        }

        /// <summary>
        /// A cached function to override default(T)
        /// so that for value types we can return min value as default.
        /// </summary>
        private readonly Lazy<T> defaultValue = new Lazy<T>(() =>
        {
            var s = typeof(T);

            bool isValueType;

#if NET40
            isValueType = s.IsValueType;
#else
            isValueType = s.GetTypeInfo().IsValueType;
#endif

            if (isValueType)
            {
                return (T)Convert.ChangeType(int.MinValue, s);
            }

            return default(T);
        });

        /// <summary>
        /// validate dimensions for point length.
        /// </summary>
        private void validateDimensions(T[] start, T[] end)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            if (start.Length != dimensions || start.Length != end.Length)
            {
                throw new Exception($"Expecting {dimensions} points in start and end values for this interval.");
            }

            if (start.Where((t, i) => t.Equals(defaultValue.Value)
                                      || end[i].Equals(defaultValue.Value)).Any())
            {
                throw new Exception("Points cannot contain Minimum Value or Null values");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tuple<T[], T[]>> GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }

    /// <summary>
    /// An interval tree implementation in one dimension using augmentation tree method.
    /// </summary>
    internal class OneDimentionalIntervalTree<T> where T : IComparable
    {
        //use a height balanced binary search tree
        private readonly RedBlackTree<OneDimentionalInterval<T>> redBlackTree
            = new RedBlackTree<OneDimentionalInterval<T>>();

        internal int Count { get; private set; }


        /// <summary>
        /// A cached function to override default(T)
        /// so that for value types we can return min value as default.
        /// </summary>
        private readonly Lazy<T> defaultValue;

        internal OneDimentionalIntervalTree(Lazy<T> defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Insert a new Interval.
        /// </summary>
        internal void Insert(OneDimentionalInterval<T> newInterval)
        {
            sortInterval(newInterval);
            var existing = redBlackTree.FindNode(newInterval);
            if (existing != null)
            {
                existing.Value.End.Add(newInterval.End[0]);
            }
            else
            {
                existing = redBlackTree.InsertAndReturnNode(newInterval).Item1;
            }
            updateMax(existing);
            Count++;
        }

        /// <summary>
        /// Delete this interval
        /// </summary>
        internal void Delete(OneDimentionalInterval<T> interval)
        {
            sortInterval(interval);

            var existing = redBlackTree.FindNode(interval);
            if (existing != null && existing.Value.End.Count > 1)
            {
                existing.Value.End.RemoveAt(existing.Value.End.Count - 1);
                updateMax(existing);
            }
            else if (existing != null)
            {
                redBlackTree.Delete(interval);
                updateMax(existing.Parent);
            }
            else
            {
                throw new Exception("Interval not found in this interval tree.");
            }

            Count--;
        }

        /// <summary>
        ///  Returns an interval in this tree that overlaps with this search interval 
        /// </summary>
        internal OneDimentionalInterval<T> GetOverlap(OneDimentionalInterval<T> searchInterval)
        {
            sortInterval(searchInterval);
            return getOverlap(redBlackTree.Root, searchInterval);
        }

        /// <summary>
        ///  Returns an interval in this tree that overlaps with this search interval.
        /// </summary>
        internal List<OneDimentionalInterval<T>> GetOverlaps(OneDimentionalInterval<T> searchInterval)
        {
            sortInterval(searchInterval);
            return getOverlaps(redBlackTree.Root, searchInterval);
        }

        /// <summary>
        ///  Does any interval overlaps with this search interval.
        /// </summary>
        internal bool DoOverlap(OneDimentionalInterval<T> searchInterval)
        {
            sortInterval(searchInterval);
            return getOverlap(redBlackTree.Root, searchInterval) != null;
        }

        /// <summary>
        /// Swap intervals so that start always appear before end.
        /// </summary>
        private void sortInterval(OneDimentionalInterval<T> value)
        {
            if (value.Start.CompareTo(value.End[0]) <= 0)
            {
                return;
            }

            var tmp = value.End[0];
            value.End[0] = value.Start;
            value.Start = tmp;
        }

        /// <summary>
        /// Returns an interval that overlaps with this interval
        /// </summary>
        private OneDimentionalInterval<T> getOverlap(RedBlackTreeNode<OneDimentionalInterval<T>> current, OneDimentionalInterval<T> searchInterval)
        {
            while (true)
            {
                if (current == null)
                {
                    return null;
                }

                if (doOverlap(current.Value, searchInterval))
                {
                    return current.Value;
                }

                //if left max is greater than search start
                //then the search interval can occur in left sub tree
                if (current.Left != null && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
                {
                    current = current.Left;
                    continue;
                }

                //otherwise look in right subtree
                current = current.Right;
            }
        }

        /// <summary>
        /// Returns all intervals that overlaps with this interval.
        /// </summary>
        private List<OneDimentionalInterval<T>> getOverlaps(RedBlackTreeNode<OneDimentionalInterval<T>> current,
            OneDimentionalInterval<T> searchInterval, List<OneDimentionalInterval<T>> result = null)
        {
            if (result == null)
            {
                result = new List<OneDimentionalInterval<T>>();
            }

            if (current == null)
            {
                return result;
            }

            if (doOverlap(current.Value, searchInterval))
            {
                result.Add(current.Value);
            }

            //if left max is greater than search start
            //then the search interval can occur in left sub tree
            if (current.Left != null
                && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
            {
                getOverlaps(current.Left, searchInterval, result);
            }

            //otherwise look in right subtree
            getOverlaps(current.Right, searchInterval, result);

            return result;
        }

        /// <summary>
        /// Does this interval a overlap with b.
        /// </summary>
        private bool doOverlap(OneDimentionalInterval<T> a, OneDimentionalInterval<T> b)
        {
            //lazy reset
            a.MatchingEndIndex = -1;
            b.MatchingEndIndex = -1;

            for (var i = 0; i < a.End.Count; i++)
            {
                for (var j = 0; j < b.End.Count; j++)
                {

                    //a.Start less than b.End and a.End greater than b.Start
                    if (a.Start.CompareTo(b.End[j]) > 0 || a.End[i].CompareTo(b.Start) < 0)
                    {
                        continue;
                    }

                    a.MatchingEndIndex = i;
                    b.MatchingEndIndex = j;

                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// update max end value under each node in red-black tree recursively.
        /// </summary>
        private void updateMax(RedBlackTreeNode<OneDimentionalInterval<T>> node, T currentMax, bool recurseUp = true)
        {
            while (true)
            {
                if (node == null)
                {
                    return;
                }

                if (node.Left != null && node.Right != null)
                {
                    //if current Max is less than current End
                    //then update current Max
                    if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Left.Value.MaxEnd;
                    }

                    if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Right.Value.MaxEnd;
                    }
                }
                else if (node.Left != null)
                {
                    //if current Max is less than current End
                    //then update current Max
                    if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Left.Value.MaxEnd;
                    }
                }
                else if (node.Right != null)
                {
                    if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Right.Value.MaxEnd;
                    }
                }

                foreach (var v in node.Value.End)
                {
                    if (currentMax.CompareTo(v) < 0)
                    {
                        currentMax = v;
                    }
                }

                node.Value.MaxEnd = currentMax;


                if (recurseUp)
                {
                    node = node.Parent;
                    continue;
                }


                break;
            }
        }

        /// <summary>
        /// Update Max recursively up each node in red-black tree.
        /// </summary>
        private void updateMax(RedBlackTreeNode<OneDimentionalInterval<T>> newRoot, bool recurseUp = true)
        {
            if (newRoot == null)
                return;

            newRoot.Value.MaxEnd = defaultValue.Value;

            if (newRoot.Left != null)
            {
                newRoot.Left.Value.MaxEnd = defaultValue.Value;
                updateMax(newRoot.Left, newRoot.Left.Value.MaxEnd, recurseUp);
            }

            if (newRoot.Right != null)
            {
                newRoot.Right.Value.MaxEnd = defaultValue.Value;
                updateMax(newRoot.Right, newRoot.Right.Value.MaxEnd, recurseUp);
            }

            updateMax(newRoot, newRoot.Value.MaxEnd, recurseUp);

        }

    }

    /// <summary>
    /// One dimensional interval.
    /// </summary>
    internal class OneDimentionalInterval<T> : IComparable where T : IComparable
    {
        /// <summary>
        /// Start of this interval range.
        /// </summary>
        public T Start { get; set; }

        /// <summary>
        /// End of this interval range.
        /// </summary>
        public List<T> End { get; set; }

        /// <summary>
        /// Max End interval under this interval.
        /// </summary>
        internal T MaxEnd { get; set; }

        /// <summary>
        /// Holds intervals for the next dimension.
        /// </summary>
        internal OneDimentionalIntervalTree<T> NextDimensionIntervals { get; set; }

        /// <summary>
        /// Mark the matching end index during overlap search 
        /// so that we can return the overlapping interval.
        /// </summary>
        internal int MatchingEndIndex { get; set; }

        public int CompareTo(object obj)
        {
            return Start.CompareTo(((OneDimentionalInterval<T>)obj).Start);
        }

        public OneDimentionalInterval(T start, T end, Lazy<T> defaultValue)
        {
            Start = start;
            End = new List<T> { end };
            NextDimensionIntervals = new OneDimentionalIntervalTree<T>(defaultValue);
        }
    }

    /// <summary>
    /// Compares two intervals.
    /// </summary>
    internal class IntervalComparer<T> : IEqualityComparer<Tuple<T[], T[]>> where T : IComparable
    {
        public bool Equals(Tuple<T[], T[]> x, Tuple<T[], T[]> y)
        {
            if (x == y)
            {
                return true;
            }

            for (int i = 0; i < x.Item1.Length; i++)
            {
                if (!x.Item1[i].Equals(y.Item1[i]))
                {
                    return false;
                }

                if (!x.Item2[i].Equals(y.Item2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(Tuple<T[], T[]> x)
        {
            unchecked
            {
                if (x == null)
                {
                    return 0;
                }
                int hash = 17;
                foreach (var element in x.Item1)
                {
                    hash = hash * 31 + element.GetHashCode();
                }
                foreach (var element in x.Item2)
                {
                    hash = hash * 31 + element.GetHashCode();
                }
                return hash;
            }
        }
    }
}
