using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A binary search tree implementation.
/// </summary>
public class Bst<T> : IEnumerable<T> where T : IComparable
{
    public Bst()
    {
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    public Bst(IEnumerable<T> sortedCollection) : this()
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new BstNode<T>(null, x)).ToArray();
        Root = (BstNode<T>)BstHelpers.ToBst(nodes);
        BstHelpers.AssignCount(Root);
    }

    internal BstNode<T> Root { get; set; }

    public int Count => Root == null ? 0 : Root.Count;

    //Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new BstEnumerator<T>(Root);
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
    internal int GetHeight()
    {
        return GetHeight(Root);
    }

    //worst O(n) for unbalanced tree
    private int GetHeight(BstNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }


    internal BstNode<T> InsertAndReturnNewNode(T value)
    {
        if (Root == null)
        {
            Root = new BstNode<T>(null, value);
            return Root;
        }

        var newNode = Insert(Root, value);
        return newNode;
    }


    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new BstNode<T>(null, value);
            return;
        }

        var newNode = Insert(Root, value);
        newNode.UpdateCounts(true);
    }


    //worst O(n) for unbalanced tree
    private BstNode<T> Insert(BstNode<T> currentNode, T newNodeValue)
    {
        while (true)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right != null)
                {
                    currentNode = currentNode.Right;
                    continue;
                }

                //insert
                currentNode.Right = new BstNode<T>(currentNode, newNodeValue);
                return currentNode.Right;
            }
            //current node is greater than new node

            if (compareResult > 0)
            {
                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new BstNode<T>(currentNode, newNodeValue);
                    return currentNode.Left;
                }

                currentNode = currentNode.Left;
            }
            else
            {
                throw new Exception("Item exists");
            }
        }
    }

    // <summary>
    /// Time complexity: O(n)
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T ElementAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentNullException("index");

        return Root.KthSmallest(index).Value;
    }


    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Delete(T value)
    {
        if (Root == null) throw new Exception("Empty BST");

        var deleted = Delete(Root, value);
        deleted.UpdateCounts(true);
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as BstNode<T>;

        var deleted = Delete(nodeToDelete, nodeToDelete.Value);
        deleted.UpdateCounts(true);

        return nodeToDelete.Value;
    }

    //worst O(n) for unbalanced tree
    private BstNode<T> Delete(BstNode<T> node, T value)
    {
        while (true)
        {
            if (node != null)
            {
                var compareResult = node.Value.CompareTo(value);

                //node is less than the search value so move right to find the deletion node
                if (compareResult < 0)
                {
                    node = node.Right ?? throw new Exception("Item do not exist");
                    continue;
                }

                //node is less than the search value so move left to find the deletion node
                if (compareResult > 0)
                {
                    node = node.Left ?? throw new Exception("Item do not exist");
                    continue;
                }
            }

            if (node == null) return null;


            //node is a leaf node
            if (node.IsLeaf)
            {
                DeleteLeaf(node);
                return node;
            }

            //case one - right tree is null (move sub tree up)
            if (node.Left != null && node.Right == null)
            {
                DeleteLeftNode(node);
                return node;
            }

            //case two - left tree is null  (move sub tree up)
            if (node.Right != null && node.Left == null)
            {
                DeleteRightNode(node);
                return node;
            }

            //case three - two child trees 
            //replace the node value with maximum element of left subtree (left max node)
            //and then delete the left max node
            var maxLeftNode = FindMax(node.Left);

            node.Value = maxLeftNode.Value;

            //delete left max node
            node = node.Left;
            value = maxLeftNode.Value;
        }
    }

    private void DeleteLeaf(BstNode<T> node)
    {
        //if node is root
        if (node.Parent == null)
            Root = null;
        //assign nodes parent.left/right to null
        else if (node.IsLeftChild)
            node.Parent.Left = null;
        else
            node.Parent.Right = null;
    }

    private void DeleteRightNode(BstNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Right.Parent = null;
            Root = Root.Right;
        }
        else
        {
            //node is left child of parent
            if (node.IsLeftChild)
                node.Parent.Left = node.Right;
            //node is right child of parent
            else
                node.Parent.Right = node.Right;

            node.Right.Parent = node.Parent;
        }
    }

    private void DeleteLeftNode(BstNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Left.Parent = null;
            Root = Root.Left;
        }
        else
        {
            //node is left child of parent
            if (node.IsLeftChild)
                node.Parent.Left = node.Left;
            //node is right child of parent
            else
                node.Parent.Right = node.Left;

            node.Left.Parent = node.Parent;
        }
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private BstNode<T> FindMax(BstNode<T> node)
    {
        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMin()
    {
        return FindMin(Root).Value;
    }

    private BstNode<T> FindMin(BstNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;
            node = node.Left;
        }
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //worst O(n) for unbalanced tree
    internal BstNode<T> FindNode(T value)
    {
        return Find(Root, value);
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //worst O(n) for unbalanced tree
    private BstNode<T> Find(BstNode<T> parent, T value)
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

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    //O(log(n)) worst O(n) for unbalanced tree
    private BstNodeBase<T> Find(T value)
    {
        return Root.Find(value).Item1;
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    ///     Time complexity: O(n)
    /// </summary>
    public T NextLower(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextLower();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Get the next higher value to given value in this BST.
    ///     Time complexity: O(n)
    /// </summary>
    public T NextHigher(T value)
    {
        var node = Find(value);
        if (node == null) return default;

        var next = node.NextHigher();
        return next != null ? next.Value : default;
    }

    /// <summary>
    ///     Descending enumerable.
    /// </summary>
    public IEnumerable<T> AsEnumerableDesc()
    {
        return GetEnumeratorDesc().AsEnumerable();
    }

    public IEnumerator<T> GetEnumeratorDesc()
    {
        return new BstEnumerator<T>(Root, false);
    }
}

internal class BstNode<T> : BstNodeBase<T> where T : IComparable
{
    internal BstNode(BstNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal new BstNode<T> Parent
    {
        get => (BstNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new BstNode<T> Left
    {
        get => (BstNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new BstNode<T> Right
    {
        get => (BstNode<T>)base.Right;
        set => base.Right = value;
    }
}