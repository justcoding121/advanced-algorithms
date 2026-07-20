using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Geometry;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A quadtree implementation.
/// </summary>
public class QuadTree<T> : IEnumerable<Tuple<Point, T>>
{
    //used to decide when the tree should be reconstructed
    private int deletionCount;

    private QuadTreeNode<T> root;
    private readonly double tolerance;

    public QuadTree(int precision = 5)
    {
        tolerance = Math.Round(Math.Pow(0.1, precision), precision);
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Tuple<Point, T>> GetEnumerator()
    {
        return new QuadTreeEnumerator<T>(root);
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="point">The co-ordinate.</param>
    /// <param name="value">The value associated with this co-ordinate if any.</param>
    public void Insert(Point point, T value = default)
    {
        root = Insert(root, point, value);
        Count++;
    }

    private QuadTreeNode<T> Insert(QuadTreeNode<T> current, Point point, T value)
    {
        if (current == null) return new QuadTreeNode<T>(point, value);

        if (current.Point.X.IsEqual(point.X, tolerance) && current.Point.Y.IsEqual(point.Y, tolerance))
            throw new Exception("Point already exists.");

        //south-west / north-west
        if (point.X.IsLessThan(current.Point.X, tolerance))
        {
            if (point.Y.IsLessThan(current.Point.Y, tolerance))
                current.Sw = Insert(current.Sw, point, value);
            else
                current.Nw = Insert(current.Nw, point, value);
        }
        //south-east / north-east
        else if (point.Y.IsLessThan(current.Point.Y, tolerance))
        {
            current.Se = Insert(current.Se, point, value);
        }
        else
        {
            current.Ne = Insert(current.Ne, point, value);
        }

        return current;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public List<Tuple<Point, T>> RangeSearch(Rectangle searchWindow)
    {
        return RangeSearch(root, searchWindow, new List<Tuple<Point, T>>());
    }

    private List<Tuple<Point, T>> RangeSearch(QuadTreeNode<T> current, Rectangle searchWindow,
        List<Tuple<Point, T>> result)
    {
        if (current == null) return result;

        //is inside the search rectangle
        if (current.Point.X >= searchWindow.LeftTop.X
            && current.Point.X <= searchWindow.RightBottom.X
            && current.Point.Y <= searchWindow.LeftTop.Y
            && current.Point.Y >= searchWindow.RightBottom.Y
            && !current.IsDeleted)
            result.Add(new Tuple<Point, T>(current.Point, current.Value));

        //south-west
        if (searchWindow.LeftTop.X < current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
            RangeSearch(current.Sw, searchWindow, result);
        //north-west
        if (searchWindow.LeftTop.X < current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
            RangeSearch(current.Nw, searchWindow, result);
        //north-east
        if (searchWindow.RightBottom.X > current.Point.X && searchWindow.LeftTop.Y >= current.Point.Y)
            RangeSearch(current.Ne, searchWindow, result);
        //south-east
        if (searchWindow.RightBottom.X > current.Point.X && searchWindow.RightBottom.Y < current.Point.Y)
            RangeSearch(current.Se, searchWindow, result);

        return result;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(Point p)
    {
        var point = Find(root, p);

        if (point == null || point.IsDeleted) throw new Exception("Point not found.");

        point.IsDeleted = true;
        Count--;
        deletionCount++;

        if (deletionCount >= Count)
        {
            Reconstruct();
            deletionCount = 0;
        }
    }

    private void Reconstruct()
    {
        QuadTreeNode<T> newRoot = null;

        foreach (var exisiting in this) newRoot = Insert(newRoot, exisiting.Item1, exisiting.Item2);

        root = newRoot;
    }

    private QuadTreeNode<T> Find(QuadTreeNode<T> current, Point point)
    {
        if (current == null) return null;

        if (current.Point.X.IsEqual(point.X, tolerance) && current.Point.Y.IsEqual(point.Y, tolerance))
            return current;

        //south-west / north-west
        if (point.X.IsLessThan(current.Point.X, tolerance))
        {
            if (point.Y.IsLessThan(current.Point.Y, tolerance))
                return Find(current.Sw, point);
            return Find(current.Nw, point);
        }

        //south-east / north-east
        if (point.Y.IsLessThan(current.Point.Y, tolerance))
            return Find(current.Se, point);
        return Find(current.Ne, point);
    }
}

internal class QuadTreeNode<T>
{
    //marked as deleted
    internal bool IsDeleted;

    //quadrants
    internal QuadTreeNode<T> Nw, Ne, Se, Sw;

    //co-ordinate
    internal Point Point;

    //actual data if any associated with this point
    internal T Value;

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
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<QuadTreeNode<T>>(new[] { root.Ne, root.Nw, root.Se, root.Sw }.Where(x => x != null));
            current = root;

            if (!current.IsDeleted) return true;

            return MoveNext();
        }

        while (progress.Count > 0)
        {
            var next = progress.Pop();

            foreach (var child in new[] { next.Ne, next.Nw, next.Se, next.Sw }.Where(x => x != null))
                progress.Push(child);

            if (next.IsDeleted) continue;

            current = next;
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

    public Tuple<Point, T> Current => new(current.Point, current.Value);

    public void Dispose()
    {
        progress = null;
    }
}