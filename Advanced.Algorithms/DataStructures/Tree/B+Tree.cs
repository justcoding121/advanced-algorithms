using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Tree
{
    internal class BPTreeNode<T> : BNode<T> where T : IComparable
    {
        internal BPTreeNode<T> Parent { get; set; }
        internal BPTreeNode<T>[] Children { get; set; }

        internal bool IsLeaf => Children[0] == null;

        internal BPTreeNode(int maxKeysPerNode, BPTreeNode<T> parent)
            : base(maxKeysPerNode)
        {

            Parent = parent;
            Children = new BPTreeNode<T>[maxKeysPerNode + 1];

        }

        /// <summary>
        /// For shared test method accross B & B+ tree
        /// </summary>
        /// <returns></returns>
        internal override BNode<T> GetParent()
        {
            return Parent;
        }

        /// <summary>
        /// For shared test method accross B & B+ tree
        /// </summary>
        /// <returns></returns>
        internal override BNode<T>[] GetChildren()
        {
            return Children;
        }

        /// <summary>
        /// Pointer to sibling leaf on left for faster enumeration
        /// </summary>
        public BPTreeNode<T> Prev { get; set; }

        /// <summary>
        /// Pointer to sibling leaf on right for faster enumeration
        /// </summary>
        public BPTreeNode<T> Next { get; set; }

    }

    /// <summary>
    /// A B+ Tree implementation
    /// TODO support initial  bulk loading
    /// TODO: make sure duplicates are handled correctly if its not already
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BPTree<T> : IEnumerable<T> where T : IComparable
    {
        public int Count { get; private set; }

        internal BPTreeNode<T> Root;

        /// <summary>
        ///Keep a reference of Bottom Left Node
        ///For faster enumeration with IEnumerable implementation using Next pointer
        /// </summary>
        internal BPTreeNode<T> BottomLeftNode;

        private int maxKeysPerNode;
        private int minKeysPerNode => maxKeysPerNode / 2;

        public T Max
        {
            get
            {
                if (Root == null) return default(T);

                var maxNode = findMaxNode(Root);
                return maxNode.Keys[maxNode.KeyCount - 1];
            }
        }

        public T Min
        {
            get
            {
                if (Root == null) return default(T);

                var minNode = BottomLeftNode;
                return minNode.Keys[0];
            }
        }

        public BPTree(int maxKeysPerNode)
        {
            if (maxKeysPerNode < 3)
            {
                throw new Exception("Max keys per node should be atleast 3.");
            }

            this.maxKeysPerNode = maxKeysPerNode;
        }

        public bool HasItem(T value)
        {
            return find(Root, value) != null;
        }
        /// <summary>
        /// Find the given value node under given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BPTreeNode<T> find(BPTreeNode<T> node, T value)
        {
            //if leaf then its time to insert
            if (node.IsLeaf)
            {
                for (int i = 0; i < node.KeyCount; i++)
                {
                    if (value.CompareTo(node.Keys[i]) == 0)
                    {
                        return node;
                    }
                }
            }
            else
            {
                //if not leaf then drill down to leaf
                for (int i = 0; i < node.KeyCount; i++)
                {

                    //current value is less than new value
                    //drill down to left child of current value
                    if (value.CompareTo(node.Keys[i]) < 0)
                    {
                        return find(node.Children[i], value);
                    }
                    //current value is grearer than new value
                    //and current value is last element 
                    else if (node.KeyCount == i + 1)
                    {
                        return find(node.Children[i + 1], value);
                    }
                }

            }

            return null;
        }
        /// <summary>
        /// Inserts and element to B-Tree
        /// </summary>
        /// <param name="newValue"></param>
        public void Insert(T newValue)
        {
            if (Root == null)
            {
                Root = new BPTreeNode<T>(maxKeysPerNode, null);
                Root.Keys[0] = newValue;
                Root.KeyCount++;
                Count++;
                BottomLeftNode = Root;
                return;
            }

            var leafToInsert = findInsertionLeaf(Root, newValue);

            insertAndSplit(ref leafToInsert, newValue, null, null);
            Count++;
        }


        /// <summary>
        /// Find the leaf node to start initial insertion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private BPTreeNode<T> findInsertionLeaf(BPTreeNode<T> node, T newValue)
        {
            //if leaf then its time to insert
            if (node.IsLeaf)
            {
                return node;
            }

            //if not leaf then drill down to leaf
            for (int i = 0; i < node.KeyCount; i++)
            {
                //current value is less than new value
                //drill down to left child of current value
                if (newValue.CompareTo(node.Keys[i]) < 0)
                {
                    return findInsertionLeaf(node.Children[i], newValue);
                }
                //current value is grearer than new value
                //and current value is last element 
                else if (node.KeyCount == i + 1)
                {
                    return findInsertionLeaf(node.Children[i + 1], newValue);
                }

            }

            return node;
        }

        /// <summary>
        /// Insert and split recursively up until no split is required
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        private void insertAndSplit(ref BPTreeNode<T> node, T newValue,
            BPTreeNode<T> newValueLeft, BPTreeNode<T> newValueRight)
        {
            //add new item to current node
            //this increases the height of B+ tree by one by adding a new root at top
            if (node == null)
            {
                node = new BPTreeNode<T>(maxKeysPerNode, null);
                Root = node;
            }

            //if node is full
            //then split node
            //and insert new median to parent
            if (node.KeyCount == maxKeysPerNode)
            {
                //divide the current node values + new Node as left & right sub nodes
                var left = new BPTreeNode<T>(maxKeysPerNode, null);
                var right = new BPTreeNode<T>(maxKeysPerNode, null);

                //connect leaves via linked list for faster enumeration
                if (node.IsLeaf)
                {
                    left.Next = right;
                    right.Prev = left;

                    if (node.Next != null)
                    {
                        right.Next = node.Next;
                        node.Next.Prev = right;
                    }

                    if (node.Prev != null)
                    {
                        left.Prev = node.Prev;
                        node.Prev.Next = left;
                    }
                    else
                    {
                        //left most bottom node
                        BottomLeftNode = left;
                    }

                }

                //median of current Node
                var currentMedianIndex = node.GetMedianIndex();

                //init currentNode under consideration to left
                var currentNode = left;
                var currentNodeIndex = 0;

                //new Median also takes new Value in to Account
                var newMedian = default(T);
                var newMedianSet = false;
                var newValueInserted = false;

                //keep track of each insertion
                int insertionCount = 0;

                //insert newValue and existing values in sorted order
                //to left & right nodes
                //set new median during sorting
                for (int i = 0; i < node.KeyCount; i++)
                {

                    //if insertion count reached new median
                    //set the new median by picking the next smallest value
                    if (!newMedianSet && insertionCount == currentMedianIndex)
                    {
                        newMedianSet = true;

                        //median can be the new value or node.keys[i] (next node key)
                        //whichever is smaller
                        if (!newValueInserted && newValue.CompareTo(node.Keys[i]) < 0)
                        {
                            //median is new value
                            newMedian = newValue;
                            newValueInserted = true;

                            if (newValueLeft != null)
                            {
                                setChild(currentNode, currentNode.KeyCount, newValueLeft);
                            }

                            //now fill right node
                            currentNode = right;
                            currentNodeIndex = 0;

                            if (newValueRight != null)
                            {
                                setChild(currentNode, 0, newValueRight);
                            }

                            i--;
                            insertionCount++;
                            continue;
                        }
                        else
                        {
                            //median is next node
                            newMedian = node.Keys[i];

                            //now fill right node
                            currentNode = right;
                            currentNodeIndex = 0;

                            continue;
                        }

                    }

                    //pick the smaller among newValue & node.Keys[i]
                    //and insert in to currentNode (left & right nodes)
                    //if new Value was already inserted then just copy from node.Keys in sequence
                    //since node.Keys is already in sorted order it should be fine
                    if (newValueInserted || node.Keys[i].CompareTo(newValue) < 0)
                    {
                        currentNode.Keys[currentNodeIndex] = node.Keys[i];
                        currentNode.KeyCount++;

                        //if child is set don't set again
                        //the child was already set by last newValueRight or last node
                        if (currentNode.Children[currentNodeIndex] == null)
                        {
                            setChild(currentNode, currentNodeIndex, node.Children[i]);
                        }

                        setChild(currentNode, currentNodeIndex + 1, node.Children[i + 1]);

                    }
                    else
                    {
                        currentNode.Keys[currentNodeIndex] = newValue;
                        currentNode.KeyCount++;

                        setChild(currentNode, currentNodeIndex, newValueLeft);
                        setChild(currentNode, currentNodeIndex + 1, newValueRight);

                        i--;
                        newValueInserted = true;
                    }

                    currentNodeIndex++;
                    insertionCount++;
                }

                //could be that thew newKey is the greatest 
                //so insert at end
                if (!newValueInserted)
                {
                    currentNode.Keys[currentNodeIndex] = newValue;
                    currentNode.KeyCount++;

                    setChild(currentNode, currentNodeIndex, newValueLeft);
                    setChild(currentNode, currentNodeIndex + 1, newValueRight);
                }

                if (node.IsLeaf)
                {
                    InsertAt(right.Keys, 0, newMedian);
                    right.KeyCount++;
                }

                //insert overflow element (newMedian) to parent
                var parent = node.Parent;
                insertAndSplit(ref parent, newMedian, left, right);

            }
            //newValue have room to fit in this node
            //so just insert in right spot in asc order of keys
            else
            {
                insertNonFullNode(ref node, newValue, newValueLeft, newValueRight);
            }

        }

        /// <summary>
        /// Insert to a node that is not full
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <param name="newValueLeft"></param>
        /// <param name="newValueRight"></param>
        private void insertNonFullNode(ref BPTreeNode<T> node, T newValue,
            BPTreeNode<T> newValueLeft, BPTreeNode<T> newValueRight)
        {
            var inserted = false;

            //if left is not null
            //then right should'nt be null
            if (newValueLeft != null)
            {
                newValueLeft.Parent = node;
                newValueRight.Parent = node;
            }

            //insert in sorted order
            for (int i = 0; i < node.KeyCount; i++)
            {
                if (newValue.CompareTo(node.Keys[i]) < 0)
                {
                    InsertAt(node.Keys, i, newValue);
                    node.KeyCount++;

                    //Insert children if any
                    setChild(node, i, newValueLeft);
                    insertChild(node, i + 1, newValueRight);

                    inserted = true;
                    break;
                }
            }

            //newValue is the greatest
            //element should be inserted at the end then
            if (!inserted)
            {
                node.Keys[node.KeyCount] = newValue;
                node.KeyCount++;

                setChild(node, node.KeyCount - 1, newValueLeft);
                setChild(node, node.KeyCount, newValueRight);

            }
        }

        /// <summary>
        /// Delete the given value from this BPTree
        /// </summary>
        /// <param name="value"></param>
        public void Delete(T value)
        {
            var node = findDeletionNode(Root, value);

            if (node == null)
            {
                throw new Exception("Item do not exist in this tree.");
            }

            for (int i = 0; i < node.KeyCount; i++)
            {
                if (value.CompareTo(node.Keys[i]) == 0)
                {

                    removeAt(node.Keys, i);
                    node.KeyCount--;

                    if (node.Parent != null && node != node.Parent.Children[0] && node.KeyCount > 0)
                    {
                        var separatorIndex = getPrevSeparatorIndex(node);
                        node.Parent.Keys[separatorIndex] = node.Keys[0];
                    }

                    balance(node, value);

                    Count--;
                    return;
                }

            }


        }

        /// <summary>
        /// return the node containing min value which will be a leaf at the left most
        /// </summary>
        /// <param name="AsBPTreeNode"></param>
        /// <returns></returns>
        private BPTreeNode<T> findMinNode(BPTreeNode<T> node)
        {
            //if leaf return node
            if (node.IsLeaf)
            {
                return node;
            }

            //step in to left most child
            return findMinNode(node.Children[0]);

        }

        /// <summary>
        /// return the node containing max value which will be a leaf at the right most
        /// </summary>
        /// <param name="AsBPTreeNode"></param>
        /// <returns></returns>
        private BPTreeNode<T> findMaxNode(BPTreeNode<T> node)
        {
            //if leaf return node
            if (node.IsLeaf)
            {
                return node;
            }

            //step in to right most child
            return findMaxNode(node.Children[node.KeyCount]);

        }

        /// <summary>
        /// Balance a node which is short of Keys by rotations or merge
        /// </summary>
        /// <param name="node"></param>
        private void balance(BPTreeNode<T> node, T deleteKey)
        {
            if (node == Root)
            {
                return;
            }

            if (node.KeyCount >= minKeysPerNode)
            {

                updateIndex(node, deleteKey, true);
                return;
            }

            var rightSibling = getRightSibling(node);

            if (rightSibling != null
                && (rightSibling.KeyCount) > minKeysPerNode)
            {
                leftRotate(node, rightSibling);

                var minNode = findMinNode(node);
                updateIndex(node, deleteKey, true);

                return;
            }

            var leftSibling = getLeftSibling(node);

            if (leftSibling != null
                && (leftSibling.KeyCount) > minKeysPerNode)
            {
                rightRotate(leftSibling, node);

                updateIndex(node, deleteKey, true);

                return;
            }

            if (rightSibling != null)
            {
                sandwich(node, rightSibling, deleteKey);
            }
            else
            {
                sandwich(leftSibling, node, deleteKey);
            }


        }

        /// <summary>
        /// optionally recursively update outdated index with new min of right node 
        /// after deletion of a value
        /// </summary>
        /// <param name="node"></param>
        /// <param name="deleteKey"></param>
        /// <param name="nextMin"></param>
        private void updateIndex(BPTreeNode<T> node, T deleteKey, bool spiralUp)
        {
            if (node == null)
                return;

            if (node.IsLeaf || node.Children[0].IsLeaf)
            {
                updateIndex(node.Parent, deleteKey, spiralUp);
                return;
            }

            for (int i = 0; i < node.KeyCount; i++)
            {
                if (node.Keys[i].CompareTo(deleteKey) == 0)
                {
                    node.Keys[i] = findMinNode(node.Children[i + 1]).Keys[0];
                }
            }

            if (spiralUp)
            {
                updateIndex(node.Parent, deleteKey, true);
            }

        }


        /// <summary>
        /// merge two adjacent siblings to one node
        /// </summary>
        /// <param name="leftSibling"></param>
        /// <param name="rightSibling"></param>
        /// <param name="deleteKey"></param>
        private void sandwich(BPTreeNode<T> leftSibling, BPTreeNode<T> rightSibling, T deleteKey)
        {
            var separatorIndex = getNextSeparatorIndex(leftSibling);
            var parent = leftSibling.Parent;

            var newNode = new BPTreeNode<T>(maxKeysPerNode, leftSibling.Parent);

            //if leaves are merged then update the Next & Prev pointers
            if (leftSibling.IsLeaf)
            {
                if (leftSibling.Prev != null)
                {
                    newNode.Prev = leftSibling.Prev;
                    leftSibling.Prev.Next = newNode;
                }
                else
                {
                    BottomLeftNode = newNode;
                }

                if (rightSibling.Next != null)
                {
                    newNode.Next = rightSibling.Next;
                    rightSibling.Next.Prev = newNode;
                }

            }

            var newIndex = 0;


            for (int i = 0; i < leftSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = leftSibling.Keys[i];

                if (leftSibling.Children[i] != null)
                {
                    setChild(newNode, newIndex, leftSibling.Children[i]);
                }

                if (leftSibling.Children[i + 1] != null)
                {
                    setChild(newNode, newIndex + 1, leftSibling.Children[i + 1]);
                }

                newIndex++;
            }

            //special case when left sibling is empty 
            if (leftSibling.KeyCount == 0 && leftSibling.Children[0] != null)
            {
                setChild(newNode, newIndex, leftSibling.Children[0]);
            }

            if (!rightSibling.IsLeaf)
            {
                newNode.Keys[newIndex] = parent.Keys[separatorIndex];
                newIndex++;
            }

            for (int i = 0; i < rightSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = rightSibling.Keys[i];

                if (rightSibling.Children[i] != null)
                {
                    setChild(newNode, newIndex, rightSibling.Children[i]);

                    //when a left leaf is added as right leaf
                    //we need to push parent key as first node of new leaf
                    if (i == 0 && rightSibling.Children[i].IsLeaf
                        && rightSibling.Children[i].Keys[0].CompareTo(newNode.Keys[newIndex - 1]) != 0)
                    {
                        InsertAt(rightSibling.Children[i].Keys, 0, newNode.Keys[newIndex - 1]);
                        rightSibling.Children[i].KeyCount++;
                    }

                }

                if (rightSibling.Children[i + 1] != null)
                {
                    setChild(newNode, newIndex + 1, rightSibling.Children[i + 1]);
                }

                newIndex++;
            }

            //special case when left sibling is empty 
            if (rightSibling.KeyCount == 0 && rightSibling.Children[0] != null)
            {
                setChild(newNode, newIndex, rightSibling.Children[0]);

                if (newNode.Children[newIndex].IsLeaf)
                {
                    newNode.Keys[newIndex - 1] = newNode.Children[newIndex].Keys[0];
                }
            }

            newNode.KeyCount = newIndex;

            setChild(parent, separatorIndex, newNode);

            removeAt(parent.Keys, separatorIndex);
            parent.KeyCount--;

            removeChild(parent, separatorIndex + 1);

            if (newNode.IsLeaf && newNode.Parent.Children[0] != newNode)
            {
                separatorIndex = getPrevSeparatorIndex(newNode);
                newNode.Parent.Keys[separatorIndex] = newNode.Keys[0];
            }

            updateIndex(newNode, deleteKey, false);

            if (parent.KeyCount == 0
                && parent == Root)
            {
                Root = newNode;
                Root.Parent = null;

                if (Root.KeyCount == 0)
                {
                    Root = null;
                }

                return;
            }


            if (parent.KeyCount < minKeysPerNode)
            {
                balance(parent, deleteKey);
            }

            updateIndex(newNode, deleteKey, true);

        }


        /// <summary>
        /// do a right rotation 
        /// </summary>
        /// <param name="rightSibling"></param>
        /// <param name="leftSibling"></param>
        private void rightRotate(BPTreeNode<T> leftSibling, BPTreeNode<T> rightSibling)
        {
            var parentIndex = getNextSeparatorIndex(leftSibling);

            //move parent value to right
            InsertAt(rightSibling.Keys, 0, rightSibling.Parent.Keys[parentIndex]);
            rightSibling.KeyCount++;

            insertChild(rightSibling, 0, leftSibling.Children[leftSibling.KeyCount]);

            if (rightSibling.Children[1] != null
                && rightSibling.Children[1].IsLeaf)
            {
                rightSibling.Keys[0] = rightSibling.Children[1].Keys[0];
            }


            //move rightmost element in left sibling to parent
            rightSibling.Parent.Keys[parentIndex] = leftSibling.Keys[leftSibling.KeyCount - 1];

            //remove rightmost element of left sibling
            removeAt(leftSibling.Keys, leftSibling.KeyCount - 1);
            leftSibling.KeyCount--;

            removeChild(leftSibling, leftSibling.KeyCount + 1);

            if (rightSibling.IsLeaf)
            {
                rightSibling.Keys[0] = rightSibling.Parent.Keys[parentIndex];
            }

        }

        /// <summary>
        /// do a left rotation
        /// </summary>
        /// <param name="leftSibling"></param>
        /// <param name="rightSibling"></param>
        private void leftRotate(BPTreeNode<T> leftSibling, BPTreeNode<T> rightSibling)
        {
            var parentIndex = getNextSeparatorIndex(leftSibling);

            //move root to left
            leftSibling.Keys[leftSibling.KeyCount] = leftSibling.Parent.Keys[parentIndex];
            leftSibling.KeyCount++;

            setChild(leftSibling, leftSibling.KeyCount, rightSibling.Children[0]);

            if (leftSibling.Children[leftSibling.KeyCount] != null
                && leftSibling.Children[leftSibling.KeyCount].IsLeaf)
            {
                leftSibling.Keys[leftSibling.KeyCount - 1] = leftSibling.Children[leftSibling.KeyCount].Keys[0];
            }

            //move right to parent
            leftSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];
            //remove right
            removeAt(rightSibling.Keys, 0);
            rightSibling.KeyCount--;

            removeChild(rightSibling, 0);

            if (rightSibling.IsLeaf)
            {
                rightSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];
            }

            if (leftSibling.IsLeaf && leftSibling.Parent.Children[0] != leftSibling)
            {
                parentIndex = getPrevSeparatorIndex(leftSibling);
                leftSibling.Parent.Keys[parentIndex] = leftSibling.Keys[0];
            }
        }


        /// <summary>
        /// Locate the node in which the item to delete exist
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BPTreeNode<T> findDeletionNode(BPTreeNode<T> node, T value)
        {
            //if leaf then its time to insert
            if (node.IsLeaf)
            {
                for (int i = 0; i < node.KeyCount; i++)
                {
                    if (value.CompareTo(node.Keys[i]) == 0)
                    {
                        return node;
                    }
                }
            }
            else
            {
                //if not leaf then drill down to leaf
                for (int i = 0; i < node.KeyCount; i++)
                {
                    //current value is less than new value
                    //drill down to left child of current value
                    if (value.CompareTo(node.Keys[i]) < 0)
                    {
                        return findDeletionNode(node.Children[i], value);
                    }
                    //current value is grearer than new value
                    //and current value is last element 
                    else if (node.KeyCount == i + 1)
                    {
                        return findDeletionNode(node.Children[i + 1], value);
                    }

                }
            }

            return null;
        }

        /// <summary>
        /// Get prev separator key of this child Node in parent
        /// </summary>
        /// <param name="leftChildNode"></param>
        /// <returns></returns>
        private int getPrevSeparatorIndex(BPTreeNode<T> node)
        {
            var parent = node.Parent;

            if (node.Index == 0)
            {
                return 0;
            }
            else if (node.Index == parent.KeyCount)
            {
                return node.Index - 1;
            }
            else
            {
                return node.Index - 1;
            }

        }


        /// <summary>
        /// Get next separator key of this child Node in parent
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int getNextSeparatorIndex(BPTreeNode<T> node)
        {
            var parent = node.Parent;

            if (node.Index == 0)
            {
                return 0;
            }
            else if (node.Index == parent.KeyCount)
            {
                return node.Index - 1;
            }
            else
            {
                return node.Index;
            }

        }
        /// <summary>
        /// get the right sibling node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BPTreeNode<T> getRightSibling(BPTreeNode<T> node)
        {
            var parent = node.Parent;

            if (node.Index == parent.KeyCount)
            {
                return null;
            }

            return parent.Children[node.Index + 1];

        }

        /// <summary>
        /// get left sibling node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private BPTreeNode<T> getLeftSibling(BPTreeNode<T> node)
        {
            if (node.Index == 0)
            {
                return null;
            }

            return node.Parent.Children[node.Index - 1];
        }

        private void setChild(BPTreeNode<T> parent, int childIndex, BPTreeNode<T> child)
        {
            parent.Children[childIndex] = child;

            if (child != null)
            {
                child.Parent = parent;
                child.Index = childIndex;
            }

        }

        private void insertChild(BPTreeNode<T> parent, int childIndex, BPTreeNode<T> child)
        {
            InsertAt(parent.Children, childIndex, child);

            if (child != null)
            {
                child.Parent = parent;
            }

            //update indices
            for (int i = childIndex; i <= parent.KeyCount; i++)
            {
                if (parent.Children[i] != null)
                {
                    parent.Children[i].Index = i;
                }
            }
        }

        private void removeChild(BPTreeNode<T> parent, int childIndex)
        {
            removeAt(parent.Children, childIndex);

            //update indices
            for (int i = childIndex; i <= parent.KeyCount; i++)
            {
                if (parent.Children[i] != null)
                {
                    parent.Children[i].Index = i;
                }

            }
        }

        /// <summary>
        /// Shift array right at index to make room for new insertion
        /// And then insert at index
        /// Assumes array have atleast one empty index at end
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        private void InsertAt<S>(S[] array, int index, S newValue)
        {
            //shift elements right by one indice from index
            Array.Copy(array, index, array, index + 1, array.Length - index - 1);
            //now set the value
            array[index] = newValue;
        }

        /// <summary>
        /// Shift array left at index    
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        private void removeAt<S>(S[] array, int index)
        {

            //shift elements right by one indice from index
            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
        }

        //Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BPTreeEnumerator<T>(this);
        }

    }

    //  implement IEnumerator.
    public class BPTreeEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private BPTreeNode<T> bottomLeftNode;
        private BPTreeNode<T> current;
        private int i = -1;

        public BPTreeEnumerator(BPTree<T> tree)
        {
            bottomLeftNode = tree.BottomLeftNode;
            current = bottomLeftNode;
        }

        public bool MoveNext()
        {
            if (i + 1 < current.KeyCount)
            {
                i++;
                return true;
            }

            current = current.Next;
            i = 0;

            return current != null && current.KeyCount > 0;
        }

        public void Reset()
        {
            current = bottomLeftNode;
            i = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {

                try
                {
                    return current.Keys[i];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
            current = null;
            bottomLeftNode = null;
            i = -1;
        }
    }

}