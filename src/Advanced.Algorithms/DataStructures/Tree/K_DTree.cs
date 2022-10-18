using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A multiDimensional k-d tree implementation (Unbalanced).
/// </summary>
public class KdTree<T> : IEnumerable<T[]> where T : IComparable
{
    private readonly int dimensions;
    private KdTreeNode<T> root;

    public KdTree(int dimensions)
    {
        this.dimensions = dimensions;
        if (dimensions <= 0) throw new Exception("Dimension should be greater than 0.");
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return new KdTreeEnumerator<T>(root);
    }

    /// <summary>
    ///     Inserts a new item to this Kd tree.
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Insert(T[] point)
    {
        if (root == null)
        {
            root = new KdTreeNode<T>(dimensions, null);
            root.Points = new T[dimensions];
            CopyPoints(root.Points, point);
            Count++;
            return;
        }

        Insert(root, point, 0);
        Count++;
    }

    /// <summary>
    ///     Recursively find leaf node to insert
    ///     at each level comparing against the next dimension.
    /// </summary>
    private void Insert(KdTreeNode<T> currentNode, T[] point, int depth)
    {
        var currentDimension = depth % dimensions;

        if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
        {
            if (currentNode.Left == null)
            {
                currentNode.Left = new KdTreeNode<T>(dimensions, currentNode);
                currentNode.Left.Points = new T[dimensions];
                CopyPoints(currentNode.Left.Points, point);
                return;
            }

            Insert(currentNode.Left, point, depth + 1);
        }
        else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
        {
            if (currentNode.Right == null)
            {
                currentNode.Right = new KdTreeNode<T>(dimensions, currentNode);
                currentNode.Right.Points = new T[dimensions];
                CopyPoints(currentNode.Right.Points, point);
                return;
            }

            Insert(currentNode.Right, point, depth + 1);
        }
    }

    /// <summary>
    ///     Delete point.
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Delete(T[] point)
    {
        if (root == null) throw new Exception("Empty tree");

        Delete(root, point, 0);
        Count--;
    }

    /// <summary>
    ///     Delete point by locating it recursively.
    /// </summary>
    private void Delete(KdTreeNode<T> currentNode, T[] point, int depth)
    {
        if (currentNode == null) throw new Exception("Given deletion point do not exist in this kd tree.");

        var currentDimension = depth % dimensions;

        if (DoMatch(currentNode.Points, point))
        {
            HandleDeleteCases(currentNode, point, depth);
            return;
        }

        if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            Delete(currentNode.Left, point, depth + 1);
        else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            Delete(currentNode.Right, point, depth + 1);
    }

    /// <summary>
    ///     Handle the three cases for deletion.
    /// </summary>
    private void HandleDeleteCases(KdTreeNode<T> currentNode, T[] point, int depth)
    {
        //case one node is leaf
        if (currentNode.IsLeaf)
        {
            if (currentNode == root)
            {
                root = null;
            }
            else
            {
                if (currentNode.IsLeftChild)
                    currentNode.Parent.Left = null;
                else
                    currentNode.Parent.Right = null;

                return;
            }
        }

        //case 2 right subtree is not null
        if (currentNode.Right != null)
        {
            var minNode = FindMin(currentNode.Right, depth % dimensions, depth + 1);
            CopyPoints(currentNode.Points, minNode.Points);

            Delete(currentNode.Right, minNode.Points, depth + 1);
        }
        //case 3 left subtree is not null
        else if (currentNode.Left != null)
        {
            var minNode = FindMin(currentNode.Left, depth % dimensions, depth + 1);
            CopyPoints(currentNode.Points, minNode.Points);

            Delete(currentNode.Left, minNode.Points, depth + 1);

            //now move to right
            currentNode.Right = currentNode.Left;
            currentNode.Left = null;
        }
    }

    /// <summary>
    ///     Copy points2 to point1.
    /// </summary>
    private void CopyPoints(T[] points1, T[] points2)
    {
        for (var i = 0; i < points1.Length; i++) points1[i] = points2[i];
    }

    /// <summary>
    ///     Find min value under this dimension.
    /// </summary>
    private KdTreeNode<T> FindMin(KdTreeNode<T> node, int searchdimension, int depth)
    {
        var currentDimension = depth % dimensions;

        if (currentDimension == searchdimension)
        {
            if (node.Left == null) return node;

            return FindMin(node.Left, searchdimension, depth + 1);
        }

        KdTreeNode<T> leftMin = null;
        if (node.Left != null) leftMin = FindMin(node.Left, searchdimension, depth + 1);

        KdTreeNode<T> rightMin = null;
        if (node.Right != null) rightMin = FindMin(node.Right, searchdimension, depth + 1);

        return Min(node, leftMin, rightMin, searchdimension);
    }

    /// <summary>
    ///     Returns min of given three nodes on search dimension.
    /// </summary>
    private KdTreeNode<T> Min(KdTreeNode<T> node,
        KdTreeNode<T> leftMin, KdTreeNode<T> rightMin,
        int searchdimension)
    {
        var min = node;

        if (leftMin != null && min.Points[searchdimension]
                .CompareTo(leftMin.Points[searchdimension]) > 0)
            min = leftMin;

        if (rightMin != null && min.Points[searchdimension]
                .CompareTo(rightMin.Points[searchdimension]) > 0)
            min = rightMin;

        return min;
    }

    /// <summary>
    ///     Are these two points matching.
    /// </summary>
    private bool DoMatch(T[] a, T[] b)
    {
        for (var i = 0; i < a.Length; i++)
            if (a[i].CompareTo(b[i]) != 0)
                return false;

        return true;
    }

    /// <summary>
    ///     Returns the nearest neigbour to point.
    ///     Time complexity: O(log(n))
    /// </summary>
    public T[] NearestNeighbour(IDistanceCalculator<T> distanceCalculator, T[] point)
    {
        if (root == null) throw new Exception("Empty tree");

        return FindNearestNeighbour(root, point, 0, distanceCalculator).Points;
    }

    /// <summary>
    ///     Recursively find leaf node to insert
    ///     at each level comparing against the next dimension.
    /// </summary>
    private KdTreeNode<T> FindNearestNeighbour(KdTreeNode<T> currentNode,
        T[] searchPoint, int depth,
        IDistanceCalculator<T> distanceCalculator)
    {
        var currentDimension = depth % dimensions;
        KdTreeNode<T> currentBest = null;

        var compareResult = searchPoint[currentDimension]
            .CompareTo(currentNode.Points[currentDimension]);

        //move toward search point until leaf is reached
        if (compareResult < 0)
        {
            if (currentNode.Left != null)
                currentBest = FindNearestNeighbour(currentNode.Left,
                    searchPoint, depth + 1, distanceCalculator);
            else
                currentBest = currentNode;

            //currentBest is greater than point to current node distance
            //or if right node sits on split plane
            //then also move left
            if (currentNode.Right != null &&
                (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension],
                     searchPoint, currentBest.Points) < 0
                 || currentNode.Right.Points[currentDimension]
                     .CompareTo(currentNode.Points[currentDimension]) == 0))
            {
                var rightBest = FindNearestNeighbour(currentNode.Right,
                    searchPoint, depth + 1,
                    distanceCalculator);

                currentBest = GetClosestToPoint(distanceCalculator, currentBest, rightBest, searchPoint);
            }

            //now recurse up from leaf updating current Best
            currentBest = GetClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);
        }
        else if (compareResult >= 0)
        {
            if (currentNode.Right != null)
                currentBest = FindNearestNeighbour(currentNode.Right,
                    searchPoint, depth + 1, distanceCalculator);
            else
                currentBest = currentNode;

            //currentBest is greater than point to current node distance
            //or if search point lies on split plane
            //then also move left
            if (currentNode.Left != null
                && (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension],
                    searchPoint, currentBest.Points) < 0 || compareResult == 0))
            {
                var leftBest = FindNearestNeighbour(currentNode.Left,
                    searchPoint, depth + 1,
                    distanceCalculator);

                currentBest = GetClosestToPoint(distanceCalculator, currentBest, leftBest, searchPoint);
            }

            //now recurse up from leaf updating current Best
            currentBest = GetClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);
        }


        return currentBest;
    }

    /// <summary>
    ///     Returns the closest node between currentBest and CurrentNode to point
    /// </summary>
    private KdTreeNode<T> GetClosestToPoint(IDistanceCalculator<T> distanceCalculator,
        KdTreeNode<T> currentBest, KdTreeNode<T> currentNode, T[] point)
    {
        if (distanceCalculator.Compare(currentBest.Points,
                currentNode.Points, point) < 0)
            return currentBest;

        return currentNode;
    }

    /// <summary>
    ///     Returns a list of nodes that are withing the given area
    ///     start and end ranges
    /// </summary>
    public List<T[]> RangeSearch(T[] start, T[] end)
    {
        var result = RangeSearch(new List<T[]>(), root,
            start, end, 0);

        return result;
    }

    /// <summary>
    ///     Recursively find points in given range.
    /// </summary>
    private List<T[]> RangeSearch(List<T[]> result,
        KdTreeNode<T> currentNode,
        T[] start, T[] end, int depth)
    {
        if (currentNode == null) return result;

        var currentDimension = depth % dimensions;

        if (currentNode.IsLeaf)
        {
            //start is less than current node
            if (InRange(currentNode, start, end)) result.Add(currentNode.Points);
        }
        //if start is less than current
        //move left
        else
        {
            if (start[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
                RangeSearch(result, currentNode.Left, start, end, depth + 1);
            //if start is greater than current
            //and end is greater than current
            //move right
            if (end[currentDimension].CompareTo(currentNode.Points[currentDimension]) > 0)
                RangeSearch(result, currentNode.Right, start, end, depth + 1);

            //start is less than current node
            if (InRange(currentNode, start, end)) result.Add(currentNode.Points);
        }

        return result;
    }

    /// <summary>
    ///     Is the point in node is within start and end points.
    /// </summary>
    private bool InRange(KdTreeNode<T> node, T[] start, T[] end)
    {
        for (var i = 0; i < node.Points.Length; i++)
            //if not (start is less than node && end is greater than node)
            if (!(start[i].CompareTo(node.Points[i]) <= 0
                  && end[i].CompareTo(node.Points[i]) >= 0))
                return false;

        return true;
    }
}

/// <summary>
///     k-d tree node.
/// </summary>
internal class KdTreeNode<T> where T : IComparable
{
    internal KdTreeNode(int dimensions, KdTreeNode<T> parent)
    {
        Points = new T[dimensions];
        Parent = parent;
    }

    internal T[] Points { get; set; }

    internal KdTreeNode<T> Left { get; set; }
    internal KdTreeNode<T> Right { get; set; }
    internal bool IsLeaf => Left == null && Right == null;

    internal KdTreeNode<T> Parent { get; set; }
    internal bool IsLeftChild => Parent.Left == this;
}

/// <summary>
///     A concrete implementation of this interface is required
///     when calling NearestNeigbour() for k-d tree.
/// </summary>
public interface IDistanceCalculator<T> where T : IComparable
{
    /// <summary>
    ///     Compare the distance between point A to point
    ///     and point B to point.
    /// </summary>
    /// <returns>similar result as IComparable.</returns>
    int Compare(T[] a, T[] b, T[] point);

    /// <summary>
    ///     Compare distance between point A to B
    ///     and the distance between point Start to End.
    /// </summary>
    /// <returns>similar result as IComparabl.e</returns>
    int Compare(T a, T b, T[] start, T[] end);
}

internal class KdTreeEnumerator<T> : IEnumerator<T[]> where T : IComparable
{
    private readonly KdTreeNode<T> root;
    private Stack<KdTreeNode<T>> progress;

    internal KdTreeEnumerator(KdTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<KdTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
            Current = root.Points;
            return true;
        }

        if (progress.Count > 0)
        {
            var next = progress.Pop();
            Current = next.Points;

            foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null)) progress.Push(node);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = null;
    }

    public T[] Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }
}