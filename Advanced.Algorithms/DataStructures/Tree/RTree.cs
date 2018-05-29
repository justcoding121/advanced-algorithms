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
        ///     The actual polygon if this MBR is a leaf.
        /// </summary>
        internal Polygon Polygon { get; set; }

        /// <summary>
        ///     returns the required enlargement by area to fit the given polygon inside this minimum bounded rectangle.
        /// </summary>
        /// <param name="polygonToFit">The polygon to fit inside current MBR.</param>
        internal double GetEnlargementArea(Polygon polygonToFit)
        {
            return Math.Abs(getMergedRectangle(polygonToFit.GetContainingRectangle()).Area() - Area());
        }

        /// <summary>
        ///     returns the required enlargement area to fit the given rectangle inside this minimum bounded rectangle.
        /// </summary>
        /// <param name="polygonToFit">The rectangle to fit inside current MBR.</param>
        internal double GetEnlargementArea(MBRectangle rectangleToFit)
        {
            return Math.Abs(getMergedRectangle(rectangleToFit).Area() - Area());
        }

        /// <summary>
        ///     set current rectangle with the merge of given rectangle.
        /// </summary>
        internal void Merge(MBRectangle rectangleToMerge)
        {
            var merged = getMergedRectangle(rectangleToMerge);

            LeftTopCorner = merged.LeftTopCorner;
            RightBottomCorner = merged.RightBottomCorner;
        }

        /// <summary>
        ///     Merge the current rectangle with given rectangle. 
        /// </summary>
        /// <param name="rectangleToMerge">The new rectangle.</param>
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

        internal void AddChild(MBRectangle rectangle)
        {
            if (KeyCount == Children.Length)
            {
                throw new Exception("No space to add child.");
            }

            Children[KeyCount] = new RTreeNode(Children.Length, this);
            Children[KeyCount].Index = KeyCount;
            Children[KeyCount].MBRectangle = rectangle;
            MBRectangle.Merge(rectangle);
            KeyCount++;
        }

        /// <summary>
        ///     Set the child at specifid index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="child"></param>
        internal void SetChild(int index, RTreeNode child)
        {
            child.Index = index;
            Children[index] = child;
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
            var min = Children.Select(x => x.MBRectangle)
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
        private readonly int maxKeysPerNode;
        private readonly int minKeysPerNode;

        public int Count { get; private set; }

        internal RTreeNode Root;

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
        /// Inserts  to B-Tree
        /// </summary>
        /// <param name="newPolygon"></param>
        public void Insert(Polygon newPolygon)
        {
            var newNode = new RTreeNode(maxKeysPerNode, null) { MBRectangle = newPolygon.GetContainingRectangle() };

            if (Root == null)
            {
                Root = newNode;
                Root.KeyCount++;
                Count++;
                return;
            }

            var leafToInsert = findInsertionLeaf(Root, newPolygon);

            insertAndSplit(ref leafToInsert, newNode);
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
        private void insertAndSplit(ref RTreeNode node, RTreeNode newValue)
        {
            //newValue have room to fit in this node
            if (node.KeyCount < maxKeysPerNode)
            {
                node.AddChild(newValue.MBRectangle);
                return;
            }

            //Let E be the set consisting of all current entries and new entry.
            //Select as seeds two entries e1, e2 ∈ E, where the distance between
            //left and right is the maximum among all other pairs of entries from E
            var e1 = new RTreeNode(maxKeysPerNode, null);
            var e2 = new RTreeNode(maxKeysPerNode, null);

            var e = node.Children.Select(x => x.MBRectangle).ToList();
            e.Add(newValue.MBRectangle);

            Tuple<MBRectangle, MBRectangle> distantPairs = getDistantPairs(e);

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

                var leftEnlargementArea = e1.MBRectangle.GetEnlargementArea(current);
                var rightEnlargementArea = e2.MBRectangle.GetEnlargementArea(current);

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
                if (leftEnlargementArea < rightEnlargementArea)
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

            //insert overflow element (newMedian) to parent
            var parent = node.Parent;
            if (parent != null)
            {
                //replace current node with e1
                node.Parent.SetChild(node.Index, e1);
                //insert e2
                insertAndSplit(ref parent, e2);
            }
            else
            {
                //node is the root.
                //increase the height of RTree by one by adding a new root.
                Root = new RTreeNode(maxKeysPerNode, null);
                Root.AddChild(e1.MBRectangle);
                Root.AddChild(e2.MBRectangle);
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

            return new MBRectangle()
            {
                LeftTopCorner = new Point(xMin, yMax),
                RightBottomCorner = new Point(xMax, yMin),
                Polygon = polygon
            };
        }
    }

}