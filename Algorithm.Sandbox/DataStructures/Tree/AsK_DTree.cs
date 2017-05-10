using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A concrete implementation of this interface is required
    /// when calling GetNearestNeigbour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDistanceCalculator<T> where T : IComparable
    {
        /// <summary>
        /// Compare eucledian distance between point a to point
        /// and point b to point
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="point"></param>
        /// <returns>similar result as IComparable</returns>
        int Compare(T[] a, T[] b, T[] point);

        /// <summary>
        /// Compare distance between point a to b
        /// and eucledian distance betwen point start to end
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>similar result as IComparable</returns>
        int Compare(T a, T b, T[] start, T[] end);
    }

    /// <summary>
    /// Kd tree node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsKDTreeNode<T> where T : IComparable
    {
        internal T[] Points { get; set; }

        internal AsKDTreeNode(int dimensions, AsKDTreeNode<T> parent)
        {
            Points = new T[dimensions];
            Parent = parent;
        }

        internal AsKDTreeNode<T> Left { get; set; }
        internal AsKDTreeNode<T> Right { get; set; }
        internal bool IsLeaf => Left == null && Right == null;

        internal AsKDTreeNode<T> Parent { get; set; }
        internal bool IsLeftChild => Parent.Left == this;
    }

    /// <summary>
    /// A multiDimensional Kd tree implementation (Unbalanced)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsKDTree<T> where T : IComparable
    {
        private int dimensions;
        internal AsKDTreeNode<T> Root;
        public int Count { get; private set; }
        public AsKDTree(int dimensions)
        {
            this.dimensions = dimensions;
            if (dimensions <= 0)
            {
                throw new Exception("Dimension should be greater than 0.");
            }
        }

        /// <summary>
        /// Inserts a new item to this Kd tree
        /// </summary>
        /// <param name="point"></param>
        public void Insert(T[] point)
        {
            if (Root == null)
            {
                Root = new AsKDTreeNode<T>(dimensions, null);
                Root.Points = new T[dimensions];
                CopyPoints(Root.Points, point);
                Count++;
                return;
            }

            Insert(Root, point, 0);
            Count++;
        }

        /// <summary>
        /// recursively find leaf node to insert
        /// at each level comparing against the next dimension
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <param name="depth"></param>
        private void Insert(AsKDTreeNode<T> currentNode, T[] point, int depth)
        {
            var currentDimension = depth % dimensions;

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new AsKDTreeNode<T>(dimensions, currentNode);
                    currentNode.Left.Points = new T[dimensions];
                    CopyPoints(currentNode.Left.Points, point);
                    return;
                }
                else
                {
                    depth++;
                    Insert(currentNode.Left, point, depth);
                }
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new AsKDTreeNode<T>(dimensions, currentNode);
                    currentNode.Right.Points = new T[dimensions];
                    CopyPoints(currentNode.Right.Points, point);
                    return;
                }
                else
                {
                    depth++;
                    Insert(currentNode.Right, point, depth);
                }

            }

        }

        /// <summary>
        /// delete point
        /// </summary>
        /// <param name="point"></param>
        public void Delete(T[] point)
        {
            if (Root == null)
            {
                throw new Exception("Empty tree");
            }

            Delete(Root, point, 0);
            Count--;
        }

        /// <summary>
        /// delete point by locating it recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <param name="depth"></param>
        private void Delete(AsKDTreeNode<T> currentNode, T[] point, int depth)
        {
            var currentDimension = depth % dimensions;

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (DoMatch(currentNode.Points, point))
                {
                    HandleDeleteCases(currentNode, point, depth);
                }
                else
                {
                    depth++;
                    Delete(currentNode.Left, point, depth);
                }
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (DoMatch(currentNode.Points, point))
                {
                    HandleDeleteCases(currentNode, point, depth);
                }
                else
                {
                    depth++;
                    Delete(currentNode.Right, point, depth);
                }

            }
        }

        /// <summary>
        /// Handle the three cases for deletion
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <param name="depth"></param>
        private void HandleDeleteCases(AsKDTreeNode<T> currentNode, T[] point, int depth)
        {
            //case one node is leaf
            if (currentNode.IsLeaf)
            {
                if (currentNode == Root)
                {
                    Root = null;
                }
                else
                {
                    if (currentNode.IsLeftChild)
                    {
                        currentNode.Parent.Left = null;
                    }
                    else
                    {
                        currentNode.Parent.Right = null;
                    }

                    return;
                }
            }

            //case 2 right subtree is not null
            if (currentNode.Right != null)
            {
                var minNode = FindMin(currentNode.Right, depth % dimensions, currentNode.Right);
                CopyPoints(currentNode.Points, minNode.Points);

                depth++;
                Delete(currentNode.Right, minNode.Points, depth);


            }
            //case 3 left subtree is not null
            else if (currentNode.Left != null)
            {
                var minNode = FindMin(currentNode.Left, depth % dimensions, currentNode.Left);
                CopyPoints(currentNode.Points, minNode.Points);

                depth++;
                Delete(currentNode.Left, minNode.Points, depth);

                //now move to right
                currentNode.Right = currentNode.Left;
                currentNode.Left = null;
            }


        }

        /// <summary>
        /// copy points2 to point1
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        private void CopyPoints(T[] points1, T[] points2)
        {

            for (int i = 0; i < points1.Length; i++)
            {
                points1[i] = points2[i];
            }
        }

        /// <summary>
        /// Find min value under this dimension
        /// </summary>
        /// <param name="right"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private AsKDTreeNode<T> FindMin(AsKDTreeNode<T> node, int dimension, AsKDTreeNode<T> currentMin)
        {
            if (node.Points[dimension].CompareTo(currentMin.Points[dimension]) < 0)
            {
                currentMin = node;
            }

            if (node.Left != null)
            {
                currentMin = FindMin(node.Left, dimension, currentMin);
            }

            if (node.Right != null)
            {
                currentMin = FindMin(node.Right, dimension, currentMin);
            }

            return currentMin;
        }

        /// <summary>
        /// Are these two points matching
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool DoMatch(T[] a, T[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].CompareTo(b[i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        //keep track of visited nodes during neighbour search
        //so that we don't visit them again
        //TODO: could have better ways
        private Dictionary<AsKDTreeNode<T>, bool> visitTracker
            = new Dictionary<AsKDTreeNode<T>, bool>();

        /// <summary>
        /// returns the nearest neigbour to point
        /// </summary>
        /// <param name="distanceCalculator"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public T[] FindNearestNeighbour(IDistanceCalculator<T> distanceCalculator, T[] point)
        {
            if (Root == null)
            {
                throw new Exception("Empty tree");
            }

            var result = FindNearestNeighbour(Root, visitTracker, point, 0, null, distanceCalculator).Points;
            Debug.WriteLine(visitTracker.Count);
            visitTracker.Clear();

            return result;
        }

        /// <summary>
        /// recursively find leaf node to insert
        /// at each level comparing against the next dimension
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="searchPoint"></param>
        /// <param name="depth"></param>
        private AsKDTreeNode<T> FindNearestNeighbour(AsKDTreeNode<T> currentNode,
            Dictionary<AsKDTreeNode<T>, bool> visited,
            T[] searchPoint, int depth,
            AsKDTreeNode<T> currentBest, IDistanceCalculator<T> distanceCalculator)
        {
            var currentDimension = depth % dimensions;

            //just do regular insertion procedure to until leaf is reached
            if (searchPoint[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (currentNode.Left == null)
                {
                    if (currentBest == null)
                    {
                        currentBest = currentNode;
                    }
                    else
                    {
                        currentBest = GetClosestNeigbour(distanceCalculator, currentBest, currentNode, searchPoint);
                    }
                }
                else
                {
                    currentBest = FindNearestNeighbour(currentNode.Left, visited,
                        searchPoint, ++depth, currentBest, distanceCalculator);
                }
            }
            else if (searchPoint[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (currentNode.Right == null)
                {
                    if (currentBest == null)
                    {
                        currentBest = currentNode;
                    }
                    else
                    {
                        currentBest = GetClosestNeigbour(distanceCalculator, currentBest, currentNode, searchPoint);
                    }
                }
                else
                {
                    currentBest = FindNearestNeighbour(currentNode.Right, visited,
                        searchPoint, ++depth, currentBest, distanceCalculator);
                }

            }

            //now recurse up from leaf updating current Best
            currentBest = GetClosestNeigbour(distanceCalculator, currentBest, currentNode, searchPoint);
            visited.Add(currentNode, false);


            if (currentNode.Right != null && !visited.ContainsKey(currentNode.Right))
            {
                //whether the distance between the splitting coordinate of the search point and current node
                //is lesser than the distance (overall coordinates) from the search point to the current best.
                if (distanceCalculator.Compare(searchPoint[currentDimension], currentNode.Points[currentDimension],
                    searchPoint, currentBest.Points) < 0)
                {
                    currentBest = FindNearestNeighbour(currentNode.Right, visited,
                        searchPoint, ++depth, currentBest,
                        distanceCalculator);

                }

                return currentBest;

            }

            if (currentNode.Left != null && !visited.ContainsKey(currentNode.Left))
            {
                //whether the distance between the splitting coordinate of the search point and current node
                //is lesser than the distance (overall coordinates) from the search point to the current best.
                if (distanceCalculator.Compare(searchPoint[currentDimension], currentNode.Points[currentDimension],
                    searchPoint, currentBest.Points) < 0)
                {
                    currentBest = FindNearestNeighbour(currentNode.Left, visited,
                        searchPoint, ++depth, currentBest,
                        distanceCalculator);
                }

                return currentBest;

            }


            return currentBest;
        }

        /// <summary>
        /// returns the closest node between currentBest and CurrentNode to point 
        /// </summary>
        /// <param name="distanceCalculator"></param>
        /// <param name="currentBest"></param>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private AsKDTreeNode<T> GetClosestNeigbour(IDistanceCalculator<T> distanceCalculator,
            AsKDTreeNode<T> currentBest, AsKDTreeNode<T> currentNode, T[] point)
        {
            if (distanceCalculator.Compare(currentBest.Points,
                currentNode.Points, point) < 0)
            {
                return currentBest;
            }

            return currentNode;
        }

        /// <summary>
        /// returns a list of nodes that are withing the given
        /// start and end ranges
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<T[]> GetInRange(T[] start, T[] end)
        {
            var visitTracker = new Dictionary<AsKDTreeNode<T>,bool>();

            var result = GetInRange(new List<T[]>(), Root, 
                visitTracker, start, end, 0);

            Debug.WriteLine(visitTracker.Count);

            return result;

        }

        /// <summary>
        /// recursively find points in given range
        /// </summary>
        /// <param name="result"></param>
        /// <param name="currentNode"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private List<T[]> GetInRange(List<T[]> result, 
            AsKDTreeNode<T> currentNode,
            Dictionary<AsKDTreeNode<T>, bool> visited,
            T[] start, T[] end, int depth)
        {
            if (currentNode == null)
                return result;

            var currentDimension = depth % dimensions;

            if (currentNode.IsLeaf)
            {
                //start is less than current node
                if (InRange(currentNode, start, end))
                {
                    result.Add(currentNode.Points);
                }
            }
            //if start is less than current
            //move left
            else
            {
                if (start[currentDimension].CompareTo(currentNode.Points[currentDimension]) <= 0)
                {
                    GetInRange(result, currentNode.Left, visited, start, end, ++depth);

                    //start is less than current node
                    if (!visited.ContainsKey(currentNode)
                        && InRange(currentNode, start, end))
                    {
                        result.Add(currentNode.Points);
                        visited.Add(currentNode, false);
                    }
                }
                //if start is greater than current
                //and end is greater than current
                //move right
                if (end[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
                {
                    GetInRange(result, currentNode.Right, visited, start, end, ++depth);

                    //start is less than current node
                    if (!visited.ContainsKey(currentNode)
                        && InRange(currentNode, start, end))
                    {
                        result.Add(currentNode.Points);
                        visited.Add(currentNode, false);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// is the point in node is within start & end points
        /// </summary>
        /// <param name="node"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private bool InRange(AsKDTreeNode<T> node, T[] start, T[] end)
        {
            for (int i = 0; i < node.Points.Length; i++)
            {
                //if not (start is less than node && end is greater than node)
                if (!(start[i].CompareTo(node.Points[i]) <= 0
                    && end[i].CompareTo(node.Points[i]) >= 0))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
