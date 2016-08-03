using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsBTreeNode<T> : IComparable where T : IComparable
    {
        public T Value { get; set; }

        public AsBTreeNode<T> Parent { get; set; }

        public AsBTreeNode<T> Left { get; set; }
        public AsBTreeNode<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBTreeNode(AsBTreeNode<T> parent, T value)
        {
            this.Parent = parent;
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as AsBTreeNode<T>);
        }

        public int CompareTo(AsBTreeNode<T> node)
        {
            return Value.CompareTo(node.Value);
        }
    }

    public class AsBTree<T> where T : IComparable
    {
        public AsBTreeNode<T> Root { get; set; }
        public int Count { get; private set; }

        private AsBTreeNode<T> lastInsertionNode { get; set; }

        //constructor
        public AsBTree(T value)
        {
            Insert(value);
        }

        //O(logn)
        public bool HasItem(T value)
        {
            if (Root == null)
            {
                return false;
            }

            return Find(Root, value) != null;
        }

        //O(logn)
        public int GetHeight()
        {
            return GetHeight(Root);
        }

        //O(logn)
        public int GetHeight(AsBTreeNode<T> node)
        {
            if (node == null)
            {
                return -1;
            }

            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        //O(1) amortized (worst O(logn))
        public void Insert(T value)
        {
            if(Count == 0)
            {
                Root = new AsBTreeNode<T>(Root, value);
                lastInsertionNode = Root;
                Count++;
                return;
            }

            if (Count == 1)
            {
                Root.Left = new AsBTreeNode<T>(Root, value);
                lastInsertionNode = Root.Left;
                Count++;
                return;
            }

            //check if its a perfect binary tree
            //insert all the way to left then
            if ((Math.Log(Count + 1, 2)) % 1 == 0)
            {
                var left = Root.Left == null ? Root : Root.Left;

                while (left != null && left.Left != null)
                {
                    left = left.Left;
                }

                left.Left = new AsBTreeNode<T>(left, value);

                lastInsertionNode = left.Left;
            }
            //has even number of nodes (visualize to understand logic)
            else if (Count % 2 == 0)
            {
                lastInsertionNode.Parent.Right = new AsBTreeNode<T>(lastInsertionNode.Parent, value);

                lastInsertionNode = lastInsertionNode.Parent.Right;
            }
            //has odd number of nodes (visualize to understand logic)
            else
            {
                var visited = lastInsertionNode.Value;

                lastInsertionNode = lastInsertionNode.Parent;

                //backtrack to next empty sibling parent
                while (visited.CompareTo(lastInsertionNode.Right.Value) == 0)
                {
                    visited = lastInsertionNode.Value;
                    lastInsertionNode = lastInsertionNode.Parent;
                }

                lastInsertionNode = lastInsertionNode.Right;

                while (lastInsertionNode.Left != null)
                {
                    lastInsertionNode = lastInsertionNode.Left;
                }

                lastInsertionNode.Left = new AsBTreeNode<T>(lastInsertionNode, value);

                lastInsertionNode = lastInsertionNode.Left;
            }

            //todo
            Count++;
        }

        //remove the node with the given identifier from the descendants 
        //O(log(n))
        public void Delete(T value)
        {

            if (Count == 1)
            {
                Root = null;
                lastInsertionNode = null;
                Count--;
                return;
            }

            var node = Find(value);

            //update the node with the last inserted node value
            node.Value = lastInsertionNode.Value;

            var nodeToDelete = lastInsertionNode;

            //now delete the last inserted node and update the last inserted node pointer to previous insertion location
            //check if deleting a node will make this a perfect binary tree
            //set last insert node all the way to right
            if ((Math.Log((Count - 1) + 1, 2)) % 1 == 0)
            {
                var right = Root.Right == null ? Root : Root.Right;

                while (right != null && right.Right != null)
                {
                    right = right.Right;
                }
                lastInsertionNode = right;
            }
            //deletion causes even number of rows (visualize about reversing the last insertion)
            else if ((Count - 1) % 2 == 0)
            {
                lastInsertionNode = lastInsertionNode.Parent.Left;

            }
            //deletion causes odd number of rows (visualize about reversing the last insertion)
            else
            {
                var visited = lastInsertionNode.Value;

                lastInsertionNode = lastInsertionNode.Parent;

                //backtrack to next empty sibling parent
                while (visited.CompareTo(lastInsertionNode.Left.Value) == 0)
                {
                    visited = lastInsertionNode.Value;
                    lastInsertionNode = lastInsertionNode.Parent;
                }

                lastInsertionNode = lastInsertionNode.Left;

                while (lastInsertionNode.Right != null)
                {
                    lastInsertionNode = lastInsertionNode.Right;
                }

            }

            if(nodeToDelete.Parent.Left == nodeToDelete)
            {
                nodeToDelete.Parent.Left = null;
            }
            else
            {
                nodeToDelete.Parent.Right = null;
            }

            Count--;
        }

        //O(logn)
        public AsBTreeNode<T> Find(T value)
        {
            if (Root == null)
            {
                return null;
            }

            return Find(Root, value);
        }


        //find the node with the given identifier among descendants of parent and parent
        //uses pre-order traversal
        //O(logn)
        public AsBTreeNode<T> Find(AsBTreeNode<T> parent, T value)
        {
            if(parent == null)
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
