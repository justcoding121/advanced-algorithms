using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DataStructures.Tree;
using System;


namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// Interval object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsInterval<T> : IComparable where T : IComparable
    {
        /// <summary>
        /// Start of this interval range
        /// </summary>
        public T Start { get; set; }

        /// <summary>
        /// End of this interval range
        /// </summary>
        public T End { get; set; }

        /// <summary>
        /// Max End interval under this interval
        /// </summary>
        internal T MaxEnd { get; set; }

        public int CompareTo(object obj)
        {
            return this.Start.CompareTo((obj as AsInterval<T>).Start);
        }

        public AsInterval(T start, T end)
        {
            Start = start;
            End = end;
        }
    }

    /// <summary>
    /// An interval tree implementation in 1-dimension
    /// TODO support interval start range that collide
    /// TODO support multiple dimensions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsIntervalTree<T> where T : IComparable
    {
        //use a height balanced binary search tree
        private AsIntervalRedBlackTree<T> RedBlackTree
            = new AsIntervalRedBlackTree<T>();

        public int Count { get; private set; }
        /// <summary>
        /// Insert a new Interval
        /// </summary>
        /// <param name="newInterval"></param>
        public void Insert(AsInterval<T> newInterval)
        {
            RedBlackTree.Insert(newInterval);
            Count++;
        }

        /// <summary>
        /// Delete this interval
        /// </summary>
        /// <param name="interval"></param>
        public void Delete(AsInterval<T> interval)
        {
            RedBlackTree.Delete(interval);
            Count--;
        }

        /// <summary>
        ///  Returns an interval in this tree that overlaps with this search interval 
        /// </summary>
        /// <param name="searchInterval"></param>
        /// <returns></returns>
        internal AsInterval<T> GetOverlap(AsInterval<T> searchInterval)
        {
            return GetOverlap(RedBlackTree.Root, searchInterval);
        }


        /// <summary>
        ///  does any interval overlaps with this search interval
        /// </summary>
        /// <param name="searchInterval"></param>
        /// <returns></returns>
        internal bool DoOverlap(AsInterval<T> searchInterval)
        {
            return GetOverlap(RedBlackTree.Root, searchInterval) != null;
        }

        /// <summary>
        /// Returns an interval that overlaps with this interval
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        private AsInterval<T> GetOverlap(AsIntervalRedBlackTreeNode<AsInterval<T>> current,
            AsInterval<T> searchInterval)
        {
            if (current == null)
            {
                return null;
            }

            if (doOverlap(current.Value, searchInterval))
            {
                return current.Value;
            }

            //if left max is greater than search start
            //then the search interval can occur in left sub tree
            if (current.Left != null
                && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) > 0)
            {
                return GetOverlap(current.Left, searchInterval);
            }

            //otherwise look in right subtree
            return GetOverlap(current.Right, searchInterval);

        }

        /// <summary>
        /// Does this interval a overlap with b 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool doOverlap(AsInterval<T> a, AsInterval<T> b)
        {
            //a.Start less than b.End and a.End greater than b.Start
            return a.Start.CompareTo(b.End) <= 0 && a.End.CompareTo(b.Start) >= 0;

        }


        internal class AsIntervalRedBlackTreeNode<T> : AsIBSTNode<T> where T : IComparable
        {
            internal T Value { get; set; }

            internal AsIntervalRedBlackTreeNode<T> Parent { get; set; }

            internal AsIntervalRedBlackTreeNode<T> Left { get; set; }
            internal AsIntervalRedBlackTreeNode<T> Right { get; set; }

            internal bool IsLeaf => Left == null && Right == null;
            internal RedBlackTreeNodeColor NodeColor { get; set; }

            internal AsIntervalRedBlackTreeNode<T> Sibling => this.Parent == null ? null : this.Parent.Left == this ?
                                                    this.Parent.Right : this.Parent.Left;

            internal bool IsLeftChild => this.Parent.Left == this;
            internal bool IsRightChild => this.Parent.Right == this;

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

            T AsIBSTNode<T>.Value
            {
                get
                {
                    return Value;
                }
            }

            internal AsIntervalRedBlackTreeNode(AsIntervalRedBlackTreeNode<T> parent, T value)
            {
                Parent = parent;
                Value = value;
                NodeColor = RedBlackTreeNodeColor.Red;
            }
        }

        internal class AsIntervalRedBlackTree<T> where T : IComparable
        {
            internal AsIntervalRedBlackTreeNode<AsInterval<T>> Root { get; private set; }
            public int Count { get; private set; }


            internal void Clear()
            {
                Root = null;
                Count = 0;
            }


            /// <summary>
            /// update max end value under each node recursively
            /// </summary>
            /// <param name="node"></param>
            /// <param name="currentMax"></param>
            private void UpdateMax(AsIntervalRedBlackTreeNode<AsInterval<T>> node, T currentMax, bool recurseUp = true)
            {
                if (node == null)
                {
                    return;
                }

                if (node.Left != null && node.Right != null)
                {
                    //if current Max is less than current End
                    //then update current Max
                    if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Left.Value.MaxEnd;
                    }

                    if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Right.Value.MaxEnd;
                    }
                }
                else if (node.Left != null)
                {
                    //if current Max is less than current End
                    //then update current Max
                    if (currentMax.CompareTo(node.Left.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Left.Value.MaxEnd;
                    }

                }
                else if (node.Right != null)
                {
                    if (currentMax.CompareTo(node.Right.Value.MaxEnd) < 0)
                    {
                        currentMax = node.Right.Value.MaxEnd;
                    }
                }

                if (currentMax.CompareTo(node.Value.End) < 0)
                {
                    currentMax = node.Value.End;
                }

                node.Value.MaxEnd = currentMax;

                if (recurseUp)
                {
                    UpdateMax(node.Parent, currentMax);
                }


            }

            /// <summary>
            /// Update Max on new root after rotations
            /// </summary>
            /// <param name="newRoot"></param>
            private void UpdateMax(AsIntervalRedBlackTreeNode<AsInterval<T>> newRoot)
            {
                if (newRoot.Right != null)
                {
                    if (newRoot.Right.Right != null)
                    {
                        UpdateMax(newRoot.Right.Right, newRoot.Right.Right.Value.MaxEnd);
                    }

                    if (newRoot.Right.Left != null)
                    {
                        UpdateMax(newRoot.Right.Left, newRoot.Right.Left.Value.MaxEnd);
                    }
                }

                if (newRoot.Left != null)
                {
                    if (newRoot.Left.Left != null)
                    {
                        UpdateMax(newRoot.Left.Left, newRoot.Left.Left.Value.MaxEnd);
                    }

                    if (newRoot.Left.Right != null)
                    {
                        UpdateMax(newRoot.Left.Right, newRoot.Left.Right.Value.MaxEnd);
                    }
                }

                //if (newRoot.Parent != null)
                //    UpdateMax(newRoot.Parent, newRoot.Parent.Value.End);
            }

            /// <summary>
            /// Rotate right
            /// </summary>
            /// <param name="node"></param>
            private void RightRotate(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
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

                UpdateMax(newRoot);
            }

            /// <summary>
            /// rotate left
            /// </summary>
            /// <param name="node"></param>
            private void LeftRotate(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
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

                UpdateMax(newRoot);
            }

            /// <summary>
            /// Get max under this element
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            private AsIntervalRedBlackTreeNode<AsInterval<T>> FindMax(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
            {
                if (node.Right == null)
                {
                    return node;
                }

                return FindMax(node.Right);
            }

            //O(log(n)) always
            public void Insert(AsInterval<T> value)
            {
                //empty tree
                if (Root == null)
                {
                    Root = new AsIntervalRedBlackTreeNode<AsInterval<T>>(null, value);
                    Root.NodeColor = RedBlackTreeNodeColor.Black;
                    Count++;
                    return;
                }

                insert(Root, value);
                Count++;
            }


            //O(log(n)) always
            private AsIntervalRedBlackTreeNode<AsInterval<T>> insert(
                AsIntervalRedBlackTreeNode<AsInterval<T>> currentNode, AsInterval<T> newNodeValue)
            {

                var compareResult = currentNode.Value.CompareTo(newNodeValue);

                //current node is less than new item
                if (compareResult < 0)
                {
                    //no right child
                    if (currentNode.Right == null)
                    {
                        //insert
                        var newNode = new AsIntervalRedBlackTreeNode<AsInterval<T>>(currentNode, newNodeValue);
                        currentNode.Right = newNode;
                        UpdateMax(newNode, newNode.Value.End);
                        BalanceInsertion(newNode);
                        return newNode;
                    }
                    else
                    {
                        var newNode = insert(currentNode.Right, newNodeValue);
                        UpdateMax(newNode, newNode.Value.End);
                        BalanceInsertion(currentNode);
                        return newNode;
                    }

                }
                //current node is greater than new node
                else if (compareResult > 0)
                {

                    if (currentNode.Left == null)
                    {
                        //insert
                        var newNode = new AsIntervalRedBlackTreeNode<AsInterval<T>>(currentNode, newNodeValue);
                        currentNode.Left = newNode;
                        BalanceInsertion(newNode);
                        return newNode;
                    }
                    else
                    {
                        var newNode = insert(currentNode.Left, newNodeValue);
                        BalanceInsertion(currentNode);
                        return newNode;
                    }
                }
                else
                {
                    throw new Exception("Item exists");
                }


            }

            /// <summary>
            /// balance after insertion
            /// </summary>
            /// <param name="nodeToBalance"></param>
            private void BalanceInsertion(AsIntervalRedBlackTreeNode<AsInterval<T>> nodeToBalance)
            {
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
            }

            /// <summary>
            /// swap colors of two nodes
            /// </summary>
            /// <param name="node1"></param>
            /// <param name="node2"></param>
            private void swapColors(AsIntervalRedBlackTreeNode<AsInterval<T>> node1, AsIntervalRedBlackTreeNode<AsInterval<T>> node2)
            {
                var tmpColor = node2.NodeColor;
                node2.NodeColor = node1.NodeColor;
                node1.NodeColor = tmpColor;
            }

            //O(log(n)) always
            public void Delete(AsInterval<T> value)
            {
                if (Root == null)
                {
                    throw new Exception("Empty Tree");
                }

                delete(Root, value);
                Count--;

            }


            /// <summary>
            /// Deletes the node and return a reference to the parent of actual node deleted
            /// O(log(n)) always
            /// </summary>
            /// <param name="node"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            private AsIntervalRedBlackTreeNode<AsInterval<T>> delete(AsIntervalRedBlackTreeNode<AsInterval<T>> node, AsInterval<T> value)
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
                    AsIntervalRedBlackTreeNode<AsInterval<T>> nodeToBalance = null;

                    //node is a leaf node
                    if (node.IsLeaf)
                    {
                        if (node.Parent != null)
                        {
                            UpdateMax(node.Parent, node.Parent.Value.End);
                        }


                        //if color is red, we are good; no need to balance
                        if (node.NodeColor == RedBlackTreeNodeColor.Red)
                        {

                            deleteLeaf(node);
                            return node.Parent;
                        }

                        nodeToBalance = handleDoubleBlack(node);
                        deleteLeaf(node);

                    }
                    else
                    {
                        //case one - right tree is null (move sub tree up)
                        if (node.Left != null && node.Right == null)
                        {
                            if (node.Parent != null)
                            {
                                UpdateMax(node.Parent, node.Parent.Value.End);
                            }

                            nodeToBalance = handleDoubleBlack(node);
                            deleteLeftNode(node);

                        }
                        //case two - left tree is null  (move sub tree up)
                        else if (node.Right != null && node.Left == null)
                        {
                            if (node.Parent != null)
                            {
                                UpdateMax(node.Parent, node.Parent.Value.End);
                            }

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
                            return delete(node.Left, maxLeftNode.Value);
                        }
                    }

                    var returnNode = nodeToBalance;

                    //handle six cases
                    while (nodeToBalance != null)
                    {
                        nodeToBalance = handleDoubleBlack(nodeToBalance);
                    }

                    return returnNode;
                }

            }

            /// <summary>
            /// deletes leaf
            /// </summary>
            /// <param name="node"></param>
            private void deleteLeaf(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
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

            /// <summary>
            /// deletes right node
            /// </summary>
            /// <param name="node"></param>
            private void deleteRightNode(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
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

            /// <summary>
            /// deletes left node
            /// </summary>
            /// <param name="node"></param>
            private void deleteLeftNode(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
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

            private AsIntervalRedBlackTreeNode<AsInterval<T>> handleDoubleBlack(AsIntervalRedBlackTreeNode<AsInterval<T>> node)
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


        }
    }


}
