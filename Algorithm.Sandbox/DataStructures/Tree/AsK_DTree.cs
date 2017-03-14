using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A concrete implementation of this interface is required
    /// when calling GetNearestNeigbour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDistanceCalculator<T> where T : IComparable
    {
        int Compare(T[] a, T[] b, T[] point);

        int Compare(T a, T b, T[] start, T[] end);
    }

    public class AsKDTreeNode<T> where T : IComparable
    {
        public T[] Points { get; set; }

        public AsKDTreeNode(int dimensions, AsKDTreeNode<T> parent)
        {
            Points = new T[dimensions];
            Parent = parent;
        }

        internal AsKDTreeNode<T> Left { get; set; }
        internal AsKDTreeNode<T> Right { get; set; }
        public bool IsLeaf => Left == null && Right == null;

        public AsKDTreeNode<T> Parent { get; internal set; }
        public bool IsLeftChild => Parent.Left == this;
    }

    public class AsKDTree<T> where T : IComparable
    {
        private int dimensions;
        internal AsKDTreeNode<T> Root;
        public int Count { get; private set; }
        public AsKDTree(int dimensions)
        {
            this.dimensions = dimensions;
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
                Root.Points = point;
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
                    currentNode.Left.Points = point;
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
                    currentNode.Right.Points = point;
                    return;
                }
                else
                {
                    depth++;
                    Insert(currentNode.Right, point, depth);
                }

            }

        }

        public void Delete(T[] point)
        {
            if (Root == null)
            {
                throw new Exception("Empty tree");
            }

            Delete(Root, point, 0);
            Count--;
        }

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
        /// copy points2 to point2
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
        /// Does these two points are matching
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

        public T[] FindNearestNeighbour(IDistanceCalculator<T> distanceCalculator, T[] point)
        {
            if (Root == null)
            {
                throw new Exception("Empty tree");
            }

            return FindNearestNeighbour(Root, point, 0, null, distanceCalculator).Points;

        }

        /// <summary>
        /// recursively find leaf node to insert
        /// at each level comparing against the next dimension
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="point"></param>
        /// <param name="depth"></param>
        private AsKDTreeNode<T> FindNearestNeighbour(AsKDTreeNode<T> currentNode, T[] point, int depth,
            AsKDTreeNode<T> currentBest, IDistanceCalculator<T> distanceCalculator)
        {
            var currentDimension = depth % dimensions;

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (currentNode.Left == null)
                {
                    if (currentBest == null)
                    {
                        currentBest = currentNode;
                    }
                    else
                    {
                        currentBest = GetClosestNeigbour(distanceCalculator, currentBest, currentNode, point);
                    }
                }
                else
                {
                    depth++;
                    currentBest = FindNearestNeighbour(currentNode.Left, point, depth, currentBest, distanceCalculator);
                    depth--;
                }
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (currentNode.Right == null)
                {
                    if (currentBest == null)
                    {
                        currentBest = currentNode;
                    }
                    else
                    {
                        currentBest = GetClosestNeigbour(distanceCalculator, currentBest, currentNode, point);
                    }
                }
                else
                {
                    depth++;
                    currentBest = FindNearestNeighbour(currentNode.Right, point, depth, currentBest, distanceCalculator);
                    depth--;
                }

            }

            if (currentNode == null)
            {
                return currentBest;
            }

            currentBest = GetClosestNeigbour(distanceCalculator, currentBest, currentNode, point);

            if (currentNode.Right != null)
            {
                //whether the distance between the splitting coordinate of the search point and current node
                //is lesser than the distance (overall coordinates) from the search point to the current best.
                if (distanceCalculator.Compare(point[currentDimension], currentNode.Right.Points[currentDimension],
                    point, currentBest.Points) < 0)
                {
                    depth++;
                    currentBest = FindNearestNeighbour(currentNode.Right, point, depth, currentBest,
                        distanceCalculator);
                    depth--;
                }

            }

            if (currentNode.Left != null)
            {
                //whether the distance between the splitting coordinate of the search point and current node
                //is lesser than the distance (overall coordinates) from the search point to the current best.
                if (distanceCalculator.Compare(point[currentDimension], currentNode.Left.Points[currentDimension],
                    point, currentBest.Points) > 0)
                {
                    depth++;
                    currentBest = FindNearestNeighbour(currentNode.Left, point, depth, currentBest,
                        distanceCalculator);
                    depth--;
                }

            }


            return currentBest;
        }

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

        public AsArrayList<T[]> FindRange(T[] start, T[] end)
        {
            throw new NotImplementedException();
        }
    }
}
