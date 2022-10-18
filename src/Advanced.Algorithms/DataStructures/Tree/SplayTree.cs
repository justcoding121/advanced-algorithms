using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A splay tree implementation.
/// </summary>
public class SplayTree<T> : IEnumerable<T> where T : IComparable
{
    public SplayTree()
    {
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="sortedCollection">The sorted collection.</param>
    public SplayTree(IEnumerable<T> sortedCollection) : this()
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new SplayTreeNode<T>(null, x)).ToArray();
        Root = (SplayTreeNode<T>)BstHelpers.ToBst(nodes);
        BstHelpers.AssignCount(Root);
    }

    internal SplayTreeNode<T> Root { get; set; }
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

    private int GetHeight(SplayTreeNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new SplayTreeNode<T>(null, value);
            return;
        }

        var newNode = Insert(Root, value);
        Splay(newNode);
    }

    //O(log(n)) always
    private SplayTreeNode<T> Insert(SplayTreeNode<T> currentNode, T newNodeValue)
    {
        while (true)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right == null)
                {
                    //insert
                    currentNode.Right = new SplayTreeNode<T>(currentNode, newNodeValue);
                    return currentNode.Right;
                }

                currentNode = currentNode.Right;
            }
            //current node is greater than new node
            else if (compareResult > 0)
            {
                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new SplayTreeNode<T>(currentNode, newNodeValue);
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

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public int IndexOf(T item)
    {
        return Root.Position(item);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
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
        if (Root == null) throw new Exception("Empty SplayTree");

        Delete(Root, value);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as SplayTreeNode<T>;

        Delete(nodeToDelete, nodeToDelete.Value);

        return nodeToDelete.Value;
    }

    private void Delete(SplayTreeNode<T> node, T value)
    {
        while (true)
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

            var parent = node.Parent;
            //node is a leaf node
            if (node.IsLeaf)
            {
                DeleteLeaf(node);
            }
            else
            {
                //case one - right tree is null (move sub tree up)
                if (node.Left != null && node.Right == null)
                {
                    DeleteLeftNode(node);
                }
                //case two - left tree is null  (move sub tree up)
                else if (node.Right != null && node.Left == null)
                {
                    DeleteRightNode(node);
                }
                //case three - two child trees 
                //replace the node value with maximum element of left subtree (left max node)
                //and then delete the left max node
                else
                {
                    var maxLeftNode = FindMax(node.Left);

                    node.Value = maxLeftNode.Value;

                    //delete left max node
                    Delete(node.Left, maxLeftNode.Value);
                }
            }

            if (parent != null) Splay(parent);

            break;
        }
    }

    private void DeleteLeaf(SplayTreeNode<T> node)
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

    private void DeleteRightNode(SplayTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Right.Parent = null;
            Root = Root.Right;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Right;
        //node is right child of parent
        else
            node.Parent.Right = node.Right;

        node.Right.Parent = node.Parent;
    }

    private void DeleteLeftNode(SplayTreeNode<T> node)
    {
        //root
        if (node.Parent == null)
        {
            Root.Left.Parent = null;
            Root = Root.Left;
            return;
        }

        //node is left child of parent
        if (node.IsLeftChild)
            node.Parent.Left = node.Left;
        //node is right child of parent
        else
            node.Parent.Right = node.Left;

        node.Left.Parent = node.Parent;
    }

    /// <summary>
    ///     Time complexity: O(n)
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private SplayTreeNode<T> FindMax(SplayTreeNode<T> node)
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

    private SplayTreeNode<T> FindMin(SplayTreeNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;
            node = node.Left;
        }
    }

    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private SplayTreeNode<T> Find(SplayTreeNode<T> parent, T value)
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

    private void Splay(SplayTreeNode<T> x)
    {
        x.UpdateCounts();

        while (x.Parent != null)
        {
            if (x.Parent.Parent == null)
            {
                //zig step
                x = x.IsLeftChild ? RightRotate(x.Parent) : LeftRotate(x.Parent);
            }
            //zig-zig step
            else if (x.IsLeftChild && x.Parent.IsLeftChild)

            {
                RightRotate(x.Parent.Parent);
                x = RightRotate(x.Parent);
            }
            //zig-zig step mirror
            else if (x.IsRightChild && x.Parent.IsRightChild)
            {
                LeftRotate(x.Parent.Parent);
                x = LeftRotate(x.Parent);
            }
            //zig-zag step
            else if (x.IsLeftChild && x.Parent.IsRightChild)
            {
                RightRotate(x.Parent);
                x = LeftRotate(x.Parent);
            }
            //zig-zag step mirror
            else //if (x.IsRightChild && x.Parent.IsLeftChild)
            {
                LeftRotate(x.Parent);
                x = RightRotate(x.Parent);
            }

            x.UpdateCounts();
        }
    }

    /// <summary>
    ///     Rotates current root right and returns the new root node
    /// </summary>
    private SplayTreeNode<T> RightRotate(SplayTreeNode<T> currentRoot)
    {
        var prevRoot = currentRoot;
        var leftRightChild = prevRoot.Left.Right;

        var newRoot = currentRoot.Left;

        //make left child as root
        prevRoot.Left.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Left;
            else
                prevRoot.Parent.Right = prevRoot.Left;
        }

        //move prev root as right child of current root
        newRoot.Right = prevRoot;
        prevRoot.Parent = newRoot;

        //move right child of left child of prev root to left child of right child of new root
        newRoot.Right.Left = leftRightChild;
        if (newRoot.Right.Left != null) newRoot.Right.Left.Parent = newRoot.Right;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;

        return newRoot;
    }

    /// <summary>
    ///     Rotates the current root left and returns new root
    /// </summary>
    private SplayTreeNode<T> LeftRotate(SplayTreeNode<T> currentRoot)
    {
        var prevRoot = currentRoot;
        var rightLeftChild = prevRoot.Right.Left;

        var newRoot = currentRoot.Right;

        //make right child as root
        prevRoot.Right.Parent = prevRoot.Parent;

        if (prevRoot.Parent != null)
        {
            if (prevRoot.Parent.Left == prevRoot)
                prevRoot.Parent.Left = prevRoot.Right;
            else
                prevRoot.Parent.Right = prevRoot.Right;
        }

        //move prev root as left child of current root
        newRoot.Left = prevRoot;
        prevRoot.Parent = newRoot;

        //move left child of right child of prev root to right child of left child of new root
        newRoot.Left.Right = rightLeftChild;
        if (newRoot.Left.Right != null) newRoot.Left.Right.Parent = newRoot.Left;

        newRoot.Left.UpdateCounts();
        newRoot.Right.UpdateCounts();
        newRoot.UpdateCounts();

        if (prevRoot == Root) Root = newRoot;

        return newRoot;
    }


    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private BstNodeBase<T> Find(T value)
    {
        return Root.Find(value).Item1;
    }

    /// <summary>
    ///     Get the next lower value to given value in this BST.
    ///     Time complexity: O(n).
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
    ///     Time complexity: O(n).
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

internal class SplayTreeNode<T> : BstNodeBase<T> where T : IComparable
{
    internal SplayTreeNode(SplayTreeNode<T> parent, T value)
    {
        Parent = parent;
        Value = value;
    }

    internal new SplayTreeNode<T> Parent
    {
        get => (SplayTreeNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new SplayTreeNode<T> Left
    {
        get => (SplayTreeNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new SplayTreeNode<T> Right
    {
        get => (SplayTreeNode<T>)base.Right;
        set => base.Right = value;
    }
}