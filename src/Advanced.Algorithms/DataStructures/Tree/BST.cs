using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A binary search tree implementation.
    /// </summary>
    public class BST<T> : IEnumerable<T> where T : IComparable
    {
        internal BSTNode<T> Root { get; set; }

        public int Count => Root == null ? 0 : Root.Count;

        public BST() { }

        /// <summary>
        /// Initialize the BST with given sorted keys.
        /// Time complexity: O(n).
        /// </summary>
        public BST(IEnumerable<T> sortedCollection) : this()
        {
            BSTHelpers.ValidateSortedCollection(sortedCollection);
            var nodes = sortedCollection.Select(x => new BSTNode<T>(null, x)).ToArray();
            Root = (BSTNode<T>)BSTHelpers.ToBST(nodes);
            BSTHelpers.AssignCount(Root);
        }

        /// <summary>
        /// Time complexity: O(n)
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
        /// Time complexity: O(n)
        /// </summary>
        internal int GetHeight()
        {
            return getHeight(Root);
        }

        //worst O(n) for unbalanced tree
        private int getHeight(BSTNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(getHeight(node.Left), getHeight(node.Right)) + 1;
        }


        internal BSTNode<T> InsertAndReturnNewNode(T value)
        {
            if (Root == null)
            {
                Root = new BSTNode<T>(null, value);
                return Root;
            }

            var newNode = insert(Root, value);
            return newNode;
        }


        /// <summary>
        /// Time complexity: O(n)
        /// </summary>
        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new BSTNode<T>(null, value);
                return;
            }

            var newNode = insert(Root, value);
            newNode.UpdateCounts(true);
        }



        //worst O(n) for unbalanced tree
        private BSTNode<T> insert(BSTNode<T> currentNode, T newNodeValue)
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
                    currentNode.Right = new BSTNode<T>(currentNode, newNodeValue);
                    return currentNode.Right;
                }
                //current node is greater than new node

                if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        //insert
                        currentNode.Left = new BSTNode<T>(currentNode, newNodeValue);
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
        ///  Time complexity: O(n)
        /// </summary>
        public int IndexOf(T item)
        {
            return Root.Position(item);
        }

        /// <summary>
        ///  Time complexity: O(n)
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
        /// Time complexity: O(n)
        /// </summary>
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty BST");
            }

            var deleted = delete(Root, value);
            deleted.UpdateCounts(true);
        }

        /// <summary>
        ///  Time complexity: O(n)
        /// </summary>
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentException("index");
            }

            var nodeToDelete = Root.KthSmallest(index) as BSTNode<T>;

            var deleted = delete(nodeToDelete, nodeToDelete.Value);
            deleted.UpdateCounts(true);

            return nodeToDelete.Value;
        }

        //worst O(n) for unbalanced tree
        private BSTNode<T> delete(BSTNode<T> node, T value)
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

                if (node == null)
                {
                    return null;
                }


                //node is a leaf node
                if (node.IsLeaf)
                {
                    deleteLeaf(node);
                    return node;
                }

                //case one - right tree is null (move sub tree up)
                if (node.Left != null && node.Right == null)
                {
                    deleteLeftNode(node);
                    return node;
                }

                //case two - left tree is null  (move sub tree up)
                if (node.Right != null && node.Left == null)
                {
                    deleteRightNode(node);
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

        private void deleteLeaf(BSTNode<T> node)
        {
            //if node is root
            if (node.Parent == null)
            {
                Root = null;
            }
            //assign nodes parent.left/right to null
            else if (node.IsLeftChild)
            {
                node.Parent.Left = null;
            }
            else
            {
                node.Parent.Right = null;
            }
        }

        private void deleteRightNode(BSTNode<T> node)
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
        }

        private void deleteLeftNode(BSTNode<T> node)
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
        }

        /// <summary>
        /// Time complexity: O(n)
        /// </summary>
        public T FindMax()
        {
            return FindMax(Root).Value;
        }

        private BSTNode<T> FindMax(BSTNode<T> node)
        {
            while (true)
            {
                if (node.Right == null) return node;
                node = node.Right;
            }
        }

        /// <summary>
        /// Time complexity: O(n)
        /// </summary>
        public T FindMin()
        {
            return findMin(Root).Value;
        }

        private BSTNode<T> findMin(BSTNode<T> node)
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
        internal BSTNode<T> FindNode(T value)
        {
            return find(Root, value);
        }

        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //worst O(n) for unbalanced tree
        private BSTNode<T> find(BSTNode<T> parent, T value)
        {
            while (true)
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

                parent = parent.Right;
            }
        }

        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //O(log(n)) worst O(n) for unbalanced tree
        private BSTNodeBase<T> find(T value)
        {
            return Root.Find<T>(value).Item1 as BSTNodeBase<T>;
        }

        /// <summary>
        ///     Get the next lower value to given value in this BST.
        ///     Time complexity: O(n)
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
        ///     Time complexity: O(n)
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

    internal class BSTNode<T> : BSTNodeBase<T> where T : IComparable
    {
        internal new BSTNode<T> Parent
        {
            get { return (BSTNode<T>)base.Parent; }
            set { base.Parent = value; }
        }

        internal new BSTNode<T> Left
        {
            get { return (BSTNode<T>)base.Left; }
            set { base.Left = value; }
        }

        internal new BSTNode<T> Right
        {
            get { return (BSTNode<T>)base.Right; }
            set { base.Right = value; }
        }

        internal BSTNode(BSTNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
        }
    }
}
