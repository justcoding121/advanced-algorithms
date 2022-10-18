using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures;

/// <summary>
///     A treap tree implementation.
/// </summary>
public class TreapTree<T> : IEnumerable<T> where T : IComparable
{
    private readonly Random rndGenerator = new();

    public TreapTree()
    {
    }

    /// <summary>
    ///     Initialize the BST with given sorted keys.
    ///     Time complexity: O(n).
    /// </summary>
    /// <param name="sortedCollection">The initial sorted collection.</param>
    public TreapTree(IEnumerable<T> sortedCollection) : this()
    {
        BstHelpers.ValidateSortedCollection(sortedCollection);
        var nodes = sortedCollection.Select(x => new TreapTreeNode<T>(null, x, rndGenerator.Next())).ToArray();
        Root = (TreapTreeNode<T>)BstHelpers.ToBst(nodes);
        BstHelpers.AssignCount(Root);
        Heapify(Root);
    }

    internal TreapTreeNode<T> Root { get; set; }
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
    ///     Time complexity: O(log(n))
    /// </summary>
    public bool HasItem(T value)
    {
        if (Root == null) return false;

        return Find(Root, value) != null;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    internal int GetHeight()
    {
        return GetHeight(Root);
    }

    private int GetHeight(TreapTreeNode<T> node)
    {
        if (node == null) return -1;

        return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Insert(T value)
    {
        if (Root == null)
        {
            Root = new TreapTreeNode<T>(null, value, rndGenerator.Next());
            return;
        }

        var newNode = Insert(Root, value);

        Heapify(newNode);
    }

    //O(log(n)) always
    private TreapTreeNode<T> Insert(TreapTreeNode<T> currentNode, T newNodeValue)
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
                    currentNode.Right = new TreapTreeNode<T>(currentNode, newNodeValue, rndGenerator.Next());
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
                    currentNode.Left = new TreapTreeNode<T>(currentNode, newNodeValue, rndGenerator.Next());
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
    ///     Time complexity: O(log(n))
    /// </summary>
    public void Delete(T value)
    {
        if (Root == null) throw new Exception("Empty TreapTree");

        Delete(Root, value);
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentException("index");

        var nodeToDelete = Root.KthSmallest(index) as TreapTreeNode<T>;

        Delete(nodeToDelete, nodeToDelete.Value);

        return nodeToDelete.Value;
    }

    private void Delete(TreapTreeNode<T> node, T value)
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

            //node is a leaf node
            if (node != null && node.IsLeaf)
            {
                DeleteLeaf(node);
            }
            else
            {
                //case one - right tree is null (move sub tree up)
                if (node?.Left != null && node.Right == null)
                {
                    DeleteLeftNode(node);
                }
                //case two - left tree is null  (move sub tree up)
                else if (node?.Right != null && node.Left == null)
                {
                    DeleteRightNode(node);
                }
                //case three - two child trees 
                //replace the node value with maximum element of left subtree (left max node)
                //and then delete the left max node
                else
                {
                    if (node != null)
                    {
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        //delete left max node
                        node = node.Left;
                        value = maxLeftNode.Value;
                    }

                    continue;
                }
            }

            break;
        }

        node.UpdateCounts(true);
    }

    private void DeleteLeaf(TreapTreeNode<T> node)
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

    private void DeleteRightNode(TreapTreeNode<T> node)
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

    private void DeleteLeftNode(TreapTreeNode<T> node)
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
    ///     Time complexity: O(log(n))
    /// </summary>
    public T FindMax()
    {
        return FindMax(Root).Value;
    }

    private TreapTreeNode<T> FindMax(TreapTreeNode<T> node)
    {
        while (true)
        {
            if (node.Right == null) return node;
            node = node.Right;
        }
    }

    /// <summary>
    ///     Time complexity: O(log(n))
    /// </summary>
    public T FindMin()
    {
        return FindMin(Root).Value;
    }

    private TreapTreeNode<T> FindMin(TreapTreeNode<T> node)
    {
        while (true)
        {
            if (node.Left == null) return node;

            node = node.Left;
        }
    }


    //find the node with the given identifier among descendants of parent and parent
    //uses pre-order traversal
    private TreapTreeNode<T> Find(TreapTreeNode<T> parent, T value)
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

    //reorder the tree node so that heap property is valid
    private void Heapify(TreapTreeNode<T> node)
    {
        while (node.Parent != null)
        {
            node.UpdateCounts();
            if (node.Priority < node.Parent.Priority)
                node = node.IsLeftChild ? RightRotate(node.Parent) : LeftRotate(node.Parent);
            else
                break;
        }

        node.UpdateCounts(true);
    }

    /// <summary>
    ///     Rotates current root right and returns the new root node
    /// </summary>
    private TreapTreeNode<T> RightRotate(TreapTreeNode<T> currentRoot)
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
    private TreapTreeNode<T> LeftRotate(TreapTreeNode<T> currentRoot)
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

internal class TreapTreeNode<T> : BstNodeBase<T> where T : IComparable
{
    internal TreapTreeNode(TreapTreeNode<T> parent, T value, int priority)
    {
        Parent = parent;
        Value = value;
        Priority = priority;
    }

    internal new TreapTreeNode<T> Parent
    {
        get => (TreapTreeNode<T>)base.Parent;
        set => base.Parent = value;
    }

    internal new TreapTreeNode<T> Left
    {
        get => (TreapTreeNode<T>)base.Left;
        set => base.Left = value;
    }

    internal new TreapTreeNode<T> Right
    {
        get => (TreapTreeNode<T>)base.Right;
        set => base.Right = value;
    }

    internal int Priority { get; set; }
}