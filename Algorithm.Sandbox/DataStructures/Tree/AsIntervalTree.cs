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
        public AsArrayList<T> End { get; set; }

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
            End = new AsArrayList<T>();
            End.Add(end);
        }
    }

    /// <summary>
    /// An overlapping interval tree implementation in 1-dimension using Augmentation Tree method
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
                && current.Left.Value.MaxEnd.CompareTo(searchInterval.Start) >= 0)
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
            for (int i = 0; i < a.End.Length; i++)
            {
                for (int j = 0; j < b.End.Length; j++)
                {
                    //a.Start less than b.End and a.End greater than b.Start
                    if (a.Start.CompareTo(b.End[j]) <= 0 && a.End[i].CompareTo(b.Start) >= 0)
                    {
                        return true;
                    }
                }

            }

            return false;
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
            private void UpdateMax(AsIntervalRedBlackTreeNode<AsInterval<T>> node, T currentMax,
               bool recurseUp = true)
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

                for (int i = 0; i < node.Value.End.Length; i++)
                {
                    if (currentMax.CompareTo(node.Value.End[i]) < 0)
                    {
                        currentMax = node.Value.End[i];
                    }
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
            private void UpdateMax(AsIntervalRedBlackTreeNode<AsInterval<T>> newRoot, bool recurseUp = true)
            {
                if (newRoot == null)
                    return;

                newRoot.Value.MaxEnd = default(T);

                if (newRoot.Left != null)
                {
                    newRoot.Left.Value.MaxEnd = default(T);
                    UpdateMax(newRoot.Left, newRoot.Left.Value.MaxEnd, recurseUp);
                }

                if (newRoot.Right != null)
                {
                    newRoot.Right.Value.MaxEnd = default(T);
                    UpdateMax(newRoot.Right, newRoot.Right.Value.MaxEnd, recurseUp);
                }

                UpdateMax(newRoot, newRoot.Value.MaxEnd, recurseUp);

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

                UpdateMax(newRoot , false);
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

                UpdateMax(newRoot, false);
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
                    UpdateMax(Root);
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
                        UpdateMax(newNode);
                        BalanceInsertion(newNode);
                        return newNode;
                    }
                    else
                    {
                        var newNode = insert(currentNode.Right, newNodeValue);
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
                        UpdateMax(newNode);
                        BalanceInsertion(newNode);
                        return newNode;
                    }
                    else
                    {
                        var newNode = insert(currentNode.Left, newNodeValue);
                        return newNode;
                    }
                }
                else
                {
                    currentNode.Value.End.Add(newNodeValue.End[0]);
                    UpdateMax(currentNode);
                    return currentNode;
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
                    Root.NodeColor = RedBlackTreeNodeColor.Black;
                    UpdateMax(Root);
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

                            }
                        }

                    }

                }

                if (nodeToBalance.Parent != null)
                {
                    BalanceInsertion(nodeToBalance.Parent);
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

                delete(Root, value, false);
                Count--;

            }


            /// <summary>
            /// Deletes the node and return a reference to the parent of actual node deleted
            /// O(log(n)) always
            /// </summary>
            /// <param name="node"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            private AsIntervalRedBlackTreeNode<AsInterval<T>> delete(AsIntervalRedBlackTreeNode<AsInterval<T>> node, 
                AsInterval<T> value, bool deleteByStartOnly)
            {

                var compareResult = node.Value.CompareTo(value);

                //node is less than the search value so move right to find the deletion node
                if (compareResult < 0)
                {
                    if (node.Right == null)
                    {
                        throw new Exception("Item do not exist");
                    }

                    return delete(node.Right, value, deleteByStartOnly);
                }
                //node is less than the search value so move left to find the deletion node
                else if (compareResult > 0)
                {
                    if (node.Left == null)
                    {
                        throw new Exception("Item do not exist");
                    }

                    return delete(node.Left, value, deleteByStartOnly);
                }
                else
                {
                    AsIntervalRedBlackTreeNode<AsInterval<T>> nodeToBalance = null;

                    //if not a leaf deletion caused by replacement 
                    //of an ancestor deleted with this node
                    if(!deleteByStartOnly)
                    {
                        var index = GetIndex(node.Value.End, value);

                        if (index == -1)
                        {
                            throw new Exception("Interval do not exist");
                        }

                        if(node.Value.End.Length > 1)
                        {                  
                            node.Value.End.RemoveItem(index);
                            UpdateMax(node);
                            return node;
                        }
                    }

                    //node is a leaf node
                    if (node.IsLeaf)
                    {
                        //if color is red, we are good; no need to balance
                        if (node.NodeColor == RedBlackTreeNodeColor.Red)
                        {

                            deleteLeaf(node);
                            UpdateMax(node.Parent);
                            return node.Parent;
                        }

                      
                        nodeToBalance = handleDoubleBlack(node);
                        deleteLeaf(node);
                        UpdateMax(node.Parent);

                    }
                    else
                    {
                        //case one - right tree is null (move sub tree up)
                        if (node.Left != null && node.Right == null)
                        {

                            nodeToBalance = handleDoubleBlack(node);
                            deleteLeftNode(node);
                            UpdateMax(node.Parent);

                        }
                        //case two - left tree is null  (move sub tree up)
                        else if (node.Right != null && node.Left == null)
                        {

                            nodeToBalance = handleDoubleBlack(node);
                            deleteRightNode(node);
                            UpdateMax(node.Parent);

                        }
                        //case three - two child trees 
                        //replace the node value with maximum element of left subtree (left max node)
                        //and then delete the left max node
                        else
                        {
                            var index = GetIndex(node.Value.End, value);

                            if(index == -1)
                            {
                                throw new Exception("Interval do not exist");
                            }

                            //if this is the only element
                            //do regular bst deletion
                            if (node.Value.End.Length == 1 && index == 0)
                            {
                                var maxLeftNode = FindMax(node.Left);

                                node.Value = maxLeftNode.Value;
                                node.Value.MaxEnd = default(T);

                                //delete left max node
                                return delete(node.Left, maxLeftNode.Value, true);
                            }
                            else
                            {
                                //just remove the end
                                node.Value.End.RemoveItem(index);
                                UpdateMax(node);
                                return node;
                            }
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
            /// returns the index of a matching value in this End range list
            /// </summary>
            /// <param name="end"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            private int GetIndex(AsArrayList<T> end, AsInterval<T> value)
            {
                var index = -1;
                for (int i = 0; i < end.Length; i++)
                {
                    if (end[i].CompareTo(value.End[0]) == 0)
                    {
                        index = i;
                    }
                }

                return index;
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
