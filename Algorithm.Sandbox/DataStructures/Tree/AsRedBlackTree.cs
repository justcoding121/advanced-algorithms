using Algorithm.Sandbox.DataStructures.Tree;
using System;

namespace Algorithm.Sandbox.DataStructures
{
    public enum RedBlackTreeNodeColor
    {
        Black,
        Red
    }

    public class AsRedBlackTreeNode<T> : AsIBSTNode<T> where T : IComparable
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

        //exposed to do common tests for Binary Trees
        AsIBSTNode<T> AsIBSTNode<T>.Left
        {
            get
            {
                return Left;
            }

        }

        AsIBSTNode<T> AsIBSTNode<T>.Right
        {
            get
            {
                return Right;
            }

        }

        public AsRedBlackTreeNode(AsRedBlackTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            NodeColor = RedBlackTreeNodeColor.Red;
        }
    }

    public class AsRedBlackTree<T> where T : IComparable
    {
        public AsRedBlackTreeNode<T> Root { get; private set; }
        public int Count { get; private set; }

        //O(log(n)) worst O(n) for unbalanced tree
        public int GetHeight()
        {
            return GetHeight(Root);
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
                    if (nodeToBalance.Parent.Sibling != null
                        && nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Red)
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

        private void swapColors(AsRedBlackTreeNode<T> node1, AsRedBlackTreeNode<T> node2)
        {
            var tmpColor = node2.NodeColor;
            node2.NodeColor = node1.NodeColor;
            node1.NodeColor = tmpColor;
        }

        //O(log(n)) always
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty Tree");
            }

            if (HasItem(value) == false)
            {
                throw new Exception("Item do not exist");
            }

            delete(Root, value);
            Count--;
        }

        //O(log(n)) always
        private void delete(AsRedBlackTreeNode<T> node, T value)
        {

            var compareResult = node.Value.CompareTo(value);

            //node is less than the search value so move right to find the deletion node
            if (compareResult < 0)
            {
                delete(node.Right, value);
            }
            //node is less than the search value so move left to find the deletion node
            else if (compareResult > 0)
            {
                delete(node.Left, value);
            }
            else
            {
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
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        //delete left max node
                        delete(node.Left, maxLeftNode.Value);
                    }
                }
            }


            //handle six cases
            while (nodeToBalance != null)
            {
                nodeToBalance = handleDoubleBlack(nodeToBalance);
            }

        }

        private void deleteRightNode(AsRedBlackTreeNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                return;
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

                if (node.Right.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    //black deletion! But we can take its red child and recolor it to black
                    //and we are done!
                    node.Right.NodeColor = RedBlackTreeNodeColor.Black;
                    return;
                }
            }
        }

        private void deleteLeftNode(AsRedBlackTreeNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;
                Root.NodeColor = RedBlackTreeNodeColor.Black;
                return;
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

                if (node.Left.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    //black deletion! But we can take its red child and recolor it to black
                    //and we are done!
                    node.Left.NodeColor = RedBlackTreeNodeColor.Black;
                    return;
                }
            }
        }

        private AsRedBlackTreeNode<T> handleDoubleBlack(AsRedBlackTreeNode<T> node)
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
                    LeftRotate(node.Parent);
                }
                else
                {
                    RightRotate(node.Parent);
                }

                return node;
            }
            //case 3
            if (node.Parent != null
             && node.Parent.NodeColor == RedBlackTreeNodeColor.Black
             && node.Sibling != null
             && node.Sibling.NodeColor == RedBlackTreeNodeColor.Black
             && ((node.Sibling.Left == null && node.Sibling.Right == null)
             || (node.Sibling.Left != null && node.Sibling.Right != null
               && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
               && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black)))
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
                 && ((node.Sibling.Left == null && node.Sibling.Right == null)
                 || (node.Sibling.Left != null && node.Sibling.Right != null
                   && node.Sibling.Left.NodeColor == RedBlackTreeNodeColor.Black
                   && node.Sibling.Right.NodeColor == RedBlackTreeNodeColor.Black)))
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
                RightRotate(node.Sibling);

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
                LeftRotate(node.Sibling);

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
                LeftRotate(node.Parent);

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
                RightRotate(node.Parent);

                return null;
            }

            return null;
        }

        private void deleteLeaf(AsRedBlackTreeNode<T> node)
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
    }
}
