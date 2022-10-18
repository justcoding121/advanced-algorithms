using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advanced.Algorithms.Geometry;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     An RTree implementation.
/// </summary>
public class RTree : IEnumerable<Polygon>
{
    private readonly int maxKeysPerNode;
    private readonly int minKeysPerNode;

    //If we don't use leaf mappings then deletion/Exists will be slow
    //because searching for deletion leaf is expensive when data is dense.
    private readonly Dictionary<Polygon, RTreeNode> leafMappings = new();

    internal RTreeNode Root;

    public RTree(int maxKeysPerNode)
    {
        if (maxKeysPerNode < 3) throw new Exception("Max keys per node should be atleast 3.");

        this.maxKeysPerNode = maxKeysPerNode;
        minKeysPerNode = maxKeysPerNode / 2;
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Polygon> GetEnumerator()
    {
        return leafMappings.Select(x => x.Key).GetEnumerator();
    }

    /// <summary>
    ///     Inserts given polygon.
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Insert(Polygon newPolygon)
    {
        var newNode = new RTreeNode(maxKeysPerNode, null)
        {
            MbRectangle = newPolygon.GetContainingRectangle()
        };

        leafMappings.Add(newPolygon, newNode);
        InsertToLeaf(newNode);
        Count++;
    }

    private void InsertToLeaf(RTreeNode newNode)
    {
        if (Root == null)
        {
            Root = new RTreeNode(maxKeysPerNode, null);
            Root.AddChild(newNode);
            return;
        }

        var leafToInsert = FindInsertionLeaf(Root, newNode);
        InsertAndSplit(leafToInsert, newNode);
    }

    /// <summary>
    ///     Inserts the given internal node to the level where it belongs using its height.
    /// </summary>
    private void InsertInternalNode(RTreeNode internalNode)
    {
        InsertInternalNode(Root, internalNode);
    }

    private void InsertInternalNode(RTreeNode currentNode, RTreeNode internalNode)
    {
        if (currentNode.Height == internalNode.Height + 1)
            InsertAndSplit(currentNode, internalNode);
        else
            InsertInternalNode(currentNode.GetMinimumEnlargementAreaMbr(internalNode.MbRectangle), internalNode);
    }

    /// <summary>
    ///     Find the leaf node to start initial insertion.
    /// </summary>
    private RTreeNode FindInsertionLeaf(RTreeNode node, RTreeNode newNode)
    {
        //if leaf then its time to insert
        if (node.IsLeaf) return node;

        return FindInsertionLeaf(node.GetMinimumEnlargementAreaMbr(newNode.MbRectangle), newNode);
    }

    /// <summary>
    ///     Insert and split recursively up until no split is required.
    /// </summary>
    private void InsertAndSplit(RTreeNode node, RTreeNode newValue)
    {
        //newValue have room to fit in this node
        if (node.KeyCount < maxKeysPerNode)
        {
            node.AddChild(newValue);
            ExpandAncestorMbRs(node);
            return;
        }

        var e = new List<RTreeNode>(new[] { newValue });
        e.AddRange(node.Children);

        var distantPairs = GetDistantPairs(e);

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

            var leftEnlargementArea = e1.MbRectangle.GetEnlargementArea(current.MbRectangle);
            var rightEnlargementArea = e2.MbRectangle.GetEnlargementArea(current.MbRectangle);

            if (leftEnlargementArea == rightEnlargementArea)
            {
                var leftArea = e1.MbRectangle.Area();
                var rightArea = e2.MbRectangle.Area();

                if (leftArea == rightArea)
                {
                    if (e1.KeyCount < e2.KeyCount)
                        e1.AddChild(current);
                    else
                        e2.AddChild(current);
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
                foreach (var entry in e) e1.AddChild(entry);
                e.Clear();
            }
            else if (e2.KeyCount == minKeysPerNode - remaining)
            {
                foreach (var entry in e) e2.AddChild(entry);
                e.Clear();
            }
        }

        var parent = node.Parent;
        if (parent != null)
        {
            //replace current node with e1
            parent.SetChild(node.Index, e1);
            //insert overflow element to parent
            InsertAndSplit(parent, e2);
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

    private void ExpandAncestorMbRs(RTreeNode node)
    {
        while (node.Parent != null)
        {
            node.Parent.MbRectangle.Merge(node.MbRectangle);
            node.Parent.Height = node.Height + 1;
            node = node.Parent;
        }
    }

    /// <summary>
    ///     Get the pairs of rectangles farther apart by comparing enlargement areas.
    /// </summary>
    private Tuple<RTreeNode, RTreeNode> GetDistantPairs(List<RTreeNode> allEntries)
    {
        Tuple<RTreeNode, RTreeNode> result = null;

        var maxArea = double.MinValue;
        for (var i = 0; i < allEntries.Count; i++)
        for (var j = i + 1; j < allEntries.Count; j++)
        {
            var currentArea = allEntries[i].MbRectangle.GetEnlargementArea(allEntries[j].MbRectangle);
            if (currentArea > maxArea)
            {
                result = new Tuple<RTreeNode, RTreeNode>(allEntries[i], allEntries[j]);
                maxArea = currentArea;
            }
        }

        return result;
    }

    /// <summary>
    ///     Check if the given polygon exists in this Rtree.
    ///     Time complexity: O(1).
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
        return RangeSearch(Root, searchRectangle, new List<Polygon>());
    }

    /// <summary>
    ///     Returns a list of polygons that's contained within given search rectangle.
    /// </summary>
    private List<Polygon> RangeSearch(RTreeNode current, Rectangle searchRectangle, List<Polygon> result)
    {
        if (current.IsLeaf)
            foreach (var node in current.Children.Take(current.KeyCount))
                if (RectangleIntersection.DoIntersect(node.MbRectangle, searchRectangle))
                    result.Add(node.MbRectangle.Polygon);

        foreach (var node in current.Children.Take(current.KeyCount))
            if (RectangleIntersection.DoIntersect(node.MbRectangle, searchRectangle))
                RangeSearch(node, searchRectangle, result);

        return result;
    }

    /// <summary>
    ///     Time complexity: O(log(n)).
    /// </summary>
    public void Delete(Polygon polygon)
    {
        if (Root == null) throw new Exception("Empty tree.");

        if (!Exists(polygon)) throw new Exception("Given polygon do not belong to this tree.");

        var nodeToDelete = leafMappings[polygon];

        //delete 
        DeleteNode(nodeToDelete);
        CondenseTree(nodeToDelete.Parent);

        if (Root.KeyCount == 1 && !Root.IsLeaf)
        {
            Root = Root.Children[0];
            Root.Parent = null;
        }

        leafMappings.Remove(polygon);
        Count--;

        if (Count == 0) Root = null;
    }

    private void DeleteNode(RTreeNode nodeToDelete)
    {
        RemoveAt(nodeToDelete.Parent.Children, nodeToDelete.Index);
        nodeToDelete.Parent.KeyCount--;
        UpdateIndex(nodeToDelete.Parent.Children, nodeToDelete.Parent.KeyCount, nodeToDelete.Index);
    }

    private void RemoveAt(RTreeNode[] array, int index)
    {
        //shift elements right by one indice from index
        Array.Copy(array, index + 1, array, index, array.Length - index - 1);
    }

    private void UpdateIndex(RTreeNode[] children, int keyCount, int index)
    {
        for (var i = index; i < keyCount; i++) children[i].Index--;
    }

    private void CondenseTree(RTreeNode updatedleaf)
    {
        var current = updatedleaf;
        var toReinsert = new Stack<RTreeNode>();

        while (current != Root)
        {
            var parent = current.Parent;

            if (current.KeyCount < minKeysPerNode)
            {
                DeleteNode(current);
                foreach (var node in current.Children.Take(current.KeyCount)) toReinsert.Push(node);
            }
            else
            {
                ShrinkMbr(current);
            }

            current = parent;
        }

        //update root
        if (current.KeyCount > 0) ShrinkMbr(current);

        while (toReinsert.Count > 0)
        {
            var node = toReinsert.Pop();

            if (node.Height > 0)
                InsertInternalNode(node);
            else
                InsertToLeaf(node);
        }
    }

    private void ShrinkMbr(RTreeNode current)
    {
        current.MbRectangle = new MbRectangle(current.Children[0].MbRectangle);
        foreach (var node in current.Children.Skip(1).Take(current.KeyCount - 1))
            current.MbRectangle.Merge(node.MbRectangle);
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
}

internal static class PolygonExtensions
{
    /// <summary>
    ///     Gets the imaginary rectangle that contains the polygon.
    /// </summary>
    internal static MbRectangle GetContainingRectangle(this Polygon polygon)
    {
        var x = polygon.Edges.SelectMany(z => new[] { z.Left.X, z.Right.X })
            .Aggregate(new
            {
                Max = double.MinValue,
                Min = double.MaxValue
            }, (accumulator, o) => new
            {
                Max = Math.Max(o, accumulator.Max),
                Min = Math.Min(o, accumulator.Min)
            });


        var y = polygon.Edges.SelectMany(z => new[] { z.Left.Y, z.Right.Y })
            .Aggregate(new
            {
                Max = double.MinValue,
                Min = double.MaxValue
            }, (accumulator, o) => new
            {
                Max = Math.Max(o, accumulator.Max),
                Min = Math.Min(o, accumulator.Min)
            });

        return new MbRectangle(new Point(x.Min, y.Max), new Point(x.Max, y.Min))
        {
            Polygon = polygon
        };
    }
}

internal class RTreeNode
{
    internal int Height;

    /// <summary>
    ///     Array Index of this node in parent's Children array
    /// </summary>
    internal int Index;

    internal int KeyCount;

    internal RTreeNode(int maxKeysPerNode, RTreeNode parent)
    {
        Parent = parent;
        Children = new RTreeNode[maxKeysPerNode];
    }

    internal MbRectangle MbRectangle { get; set; }

    internal RTreeNode Parent { get; set; }
    internal RTreeNode[] Children { get; set; }

    //leafs will hold the actual polygon
    //we assume here that bottom two node levels as leafs
    internal bool IsLeaf => MbRectangle.Polygon != null
                            || Children[0].MbRectangle.Polygon != null;

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

        if (MbRectangle == null)
            MbRectangle = new MbRectangle(child.MbRectangle);
        else
            MbRectangle.Merge(child.MbRectangle);

        Height = child.Height + 1;
    }

    /// <summary>
    ///     Select the child node whose MBR will require the minimum area enlargement
    ///     to cover the given polygon.
    /// </summary>
    internal RTreeNode GetMinimumEnlargementAreaMbr(MbRectangle newPolygon)
    {
        //order by enlargement area
        //then by minimum area
        return Children[Children.Take(KeyCount)
            .Select((node, index) => new { node, index })
            .OrderBy(x => x.node.MbRectangle.GetEnlargementArea(newPolygon))
            .ThenBy(x => x.node.MbRectangle.Area())
            .First().index];
    }
}

/// <summary>
///     Minimum bounded rectangle (MBR).
/// </summary>
internal class MbRectangle : Rectangle
{
    internal MbRectangle(Point leftTopCorner, Point rightBottomCorner)
    {
        LeftTop = leftTopCorner;
        RightBottom = rightBottomCorner;
    }

    internal MbRectangle(Rectangle rectangle)
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
    internal double GetEnlargementArea(MbRectangle rectangleToFit)
    {
        return Math.Abs(GetMergedRectangle(rectangleToFit).Area() - Area());
    }

    /// <summary>
    ///     Set current rectangle with the merge of given rectangle.
    /// </summary>
    internal void Merge(MbRectangle rectangleToMerge)
    {
        var merged = GetMergedRectangle(rectangleToMerge);

        LeftTop = merged.LeftTop;
        RightBottom = merged.RightBottom;
    }

    /// <summary>
    ///     Merge the current rectangle with given rectangle.
    /// </summary>
    private Rectangle GetMergedRectangle(MbRectangle rectangleToMerge)
    {
        var leftTopCorner = new Point(LeftTop.X > rectangleToMerge.LeftTop.X ? rectangleToMerge.LeftTop.X : LeftTop.X,
            LeftTop.Y < rectangleToMerge.LeftTop.Y ? rectangleToMerge.LeftTop.Y : LeftTop.Y);

        var rightBottomCorner = new Point(
            RightBottom.X < rectangleToMerge.RightBottom.X ? rectangleToMerge.RightBottom.X : RightBottom.X,
            RightBottom.Y > rectangleToMerge.RightBottom.Y ? rectangleToMerge.RightBottom.Y : RightBottom.Y);

        return new MbRectangle(leftTopCorner, rightBottomCorner);
    }
}