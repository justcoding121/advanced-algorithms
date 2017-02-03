using Algorithm.Sandbox.DataStructures.Tree;
using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsAVLTreeNode<T> : AsIBSTNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsAVLTreeNode<T> Parent { get; set; }

        public AsAVLTreeNode<T> Left { get; set; }
        public AsAVLTreeNode<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsAVLTreeNode(AsAVLTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            Height = 0;
        }

        public int Height { get; set; }

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
    }

    public class AsAVLTree<T> where T : IComparable
    {
        public AsAVLTreeNode<T> Root { get; private set; }
        public int Count { get; private set; }

        //O(log(n)) always
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }

        //O(1)
        public int GetHeight()
        {
            if (Root == null)
                return -1;

            return Root.Height;
        }

        //O(log(n)) always
        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new AsAVLTreeNode<T>(null, value);
                Count++;
                return;
            }

            insert(Root, value);
            Count++;
        }

        //O(log(n)) always
        private void insert(AsAVLTreeNode<T> node, T value)
        {

            var compareResult = node.Value.CompareTo(value);


            //node is less than the value so move right for insertion
            if (compareResult < 0)
            {
                if (node.Right == null)
                {
                    node.Right = new AsAVLTreeNode<T>(node, value);
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
                    node.Left = new AsAVLTreeNode<T>(node, value);
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

            UpdateHeight(node);

            Balance(node);

        }


        //remove the node with the given identifier from the descendants 
        //O(log(n)) always
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty AVLTree");
            }

            delete(Root, value);
            Count--;
        }

        //O(log(n)) always
        private void delete(AsAVLTreeNode<T> node, T value)
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
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        //delete left max node
                        delete(node.Left, maxLeftNode.Value);
                    }
                }
            }

            if (baseCase)
            {
                UpdateHeight(node.Parent);
                Balance(node.Parent);
            }
            else
            {
                UpdateHeight(node);
                Balance(node);
            }


        }

        public T FindMax()
        {
            return FindMax(Root).Value;
        }


        private AsAVLTreeNode<T> FindMax(AsAVLTreeNode<T> node)
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

        private AsAVLTreeNode<T> FindMin(AsAVLTreeNode<T> node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private AsAVLTreeNode<T> Find(T value)
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
        private AsAVLTreeNode<T> Find(AsAVLTreeNode<T> parent, T value)
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

        private void Balance(AsAVLTreeNode<T> node)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
                return;

            var leftHeight = node.Left != null ? node.Left.Height + 1 : 0;
            var rightHeight = node.Right != null ? node.Right.Height + 1 : 0;

            var balanceFactor = leftHeight - rightHeight;
            //tree is left heavy
            //differance >=2 then do rotations
            if (balanceFactor >= 2)
            {
                leftHeight = node.Left.Left != null ? node.Left.Left.Height + 1 : 0;
                rightHeight = node.Left.Right != null ? node.Left.Right.Height + 1 : 0;

                //left child is left heavy
                if (leftHeight > rightHeight)
                {
                    RightRotate(node);
                }
                //left child is right heavy
                else
                {
                    LeftRotate(node.Left);
                    RightRotate(node);
                }
            }
            //tree is right heavy
            //differance <=-2 then do rotations
            else if (balanceFactor <= -2)
            {
                leftHeight = node.Right.Left != null ? node.Right.Left.Height + 1 : 0;
                rightHeight = node.Right.Right != null ? node.Right.Right.Height + 1 : 0;

                //right child is right heavy
                if (rightHeight > leftHeight)
                {
                    LeftRotate(node);
                }
                //right child is left heavy
                else
                {
                    RightRotate(node.Right);
                    LeftRotate(node);
                }

            }
        }

        private void RightRotate(AsAVLTreeNode<T> node)
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

            UpdateHeight(newRoot);

            if (prevRoot == Root)
            {
                Root = newRoot;
            }

        }

        private void LeftRotate(AsAVLTreeNode<T> node)
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

            UpdateHeight(newRoot);

            if (prevRoot == Root)
            {
                Root = newRoot;
            }
        }

        private void UpdateHeight(AsAVLTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                node.Left.Height = Math.Max(node.Left.Left == null ? 0 : node.Left.Left.Height + 1,
                                                 node.Left.Right == null ? 0 : node.Left.Right.Height + 1);
            }

            if (node.Right != null)
            {
                node.Right.Height = Math.Max(node.Right.Left == null ? 0 : node.Right.Left.Height + 1,
                                  node.Right.Right == null ? 0 : node.Right.Right.Height + 1);
            }

            node.Height = Math.Max(node.Left == null ? 0 : node.Left.Height + 1,
                                      node.Right == null ? 0 : node.Right.Height + 1);
        }
    }
}
