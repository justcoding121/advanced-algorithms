using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A red black tree implementation.
    /// </summary>
    public class RedBlackTree<T> : IEnumerable<T> where T : IComparable
    {     
        private readonly Dictionary<T, BSTNodeBase<T>> nodeLookUp;
        internal RedBlackTreeNode<T> Root { get; set; }
        public int Count { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enableNodeLookUp">Enabling lookup will fasten deletion/insertion/exists operations
        /// at the cost of additional space.</param>
        public RedBlackTree(bool enableNodeLookUp = false, IEqualityComparer<T> equalityComparer = null)
        {
            if (enableNodeLookUp)
            {
                nodeLookUp = new Dictionary<T, BSTNodeBase<T>>(equalityComparer);
            }
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public int GetHeight()
        {
            return Root.GetHeight();
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            if (nodeLookUp != null)
            {
                return nodeLookUp.ContainsKey(value);
            }

            return find(value) != null;
        }

        /// <summary>
        ///  Time complexity: O(1)
        /// </summary>
        internal void Clear()
        {
            Root = null;
            Count = 0;
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public T Max()
        {
            return Root.FindMax().Value;
        }

        private RedBlackTreeNode<T> findMax(RedBlackTreeNode<T> node)
        {
            return node.FindMax() as RedBlackTreeNode<T>;
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public T Min()
        {
            return Root.FindMin().Value;
        }

        //O(log(n)) worst O(n) for unbalanced tree
        internal RedBlackTreeNode<T> FindNode(T value)
        {
            return Root == null ? null : find(value);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        internal bool Exists(T value)
        {
            return FindNode(value) != null;
        }

        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //O(log(n)) worst O(n) for unbalanced tree
        private RedBlackTreeNode<T> find(T value)
        {
            if (nodeLookUp != null)
            {
                return nodeLookUp[value] as RedBlackTreeNode<T>;
            }

            return Root.Find<T>(value) as RedBlackTreeNode<T>;
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public void Insert(T value)
        {
            InsertAndReturnNode(value);
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        internal RedBlackTreeNode<T> InsertAndReturnNode(T value)
        {
            //empty tree
            if (Root == null)
            {
                Root = new RedBlackTreeNode<T>(null, value) { NodeColor = RedBlackTreeNodeColor.Black };
                if (nodeLookUp != null)
                {
                    nodeLookUp[value] = Root;
                }
                Count++;
                return Root;
            }

            var newNode = insert(Root, value);

            if (nodeLookUp != null)
            {
                nodeLookUp[value] = newNode;
            }

            Count++;

            return newNode;
        }

        //O(log(n)) always
        private RedBlackTreeNode<T> insert(RedBlackTreeNode<T> currentNode, T newNodeValue)
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
                        var node = currentNode.Right = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                        balanceInsertion(currentNode.Right);
                        return node;
                    }

                    currentNode = currentNode.Right;
                }
                //current node is greater than new node
                else if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        //insert
                        var node = currentNode.Left = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                        balanceInsertion(currentNode.Left);
                        return node;
                    }

                    currentNode = currentNode.Left;
                }
                else
                {
                    //duplicate
                    throw new Exception("Item with same key exists");
                }
            }
        }

        private void balanceInsertion(RedBlackTreeNode<T> nodeToBalance)
        {
            while (true)
            {
                if (nodeToBalance == Root)
                {
                    nodeToBalance.NodeColor = RedBlackTreeNodeColor.Black;
                    return;
                }

                //if node to balance is red
                if (nodeToBalance.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    //red-red relation; fix it!
                    if (nodeToBalance.Parent.NodeColor == RedBlackTreeNodeColor.Red)
                    {
                        //red sibling
                        if (nodeToBalance.Parent.Sibling != null && nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Red)
                        {
                            //mark both children of parent as black and move up balancing 
                            nodeToBalance.Parent.Sibling.NodeColor = RedBlackTreeNodeColor.Black;
                            nodeToBalance.Parent.NodeColor = RedBlackTreeNodeColor.Black;

                            //root is always black
                            if (nodeToBalance.Parent.Parent != Root)
                            {
                                nodeToBalance.Parent.Parent.NodeColor = RedBlackTreeNodeColor.Red;
                            }

                            nodeToBalance = nodeToBalance.Parent.Parent;
                        }
                        //absent sibling or black sibling
                        else if (nodeToBalance.Parent.Sibling == null || nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Black)
                        {
                            if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsLeftChild)
                            {
                                var newRoot = nodeToBalance.Parent;
                                swapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                                rightRotate(nodeToBalance.Parent.Parent);

                                if (newRoot == Root)
                                {
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;
                                }

                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsRightChild)
                            {
                                rightRotate(nodeToBalance.Parent);

                                var newRoot = nodeToBalance;

                                swapColors(nodeToBalance.Parent, nodeToBalance);
                                leftRotate(nodeToBalance.Parent);

                                if (newRoot == Root)
                                {
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;
                                }

                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsRightChild)
                            {
                                var newRoot = nodeToBalance.Parent;
                                swapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                                leftRotate(nodeToBalance.Parent.Parent);

                                if (newRoot == Root)
                                {
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;
                                }

                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsLeftChild)
                            {
                                leftRotate(nodeToBalance.Parent);

                                var newRoot = nodeToBalance;

                                swapColors(nodeToBalance.Parent, nodeToBalance);
                                rightRotate(nodeToBalance.Parent);

                                if (newRoot == Root)
                                {
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;
                                }

                                nodeToBalance = newRoot;
                            }
                        }
                    }
                }

                if (nodeToBalance.Parent != null)
                {
                    nodeToBalance = nodeToBalance.Parent;
                    continue;
                }

                break;
            }
        }

        private void swapColors(RedBlackTreeNode<T> node1, RedBlackTreeNode<T> node2)
        {
            var tmpColor = node2.NodeColor;
            node2.NodeColor = node1.NodeColor;
            node1.NodeColor = tmpColor;
        }

        /// <summary>
        ///  Time complexity: O(log(n))
        /// </summary>
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty Tree");
            }

            var node = find(value);

            if (node == null)
            {
                throw new Exception("The given value was not found in this bst.");
            }

            delete(node);

            if (nodeLookUp != null)
            {
                nodeLookUp.Remove(value);
            }

            Count--;
        }

        //O(log(n)) always
        private void delete(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> nodeToBalance = null;

            //node is a leaf node
            if (node.IsLeaf)
            {
                //if color is red, we are good; no need to balance
                if (node.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    deleteLeaf(node);
                    return;
                }

                nodeToBalance = handleDoubleBlack(node);
                deleteLeaf(node);
            }
            else
            {
                //case one - right tree is null (move sub tree up)
                if (node.Left != null && node.Right == null)
                {
                    nodeToBalance = handleDoubleBlack(node);
                    deleteLeftNode(node);
                }
                //case two - left tree is null  (move sub tree up)
                else if (node.Right != null && node.Left == null)
                {
                    nodeToBalance = handleDoubleBlack(node);
                    deleteRightNode(node);

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
                    delete(maxLeftNode);
                    return;
                }
            }

            //handle six cases
            while (nodeToBalance != null)
            {
                nodeToBalance = handleDoubleBlack(nodeToBalance);
            }

        }

        private void deleteLeaf(RedBlackTreeNode<T> node)
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

        private void deleteRightNode(RedBlackTreeNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                return;
            }

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

            if (node.Right.NodeColor != RedBlackTreeNodeColor.Red)
            {
                return;
            }

            //black deletion! But we can take its red child and recolor it to black
            //and we are done!
            node.Right.NodeColor = RedBlackTreeNodeColor.Black;

        }

        private void deleteLeftNode(RedBlackTreeNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                return;
            }

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

            if (node.Left.NodeColor != RedBlackTreeNodeColor.Red)
            {
                return;
            }

            //black deletion! But we can take its red child and recolor it to black
            //and we are done!
            node.Left.NodeColor = RedBlackTreeNodeColor.Black;
        }

        private void rightRotate(RedBlackTreeNode<T> node)
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

            if (prevRoot == Root)
            {
                Root = newRoot;
            }

        }

        private void leftRotate(RedBlackTreeNode<T> node)
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

            if (prevRoot == Root)
            {
                Root = newRoot;
            }
        }

        private RedBlackTreeNode<T> handleDoubleBlack(RedBlackTreeNode<T> node)
        {
            //case 1
            if (node == Root)
            {
                node.NodeColor = RedBlackTreeNodeColor.Black;
                return null;
            }

            //case 2
            if (node.Parent != null
                 && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                 && node.Sibling != null
                 && node.Sibling.NodeColor == RedBlackTreeNodeColor.Red
                 && ((node.Sibling.Left == null && node.Sibling.Right == null)
                 || (node.Sibling.Left != null && node.Sibling.Right != null
                   && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                   && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black)))
            {
                node.Parent.NodeColor = RedBlackTreeNodeColor.Red;
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Black;

                if (node.Sibling.IsRightChild)
                {
                    leftRotate(node.Parent);
                }
                else
                {
                    rightRotate(node.Parent);
                }

                return node;
            }
            //case 3
            if (node.Parent != null
             && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
             && node.Sibling != null
             && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
             && (node.Sibling.Left == null && node.Sibling.Right == null
             || node.Sibling.Left != null && node.Sibling.Right != null
                                          && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                          && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
            {
                //pushed up the double black problem up to parent
                //so now it needs to be fixed
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
                return node.Parent;
            }


            //case 4
            if (node.Parent != null
                 && node.Parent.NodeColor == RedBlackTreeNodeColor.Red
                 && node.Sibling != null
                 && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                 && (node.Sibling.Left == null && node.Sibling.Right == null
                 || node.Sibling.Left != null && node.Sibling.Right != null
                                              && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                                              && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black))
            {
                //just swap the color of parent and sibling
                //which will compensate the loss of black count 
                node.Parent.NodeColor = RedBlackTreeNodeColor.Black;
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;

                return null;
            }


            //case 5
            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.IsRightChild
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Left != null
                && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Red
                && node.Sibling.Right != null
                && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black)
            {
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
                node.Sibling.Left.NodeColor = RedBlackTreeNodeColor.Black;
                rightRotate(node.Sibling);

                return node;
            }

            //case 5 mirror
            if (node.Parent != null
               && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
               && node.Sibling != null
               && node.Sibling.IsLeftChild
               && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
               && node.Sibling.Left != null
               && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
               && node.Sibling.Right != null
               && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Red)
            {
                node.Sibling.NodeColor = RedBlackTreeNodeColor.Red;
                node.Sibling.Right.NodeColor = RedBlackTreeNodeColor.Black;
                leftRotate(node.Sibling);

                return node;
            }

            //case 6
            if (node.Parent != null
                && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling != null
                && node.Sibling.IsRightChild
                && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
                && node.Sibling.Right != null
                && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Red)
            {
                //left rotate to increase the black count on left side by one
                //and mark the red right child of sibling to black 
                //to compensate the loss of Black on right side of parent
                node.Sibling.Right.NodeColor = RedBlackTreeNodeColor.Black;
                leftRotate(node.Parent);

                return null;
            }

            //case 6 mirror
            if (node.Parent != null
              && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
              && node.Sibling != null
              && node.Sibling.IsLeftChild
              && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
              && node.Sibling.Left != null
              && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Red)
            {
                //right rotate to increase the black count on right side by one
                //and mark the red left child of sibling to black
                //to compensate the loss of Black on right side of parent
                node.Sibling.Left.NodeColor = RedBlackTreeNodeColor.Black;
                rightRotate(node.Parent);

                return null;
            }
            return null;
        }

        /// <summary>
        ///     Get the next lower value to given value in this BST.
        /// </summary>
        public T NextLower(T value)
        {
            var node = FindNode(value);
            if (node == null)
            {
                return default(T);
            }

            var next = (node as BSTNodeBase<T>).NextLower();
            return next != null ? next.Value : default(T);
        }

        /// <summary>
        ///     Get the next higher to given value in this BST.
        /// </summary>
        public T NextHigher(T value)
        {
            var node = FindNode(value);
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

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (nodeLookUp != null)
            {
                return new BSTNodeLookUpEnumerator<T>(nodeLookUp);
            }

            return new BSTEnumerator<T>(Root);
        }
    }

    internal enum RedBlackTreeNodeColor
    {
        Black,
        Red
    }

    /// <summary>
    /// Red black tree node
    /// </summary>
    internal class RedBlackTreeNode<T> : BSTNodeBase<T> where T : IComparable
    {
        internal new RedBlackTreeNode<T> Parent
        {
            get { return (RedBlackTreeNode<T>)base.Parent; }
            set { base.Parent = value; }
        }

        internal new RedBlackTreeNode<T> Left
        {
            get { return (RedBlackTreeNode<T>)base.Left; }
            set { base.Left = value; }
        }

        internal new RedBlackTreeNode<T> Right
        {
            get { return (RedBlackTreeNode<T>)base.Right; }
            set { base.Right = value; }
        }

        internal RedBlackTreeNodeColor NodeColor { get; set; }

        internal RedBlackTreeNode<T> Sibling => Parent.Left == this ?
                                                Parent.Right : Parent.Left;

        internal RedBlackTreeNode(RedBlackTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            NodeColor = RedBlackTreeNodeColor.Red;
        }
    }
}