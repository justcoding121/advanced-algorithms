using System;
using System.Diagnostics;
using System.Linq;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    internal class BTreeNode<T> where T : IComparable
    {
        /// <summary>
        /// Array Index of this node in parent's Children array
        /// </summary>
        internal int Index;

        internal T[] Keys { get; set; }
        internal int KeyCount;

        internal BTreeNode<T> Parent { get; set; }
        internal BTreeNode<T>[] Children { get; set; }

        internal bool IsLeaf => Children[0] == null;

        internal BTreeNode(int maxKeysPerNode, BTreeNode<T> parent)
        {

            Parent = parent;
            Keys = new T[maxKeysPerNode];
            Children = new BTreeNode<T>[maxKeysPerNode + 1];

        }

        internal int GetMedianIndex()
        {
            return (KeyCount / 2) + 1;
        }
    }
    /// <summary>
    /// A BTree implementation
    /// TODO Implement IEnumerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BTree<T> where T : IComparable
    {
        public int Count { get; private set; }

        internal BTreeNode<T> Root;

        private int maxKeysPerNode;
        private int minKeysPerNode => maxKeysPerNode / 2;

        public BTree(int maxKeysPerNode)
        {
            if (maxKeysPerNode < 3)
            {
                throw new Exception("Max keys per node should be atleast 3.");
            }

            this.maxKeysPerNode = maxKeysPerNode;
        }

        public bool HasItem(T value)
        {
            return Find(Root, value) != null;
        }

        /// <summary>
        /// Find the value node under given node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BTreeNode<T> Find(BTreeNode<T> node, T value)
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
                    if (value.CompareTo(node.Keys[i]) == 0)
                    {
                        return node;
                    }

                    //current value is less than new value
                    //drill down to left child of current value
                    if (value.CompareTo(node.Keys[i]) < 0)
                    {
                        return Find(node.Children[i], value);
                    }
                    //current value is grearer than new value
                    //and current value is last element 
                    else if (node.KeyCount == i + 1)
                    {
                        return Find(node.Children[i + 1], value);
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
                Root = new BTreeNode<T>(maxKeysPerNode, null);
                Root.Keys[0] = newValue;
                Root.KeyCount++;
                Count++;
                return;
            }

            var leafToInsert = FindInsertionLeaf(Root, newValue);

            InsertAndSplit(ref leafToInsert, newValue, null, null);
            Count++;
        }


        /// <summary>
        /// Find the leaf node to start initial insertion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private BTreeNode<T> FindInsertionLeaf(BTreeNode<T> node, T newValue)
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
                    return FindInsertionLeaf(node.Children[i], newValue);
                }
                //current value is grearer than new value
                //and current value is last element 
                else if (node.KeyCount == i + 1)
                {
                    return FindInsertionLeaf(node.Children[i + 1], newValue);
                }

            }

            return node;
        }

        /// <summary>
        /// Insert and split recursively up until no split is required
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        private void InsertAndSplit(ref BTreeNode<T> node, T newValue,
            BTreeNode<T> newValueLeft, BTreeNode<T> newValueRight)
        {
            //add new item to current node
            if (node == null)
            {
                node = new BTreeNode<T>(maxKeysPerNode, null);
                Root = node;
            }

            //if node is full
            //then split node
            //and insert new median to parent
            if (node.KeyCount == maxKeysPerNode)
            {
                //divide the current node values + new Node as left & right sub nodes
                var left = new BTreeNode<T>(maxKeysPerNode, null);
                var right = new BTreeNode<T>(maxKeysPerNode, null);

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
                                SetChild(currentNode, currentNode.KeyCount, newValueLeft);
                            }

                            //now fill right node
                            currentNode = right;
                            currentNodeIndex = 0;

                            if (newValueRight != null)
                            {
                                SetChild(currentNode, 0, newValueRight);
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
                            SetChild(currentNode, currentNodeIndex, node.Children[i]);
                        }

                        SetChild(currentNode, currentNodeIndex + 1, node.Children[i + 1]);

                    }
                    else
                    {
                        currentNode.Keys[currentNodeIndex] = newValue;
                        currentNode.KeyCount++;

                        SetChild(currentNode, currentNodeIndex, newValueLeft);
                        SetChild(currentNode, currentNodeIndex + 1, newValueRight);

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

                    SetChild(currentNode, currentNodeIndex, newValueLeft);
                    SetChild(currentNode, currentNodeIndex + 1, newValueRight);

                }

                //insert overflow element (newMedian) to parent
                var parent = node.Parent;
                InsertAndSplit(ref parent, newMedian, left, right);

            }
            //newValue have room to fit in this node
            //so just insert in right spot in asc order of keys
            else
            {
                InsertNonFullNode(ref node, newValue, newValueLeft, newValueRight);
            }

        }

        /// <summary>
        /// Insert to a node that is not full
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newValue"></param>
        /// <param name="newValueLeft"></param>
        /// <param name="newValueRight"></param>
        private void InsertNonFullNode(ref BTreeNode<T> node, T newValue,
            BTreeNode<T> newValueLeft, BTreeNode<T> newValueRight)
        {
            var inserted = false;

            //insert in sorted order
            for (int i = 0; i < node.KeyCount; i++)
            {
                if (newValue.CompareTo(node.Keys[i]) < 0)
                {
                    InsertAt(node.Keys, i, newValue);
                    node.KeyCount++;

                    //Insert children if any
                    SetChild(node, i, newValueLeft);
                    InsertChild(node, i + 1, newValueRight);


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

                SetChild(node, node.KeyCount - 1, newValueLeft);
                SetChild(node, node.KeyCount, newValueRight);


            }
        }



        /// <summary>
        /// Delete the given value from this BTree
        /// </summary>
        /// <param name="value"></param>
        public void Delete(T value)
        {
            var node = FindDeletionNode(Root, value);

            if (node == null)
            {
                throw new Exception("Item do not exist in this tree.");
            }

            for (int i = 0; i < node.KeyCount; i++)
            {
                if (value.CompareTo(node.Keys[i]) == 0)
                {
                    //if node is leaf and no underflow
                    //then just remove the node
                    if (node.IsLeaf)
                    {
                        RemoveAt(node.Keys, i);
                        node.KeyCount--;

                        Balance(node);

                    }
                    else
                    {
                        //replace with max node of left tree
                        var maxNode = FindMaxNode(node.Children[i]);
                        node.Keys[i] = maxNode.Keys[maxNode.KeyCount - 1];

                        RemoveAt(maxNode.Keys, maxNode.KeyCount - 1);
                        maxNode.KeyCount--;

                        Balance(maxNode);

                    }

                    Count--;
                    return;
                }

            }


        }

        /// <summary>
        /// return the node containing max value which will be a leaf at the right most
        /// </summary>
        /// <param name="asBTreeNode"></param>
        /// <returns></returns>
        private BTreeNode<T> FindMaxNode(BTreeNode<T> node)
        {
            //if leaf return node
            if (node.IsLeaf)
            {
                return node;
            }

            //step in to right most child
            return FindMaxNode(node.Children[node.KeyCount]);

        }

        /// <summary>
        /// Balance a node which is short of Keys by rotations or merge
        /// </summary>
        /// <param name="node"></param>
        private void Balance(BTreeNode<T> node)
        {
            if (node == Root || node.KeyCount >= minKeysPerNode)
            {
                return;
            }

            var rightSibling = GetRightSibling(node);

            if (rightSibling != null
                && rightSibling.KeyCount > minKeysPerNode)
            {
                LeftRotate(node, rightSibling);
                return;
            }

            var leftSibling = GetLeftSibling(node);

            if (leftSibling != null
                && leftSibling.KeyCount > minKeysPerNode)
            {
                RightRotate(leftSibling, node);
                return;
            }

            if (rightSibling != null)
            {
                Sandwich(node, rightSibling);
            }
            else
            {
                Sandwich(leftSibling, node);
            }


        }

        /// <summary>
        ///  merge two adjacent siblings to one node
        /// </summary>
        /// <param name="leftSibling"></param>
        /// <param name="rightSibling"></param>
        private void Sandwich(BTreeNode<T> leftSibling, BTreeNode<T> rightSibling)
        {
            var separatorIndex = GetNextSeparatorIndex(leftSibling);
            var parent = leftSibling.Parent;

            var newNode = new BTreeNode<T>(maxKeysPerNode, leftSibling.Parent);
            var newIndex = 0;

            for (int i = 0; i < leftSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = leftSibling.Keys[i];

                if (leftSibling.Children[i] != null)
                {
                    SetChild(newNode, newIndex, leftSibling.Children[i]);
                }

                if (leftSibling.Children[i + 1] != null)
                {
                    SetChild(newNode, newIndex + 1, leftSibling.Children[i + 1]);
                }

                newIndex++;
            }

            //special case when left sibling is empty 
            if (leftSibling.KeyCount == 0 && leftSibling.Children[0] != null)
            {
                SetChild(newNode, newIndex, leftSibling.Children[0]);
            }

            newNode.Keys[newIndex] = parent.Keys[separatorIndex];
            newIndex++;

            for (int i = 0; i < rightSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = rightSibling.Keys[i];

                if (rightSibling.Children[i] != null)
                {
                    SetChild(newNode, newIndex, rightSibling.Children[i]);
                }

                if (rightSibling.Children[i + 1] != null)
                {
                    SetChild(newNode, newIndex + 1, rightSibling.Children[i + 1]);
                }

                newIndex++;
            }

            //special case when left sibling is empty 
            if (rightSibling.KeyCount == 0 && rightSibling.Children[0] != null)
            {
                SetChild(newNode, newIndex, rightSibling.Children[0]);
            }

            newNode.KeyCount = newIndex;
            SetChild(parent, separatorIndex, newNode);
            RemoveAt(parent.Keys, separatorIndex);
            parent.KeyCount--;

            RemoveChild(parent, separatorIndex + 1);


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
                Balance(parent);
            }
        }


        /// <summary>
        /// do a right rotation 
        /// </summary>
        /// <param name="rightSibling"></param>
        /// <param name="leftSibling"></param>
        private void RightRotate(BTreeNode<T> leftSibling, BTreeNode<T> rightSibling)
        {
            var parentIndex = GetNextSeparatorIndex(leftSibling);

            InsertAt(rightSibling.Keys, 0, rightSibling.Parent.Keys[parentIndex]);
            rightSibling.KeyCount++;

            InsertChild(rightSibling, 0, leftSibling.Children[leftSibling.KeyCount]);

            rightSibling.Parent.Keys[parentIndex] = leftSibling.Keys[leftSibling.KeyCount - 1];

            RemoveAt(leftSibling.Keys, leftSibling.KeyCount - 1);
            leftSibling.KeyCount--;

            RemoveChild(leftSibling, leftSibling.KeyCount + 1);


        }

        /// <summary>
        /// do a left rotation
        /// </summary>
        /// <param name="leftSibling"></param>
        /// <param name="rightSibling"></param>
        private void LeftRotate(BTreeNode<T> leftSibling, BTreeNode<T> rightSibling)
        {
            var parentIndex = GetNextSeparatorIndex(leftSibling);
            leftSibling.Keys[leftSibling.KeyCount] = leftSibling.Parent.Keys[parentIndex];
            leftSibling.KeyCount++;

            SetChild(leftSibling, leftSibling.KeyCount, rightSibling.Children[0]);


            leftSibling.Parent.Keys[parentIndex] = rightSibling.Keys[0];

            RemoveAt(rightSibling.Keys, 0);
            rightSibling.KeyCount--;

            RemoveChild(rightSibling, 0);

        }

        /// <summary>
        /// Locate the node in which the item to delete exist
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private BTreeNode<T> FindDeletionNode(BTreeNode<T> node, T value)
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
                    if (value.CompareTo(node.Keys[i]) == 0)
                    {
                        return node;
                    }

                    //current value is less than new value
                    //drill down to left child of current value
                    if (value.CompareTo(node.Keys[i]) < 0)
                    {
                        return FindDeletionNode(node.Children[i], value);
                    }
                    //current value is grearer than new value
                    //and current value is last element 
                    else if (node.KeyCount == i + 1)
                    {
                        return FindDeletionNode(node.Children[i + 1], value);
                    }

                }
            }

            return null;
        }

        /// <summary>
        /// Get next key separator index after this child Node in parent 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetNextSeparatorIndex(BTreeNode<T> node)
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
        private BTreeNode<T> GetRightSibling(BTreeNode<T> node)
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
        private BTreeNode<T> GetLeftSibling(BTreeNode<T> node)
        {
            if (node.Index == 0)
            {
                return null;
            }

            return node.Parent.Children[node.Index - 1];

        }

        private void SetChild(BTreeNode<T> parent, int childIndex, BTreeNode<T> child)
        {
            parent.Children[childIndex] = child;

            if (child != null)
            {
                child.Parent = parent;
                child.Index = childIndex;
            }

        }

        private void InsertChild(BTreeNode<T> parent, int childIndex, BTreeNode<T> child)
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

        private void RemoveChild(BTreeNode<T> parent, int childIndex)
        {
            RemoveAt(parent.Children, childIndex);

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
        private void RemoveAt<S>(S[] array, int index)
        {
            //shift elements right by one indice from index
            Array.Copy(array, index + 1, array, index, array.Length - index - 1);
        }

    }
}