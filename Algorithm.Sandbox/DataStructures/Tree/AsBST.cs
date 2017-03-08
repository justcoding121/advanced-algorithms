using Algorithm.Sandbox.DataStructures.Tree;
using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsBSTNode<T> : AsIBSTNode<T> where T : IComparable
    {
        internal T Value { get; set; }

        internal AsBSTNode<T> Parent { get; set; }

        internal AsBSTNode<T> Left { get; set; }
        internal AsBSTNode<T> Right { get; set; }

        internal bool IsLeaf => Left == null && Right == null;
        internal bool IsLeftChild => this.Parent.Left == this;
        internal bool IsRightChild => this.Parent.Right == this;

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

        T AsIBSTNode<T>.Value
        {
            get
            {
                return Value;
            }
        }

        internal AsBSTNode(AsBSTNode<T> parent, T value)
        {
            this.Parent = parent;
            this.Value = value;
        }

    }

    public class AsBST<T> where T : IComparable
    {
        internal AsBSTNode<T> Root { get; set; }
        public int Count { get; private set; }

        //worst O(n) for unbalanced tree
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }

        //worst O(n) for unbalanced tree
        public int GetHeight()
        {
            return GetHeight(Root);
        }

        //worst O(n) for unbalanced tree
        private int GetHeight(AsBSTNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }


        internal AsBSTNode<T> InsertAndReturnNewNode(T value)
        {
            if (Root == null)
            {
                Root = new AsBSTNode<T>(null, value);
                Count++;
                return Root;
            }

            var newNode = insert(Root, value);
            Count++;

            return newNode;
        }


        //worst O(n) for unbalanced tree
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

        //worst O(n) for unbalanced tree
        private AsBSTNode<T> insert(
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
                    return currentNode.Right;
                }
                else
                {
                    return insert(currentNode.Right, newNodeValue);
                }

            }
            //current node is greater than new node
            else if (compareResult > 0)
            {

                if (currentNode.Left == null)
                {
                    //insert
                    currentNode.Left = new AsBSTNode<T>(currentNode, newNodeValue);
                    return currentNode.Left;
                }
                else
                {
                    return insert(currentNode.Left, newNodeValue);
                }
            }
            else
            {
                throw new Exception("Item exists");
            }


        }

        //remove the node with the given identifier from the descendants 
        //worst O(n) for unbalanced tree
        public void Delete(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty BST");
            }

            delete(Root, value);
            Count--;
        }

        internal AsBSTNode<T> DeleteAndReturnParent(T value)
        {
            if (Root == null)
            {
                throw new Exception("Empty BST");
            }

            var parentNode = delete(Root, value);

            Count--;

            return parentNode;
        }

        //worst O(n) for unbalanced tree
        private AsBSTNode<T> delete(AsBSTNode<T> node, T value)
        {
            var compareResult = node.Value.CompareTo(value);

            //node is less than the search value so move right to find the deletion node
            if (compareResult < 0)
            {
                if (node.Right == null)
                {
                    throw new Exception("Item do not exist");
                }

                return delete(node.Right, value);
            }
            //node is less than the search value so move left to find the deletion node
            else if (compareResult > 0)
            {
                if (node.Left == null)
                {
                    throw new Exception("Item do not exist");
                }

                return delete(node.Left, value);
            }
            else
            {
                //node is a leaf node
                if (node.IsLeaf)
                {
                    deleteLeaf(node);
                    return node.Parent;
                }
                else
                {
                    //case one - right tree is null (move sub tree up)
                    if (node.Left != null && node.Right == null)
                    {
                        deleteLeftNode(node);
                        return node.Parent;
                    }
                    //case two - left tree is null  (move sub tree up)
                    else if (node.Right != null && node.Left == null)
                    {
                        deleteRightNode(node);
                        return node.Parent;

                    }
                    //case three - two child trees 
                    //replace the node value with maximum element of left subtree (left max node)
                    //and then delete the left max node
                    else
                    {
                        var maxLeftNode = FindMax(node.Left);

                        node.Value = maxLeftNode.Value;

                        //delete left max node
                        return delete(node.Left, maxLeftNode.Value);
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

        //worst O(n) for unbalanced tree
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
        //worst O(n) for unbalanced tree
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
