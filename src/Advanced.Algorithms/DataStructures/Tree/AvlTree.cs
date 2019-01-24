using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// An AVL tree implementation.
    /// </summary>
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        private readonly Dictionary<T, BSTNodeBase<T>> nodeLookUp;

        internal AVLTreeNode<T> Root { get; set; }

        public int Count => Root == null ? 0 : Root.Count;

        /// <param name="enableNodeLookUp">Enabling lookup will fasten deletion/insertion/exists operations
        /// at the cost of additional space.</param>
        public AVLTree(bool enableNodeLookUp = false)
        {
            if (enableNodeLookUp)
            {
                nodeLookUp = new Dictionary<T, BSTNodeBase<T>>();
            }
        }

        /// <summary>
        /// Initialize the BST with given sorted keys.
        /// Time complexity: O(n).
        /// </summary>
        /// <param name="sortedCollection">The initial sorted collection.</param>
        /// <param name="enableNodeLookUp">Enabling lookup will fasten deletion/insertion/exists operations
        ///  at the cost of additional space.</param>
        public AVLTree(IEnumerable<T> sortedCollection, bool enableNodeLookUp = false)
        {
            BSTHelpers.ValidateSortedCollection(sortedCollection);
            var nodes = sortedCollection.Select(x => new AVLTreeNode<T>(null, x)).ToArray();
            Root = (AVLTreeNode<T>)BSTHelpers.ToBST(nodes);
            recomputeHeight(Root);
            BSTHelpers.AssignCount(Root);

            if (enableNodeLookUp)
            {
                nodeLookUp = nodes.ToDictionary(x => x.Value, x => x as BSTNodeBase<T>);
            }
        }


        /// <summary>
        /// Time complexity: O(log(n))
        /// </summary>
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return find(Root, value) != null;
        }

        /// <summary>
        /// Time complexity: O(log(n))
        /// </summary>
        internal int GetHeight()
        {
            if (Root == null)
                return -1;

            return Root.Height;
        }

        /// <summary>
        /// Time complexity: O(log(n))
        /// </summary>
        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new AVLTreeNode<T>(null, value);
                if (nodeLookUp != null)
                {
                    nodeLookUp[value] = Root;
                }

                return;
            }

            insert(Root, value);

        }

        /// <summary>
        /// Time complexity: O(log(n))
        /// </summary>
        private void insert(AVLTreeNode<T> node, T value)
        {
            var compareResult = node.Value.CompareTo(value);

            //node is less than the value so move right for insertion
            if (compareResult < 0)
            {
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(node, value);
                    if (nodeLookUp != null)
                    {
                        nodeLookUp[value] = node.Right;
                    }
                }
                else
                {
                    insert(node.Right, value);
                }
            }
            //node is greater than the value so move left for insertion
            else if (compareResult > 0)
            {
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(node, value);
                    if (nodeLookUp != null)
                    {
                        nodeLookUp[value] = node.Left;
                    }
                }
                else
                {
                    insert(node.Left, value);
                }

            }
            else
            {
                throw new Exception("Item exists");
            }

            updateHeight(node);
            balance(node);

            node.UpdateCounts();
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public int IndexOf(T item)
        {
            return Root.Position(item);
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public T ElementAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentNullException("index");
            }

            return Root.KthSmallest(index).Value;
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty AVLTree");
            }

            delete(Root, value);

            if (nodeLookUp != null)
            {
                nodeLookUp.Remove(value);
            }

        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentException("index");
            }

            var nodeToDelete = Root.KthSmallest(index) as AVLTreeNode<T>;
            var nodeToBalance = delete(nodeToDelete, nodeToDelete.Value);

            while (nodeToBalance != null)
            {
                nodeToBalance.UpdateCounts();
                updateHeight(nodeToBalance);
                balance(nodeToBalance);

                nodeToBalance = nodeToBalance.Parent;
            }

            if (nodeLookUp != null)
            {
                nodeLookUp.Remove(nodeToDelete.Value);
            }

            return nodeToDelete.Value;
        }

        private AVLTreeNode<T> delete(AVLTreeNode<T> node, T value)
        {
            var baseCase = false;

            var compareResult = node.Value.CompareTo(value);

            //node is less than the search value so move right to find the deletion node
            if (compareResult < 0)
            {
                if (node.Right == null)
                {
                    throw new Exception("Item do not exist");
                }

                delete(node.Right, value);
            }
            //node is less than the search value so move left to find the deletion node
            else if (compareResult > 0)
            {
                if (node.Left == null)
                {
                    throw new Exception("Item do not exist");
                }

                delete(node.Left, value);
            }
            else
            {
                //node is a leaf node
                if (node.IsLeaf)
                {
                    //if node is root
                    if (node.Parent == null)
                    {
                        Root = null;
                    }
                    //assign nodes parent.left/right to null
                    else if (node.Parent.Left == node)
                    {
                        node.Parent.Left = null;
                    }
                    else
                    {
                        node.Parent.Right = null;
                    }

                    baseCase = true;
                }
                else
                {
                    //case one - right tree is null (move sub tree up)
                    if (node.Left != null && node.Right == null)
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
                            if (node.Parent.Left == node)
                            {
                                node.Parent.Left = node.Left;
                            }
                            //node is right child of parent
                            else
                            {
                                node.Parent.Right = node.Left;
                            }

                            node.Left.Parent = node.Parent;
                        }

                        baseCase = true;
                    }
                    //case two - left tree is null  (move sub tree up)
                    else if (node.Right != null && node.Left == null)
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
                            if (node.Parent.Left == node)
                            {
                                node.Parent.Left = node.Right;
                            }
                            //node is right child of parent
                            else
                            {
                                node.Parent.Right = node.Right;
                            }

                            node.Right.Parent = node.Parent;
                        }

                        baseCase = true;
                    }
                    //case three - two child trees 
                    //replace the node value with maximum element of left subtree (left max node)
                    //and then delete the left max node
                    else
                    {
                        var maxLeftNode = findMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        if (nodeLookUp != null)
                        {
                            nodeLookUp[node.Value] = node;
                        }

                        //delete left max node
                        delete(node.Left, maxLeftNode.Value);
                    }
                }
            }

            if (baseCase)
            {
                node.Parent.UpdateCounts();
                updateHeight(node.Parent);
                balance(node.Parent);
                return node.Parent;
            }
            else
            {
                node.UpdateCounts();
                updateHeight(node);
                balance(node);
                return node;
            }
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T FindMax()
        {
            return findMax(Root).Value;
        }

        private AVLTreeNode<T> findMax(AVLTreeNode<T> node)
        {
            while (true)
            {
                if (node.Right == null)
                {
                    return node;
                }

                node = node.Right;
            }
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T FindMin()
        {
            return findMin(Root).Value;
        }

        private AVLTreeNode<T> findMin(AVLTreeNode<T> node)
        {
            while (true)
            {
                if (node.Left == null)
                {
                    return node;
                }

                node = node.Left;
            }
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public bool Contains(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return find(Root, value) != null;
        }


        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        private AVLTreeNode<T> find(T value)
        {
            if (nodeLookUp != null)
            {
                return nodeLookUp[value] as AVLTreeNode<T>;
            }

            return Root.Find(value).Item1 as AVLTreeNode<T>;
        }

        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        private AVLTreeNode<T> find(AVLTreeNode<T> parent, T value)
        {
            if (parent == null)
            {
                return null;
            }

            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            var left = find(parent.Left, value);

            if (left != null)
            {
                return left;
            }

            var right = find(parent.Right, value);

            return right;
        }

        private void balance(AVLTreeNode<T> node)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
                return;

            var leftHeight = node.Left?.Height + 1 ?? 0;
            var rightHeight = node.Right?.Height + 1 ?? 0;

            var balanceFactor = leftHeight - rightHeight;
            //tree is left heavy
            //differance >=2 then do rotations
            if (balanceFactor >= 2)
            {
                leftHeight = node.Left?.Left?.Height + 1 ?? 0;
                rightHeight = node.Left?.Right?.Height + 1 ?? 0;

                //left child is left heavy
                if (leftHeight > rightHeight)
                {
                    rightRotate(node);
                }
                //left child is right heavy
                else
                {
                    leftRotate(node.Left);
                    rightRotate(node);
                }
            }
            //tree is right heavy
            //differance <=-2 then do rotations
            else if (balanceFactor <= -2)
            {
                leftHeight = node.Right?.Left?.Height + 1 ?? 0;
                rightHeight = node.Right?.Right?.Height + 1 ?? 0;

                //right child is right heavy
                if (rightHeight > leftHeight)
                {
                    leftRotate(node);
                }
                //right child is left heavy
                else
                {
                    rightRotate(node.Right);
                    leftRotate(node);
                }

            }
        }

        private void rightRotate(AVLTreeNode<T> node)
        {
            var prevRoot = node;
            var leftRightChild = prevRoot.Left.Right;

            var newRoot = node.Left;

            //make left child as root
            prevRoot.Left.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                {
                    prevRoot.Parent.Left = prevRoot.Left;
                }
                else
                {
                    prevRoot.Parent.Right = prevRoot.Left;
                }
            }

            //move prev root as right child of current root
            newRoot.Right = prevRoot;
            prevRoot.Parent = newRoot;

            //move right child of left child of prev root to left child of right child of new root
            newRoot.Right.Left = leftRightChild;
            if (newRoot.Right.Left != null)
            {
                newRoot.Right.Left.Parent = newRoot.Right;
            }

            updateHeight(newRoot);

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();

            if (prevRoot == Root)
            {
                Root = newRoot;
            }

        }

        private void leftRotate(AVLTreeNode<T> node)
        {
            var prevRoot = node;
            var rightLeftChild = prevRoot.Right.Left;

            var newRoot = node.Right;

            //make right child as root
            prevRoot.Right.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                {
                    prevRoot.Parent.Left = prevRoot.Right;
                }
                else
                {
                    prevRoot.Parent.Right = prevRoot.Right;
                }
            }

            //move prev root as left child of current root
            newRoot.Left = prevRoot;
            prevRoot.Parent = newRoot;

            //move left child of right child of prev root to right child of left child of new root
            newRoot.Left.Right = rightLeftChild;
            if (newRoot.Left.Right != null)
            {
                newRoot.Left.Right.Parent = newRoot.Left;
            }

            updateHeight(newRoot);

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();

            if (prevRoot == Root)
            {
                Root = newRoot;
            }
        }

        private void updateHeight(AVLTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                node.Left.Height = Math.Max(node.Left.Left?.Height + 1 ?? 0,
                                                 node.Left.Right?.Height + 1 ?? 0);
            }

            if (node.Right != null)
            {
                node.Right.Height = Math.Max(node.Right.Left?.Height + 1 ?? 0,
                                  node.Right.Right?.Height + 1 ?? 0);
            }

            node.Height = Math.Max(node.Left?.Height + 1 ?? 0,
                                      node.Right?.Height + 1 ?? 0);
        }

        private void recomputeHeight(AVLTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            recomputeHeight(node.Left);
            recomputeHeight(node.Right);

            updateHeight(node);
        }

        /// <summary>
        ///     Get the next lower value to given value in this BST.
        ///     Time complexity: O(log(n))
        /// </summary>
        public T NextLower(T value)
        {
            var node = find(value);
            if (node == null)
            {
                return default(T);
            }

            var next = (node as BSTNodeBase<T>).NextLower();
            return next != null ? next.Value : default(T);
        }

        /// <summary>
        ///     Get the next higher value to given value in this BST.
        ///     Time complexity: O(log(n))
        /// </summary>
        public T NextHigher(T value)
        {
            var node = find(value);
            if (node == null)
            {
                return default(T);
            }

            var next = (node as BSTNodeBase<T>).NextHigher();
            return next != null ? next.Value : default(T);
        }

        internal void Swap(T value1, T value2)
        {
            var node1 = find(value1);
            var node2 = find(value2);

            if (node1 == null || node2 == null)
            {
                throw new Exception("Value1, Value2 or both was not found in this BST.");
            }

            var tmp = node1.Value;
            node1.Value = node2.Value;
            node2.Value = tmp;

            if (nodeLookUp != null)
            {
                nodeLookUp[node1.Value] = node1;
                nodeLookUp[node2.Value] = node2;
            }
        }

        /// <summary>
        /// Descending enumerable.
        /// </summary>
        public IEnumerable<T> AsEnumerableDesc()
        {
            return GetEnumeratorDesc().AsEnumerable();
        }

        public IEnumerator<T> GetEnumeratorDesc()
        {
            return new BSTEnumerator<T>(Root, false);
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BSTEnumerator<T>(Root);
        }
    }

    internal class AVLTreeNode<T> : BSTNodeBase<T> where T : IComparable
    {
        internal new AVLTreeNode<T> Parent
        {
            get { return (AVLTreeNode<T>)base.Parent; }
            set { base.Parent = value; }
        }

        internal new AVLTreeNode<T> Left
        {
            get { return (AVLTreeNode<T>)base.Left; }
            set { base.Left = value; }
        }

        internal new AVLTreeNode<T> Right
        {
            get { return (AVLTreeNode<T>)base.Right; }
            set { base.Right = value; }
        }

        internal AVLTreeNode(AVLTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            Height = 0;
        }

        internal int Height { get; set; }
    }

}
