using System;

namespace Algorithm.Sandbox.DataStructures
{
    public enum RedBlackTreeNodeColor
    {
        Black,
        Red
    }

    public class AsRedBlackTreeNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsRedBlackTreeNode<T> Parent { get; set; }

        public AsRedBlackTreeNode<T> Left { get; set; }
        public AsRedBlackTreeNode<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;
        public RedBlackTreeNodeColor NodeColor { get; set; }

        public AsRedBlackTreeNode<T> Sibling => this.Parent.Left == this ?
                                                this.Parent.Right : this.Parent.Left;

        public bool IsLeftChild => this.Parent.Left == this;
        public bool IsRightChild => this.Parent.Right == this;

        public AsRedBlackTreeNode(AsRedBlackTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            NodeColor = RedBlackTreeNodeColor.Red;
        }
    }

    public class AsRedBlackTree<T> where T : IComparable
    {
        public AsRedBlackTreeNode<T> Root { get; set; }
        public int Count { get; private set; }

        //O(log(n)) worst O(n) for unbalanced tree
        public int GetHeight()
        {
            return GetHeight(Root);
        }

        public bool VerifyIsBinarySearchTree()
        {
            return VerifyIsBinarySearchTree(Root);
        }

        private bool VerifyIsBinarySearchTree(AsRedBlackTreeNode<T> node)
        {
            if (node == null)
            {
                return true;
            }

            if(node.Left!=null)
            {
                if (node.Left.Value.CompareTo(node.Value) > 0)
                {
                    return false;
                }
            }

            if (node.Right != null)
            {
                if (node.Right.Value.CompareTo(node.Value) < 0)
                {
                    return false;
                }
            }
            return VerifyIsBinarySearchTree(node.Left) &&
                VerifyIsBinarySearchTree(node.Right);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private int GetHeight(AsRedBlackTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }
        //O(log(n)) always
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }
        public T FindMax()
        {
            return FindMax(Root).Value;
        }


        private AsRedBlackTreeNode<T> FindMax(AsRedBlackTreeNode<T> node)
        {
            if (node.Right == null)
            {
                return node;
            }

            return FindMax(node.Right);
        }

        public T FindMin()
        {
            return FindMin(Root).Value;
        }

        private AsRedBlackTreeNode<T> FindMin(AsRedBlackTreeNode<T> node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private AsRedBlackTreeNode<T> Find(T value)
        {
            if (Root == null)
            {
                return null;
            }

            return Find(Root, value);
        }


        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //O(log(n)) worst O(n) for unbalanced tree
        private AsRedBlackTreeNode<T> Find(AsRedBlackTreeNode<T> parent, T value)
        {
            if (parent == null)
            {
                return null;
            }

            if (parent.Value.CompareTo(value) == 0)
            {
                return parent;
            }

            var left = Find(parent.Left, value);

            if (left != null)
            {
                return left;
            }

            var right = Find(parent.Right, value);

            if (right != null)
            {
                return right;
            }

            return null;

        }

        private void RightRotate(AsRedBlackTreeNode<T> node)
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

        private void LeftRotate(AsRedBlackTreeNode<T> node)
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


        //O(log(n)) always
        public void Insert(T value)
        {
            //empty tree
            if (Root == null)
            {
                Root = new AsRedBlackTreeNode<T>(null, value);
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                Count++;
                return;
            }

            if (HasItem(value))
            {
                throw new Exception("Item exists");
            }

            insert(Root, value);
            Count++;
        }

        private AsRedBlackTreeNode<T> nodeToBalance;
        //O(log(n)) always
        private void insert(
            AsRedBlackTreeNode<T> currentNode, T newNodeValue)
        {
            nodeToBalance = currentNode;

            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right == null)
                {
                    //insert
                    currentNode.Right = new AsRedBlackTreeNode<T>(currentNode, newNodeValue);
                    nodeToBalance = currentNode.Right;
                }
                else
                {
                    insert(currentNode.Right, newNodeValue);
                }

            }
            //current node is greater than new node
            else
            {

                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new AsRedBlackTreeNode<T>(currentNode, newNodeValue);
                    nodeToBalance = currentNode.Left;
                }
                else
                {
                    insert(currentNode.Left, newNodeValue);
                }
            }

            if (nodeToBalance == Root)
            {
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
                        return;
                    }
                    //absent sibling or black sibling
                    else if (nodeToBalance.Parent.Sibling == null
                        || nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Black)
                    {

                        if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsLeftChild)
                        {

                            var newRoot = nodeToBalance.Parent;
                            swapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                            RightRotate(nodeToBalance.Parent.Parent);

                            if (newRoot == Root)
                            {
                                Root.NodeColor = RedBlackTreeNodeColor.Black;
                            }

                            nodeToBalance = newRoot;
                            return;
                        }
                        else if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsRightChild)
                        {

                            RightRotate(nodeToBalance.Parent);

                            var newRoot = nodeToBalance;

                            swapColors(nodeToBalance.Parent, nodeToBalance);
                            LeftRotate(nodeToBalance.Parent);

                            if (newRoot == Root)
                            {
                                Root.NodeColor = RedBlackTreeNodeColor.Black;
                            }

                            nodeToBalance = newRoot;
                            return;
                        }
                        else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsRightChild)
                        {

                            var newRoot = nodeToBalance.Parent;
                            swapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                            LeftRotate(nodeToBalance.Parent.Parent);

                            if (newRoot == Root)
                            {
                                Root.NodeColor = RedBlackTreeNodeColor.Black;
                            }
                            nodeToBalance = newRoot;
                            return;
                        }
                        else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsLeftChild)
                        {

                            LeftRotate(nodeToBalance.Parent);

                            var newRoot = nodeToBalance;

                            swapColors(nodeToBalance.Parent, nodeToBalance);
                            RightRotate(nodeToBalance.Parent);

                            if (newRoot == Root)
                            {
                                Root.NodeColor = RedBlackTreeNodeColor.Black;
                            }
                            nodeToBalance = newRoot;
                            return;
                        }
                    }

                }

            }

            nodeToBalance = nodeToBalance.Parent;
        }

        private void swapColors(AsRedBlackTreeNode<T> parent1, AsRedBlackTreeNode<T> parent2)
        {
            var tmpColor = parent2.NodeColor;
            parent2.NodeColor = parent1.NodeColor;
            parent1.NodeColor = tmpColor;
        }

        //O(log(n)) always
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty Tree");
            }

            delete(Root, value);
        }
        //O(log(n)) always
        private void delete(AsRedBlackTreeNode<T> node, T value)
        {
            if (HasItem(value) == false)
            {
                throw new Exception("Item do not exist");
            }
        }
    }
}
