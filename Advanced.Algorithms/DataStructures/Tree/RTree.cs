using Advanced.Algorithms.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// An RTree implementation.
    /// </summary>
    public class RTree : IEnumerable<Polygon>
    {
        private readonly int maxKeysPerNode;
        private readonly int minKeysPerNode;

        //If we don't use leaf mappings then deletion/Exists will be slow
        //because searching for deletion leaf is expensive when data is dense.
        private Dictionary<Polygon, RTreeNode> leafMappings = new Dictionary<Polygon, RTreeNode>();

        internal RTreeNode Root;

        public int Count { get; private set; }

        public RTree(int maxKeysPerNode)
        {
            if (maxKeysPerNode < 3)
            {
                throw new Exception("Max keys per node should be atleast 3.");
            }

            this.maxKeysPerNode = maxKeysPerNode;
            this.minKeysPerNode = maxKeysPerNode / 2;
        }

        /// <summary>
        /// Inserts given polygon.
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Insert(Polygon newPolygon)
        {
            var newNode = new RTreeNode(maxKeysPerNode, null)
            {
                MBRectangle = newPolygon.GetContainingRectangle()
            };

            leafMappings.Add(newPolygon, newNode);
            insertToLeaf(newNode);
            Count++;
        }

        private void insertToLeaf(RTreeNode newNode)
        {
            if (Root == null)
            {
                Root = new RTreeNode(maxKeysPerNode, null);
                Root.AddChild(newNode);
                return;
            }

            var leafToInsert = findInsertionLeaf(Root, newNode);
            insertAndSplit(leafToInsert, newNode);
        }

        /// <summary>
        ///     Inserts the given internal node to the level where it belongs using its height.
        /// </summary>
        private void insertInternalNode(RTreeNode internalNode)
        {
            insertInternalNode(Root, internalNode);
        }

        private void insertInternalNode(RTreeNode currentNode, RTreeNode internalNode)
        {
            if (currentNode.Height == internalNode.Height + 1)
            {
                insertAndSplit(currentNode, internalNode);
            }
            else
            {
                insertInternalNode(currentNode.GetMinimumEnlargementAreaMBR(internalNode.MBRectangle), internalNode);
            }
        }

        /// <summary>
        ///     Find the leaf node to start initial insertion.
        /// </summary>
        private RTreeNode findInsertionLeaf(RTreeNode node, RTreeNode newNode)
        {
            //if leaf then its time to insert
            if (node.IsLeaf)
            {
                return node;
            }

            return findInsertionLeaf(node.GetMinimumEnlargementAreaMBR(newNode.MBRectangle), newNode);
        }

        /// <summary>
        ///     Insert and split recursively up until no split is required.
        /// </summary>
        private void insertAndSplit(RTreeNode node, RTreeNode newValue)
        {
            //newValue have room to fit in this node
            if (node.KeyCount < maxKeysPerNode)
            {
                node.AddChild(newValue);
                expandAncestorMBRs(node);
                return;
            }

            var e = new List<RTreeNode>(new RTreeNode[] { newValue });
            e.AddRange(node.Children);

            var distantPairs = getDistantPairs(e);

            //Let E be the set consisting of all current entries and new entry.
            //Select as seeds two entries e1, e2 ∈ E, where the distance between
            //left and right is the maximum among all other pairs of entries from E
            var e1 = new RTreeNode(maxKeysPerNode, null);
            var e2 = new RTreeNode(maxKeysPerNode, null);

            e1.AddChild(distantPairs.Item1);
            e2.AddChild(distantPairs.Item2);

            e = e.Where(x => x != distantPairs.Item1 && x != distantPairs.Item2)
                             .ToList();

            /*Examine the remaining members of E one by one and assign them
            to e1 or e2, depending on which of the MBRs of these nodes
            will require the minimum area enlargement so as to cover this entry.
            If a tie occurs, assign the entry to the node whose MBR has the smaller area.
            If a tie occurs again, assign the entry to the node that contains the smaller number of entries*/
            while (e.Count > 0)
            {
                var current = e[e.Count - 1];

                var leftEnlargementArea = e1.MBRectangle.GetEnlargementArea(current.MBRectangle);
                var rightEnlargementArea = e2.MBRectangle.GetEnlargementArea(current.MBRectangle);

                if (leftEnlargementArea == rightEnlargementArea)
                {
                    var leftArea = e1.MBRectangle.Area();
                    var rightArea = e2.MBRectangle.Area();

                    if (leftArea == rightArea)
                    {
                        if (e1.KeyCount < e2.KeyCount)
                        {
                            e1.AddChild(current);
                        }
                        else
                        {
                            e2.AddChild(current);
                        }
                    }
                    else if (leftArea < rightArea)
                    {
                        e1.AddChild(current);
                    }
                    else
                    {
                        e2.AddChild(current);
                    }
                }
                else if (leftEnlargementArea < rightEnlargementArea)
                {
                    e1.AddChild(current);
                }
                else
                {
                    e2.AddChild(current);
                }

                e.RemoveAt(e.Count - 1);

                var remaining = e.Count;

                /*if during the assignment of entries, there remain λ entries to be assigned
                and the one node contains minKeysPerNode − λ entries then
                assign all the remaining entries to this node without considering
                the aforementioned criteria
                so that the node will contain at least minKeysPerNode entries */
                if (e1.KeyCount == minKeysPerNode - remaining)
                {
                    foreach (var entry in e)
                    {
                        e1.AddChild(entry);
                    }
                    e.Clear();
                }
                else if (e2.KeyCount == minKeysPerNode - remaining)
                {
                    foreach (var entry in e)
                    {
                        e2.AddChild(entry);
                    }
                    e.Clear();
                }
            }

            var parent = node.Parent;
            if (parent != null)
            {
                //replace current node with e1
                parent.SetChild(node.Index, e1);
                //insert overflow element to parent
                insertAndSplit(parent, e2);
            }
            else
            {
                //node is the root.
                //increase the height of RTree by one by adding a new root.
                Root = new RTreeNode(maxKeysPerNode, null);
                Root.AddChild(e1);
                Root.AddChild(e2);
            }

        }

        private void expandAncestorMBRs(RTreeNode node)
        {
            while (node.Parent != null)
            {
                node.Parent.MBRectangle.Merge(node.MBRectangle);
                node.Parent.Height = node.Height + 1;
                node = node.Parent;
            }
        }

        /// <summary>
        ///     Get the pairs of rectangles farther apart by comparing enlargement areas.
        /// </summary>
        private Tuple<RTreeNode, RTreeNode> getDistantPairs(List<RTreeNode> allEntries)
        {
            Tuple<RTreeNode, RTreeNode> result = null;

            double maxArea = Double.MinValue;
            for (int i = 0; i < allEntries.Count; i++)
            {
                for (int j = i + 1; j < allEntries.Count; j++)
                {
                    var currentArea = allEntries[i].MBRectangle.GetEnlargementArea(allEntries[j].MBRectangle);
                    if (currentArea > maxArea)
                    {
                        result = new Tuple<RTreeNode, RTreeNode>(allEntries[i], allEntries[j]);
                        maxArea = currentArea;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Check if the given polygon exists in this Rtree.
        /// Time complexity: O(1).
        /// </summary>
        public bool Exists(Polygon searchPolygon)
        {
            return leafMappings.ContainsKey(searchPolygon);
        }

        /// <summary>
        ///     Returns a list of polygons whose minimum bounded rectangle intersects with given search rectangle.
        /// </summary>
        public List<Polygon> RangeSearch(Rectangle searchRectangle)
        {
            return rangeSearch(Root, searchRectangle, new List<Polygon>());
        }

        /// <summary>
        ///     Returns a list of polygons that's contained within given search rectangle.
        /// </summary>
        private List<Polygon> rangeSearch(RTreeNode current, Rectangle searchRectangle, List<Polygon> result)
        {
            if (current.IsLeaf)
            {
                foreach (var node in current.Children.Take(current.KeyCount))
                {
                    if (RectangleIntersection.DoIntersect(node.MBRectangle, searchRectangle))
                    {
                        result.Add(node.MBRectangle.Polygon);
                    }
                }
            }

            foreach (var node in current.Children.Take(current.KeyCount))
            {
                if (RectangleIntersection.DoIntersect(node.MBRectangle, searchRectangle))
                {
                    rangeSearch(node, searchRectangle, result);
                }
            }

            return result;

        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Delete(Polygon polygon)
        {
            if (Root == null)
            {
                throw new Exception("Empty tree.");
            }

            if (!Exists(polygon))
            {
                throw new Exception("Given polygon do not belong to this tree.");
            }

            var nodeToDelete = leafMappings[polygon];

            //delete 
            deleteNode(nodeToDelete);
            condenseTree(nodeToDelete.Parent);

            if (Root.KeyCount == 1 && !Root.IsLeaf)
            {
                Root = Root.Children[0];
                Root.Parent = null;
            }

            leafMappings.Remove(polygon);
            Count--;

            if (Count == 0)
            {
                Root = null;
            }
        }

        private void deleteNode(RTreeNode nodeToDelete)
        {
            removeAt(nodeToDelete.Parent.Children, nodeToDelete.Index);
            nodeToDelete.Parent.KeyCount--;
            updateIndex(nodeToDelete.Parent.Children, nodeToDelete.Parent.KeyCount, nodeToDelete.Index);
        }

        private void removeAt(RTreeNode[] array, int index)
        {
            //shift elements right by one indice from index
            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
        }

        private void updateIndex(RTreeNode[] children, int keyCount, int index)
        {
            for (int i = index; i < keyCount; i++)
            {
                children[i].Index--;
            }
        }

        private void condenseTree(RTreeNode updatedleaf)
        {
            var current = updatedleaf;
            var toReinsert = new Stack<RTreeNode>();

            while (current != Root)
            {
                var parent = current.Parent;

                if (current.KeyCount < minKeysPerNode)
                {
                    deleteNode(current);
                    foreach (var node in current.Children.Take(current.KeyCount))
                    {
                        toReinsert.Push(node);
                    }
                }
                else
                {
                    shrinkMBR(current);
                }

                current = parent;
            }

            //update root
            if (current.KeyCount > 0)
            {
                shrinkMBR(current);
            }

            while (toReinsert.Count > 0)
            {
                var node = toReinsert.Pop();

                if (node.Height > 0)
                {
                    insertInternalNode(node);
                }
                else
                {
                    insertToLeaf(node);
                }
            }

        }

        private void shrinkMBR(RTreeNode current)
        {
            current.MBRectangle = new MBRectangle(current.Children[0].MBRectangle);
            foreach (var node in current.Children.Skip(1).Take(current.KeyCount - 1))
            {
                current.MBRectangle.Merge(node.MBRectangle);
            }
        }

        /// <summary>
        ///     Clear all data in this R-tree.
        /// </summary>
        public void Clear()
        {
            Root = null;
            leafMappings.Clear();
            Count = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Polygon> GetEnumerator()
        {
            return leafMappings.Select(x => x.Key).GetEnumerator();
        }
    }

    internal static class PolygonExtensions
    {
        /// <summary>
        ///     Gets the imaginary rectangle that contains the polygon.
        /// </summary>
        internal static MBRectangle GetContainingRectangle(this Polygon polygon)
        {
            var x = polygon.Edges.SelectMany(z => new double[] { z.Left.X, z.Right.X })
                .Aggregate(new
                {
                    Max = double.MinValue,
                    Min = double.MaxValue
                }, (accumulator, o) => new
                {
                    Max = Math.Max(o, accumulator.Max),
                    Min = Math.Min(o, accumulator.Min),
                });


            var y = polygon.Edges.SelectMany(z => new double[] { z.Left.Y, z.Right.Y })
                   .Aggregate(new
                   {
                       Max = double.MinValue,
                       Min = double.MaxValue
                   }, (accumulator, o) => new
                   {
                       Max = Math.Max(o, accumulator.Max),
                       Min = Math.Min(o, accumulator.Min),
                   });

            return new MBRectangle(new Point(x.Min, y.Max), new Point(x.Max, y.Min))
            {
                Polygon = polygon
            };
        }
    }

    internal class RTreeNode
    {
        /// <summary>
        /// Array Index of this node in parent's Children array
        /// </summary>
        internal int Index;

        internal int Height;
        internal MBRectangle MBRectangle { get; set; }
        internal int KeyCount;

        internal RTreeNode Parent { get; set; }
        internal RTreeNode[] Children { get; set; }

        //leafs will hold the actual polygon
        //we assume here that bottom two node levels as leafs
        internal bool IsLeaf => MBRectangle.Polygon != null
                                || Children[0].MBRectangle.Polygon != null;

        internal RTreeNode(int maxKeysPerNode, RTreeNode parent)
        {
            Parent = parent;
            Children = new RTreeNode[maxKeysPerNode];
        }

        internal void AddChild(RTreeNode child)
        {
            SetChild(KeyCount, child);
            KeyCount++;
        }

        /// <summary>
        ///     Set the child at specifid index.
        /// </summary>
        internal void SetChild(int index, RTreeNode child)
        {
            Children[index] = child;
            Children[index].Parent = this;
            Children[index].Index = index;

            if (MBRectangle == null)
            {
                MBRectangle = new MBRectangle(child.MBRectangle);
            }
            else
            {
                MBRectangle.Merge(child.MBRectangle);
            }

            Height = child.Height + 1;
        }

        /// <summary>
        /// Select the child node whose MBR will require the minimum area enlargement
        /// to cover the given polygon.
        /// </summary>
        internal RTreeNode GetMinimumEnlargementAreaMBR(MBRectangle newPolygon)
        {
            //order by enlargement area
            //then by minimum area
            return Children[Children.Take(KeyCount)
                              .Select((node, index) => new { node, index })
                              .OrderBy(x => x.node.MBRectangle.GetEnlargementArea(newPolygon))
                              .ThenBy(x => x.node.MBRectangle.Area())
                              .First().index];
        }

    }


    /// <summary>
    ///     Minimum bounded rectangle (MBR).
    /// </summary>
    internal class MBRectangle : Rectangle
    {
        internal MBRectangle(Point leftTopCorner, Point rightBottomCorner)
        {
            LeftTop = leftTopCorner;
            RightBottom = rightBottomCorner;
        }

        internal MBRectangle(Rectangle rectangle)
        {
            LeftTop = new Point(rectangle.LeftTop.X, rectangle.LeftTop.Y);
            RightBottom = new Point(rectangle.RightBottom.X, rectangle.RightBottom.Y);
        }

        /// <summary>
        ///     The actual polygon if this MBR is a leaf.
        /// </summary>
        internal Polygon Polygon { get; set; }

        /// <summary>
        ///     Returns the required enlargement area to fit the given rectangle inside this minimum bounded rectangle.
        /// </summary>
        /// <param name="polygonToFit">The rectangle to fit inside current MBR.</param>
        internal double GetEnlargementArea(MBRectangle rectangleToFit)
        {
            return Math.Abs(getMergedRectangle(rectangleToFit).Area() - Area());
        }

        /// <summary>
        ///    Set current rectangle with the merge of given rectangle.
        /// </summary>
        internal void Merge(MBRectangle rectangleToMerge)
        {
            var merged = getMergedRectangle(rectangleToMerge);

            LeftTop = merged.LeftTop;
            RightBottom = merged.RightBottom;
        }

        /// <summary>
        ///     Merge the current rectangle with given rectangle. 
        /// </summary>
        private Rectangle getMergedRectangle(MBRectangle rectangleToMerge)
        {
            var leftTopCorner = new Point(LeftTop.X > rectangleToMerge.LeftTop.X ? rectangleToMerge.LeftTop.X : LeftTop.X,
              LeftTop.Y < rectangleToMerge.LeftTop.Y ? rectangleToMerge.LeftTop.Y : LeftTop.Y);

            var rightBottomCorner = new Point(RightBottom.X < rectangleToMerge.RightBottom.X ? rectangleToMerge.RightBottom.X : RightBottom.X,
                RightBottom.Y > rectangleToMerge.RightBottom.Y ? rectangleToMerge.RightBottom.Y : RightBottom.Y);

            return new MBRectangle(leftTopCorner, rightBottomCorner);
        }
    }
}