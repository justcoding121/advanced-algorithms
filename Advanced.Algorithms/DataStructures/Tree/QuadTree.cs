using Advanced.Algorithms.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A quadtree implementation.
    /// </summary>
    public class QuadTree<T> : IEnumerable<Tuple<Point, T>>
    {
        //used to decide when the tree should be reconstructed
        private int deletionCount = 0;

        private QuadTreeNode<T> root;

        public int Count { get; private set; }

        /// <summary>
        /// Time complexity: O(n).
        /// </summary>
        /// <param name="point">The co-ordinate.</param>
        /// <param name="value">The value associated with this co-ordinate if any.</param>
        public void Insert(Point point, T value = default(T))
        {
            root = insert(root, point, value);
            Count++;
        }

        private QuadTreeNode<T> insert(QuadTreeNode<T> current, Point point, T value)
        {
            if (current == null)
            {
                return new QuadTreeNode<T>(point, value);
            }

            //south-west
            if (point.X < current.Point.X && point.Y < current.Point.Y)
            {
                current.SW = insert(current.SW, point, value);
            }
            //north-west
            else if (point.X < current.Point.X && point.Y >= current.Point.Y)
            {
                current.NW = insert(current.NW, point, value);
            }
            //north-east
            else if (point.X > current.Point.X && point.Y >= current.Point.Y)
            {
                current.NE = insert(current.NE, point, value);
            }
            //south-east
            else if (point.X > current.Point.X && point.Y < current.Point.Y)
            {
                current.SE = insert(current.SE, point, value);
            }

            return current;
        }

        /// <summary>
        /// Time complexity: O(n).
        /// </summary>
        public List<Tuple<Point, T>> RangeSearch(Rectangle searchWindow)
        {
            return rangeSearch(root, searchWindow, new List<Tuple<Point, T>>());
        }

        private List<Tuple<Point, T>> rangeSearch(QuadTreeNode<T> current, Rectangle searchWindow, List<Tuple<Point, T>> result)
        {
            if (current == null)
            {
                return result;
            }

            //is inside the search rectangle
            if (current.Point.X >= searchWindow.LeftTop.X
                && current.Point.X <= searchWindow.RightBottom.X
                && current.Point.Y <= searchWindow.LeftTop.Y
                && current.Point.Y >= searchWindow.RightBottom.Y
                && !current.IsDeleted)
            {
                result.Add(new Tuple<Point, T>(current.Point, current.Value));
            }

            //south-west
            if (searchWindow.LeftTop.X < current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
            {
                rangeSearch(current.SW, searchWindow, result);
            }
            //north-west
            if (searchWindow.LeftTop.X < current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
            {
                rangeSearch(current.NW, searchWindow, result);
            }
            //north-east
            if (searchWindow.RightBottom.X > current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
            {
                rangeSearch(current.NE, searchWindow, result);
            }
            //south-east
            if (searchWindow.RightBottom.X > current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
            {
                rangeSearch(current.SE, searchWindow, result);
            }

            return result;
        }

        /// <summary>
        /// Time complexity: O(n).
        /// </summary>
        public void Delete(Point p)
        {
            var point = find(root, p);

            if (point == null || point.IsDeleted)
            {
                throw new Exception("Point not found.");
            }

            point.IsDeleted = true;
            Count--;
          
            if (deletionCount == Count)
            {
                reconstruct();
                deletionCount = 0;
            }
            else
            {
                deletionCount++;
            }
          
        }

        private void reconstruct()
        {
            QuadTreeNode<T> newRoot = null;

            foreach(var exisiting in this)
            {
                newRoot = insert(newRoot, exisiting.Item1, exisiting.Item2);
            }

            root = newRoot;
        }

        private QuadTreeNode<T> find(QuadTreeNode<T> current, Point point)
        {
            if (current == null)
            {
                return null;
            }

            if (current.Point.X == point.X && current.Point.Y == point.Y)
            {
                return current;
            }

            //south-west
            if (point.X < current.Point.X && point.Y < current.Point.Y)
            {
                return find(current.SW, point);
            }
            //north-west
            else if (point.X < current.Point.X && point.Y >= current.Point.Y)
            {
                return find(current.NW, point);
            }
            //north-east
            else if (point.X > current.Point.X && point.Y >= current.Point.Y)
            {
                return find(current.NE, point);
            }
            //south-east
            else if (point.X > current.Point.X && point.Y < current.Point.Y)
            {
                return find(current.SE, point);
            }

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tuple<Point, T>> GetEnumerator()
        {
            return new QuadTreeEnumerator<T>(root);
        }
    }

    internal class QuadTreeNode<T>
    {
        //co-ordinate
        internal Point Point;

        //quadrants
        internal QuadTreeNode<T> NW, NE, SE, SW;

        //actual data if any associated with this point
        internal T Value;

        //marked as deleted
        internal bool IsDeleted;

        internal QuadTreeNode(Point point, T value)
        {
            Point = point;
            Value = value;
        }
    }

    internal class QuadTreeEnumerator<T> : IEnumerator<Tuple<Point, T>> 
    {
        private readonly QuadTreeNode<T> root;

        private QuadTreeNode<T> current;
        private Stack<QuadTreeNode<T>> progress;

        internal QuadTreeEnumerator(QuadTreeNode<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null)
            {
                return false;
            }

            if (progress == null)
            {
                progress = new Stack<QuadTreeNode<T>>(new[] { root.NE, root.NW, root.SE, root.SW }.Where(x => x != null));
                current = root;
                return true;
            }

            if (progress.Count > 0)
            {
                var next = progress.Pop();
                current = next;

                foreach (var child in new[] { next.NE, next.NW, next.SE, next.SW }.Where(x => x != null))
                {
                    progress.Push(child);
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            progress = null;
            current = null;
        }

        object IEnumerator.Current => Current;

        public Tuple<Point, T> Current
        {
            get
            {
                return new Tuple<Point, T>(current.Point, current.Value);
            }
        }

        public void Dispose()
        {
            progress = null;
        }
    }
}
