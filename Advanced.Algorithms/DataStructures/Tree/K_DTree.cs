using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A multiDimensional k-d tree implementation (Unbalanced).
    /// </summary>
    public class KDTree<T> : IEnumerable<T[]> where T : IComparable
    {
        private int dimensions;
        private KDTreeNode<T> root;

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
        /// Inserts a new item to this Kd tree.
        /// Time complexity: O(log(n))
        /// </summary>
        public void Insert(T[] point)
        {
            if (root == null)
            {
                root = new KDTreeNode<T>(dimensions, null);
                root.Points = new T[dimensions];
                copyPoints(root.Points, point);
                Count++;
                return;
            }

            insert(root, point, 0);
            Count++;
        }

        /// <summary>
        /// Recursively find leaf node to insert
        /// at each level comparing against the next dimension.
        /// </summary>
        private void insert(KDTreeNode<T> currentNode, T[] point, int depth)
        {
            var currentDimension = depth % dimensions;

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new KDTreeNode<T>(dimensions, currentNode);
                    currentNode.Left.Points = new T[dimensions];
                    copyPoints(currentNode.Left.Points, point);
                    return;
                }
                else
                {
                    insert(currentNode.Left, point, depth + 1);
                }
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new KDTreeNode<T>(dimensions, currentNode);
                    currentNode.Right.Points = new T[dimensions];
                    copyPoints(currentNode.Right.Points, point);
                    return;
                }
                else
                {
                    insert(currentNode.Right, point, depth + 1);
                }

            }

        }

        /// <summary>
        /// Delete point.
        /// Time complexity: O(log(n))
        /// </summary>
        public void Delete(T[] point)
        {
            if (root == null)
            {
                throw new Exception("Empty tree");
            }

            delete(root, point, 0);
            Count--;
        }

        /// <summary>
        /// Delete point by locating it recursively.
        /// </summary>
        private void delete(KDTreeNode<T> currentNode, T[] point, int depth)
        {
            if(currentNode == null)
            {
                throw new Exception("Given deletion point do not exist in this kd tree.");
            }

            var currentDimension = depth % dimensions;

            if (DoMatch(currentNode.Points, point))
            {
                handleDeleteCases(currentNode, point, depth);
                return;
            }

            if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) < 0)
            {
                delete(currentNode.Left, point, depth + 1);
            }
            else if (point[currentDimension].CompareTo(currentNode.Points[currentDimension]) >= 0)
            {
                delete(currentNode.Right, point, depth + 1);
            }
        }

        /// <summary>
        /// Handle the three cases for deletion.
        /// </summary>
        private void handleDeleteCases(KDTreeNode<T> currentNode, T[] point, int depth)
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
                var minNode = findMin(currentNode.Right, depth % dimensions, depth + 1);
                copyPoints(currentNode.Points, minNode.Points);

                delete(currentNode.Right, minNode.Points, depth + 1);


            }
            //case 3 left subtree is not null
            else if (currentNode.Left != null)
            {
                var minNode = findMin(currentNode.Left, depth % dimensions, depth + 1);
                copyPoints(currentNode.Points, minNode.Points);

                delete(currentNode.Left, minNode.Points, depth + 1);

                //now move to right
                currentNode.Right = currentNode.Left;
                currentNode.Left = null;
            }

        }

        /// <summary>
        /// Copy points2 to point1.
        /// </summary>
        private void copyPoints(T[] points1, T[] points2)
        {

            for (int i = 0; i < points1.Length; i++)
            {
                points1[i] = points2[i];
            }
        }

        /// <summary>
        /// Find min value under this dimension.
        /// </summary>
        private KDTreeNode<T> findMin(KDTreeNode<T> node, int searchdimension, int depth)
        {
            var currentDimension = depth % dimensions;

            if (currentDimension == searchdimension)
            {
                if (node.Left == null)
                {
                    return node;
                }

                return findMin(node.Left, searchdimension, depth + 1);
            }

            KDTreeNode<T> leftMin = null;
            if (node.Left != null)
            {
                leftMin = findMin(node.Left, searchdimension, depth + 1);
            }

            KDTreeNode<T> rightMin = null;
            if (node.Right != null)
            {
                rightMin = findMin(node.Right, searchdimension, depth + 1);
            }

            return min(node, leftMin, rightMin, searchdimension);

        }

        /// <summary>
        /// Returns min of given three nodes on search dimension.
        /// </summary>
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
        /// Are these two points matching.
        /// </summary>
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
        /// Returns the nearest neigbour to point.
        /// Time complexity: O(log(n))
        /// </summary>
        public T[] NearestNeighbour(IDistanceCalculator<T> distanceCalculator, T[] point)
        {
            if (root == null)
            {
                throw new Exception("Empty tree");
            }

            return findNearestNeighbour(root, point, 0, distanceCalculator).Points;
        }

        /// <summary>
        /// Recursively find leaf node to insert
        /// at each level comparing against the next dimension.
        /// </summary>
        private KDTreeNode<T> findNearestNeighbour(KDTreeNode<T> currentNode,
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
                    currentBest = findNearestNeighbour(currentNode.Left,
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
                    var rightBest = findNearestNeighbour(currentNode.Right,
                          searchPoint, depth + 1,
                          distanceCalculator);

                    currentBest = getClosestToPoint(distanceCalculator, currentBest, rightBest, searchPoint);
                }
                //now recurse up from leaf updating current Best
                currentBest = getClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);

            }
            else if (compareResult >= 0)
            {
                if (currentNode.Right != null)
                {
                    currentBest = findNearestNeighbour(currentNode.Right,
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
                    var leftBest = findNearestNeighbour(currentNode.Left,
                          searchPoint, depth + 1,
                          distanceCalculator);

                    currentBest = getClosestToPoint(distanceCalculator, currentBest, leftBest, searchPoint);
                }

                //now recurse up from leaf updating current Best
                currentBest = getClosestToPoint(distanceCalculator, currentBest, currentNode, searchPoint);

            }


            return currentBest;
        }

        /// <summary>
        /// Returns the closest node between currentBest and CurrentNode to point 
        /// </summary>
        private KDTreeNode<T> getClosestToPoint(IDistanceCalculator<T> distanceCalculator,
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
        /// Returns a list of nodes that are withing the given area
        /// start and end ranges
        /// </summary>
        public List<T[]> RangeSearch(T[] start, T[] end)
        {
            var result = rangeSearch(new List<T[]>(), root,
                 start, end, 0);

            return result;

        }

        /// <summary>
        /// Recursively find points in given range.
        /// </summary>
        private List<T[]> rangeSearch(List<T[]> result,
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
                if (inRange(currentNode, start, end))
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
                    rangeSearch(result, currentNode.Left, start, end, depth + 1);

                }
                //if start is greater than current
                //and end is greater than current
                //move right
                if (end[currentDimension].CompareTo(currentNode.Points[currentDimension]) > 0)
                {
                    rangeSearch(result, currentNode.Right,  start, end, depth + 1);
                    
                }

                //start is less than current node
                if (inRange(currentNode, start, end))
                {
                    result.Add(currentNode.Points);
                }
            }

            return result;
        }

        /// <summary>
        /// Is the point in node is within start and end points.
        /// </summary>
        private bool inRange(KDTreeNode<T> node, T[] start, T[] end)
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return new KDTreeEnumerator<T>(root);
        }
       
    }

    /// <summary>
    /// k-d tree node.
    /// </summary>
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
    /// A concrete implementation of this interface is required
    /// when calling NearestNeigbour() for k-d tree.
    /// </summary>
    public interface IDistanceCalculator<T> where T : IComparable
    {
        /// <summary>
        /// Compare the distance between point A to point
        /// and point B to point.
        /// </summary>
        /// <returns>similar result as IComparable.</returns>
        int Compare(T[] a, T[] b, T[] point);

        /// <summary>
        /// Compare distance between point A to B
        /// and the distance between point Start to End.
        /// </summary>
        /// <returns>similar result as IComparabl.e</returns>
        int Compare(T a, T b, T[] start, T[] end);
    }

    internal class KDTreeEnumerator<T> : IEnumerator<T[]> where T : IComparable
    {
        private readonly KDTreeNode<T> root;
        private Stack<KDTreeNode<T>> progress;

        internal KDTreeEnumerator(KDTreeNode<T> root)
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
                progress = new Stack<KDTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
                Current = root.Points;
                return true;
            }

            if (progress.Count > 0)
            {
                var next = progress.Pop();
                Current = next.Points;

                foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null))
                {
                    progress.Push(node);
                }

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
}
