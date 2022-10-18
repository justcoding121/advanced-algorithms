using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A multi-dimentional range tree implementation.
/// </summary>
public class RangeTree<T> : IEnumerable<T[]> where T : IComparable
{
    private readonly int dimensions;
    private readonly HashSet<T[]> items = new(new ArrayComparer<T>());

    private readonly OneDimentionalRangeTree<T> tree = new();

    public RangeTree(int dimensions)
    {
        if (dimensions <= 0) throw new Exception("Dimension should be greater than 0.");

        this.dimensions = dimensions;
    }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T[]> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Insert(T[] value)
    {
        ValidateDimensions(value);

        if (items.Contains(value)) throw new Exception("value exists.");
        var currentTree = tree;
        //get all overlaps
        //and insert next dimension value to each overlapping node
        for (var i = 0; i < dimensions; i++) currentTree = currentTree.Insert(value[i]).Tree;

        items.Add(value);
        Count++;
    }

    /// <summary>
    ///     Time complexity: O(n).
    /// </summary>
    public void Delete(T[] value)
    {
        ValidateDimensions(value);

        if (!items.Contains(value)) throw new Exception("Item not found.");

        var found = false;
        DeleteRecursive(tree, value, 0, ref found);
        items.Remove(value);
        Count--;
    }

    /// <summary>
    ///     Recursively move until last dimension and then delete if found.
    /// </summary>
    private void DeleteRecursive(OneDimentionalRangeTree<T> tree, T[] value,
        int currentDimension, ref bool found)
    {
        var node = tree.Find(value[currentDimension]);

        if (node != null)
        {
            if (currentDimension + 1 == dimensions)
                found = true;
            else
                DeleteRecursive(node.Tree, value, currentDimension + 1, ref found);
        }

        //delete node if next dimension has no elements
        //or when this is the last dimension and we found element
        if (node != null && found && (currentDimension + 1 == dimensions
                                      || node.Tree.Count == 0 && currentDimension + 1 < dimensions))
            tree.Delete(value[currentDimension]);
    }

    /// <summary>
    ///     Get all points within given range.
    ///     Time complexity: O(n).
    /// </summary>
    public List<T[]> RangeSearch(T[] start, T[] end)
    {
        ValidateDimensions(start);
        ValidateDimensions(end);

        return RangeSearch(tree, start, end, 0);
    }

    /// <summary>
    ///     Recursively visit node and return points within given range.
    /// </summary>
    private List<T[]> RangeSearch(
        OneDimentionalRangeTree<T> currentTree,
        T[] start, T[] end, int dimension)
    {
        var nodes = currentTree.RangeSearch(start[dimension], end[dimension]);

        if (dimension + 1 == dimensions)
        {
            var result = new List<T[]>();

            foreach (var value in nodes.SelectMany(x => x.Values))
            {
                var thisDimResult = new T[dimensions];
                thisDimResult[dimension] = value;
                result.Add(thisDimResult);
            }

            return result;
        }
        else
        {
            var result = new List<T[]>();

            foreach (var node in nodes)
            {
                var nextDimResult = RangeSearch(node.Tree, start, end, dimension + 1);

                foreach (var value in node.Values)
                foreach (var nextResult in nextDimResult)
                {
                    nextResult[dimension] = value;
                    result.Add(nextResult);
                }
            }

            return result;
        }
    }

    /// <summary>
    ///     Validate dimensions for point length.
    /// </summary>
    private void ValidateDimensions(T[] start)
    {
        if (start == null) throw new ArgumentNullException(nameof(start));

        if (start.Length != dimensions) throw new Exception($"Expecting {dimensions} points.");
    }
}

/// <summary>
///     One dimensional range tree
///     by nesting node with r-b tree for next dimension.
/// </summary>
internal class OneDimentionalRangeTree<T> where T : IComparable
{
    internal RedBlackTree<RangeTreeNode<T>> Tree = new();

    internal int Count => Tree.Count;

    internal RangeTreeNode<T> Find(T value)
    {
        var result = Tree.FindNode(new RangeTreeNode<T>(value));
        if (result == null) throw new Exception("Item not found in this tree.");

        return result.Value;
    }

    internal RangeTreeNode<T> Insert(T value)
    {
        var newNode = new RangeTreeNode<T>(value);

        var existing = Tree.FindNode(newNode);
        if (existing != null)
        {
            existing.Value.Values.Add(value);
            return existing.Value;
        }

        Tree.Insert(newNode);
        return newNode;
    }

    internal void Delete(T value)
    {
        var existing = Tree.FindNode(new RangeTreeNode<T>(value));

        if (existing.Value.Values.Count == 1)
        {
            Tree.Delete(new RangeTreeNode<T>(value));
            return;
        }

        //remove last
        existing.Value.Values.RemoveAt(existing.Value.Values.Count - 1);
    }

    internal List<RangeTreeNode<T>> RangeSearch(T start, T end)
    {
        return GetInRange(new List<RangeTreeNode<T>>(),
            new Dictionary<RedBlackTreeNode<RangeTreeNode<T>>, bool>(),
            Tree.Root, start, end);
    }

    private List<RangeTreeNode<T>> GetInRange(List<RangeTreeNode<T>> result,
        Dictionary<RedBlackTreeNode<RangeTreeNode<T>>, bool> visited,
        RedBlackTreeNode<RangeTreeNode<T>> currentNode,
        T start, T end)
    {
        if (currentNode.IsLeaf)
        {
            //start is less than current node
            if (!InRange(currentNode, start, end)) return result;

            result.Add(currentNode.Value);
        }
        //if start is less than current
        //move left
        else
        {
            if (start.CompareTo(currentNode.Value.Value) <= 0)
            {
                if (currentNode.Left != null) GetInRange(result, visited, currentNode.Left, start, end);

                //start is less than current node
                if (!visited.ContainsKey(currentNode)
                    && InRange(currentNode, start, end))
                {
                    result.Add(currentNode.Value);
                    visited.Add(currentNode, false);
                }
            }

            //if start is greater than current
            //and end is greater than current
            //move right
            if (end.CompareTo(currentNode.Value.Value) < 0) return result;

            {
                if (currentNode.Right != null) GetInRange(result, visited, currentNode.Right, start, end);

                //start is less than current node
                if (visited.ContainsKey(currentNode) || !InRange(currentNode, start, end)) return result;

                result.Add(currentNode.Value);
                visited.Add(currentNode, false);
            }
        }

        return result;
    }

    /// <summary>
    ///     Checks if current node is in search range.
    /// </summary>
    private bool InRange(RedBlackTreeNode<RangeTreeNode<T>> currentNode, T start, T end)
    {
        //start is less than current and end is greater than current
        return start.CompareTo(currentNode.Value.Value) <= 0
               && end.CompareTo(currentNode.Value.Value) >= 0;
    }
}

/// <summary>
///     Range tree node.
/// </summary>
internal class RangeTreeNode<T> : IComparable where T : IComparable
{
    internal RangeTreeNode(T value)
    {
        Values = new List<T>(new[] { value });
        Tree = new OneDimentionalRangeTree<T>();
    }

    internal T Value => Values[0];

    internal List<T> Values { get; set; }

    internal OneDimentionalRangeTree<T> Tree { get; set; }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((RangeTreeNode<T>)obj).Value);
    }
}