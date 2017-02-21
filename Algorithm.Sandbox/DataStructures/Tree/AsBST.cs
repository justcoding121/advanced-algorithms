using Algorithm.Sandbox.DataStructures.Tree;
using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBSTNode<T> : AsIBSTNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsBSTNode<T> Parent { get; set; }

        public AsBSTNode<T> Left { get; set; }
        public AsBSTNode<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;
        public bool IsLeftChild => this.Parent.Left == this;
        public bool IsRightChild => this.Parent.Right == this;

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


        public AsBSTNode(AsBSTNode<T> parent, T value)
        {
            this.Parent = parent;
            this.Value = value;
        }

    }

    public class AsBST<T> where T : IComparable
    {
        internal AsBSTNode<T> Root { get; set; }
        public int Count { get; private set; }

        //O(log(n)) worst O(n) for unbalanced tree
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }

        //O(log(n)) worst O(n) for unbalanced tree
        public int GetHeight()
        {
            return GetHeight(Root);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private int GetHeight(AsBSTNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        //O(log(n)) worst O(n) for unbalanced tree
        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new AsBSTNode<T>(null, value);
                Count++;
                return;
            }

            insert(Root, value);
            Count++;
        }

        //O(log(n)) always
        private void insert(
            AsBSTNode<T> currentNode, T newNodeValue)
        {
            var compareResult = currentNode.Value.CompareTo(newNodeValue);

            //current node is less than new item
            if (compareResult < 0)
            {
                //no right child
                if (currentNode.Right == null)
                {
                    //insert
                    currentNode.Right = new AsBSTNode<T>(currentNode, newNodeValue);
                }
                else
                {
                    insert(currentNode.Right, newNodeValue);
                }

            }
            //current node is greater than new node
            else if (compareResult > 0)
            {

                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new AsBSTNode<T>(currentNode, newNodeValue);
                }
                else
                {
                    insert(currentNode.Left, newNodeValue);
                }
            }
            else
            {
                throw new Exception("Item exists");
            }


        }

        //remove the node with the given identifier from the descendants 
        //O(log(n)) worst O(n) for unbalanced tree
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty BST");
            }

            delete(Root, value);
            Count--;
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private void delete(AsBSTNode<T> node, T value)
        {
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
                    deleteLeaf(node);
                }
                else
                {
                    //case one - right tree is null (move sub tree up)
                    if (node.Left != null && node.Right == null)
                    {
                        deleteLeftNode(node);

                    }
                    //case two - left tree is null  (move sub tree up)
                    else if (node.Right != null && node.Left == null)
                    {
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
        }

        private void deleteLeaf(AsBSTNode<T> node)
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

        private void deleteRightNode(AsBSTNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Right.Parent = null;
                Root = Root.Right;
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

            }
        }

        private void deleteLeftNode(AsBSTNode<T> node)
        {
            //root
            if (node.Parent == null)
            {
                Root.Left.Parent = null;
                Root = Root.Left;       
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

            }
        }

        public T FindMax()
        {
            return FindMax(Root).Value;
        }


        private AsBSTNode<T> FindMax(AsBSTNode<T> node)
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

        private AsBSTNode<T> FindMin(AsBSTNode<T> node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        //O(log(n)) worst O(n) for unbalanced tree
        private AsBSTNode<T> Find(T value)
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
        private AsBSTNode<T> Find(AsBSTNode<T> parent, T value)
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

    }
}
