using Advanced.Algorithms.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    ///     Minimum bounded rectangle (MBR).
    /// </summary>
    internal class MBRectangle : Rectangle
    {
        /// <summary>
        ///     For non-leafs we won't have polygon inside.
        /// </summary>
        internal MBRectangle() { }

        /// <summary>
        ///     For leafs we will have the polygon inside.
        /// </summary>
        /// <param name="polygon"></param>
        internal MBRectangle(Polygon polygon)
        {
            Polygon = polygon;
        }

        /// <summary>
        ///     The actual polygon if this MBR is a leaf.
        /// </summary>
        internal Polygon Polygon { get; set; }

        /// <summary>
        ///     returns the required enlargement by area to fit the given polygon inside this minimum bounded rectangle.
        /// </summary>
        /// <param name="polygonToFit">The polygon to fit inside current MBR.</param>
        /// <returns></returns>
        internal double GetEnlargementArea(Polygon polygonToFit)
        {
            return Math.Abs(getMergedRectangle(polygonToFit.GetContainingRectangle()).Area() - Area());
        }

        /// <summary>
        ///     returns the required enlargement area to fit the given rectangle inside this minimum bounded rectangle.
        /// </summary>
        /// <param name="polygonToFit">The rectangle to fit inside current MBR.</param>
        /// <returns></returns>
        internal double GetEnlargementArea(MBRectangle rectangleToFit)
        {
            return Math.Abs(getMergedRectangle(rectangleToFit).Area() - Area());
        }

        /// <summary>
        ///     Merge the current rectangle with given rectangle. 
        /// </summary>
        /// <param name="rectangleToMerge">The new rectangle.</param>
        /// <returns></returns>
        private Rectangle getMergedRectangle(MBRectangle rectangleToMerge)
        {
            var mergedRectangle = new MBRectangle();

            mergedRectangle.RightBottomCorner.X = RightBottomCorner.X < rectangleToMerge.RightBottomCorner.X ? rectangleToMerge.RightBottomCorner.X : RightBottomCorner.X;
            mergedRectangle.RightBottomCorner.Y = RightBottomCorner.Y > rectangleToMerge.RightBottomCorner.Y ? rectangleToMerge.RightBottomCorner.Y : RightBottomCorner.Y;

            mergedRectangle.LeftTopCorner.X = LeftTopCorner.X > rectangleToMerge.LeftTopCorner.X ? rectangleToMerge.LeftTopCorner.X : LeftTopCorner.X;
            mergedRectangle.LeftTopCorner.Y = LeftTopCorner.Y < rectangleToMerge.LeftTopCorner.Y ? rectangleToMerge.LeftTopCorner.Y : LeftTopCorner.Y;

            return mergedRectangle;
        }
    }

    internal class RTreeNode
    {
        /// <summary>
        /// Array Index of this node in parent's Children array
        /// </summary>
        internal int Index;

        internal MBRectangle MBRectangle { get; set; }
        internal int KeyCount;

        internal RTreeNode Parent { get; set; }
        internal RTreeNode[] Children { get; set; }

        internal bool IsLeaf => Children[0] == null;

        internal RTreeNode(int maxKeysPerNode, RTreeNode parent)
        {
            Parent = parent;
            Children = new RTreeNode[maxKeysPerNode];
        }

        internal int GetMedianIndex()
        {
            return (KeyCount / 2) + 1;
        }

        /// <summary>
        /// Select the node whose MBR will require the minimum area enlargement
        /// to cover the new polygon to insert.
        /// </summary>
        /// <param name="newPolygon"></param>
        /// <returns></returns>
        internal RTreeNode GetMinimumEnlargementAreaMBR(Polygon newPolygon)
        {
            //order by enlargement area
            //then by minimum area
            var min = Children.Select(x=>x.MBRectangle)
                              .OrderBy(x => x.GetEnlargementArea(newPolygon))
                              .ThenBy(x => x.Area())
                              .FirstOrDefault();

            return Children[Array.FindIndex(Children, m => m.MBRectangle == min)];
        }

    }

    /// <summary>
    /// A RTree implementation
    /// TODO support initial  bulk loading
    /// TODO: make sure duplicates are handled correctly if its not already
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RTree
    {
        public int Count { get; private set; }

        internal RTreeNode Root;

        private readonly int maxKeysPerNode;
        private int minKeysPerNode => maxKeysPerNode / 2;

        public RTree(int maxKeysPerNode)
        {
            if (maxKeysPerNode < 3)
            {
                throw new Exception("Max keys per node should be atleast 3.");
            }

            this.maxKeysPerNode = maxKeysPerNode;
        }

        /// <summary>
        /// Inserts  to B-Tree
        /// </summary>
        /// <param name="newPolygon"></param>
        public void Insert(Polygon newPolygon)
        {
            if (Root == null)
            {
                Root = new RTreeNode(maxKeysPerNode, null) { MBRectangle = new MBRectangle(newPolygon) };
                Root.KeyCount++;
                Count++;
                return;
            }

            var leafToInsert = findInsertionLeaf(Root, newPolygon);

            insertAndSplit(ref leafToInsert, newPolygon, null, null);
            Count++;
        }


        /// <summary>
        ///     Find the leaf node to start initial insertion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newPolygon"></param>
        /// <returns></returns>
        private RTreeNode findInsertionLeaf(RTreeNode node, Polygon newPolygon)
        {
            //if leaf then its time to insert
            if (node.IsLeaf)
            {
                return node;
            }

            return findInsertionLeaf(node.GetMinimumEnlargementAreaMBR(newPolygon), newPolygon);
        }

        /// <summary>
        ///     Insert and split recursively up until no split is required
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <param name="newValueLeft"></param>
        /// <param name="newValueRight"></param>
        private void insertAndSplit(ref RTreeNode node, Polygon newValue,
            RTreeNode newValueLeft, RTreeNode newValueRight)
        {
            //add new item to current node
            //this increases the height of RTree tree by one by adding a new root at top
            if (node == null)
            {
                node = new RTreeNode(maxKeysPerNode, null);
                Root = node;
            }

            //if node is full then split node
            //and insert new MBR to parent.
            if (node.KeyCount == maxKeysPerNode)
            {
                //divide the current node values + new Node as left & right sub nodes
                var left = new RTreeNode(maxKeysPerNode, null);
                var right = new RTreeNode(maxKeysPerNode, null);

                var allEntries = node.Children.Select(x=>x.MBRectangle).ToList();
                allEntries.Add(newValue.GetContainingRectangle());

                //Let E be the set consisting of all current entries and new entry.
                //Select as seeds two entries left, right ∈ E, where the distance between
                //left and right is the maximum among all other pairs of entries from E
                Tuple<MBRectangle, MBRectangle> distantPairs = getDistantPairs(allEntries);

                left.MBRectangle = distantPairs.Item1;
                right.MBRectangle = distantPairs.Item2;

                allEntries = allEntries
                                .Where(x => x != distantPairs.Item1 && x != distantPairs.Item2)
                                .ToList();

                /*Examine the remaining members of E one by one and assign them
                to L or R, depending on which of the MBRs of these nodes
                will require the minimum area enlargement so as to cover this entry.
                If a tie occurs, assign the entry to the node whose MBR has the smaller area.
                If a tie occurs again, assign the entry to the node that contains the smaller number of entries*/
                while (allEntries.Count > 0)
                {
                    var current = allEntries[allEntries.Count - 1];

                    var leftArea = left.MBRectangle.GetEnlargementArea(current);
                    var rightArea = right.MBRectangle.GetEnlargementArea(current);

                    if (leftArea == rightArea)
                    {
                        if(left.MBRectangle.Area() == right.MBRectangle.Area())
                        {
                           
                        }
                    }
                    if (leftArea < rightArea)
                    {
                        left.MBRectangle = current;
                    }
                    else
                    {
                        right.MBRectangle = current;
                    }

                    allEntries.RemoveAt(allEntries.Count - 1);

                    var remaining = allEntries.Count;

                    /*if during the assignment of entries, there remain λ entries to be assigned
                    and the one node contains minKeysPerNode − λ entries then
                    assign all the remaining entries to this node without considering
                    the aforementioned criteria
                    so that the node will contain at least minKeysPerNode entries */
                    if (left.KeyCount == minKeysPerNode - remaining)
                    {
                        foreach (var entry in allEntries)
                        {
                            left.MBRectangle = entry;
                        }
                        allEntries.Clear();
                    }
                    else if (right.KeyCount == maxKeysPerNode / 2 - remaining)
                    {
                        foreach (var entry in allEntries)
                        {
                            right.MBRectangle = entry;
                        }
                        allEntries.Clear();
                    }
                }

                //insert overflow element (newMedian) to parent
                //var parent = node.Parent;
                //insertAndSplit(ref parent, newMedian, left, right);

            }
            //newValue have room to fit in this node
            else
            {
                insertNonFullNode(ref node, newValue, newValueLeft, newValueRight);
            }

        }

        /// <summary>
        ///     Get the pairs of rectangles farther apart by comparing enlargement areas.
        /// </summary>
        /// <param name="allEntries"></param>
        /// <returns></returns>
        private Tuple<MBRectangle, MBRectangle> getDistantPairs(List<MBRectangle> allEntries)
        {
            Tuple<MBRectangle, MBRectangle> result = null;

            double maxArea = 0.0;
            for (int i = 0; i < allEntries.Count; i++)
            {
                for (int j = i; j < allEntries.Count; j++)
                {
                    var currentArea = allEntries[i].GetEnlargementArea(allEntries[j]);
                    if (currentArea > maxArea)
                    {
                        result = new Tuple<MBRectangle, MBRectangle>(allEntries[i], allEntries[j]);
                        maxArea = currentArea;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Insert to a node that is not full
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <param name="newValueLeft"></param>
        /// <param name="newValueRight"></param>
        private void insertNonFullNode(ref RTreeNode node, Polygon newValue,
            RTreeNode newValueLeft, RTreeNode newValueRight)
        {
            //if left is not null
            //then right should'nt be null
            if (newValueLeft != null)
            {
                newValueLeft.Parent = node;
                newValueRight.Parent = node;
            }

            node.MBRectangle = newValue.GetContainingRectangle();
            node.KeyCount++;

            setChild(node, node.KeyCount - 1, newValueLeft);
            setChild(node, node.KeyCount, newValueRight);
        }
        /*
        /// <summary>
        /// Delete the given value from this RTree
        /// </summary>
        /// <param name="value"></param>
        public void Delete(Polygon value)
        {
            var node = findDeletionNode(Root, value);

            if (node == null)
            {
                throw new Exception("Item do not exist in this tree.");
            }

            for (var i = 0; i < node.KeyCount; i++)
            {
                if (value.CompareTo(node.MBRectangle[i]) != 0)
                {
                    continue;
                }

                removeAt(node.MBRectangle, i);
                node.KeyCount--;

                if (node.Parent != null && node != node.Parent.Children[0] && node.KeyCount > 0)
                {
                    var separatorIndex = getPrevSeparatorIndex(node);
                    node.Parent.MBRectangle[separatorIndex] = node.MBRectangle[0];
                }

                balance(node, value);

                Count--;
                return;
            }

        }


        /// <summary>
        /// Balance a node which is short of Keys by rotations or merge
        /// </summary>
        private void balance(RTreeNode node, MBRectangle deleteKey)
        {
            if (node == Root)
            {
                return;
            }

            if (node.KeyCount >= minKeysPerNode)
            {
                updateIndex(node, deleteKey, true);
                return;
            }

            var rightSibling = getRightSibling(node);

            if (rightSibling != null
                && rightSibling.KeyCount > minKeysPerNode)
            {
                leftRotate(node, rightSibling);
                findMinNode(node);
                updateIndex(node, deleteKey, true);
                return;
            }

            var leftSibling = getLeftSibling(node);

            if (leftSibling != null
                && leftSibling.KeyCount > minKeysPerNode)
            {
                rightRotate(leftSibling, node);
                updateIndex(node, deleteKey, true);
                return;
            }

            if (rightSibling != null)
            {
                sandwich(node, rightSibling, deleteKey);
            }
            else
            {
                sandwich(leftSibling, node, deleteKey);
            }

        }

        /// <summary>
        /// optionally recursively update outdated index with new min of right node 
        /// after deletion of a value
        /// </summary>
        private void updateIndex(RTreeNode node, MBRectangle deleteKey, bool spiralUp)
        {
            while (true)
            {
                if (node == null) return;

                if (node.IsLeaf || node.Children[0].IsLeaf)
                {
                    node = node.Parent;
                    continue;
                }

                for (var i = 0; i < node.KeyCount; i++)
                {
                    if (node.MBRectangle[i].CompareTo(deleteKey) == 0)
                    {
                        node.MBRectangle[i] = findMinNode(node.Children[i + 1]).MBRectangles[0];
                    }
                }

                if (spiralUp)
                {
                    node = node.Parent;
                    continue;
                }

                break;
            }
        }


        /// <summary>
        /// merge two adjacent siblings to one node
        /// </summary>
        /// <param name="leftSibling"></param>
        /// <param name="rightSibling"></param>
        /// <param name="deleteKey"></param>
        private void sandwich(RTreeNode leftSibling, RTreeNode rightSibling, T deleteKey)
        {
            var separatorIndex = getNextSeparatorIndex(leftSibling);
            var parent = leftSibling.Parent;

            var newNode = new RTreeNode(maxKeysPerNode, leftSibling.Parent);

            //if leaves are merged then update the Next & Prev pointers
            if (leftSibling.IsLeaf)
            {
                if (leftSibling.Prev != null)
                {
                    newNode.Prev = leftSibling.Prev;
                    leftSibling.Prev.Next = newNode;
                }
                else
                {
                    BottomLeftNode = newNode;
                }

                if (rightSibling.Next != null)
                {
                    newNode.Next = rightSibling.Next;
                    rightSibling.Next.Prev = newNode;
                }

            }

            var newIndex = 0;
            for (var i = 0; i < leftSibling.KeyCount; i++)
            {
                newNode.MBRectangle[newIndex] = leftSibling.MBRectangle[i];

                if (leftSibling.Children[i] != null)
                {
                    setChild(newNode, newIndex, leftSibling.Children[i]);
                }

                if (leftSibling.Children[i + 1] != null)
                {
                    setChild(newNode, newIndex + 1, leftSibling.Children[i + 1]);
                }

                newIndex++;
            }

            //special case when left sibling is empty 
            if (leftSibling.KeyCount == 0 && leftSibling.Children[0] != null)
            {
                setChild(newNode, newIndex, leftSibling.Children[0]);
            }

            if (!rightSibling.IsLeaf)
            {
                newNode.MBRectangle[newIndex] = parent.MBRectangle[separatorIndex];
                newIndex++;
            }

            for (var i = 0; i < rightSibling.KeyCount; i++)
            {
                newNode.MBRectangle[newIndex] = rightSibling.MBRectangle[i];

                if (rightSibling.Children[i] != null)
                {
                    setChild(newNode, newIndex, rightSibling.Children[i]);

                    //when a left leaf is added as right leaf
                    //we need to push parent key as first node of new leaf
                    if (i == 0 && rightSibling.Children[i].IsLeaf
                        && rightSibling.Children[i].MBRectangle[0].CompareTo(newNode.MBRectangle[newIndex - 1]) != 0)
                    {
                        insertAt(rightSibling.Children[i].MBRectangle, 0, newNode.MBRectangle[newIndex - 1]);
                        rightSibling.Children[i].KeyCount++;
                    }

                }

                if (rightSibling.Children[i + 1] != null)
                {
                    setChild(newNode, newIndex + 1, rightSibling.Children[i + 1]);
                }

                newIndex++;
            }

            //special case when left sibling is empty 
            if (rightSibling.KeyCount == 0 && rightSibling.Children[0] != null)
            {
                setChild(newNode, newIndex, rightSibling.Children[0]);

                if (newNode.Children[newIndex].IsLeaf)
                {
                    newNode.MBRectangle[newIndex - 1] = newNode.Children[newIndex].MBRectangle[0];
                }
            }

            newNode.KeyCount = newIndex;

            setChild(parent, separatorIndex, newNode);

            removeAt(parent.MBRectangle, separatorIndex);
            parent.KeyCount--;

            removeChild(parent, separatorIndex + 1);

            if (newNode.IsLeaf && newNode.Parent.Children[0] != newNode)
            {
                separatorIndex = getPrevSeparatorIndex(newNode);
                newNode.Parent.MBRectangle[separatorIndex] = newNode.MBRectangle[0];
            }

            updateIndex(newNode, deleteKey, false);

            if (parent.KeyCount == 0
                && parent == Root)
            {
                Root = newNode;
                Root.Parent = null;

                if (Root.KeyCount == 0)
                {
                    Root = null;
                }

                return;
            }


            if (parent.KeyCount < minKeysPerNode)
            {
                balance(parent, deleteKey);
            }

            updateIndex(newNode, deleteKey, true);

        }


        /// <summary>
        /// do a right rotation 
        /// </summary>
        /// <param name="rightSibling"></param>
        /// <param name="leftSibling"></param>
        private void rightRotate(RTreeNode leftSibling, RTreeNode rightSibling)
        {
            var parentIndex = getNextSeparatorIndex(leftSibling);

            //move parent value to right
            insertAt(rightSibling.MBRectangle, 0, rightSibling.Parent.MBRectangle[parentIndex]);
            rightSibling.KeyCount++;

            insertChild(rightSibling, 0, leftSibling.Children[leftSibling.KeyCount]);

            if (rightSibling.Children[1] != null
                && rightSibling.Children[1].IsLeaf)
            {
                rightSibling.MBRectangle[0] = rightSibling.Children[1].MBRectangle[0];
            }


            //move rightmost element in left sibling to parent
            rightSibling.Parent.MBRectangle[parentIndex] = leftSibling.MBRectangle[leftSibling.KeyCount - 1];

            //remove rightmost element of left sibling
            removeAt(leftSibling.MBRectangle, leftSibling.KeyCount - 1);
            leftSibling.KeyCount--;

            removeChild(leftSibling, leftSibling.KeyCount + 1);

            if (rightSibling.IsLeaf)
            {
                rightSibling.MBRectangle[0] = rightSibling.Parent.MBRectangle[parentIndex];
            }

        }

        /// <summary>
        /// do a left rotation
        /// </summary>
        /// <param name="leftSibling"></param>
        /// <param name="rightSibling"></param>
        private void leftRotate(RTreeNode leftSibling, RTreeNode rightSibling)
        {
            var parentIndex = getNextSeparatorIndex(leftSibling);

            //move root to left
            leftSibling.MBRectangle[leftSibling.KeyCount] = leftSibling.Parent.MBRectangle[parentIndex];
            leftSibling.KeyCount++;

            setChild(leftSibling, leftSibling.KeyCount, rightSibling.Children[0]);

            if (leftSibling.Children[leftSibling.KeyCount] != null
                && leftSibling.Children[leftSibling.KeyCount].IsLeaf)
            {
                leftSibling.MBRectangle[leftSibling.KeyCount - 1] = leftSibling.Children[leftSibling.KeyCount].MBRectangle[0];
            }

            //move right to parent
            leftSibling.Parent.MBRectangle[parentIndex] = rightSibling.MBRectangle[0];
            //remove right
            removeAt(rightSibling.MBRectangle, 0);
            rightSibling.KeyCount--;

            removeChild(rightSibling, 0);

            if (rightSibling.IsLeaf)
            {
                rightSibling.Parent.MBRectangle[parentIndex] = rightSibling.MBRectangle[0];
            }

            if (leftSibling.IsLeaf && leftSibling.Parent.Children[0] != leftSibling)
            {
                parentIndex = getPrevSeparatorIndex(leftSibling);
                leftSibling.Parent.MBRectangle[parentIndex] = leftSibling.MBRectangle[0];
            }
        }


        /// <summary>
        /// Locate the node in which the item to delete exist
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private RTreeNode findDeletionNode(RTreeNode node, T value)
        {
            //if leaf then its time to insert
            if (node.IsLeaf)
            {
                for (var i = 0; i < node.KeyCount; i++)
                {
                    if (value.CompareTo(node.MBRectangle[i]) == 0)
                    {
                        return node;
                    }
                }
            }
            else
            {
                //if not leaf then drill down to leaf
                for (var i = 0; i < node.KeyCount; i++)
                {
                    //current value is less than new value
                    //drill down to left child of current value
                    if (value.CompareTo(node.MBRectangle[i]) < 0)
                    {
                        return findDeletionNode(node.Children[i], value);
                    }
                    //current value is grearer than new value
                    //and current value is last element 

                    if (node.KeyCount == i + 1)
                    {
                        return findDeletionNode(node.Children[i + 1], value);
                    }

                }
            }

            return null;
        }
        */
        /// <summary>
        /// Get prev separator key of this child Node in parent
        /// </summary>
        /// <returns></returns>
        private int getPrevSeparatorIndex(RTreeNode node)
        {
            var parent = node.Parent;

            if (node.Index == 0)
            {
                return 0;
            }

            if (node.Index == parent.KeyCount)
            {
                return node.Index - 1;
            }

            return node.Index - 1;
        }


        /// <summary>
        /// Get next separator key of this child Node in parent
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int getNextSeparatorIndex(RTreeNode node)
        {
            var parent = node.Parent;

            if (node.Index == 0)
            {
                return 0;
            }

            if (node.Index == parent.KeyCount)
            {
                return node.Index - 1;
            }

            return node.Index;

        }
        /// <summary>
        /// get the right sibling node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private RTreeNode getRightSibling(RTreeNode node)
        {
            var parent = node.Parent;
            return node.Index == parent.KeyCount ? null : parent.Children[node.Index + 1];
        }

        /// <summary>
        /// get left sibling node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private RTreeNode getLeftSibling(RTreeNode node)
        {
            return node.Index == 0 ? null : node.Parent.Children[node.Index - 1];
        }

        private void setChild(RTreeNode parent, int childIndex, RTreeNode child)
        {
            parent.Children[childIndex] = child;

            if (child == null)
            {
                return;
            }

            child.Parent = parent;
            child.Index = childIndex;

        }

        private void insertChild(RTreeNode parent, int childIndex, RTreeNode child)
        {
            insertAt(parent.Children, childIndex, child);

            if (child != null)
            {
                child.Parent = parent;
            }

            //update indices
            for (var i = childIndex; i <= parent.KeyCount; i++)
            {
                if (parent.Children[i] != null)
                {
                    parent.Children[i].Index = i;
                }
            }
        }

        private void removeChild(RTreeNode parent, int childIndex)
        {
            removeAt(parent.Children, childIndex);

            //update indices
            for (var i = childIndex; i <= parent.KeyCount; i++)
            {
                if (parent.Children[i] != null)
                {
                    parent.Children[i].Index = i;
                }

            }
        }

        /// <summary>
        /// Shift array right at index to make room for new insertion
        /// And then insert at index
        /// Assumes array have atleast one empty index at end
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        private void insertAt<TS>(TS[] array, int index, TS newValue)
        {
            //shift elements right by one indice from index
            Array.Copy(array, index, array, index + 1, array.Length - index - 1);
            //now set the value
            array[index] = newValue;
        }

        /// <summary>
        /// Shift array left at index    
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        private void removeAt<TS>(TS[] array, int index)
        {
            //shift elements right by one indice from index
            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
        }

    }

    internal static class PolygonExtensions
    {
        /// <summary>
        ///     Gets the imaginary rectangle that contains the polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        internal static MBRectangle GetContainingRectangle(this Polygon polygon)
        {
            var xMax = polygon.Edges.SelectMany(x => new double[] { x.Start.X, x.End.X }).Max();
            var xMin = polygon.Edges.SelectMany(x => new double[] { x.Start.X, x.End.X }).Min();

            var yMax = polygon.Edges.SelectMany(y => new double[] { y.Start.Y, y.End.Y }).Max();
            var yMin = polygon.Edges.SelectMany(y => new double[] { y.Start.Y, y.End.Y }).Min();

            return new MBRectangle(polygon)
            {
                LeftTopCorner = new Point(xMin, yMax),
                RightBottomCorner = new Point(xMax, yMin)
            };
        }
    }

}