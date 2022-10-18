using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A tree implementation.
/// </summary>
public class Tree<T> : IEnumerable<T> where T : IComparable
{
    private TreeNode<T> Root { get; set; }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new TreeEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public int GetHeight()
    {
        return GetHeight(Root);
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public void Insert(T parent, T child)
    {
        if (Root == null)
        {
            Root = new TreeNode<T>(null, child);
            Count++;
            return;
        }

        var parentNode = Find(parent);

        if (parentNode == null) throw new ArgumentNullException();

        var exists = Find(Root, child) != null;

        if (exists) throw new ArgumentException("value already exists");

        parentNode.Children.InsertFirst(new TreeNode<T>(parentNode, child));
        Count++;
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public void Delete(T value)
    {
        Delete(Root.Value, value);
    }

    /// <summary>
    ///     Time complexity:  O(n)
    /// </summary>
    public IEnumerable<T> Children(T value)
    {
        return Find(value)?.Children.Select(x => x.Value);
    }

    private TreeNode<T> Find(T value)
    {
        if (Root == null) return null;

        return Find(Root, value);
    }

    private int GetHeight(TreeNode<T> node)
    {
        if (node == null) return -1;

        var currentHeight = -1;

        foreach (var child in node.Children)
        {
            var childHeight = GetHeight(child);

            if (currentHeight < childHeight) currentHeight = childHeight;
        }

        currentHeight++;

        return currentHeight;
    }

    private void Delete(T parentValue, T value)
    {
        var parent = Find(parentValue);

        if (parent == null) throw new Exception("Cannot find parent");

        var itemToRemove = Find(parent, value);

        if (itemToRemove == null) throw new Exception("Cannot find item");

        //if item is root
        if (itemToRemove.Parent == null)
        {
            if (itemToRemove.Children.Count() == 0)
            {
                Root = null;
            }
            else
            {
                if (itemToRemove.Children.Count() == 1)
                {
                    Root = itemToRemove.Children.DeleteFirst();
                    Root.Parent = null;
                }
                else
                {
                    throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                }
            }
        }
        else
        {
            if (itemToRemove.Children.Count() == 0)
            {
                itemToRemove.Parent.Children.Delete(itemToRemove);
            }
            else
            {
                if (itemToRemove.Children.Count() == 1)
                {
                    var orphan = itemToRemove.Children.DeleteFirst();
                    orphan.Parent = itemToRemove.Parent;

                    itemToRemove.Parent.Children.InsertFirst(orphan);
                    itemToRemove.Parent.Children.Delete(itemToRemove);
                }
                else
                {
                    throw new Exception("Node have multiple children. Cannot delete node unambiguosly");
                }
            }
        }

        Count--;
    }

    private TreeNode<T> Find(TreeNode<T> parent, T value)
    {
        if (parent.Value.CompareTo(value) == 0) return parent;

        foreach (var child in parent.Children)
        {
            var result = Find(child, value);

            if (result != null) return result;
        }

        return null;
    }
}

internal class TreeNode<T> : IComparable where T : IComparable
{
    internal TreeNode(TreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;

        Children = new SinglyLinkedList<TreeNode<T>>();
    }

    internal T Value { get; set; }

    internal TreeNode<T> Parent { get; set; }
    internal SinglyLinkedList<TreeNode<T>> Children { get; set; }

    internal bool IsLeaf => Children.Count() == 0;

    public int CompareTo(object obj)
    {
        return Value.CompareTo(obj as TreeNode<T>);
    }
}

internal class TreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly TreeNode<T> root;
    private Stack<TreeNode<T>> progress;

    internal TreeEnumerator(TreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<TreeNode<T>>(root.Children);
            Current = root.Value;
            return true;
        }

        if (progress.Count > 0)
        {
            var next = progress.Pop();
            Current = next.Value;

            foreach (var child in next.Children) progress.Push(child);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        Current = default;
    }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        progress = null;
    }
}