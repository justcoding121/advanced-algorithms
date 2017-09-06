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
    internal class KDTreeNode<T> where T : IComparable
    {
        internal T[] Points { get; set; }

        internal KDTreeNode(int dimensions, KDTreeNode<T> parent)
        {
            Points = new T[dimensions];
            Parent = parent;
        }

        internal KDTreeNode<T> Left { get; set; }
        internal KDTreeNode<T> Right { get; set; }
        internal bool IsLeaf => Left == null && Right == null;

        internal KDTreeNode<T> Parent { get; set; }
        internal bool IsLeftChild => Parent.Left == this;
    }

    /// <summary>
    /// A multiDimensional Kd tree implementation (Unbalanced)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KDTree<T> where T : IComparable
    {
        private int dimensions;
        internal KDTreeNode<T> Root;
        public int Count { get; private set; }
        public KDTree(int dimensions)
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
                Root = new KDTreeNode<T>(dimensions, null);
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
        private void Insert(KDTreeNode<T> currentNode, T[] point, int depth)
        {
            var currentDimension = depth % dimensions;

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new KDTreeNode<T>(dimensions, currentNode);
                    currentNode.Left.Points = new T[dimensions];
                    CopyPoints(currentNode.Left.Points, point);
                    return;
                }
                else
                {
                    Insert(currentNode.Left, point, depth + 1);
                }
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new KDTreeNode<T>(dimensions, currentNode);
                    currentNode.Right.Points = new T[dimensions];
                    CopyPoints(currentNode.Right.Points, point);
                    return;
                }
                else
                {
                    Insert(currentNode.Right, point, depth + 1);
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
        private void Delete(KDTreeNode<T> currentNode, T[] point, int depth)
        {
            if(currentNode == null)
            {
                throw new Exception("Given deletion point do not exist in this kd tree.");
            }

            var currentDimension = depth % dimensions;

            if (DoMatch(currentNode.Points, point))
            {
                HandleDeleteCases(currentNode, point, depth);
                return;
            }

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                Delete(currentNode.Left, point, depth + 1);
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                Delete(currentNode.Right, point, depth + 1);
            }
        }

        /// <summary>
        /// Handle the three cases for deletion
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <param name="depth"></param>
        private void HandleDeleteCases(KDTreeNode<T> currentNode, T[] point, int depth)
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
        private KDTreeNode<T> FindMin(KDTreeNode<T> node, int searchdimension, int depth)
        {
            var currentDimension = depth % dimensions;

            if (currentDimension == searchdimension)
            {
                if (node.Left == null)
                {
                    return node;
                }

                return FindMin(node.Left, searchdimension, depth + 1);
            }

            KDTreeNode<T> leftMin = null;
            if (node.Left != null)
            {
                leftMin = FindMin(node.Left, searchdimension, depth + 1);
            }

            KDTreeNode<T> rightMin = null;
            if (node.Right != null)
            {
                rightMin = FindMin(node.Right, searchdimension, depth + 1);
            }

            return min(node, leftMin, rightMin, searchdimension);

        }

        /// <summary>
        /// returns min of given three nodes on search dimension
        /// </summary>
        /// <param name="node">Should not be null</param>
        /// <param name="leftMin"></param>
        /// <param name="rightMin"></param>
        /// <param name="searchdimension"></param>
        /// <returns></returns>
        private KDTreeNode<T> min(KDTreeNode<T> node,
            KDTreeNode<T> leftMin, KDTreeNode<T> rightMin,
            int searchdimension)
        {
            var min = node;

            if (leftMin != null && min.Points[searchdimension]
                .CompareTo(leftMin.Points[searchdimension]) > 0)
            {
                min = leftMin;
            }

            if (rightMin != null && min.Points[searchdimension]
             .CompareTo(rightMin.Points[searchdimension]) > 0)
            {
                min = rightMin;
            }

            return min;
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

            return FindNearestNeighbour(Root, point, 0, distanceCalculator).Points;
        }

        /// <summary>
        /// recursively find leaf node to insert
        /// at each level comparing against the next dimension
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="searchPoint"></param>
        /// <param name="depth"></param>
        private KDTreeNode<T> FindNearestNeighbour(KDTreeNode<T> currentNode,
            T[] searchPoint, int depth,
            IDistanceCalculator<T> distanceCalculator)
        {
            var currentDimension = depth % dimensions;
            KDTreeNode<T> currentBest = null;

            var compareResult = searchPoint[currentDimension]
                .CompareTo(currentNode.Points[currentDimension]);

            //move toward search point until leaf is reached
            if (compareResult < 0)
            {
                if (currentNode.Left != null)
                {
                    currentBest = FindNearestNeighbour(currentNode.Left,
                        searchPoint, depth + 1, distanceCalculator);
                }
                else
                {
                    currentBest = currentNode;
                }

                //currentBest is greater than point to current node distance
                //or if right node sits on split plane
                //then also move left
                if (currentNode.Right != null &&
                    (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension], searchPoint, currentBest.Points) < 0
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
                {
                    currentBest = FindNearestNeighbour(currentNode.Right,
                        searchPoint, depth + 1, distanceCalculator);

                }
                else
                {
                    currentBest = currentNode;
                }

                //currentBest is greater than point to current node distance
                //or if search point lies on split plane
                //then also move left
                if (currentNode.Left != null
                    && (distanceCalculator.Compare(currentNode.Points[currentDimension], searchPoint[currentDimension], searchPoint, currentBest.Points) < 0 || compareResult == 0))
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
        /// returns the closest node between currentBest and CurrentNode to point 
        /// </summary>
        /// <param name="distanceCalculator"></param>
        /// <param name="currentBest"></param>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private KDTreeNode<T> GetClosestToPoint(IDistanceCalculator<T> distanceCalculator,
            KDTreeNode<T> currentBest, KDTreeNode<T> currentNode, T[] point)
        {
            if (distanceCalculator.Compare(currentBest.Points,
                currentNode.Points, point) < 0)
            {
                return currentBest;
            }

            return currentNode;
        }

        /// <summary>
        /// returns a list of nodes that are withing the given area
        /// start and end ranges
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<T[]> GetInRange(T[] start, T[] end)
        {
            var result = GetInRange(new List<T[]>(), Root,
                 start, end, 0);

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
            KDTreeNode<T> currentNode,
            T[] start, T[] end, int depth)
        {
            if (currentNode == null)
            {
                return result;
            }

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
                if (start[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
                {
                    GetInRange(result, currentNode.Left, start, end, depth + 1);

                }
                //if start is greater than current
                //and end is greater than current
                //move right
                if (end[currentDimension].CompareTo(currentNode.Points[currentDimension]) > 0)
                {
                    GetInRange(result, currentNode.Right,  start, end, depth + 1);
                    
                }

                //start is less than current node
                if (InRange(currentNode, start, end))
                {
                    result.Add(currentNode.Points);
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
        private bool InRange(KDTreeNode<T> node, T[] start, T[] end)
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
