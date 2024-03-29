﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A binary tree implementation using pointers.
/// </summary>
public class BinaryTree<T> : IEnumerable<T> where T : IComparable
{
    private BinaryTreeNode<T> Root { get; set; }

    public int Count { get; private set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BinaryTreeEnumerator<T>(Root);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public int GetHeight()
    {
        return GetHeight(Root);
    }

    /// <summary>
    ///     Only inserts to unambiguous nodes (a node with two children cannot be inserted with a new child unambiguously).
    ///     Time complexity: O(n)
    /// </summary>
    /// <summary>
    public void Insert(T parent, T child)
    {
        if (Root == null)
        {
            Root = new BinaryTreeNode<T>(null, child);
            Count++;
            return;
        }

        var parentNode = Find(parent);

        if (parentNode == null) throw new Exception("Cannot find parent node");

        var exists = Find(Root, child) != null;

        if (exists) throw new ArgumentNullException("value already exists");

        switch (parentNode.Left)
        {
            case null when parentNode.Right == null:
                parentNode.Left = new BinaryTreeNode<T>(parentNode, child);
                break;
            case null:
                parentNode.Left = new BinaryTreeNode<T>(parentNode, child);
                break;
            default:
                if (parentNode.Right == null)
                    parentNode.Right = new BinaryTreeNode<T>(parentNode, child);
                else
                    throw new Exception("Cannot insert to a parent with two child node unambiguosly");

                break;
        }

        Count++;
    }

    /// <summary>
    ///     Only deletes unambiguous nodes (a node with two children cannot be deleted unambiguously).
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T value)
    {
        var node = Find(value);

        if (node == null) throw new Exception("Cannot find node");

        switch (node.Left)
        {
            case null when node.Right == null:
                if (node.Parent == null)
                {
                    Root = null;
                }
                else
                {
                    if (node.Parent.Left == node)
                        node.Parent.Left = null;
                    else
                        node.Parent.Right = null;
                }

                break;
            case null when node.Right != null:
                node.Right.Parent = node.Parent;

                if (node.Parent.Left == node)
                    node.Parent.Left = node.Right;
                else
                    node.Parent.Right = node.Right;

                break;
            default:
                if (node.Right == null && node.Left != null)
                {
                    node.Left.Parent = node.Parent;

                    if (node.Parent.Left == node)
                        node.Parent.Left = node.Left;
                    else
                        node.Parent.Right = node.Left;
                }
                else
                {
                    throw new Exception("Cannot delete two child node unambiguosly");
                }

                break;
        }

        Count--;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public IEnumerable<T> Children(T value)
    {
        var node = Find(value);

        if (node != null) return new[] { node.Left, node.Right }.Where(x => x != null).Select(x => x.Value);

        return null;
    }

    private int GetHeight(BinaryTreeNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    private BinaryTreeNode<T> Find(T value)
    {
        return Root == null ? null : Find(Root, value);
    }

    private BinaryTreeNode<T> Find(BinaryTreeNode<T> parent, T value)
    {
        while (true)
        {
            if (parent == null) return null;

            if (parent.Value.CompareTo(value) == 0) return parent;

            var left = Find(parent.Left, value);

            if (left != null) return left;

            parent = parent.Right;
        }
    }
}

internal class BinaryTreeNode<T> : IComparable where T : IComparable
{
    internal BinaryTreeNode(BinaryTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal T Value { get; set; }

    internal BinaryTreeNode<T> Parent { get; set; }

    internal BinaryTreeNode<T> Left { get; set; }
    internal BinaryTreeNode<T> Right { get; set; }

    internal bool IsLeaf => Left == null && Right == null;

    public int CompareTo(object obj)
    {
        return Value.CompareTo(obj as BinaryTreeNode<T>);
    }
}

internal class BinaryTreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly BinaryTreeNode<T> root;
    private Stack<BinaryTreeNode<T>> progress;

    internal BinaryTreeEnumerator(BinaryTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            progress = new Stack<BinaryTreeNode<T>>(new[] { root.Left, root.Right }.Where(x => x != null));
            Current = root.Value;
            return true;
        }

        if (progress.Count > 0)
        {
            var next = progress.Pop();
            Current = next.Value;

            foreach (var node in new[] { next.Left, next.Right }.Where(x => x != null)) progress.Push(node);

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