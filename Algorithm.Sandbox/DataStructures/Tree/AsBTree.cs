using System;
using System.Linq;


namespace Algorithm.Sandbox.DataStructures.Tree
{
    internal class AsBTreeNode<T> where T : IComparable
    {
        internal T[] Keys { get; set; }
        internal int KeyCount;

        internal AsBTreeNode<T> Parent { get; set; }
        internal AsBTreeNode<T>[] Children { get; set; }

        internal bool IsLeaf => !Children.Any(x => x != null);

        internal AsBTreeNode(int maxKeysPerNode, AsBTreeNode<T> parent)
        {

            Parent = parent;
            Keys = new T[maxKeysPerNode];
            Children = new AsBTreeNode<T>[maxKeysPerNode + 1];

        }

        internal int GetMedianIndex()
        {
            return (KeyCount / 2) + 1;
        }
    }

    public class AsBTree<T> where T : IComparable
    {
        public int Count { get; private set; }

        internal AsBTreeNode<T> Root;

        private int maxKeysPerNode;
        private int minKeysPerNode => maxKeysPerNode / 2;

        public AsBTree(int maxKeysPerNode)
        {
            this.maxKeysPerNode = maxKeysPerNode;
        }

        public bool HasItem(T value)
        {
            return Find(Root, value) != null;
        }

        private AsBTreeNode<T> Find(AsBTreeNode<T> node, T value)
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
                    return FindInsertionLeaf(node.Children[i], value);
                }
                //current value is grearer than new value
                //and current value is last element 
                else if (node.KeyCount == i + 1)
                {
                    return FindInsertionLeaf(node.Children[i + 1], value);
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
                Root = new AsBTreeNode<T>(maxKeysPerNode, null);
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
        private AsBTreeNode<T> FindInsertionLeaf(AsBTreeNode<T> node, T newValue)
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
        private void InsertAndSplit(ref AsBTreeNode<T> node, T newValue,
            AsBTreeNode<T> newValueLeft, AsBTreeNode<T> newValueRight)
        {
            //add new item to current node
            if (node == null)
            {
                node = new AsBTreeNode<T>(maxKeysPerNode, null);
                Root = node;
            }

            //if node is full
            //then split node
            //and insert new median to parent
            if (node.KeyCount == maxKeysPerNode)
            {
                //divide the current node values + new Node as left & right sub nodes
                var left = new AsBTreeNode<T>(maxKeysPerNode, null);
                var right = new AsBTreeNode<T>(maxKeysPerNode, null);

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
                    //if reached the median
                    //then start filling right node
                    if (i == currentMedianIndex)
                    {
                        currentNode = right;
                        currentNodeIndex = 0;
                    }

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
                            i--;
                            insertionCount++;
                            continue;
                        }
                        else
                        {
                            //median is next node
                            newMedian = node.Keys[i];
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

                        //if child is set don't set again
                        //the child was already set by last newValueRight or last node
                        if (currentNode.Children[currentNodeIndex] == null)
                        {
                            currentNode.Children[currentNodeIndex] = node.Children[i];
                        }

                        currentNode.Children[currentNodeIndex + 1] = node.Children[i + 1];

                        currentNode.KeyCount++;
                    }
                    else
                    {
                        currentNode.Keys[currentNodeIndex] = newValue;
                        currentNode.Children[currentNodeIndex] = newValueLeft;
                        currentNode.Children[currentNodeIndex + 1] = newValueRight;
                        currentNode.KeyCount++;
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
                    currentNode.Children[currentNodeIndex] = newValueLeft;
                    currentNode.Children[currentNodeIndex + 1] = newValueRight;
                    currentNode.KeyCount++;
                }

                //insert overflow element (newMedian) to parent
                var parent = node.Parent;
                InsertAndSplit(ref parent, newMedian, left, right);

            }
            //newValue have room to fit in this node
            //so just insert in right spot in asc order of keys
            else
            {
                var inserted = false;

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

                        //Insert children if any
                        node.Children[i] = newValueLeft;
                        InsertAt(node.Children, i + 1, newValueRight);

                        node.KeyCount++;

                        inserted = true;
                        break;
                    }
                }

                //newValue is the greatest
                //element should be inserted at the end then
                if (!inserted)
                {
                    node.Keys[node.KeyCount] = newValue;

                    node.Children[node.KeyCount] = newValueLeft;
                    node.Children[node.KeyCount + 1] = newValueRight;

                    node.KeyCount++;
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
        /// Shift array right at index to make room for new insertion
        /// And then insert at index
        /// Assumes array have atleast one empty index at end
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

                }

            }


            Count--;
        }

        /// <summary>
        /// return the node containing max value which will be a leaf at the right most
        /// </summary>
        /// <param name="asBTreeNode"></param>
        /// <returns></returns>
        private AsBTreeNode<T> FindMaxNode(AsBTreeNode<T> node)
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
        private void Balance(AsBTreeNode<T> node)
        {
            if (node == Root || node.KeyCount >= minKeysPerNode)
            {
                return;
            }

            var rightSibling = GetRightSibling(node);

            if (rightSibling!=null 
                &&rightSibling.KeyCount > minKeysPerNode)
            {
                LeftRotate(node, rightSibling);
                return;
            }

            var leftSibling = GetLeftSibling(node);

            if (leftSibling!=null
                && leftSibling.KeyCount > minKeysPerNode)
            {
                RightRotate(node, leftSibling);
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

        //merge two adjacent siblings to one node
        private void Sandwich(AsBTreeNode<T> leftSibling, AsBTreeNode<T> rightSibling)
        {
            var separatorIndex = GetSeparatorIndex(leftSibling);
            var parent = leftSibling.Parent;

            var newNode = new AsBTreeNode<T>(maxKeysPerNode, leftSibling.Parent);

            var newIndex = 0;


            for (int i = 0; i < leftSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = leftSibling.Keys[i];
                newNode.Children[newIndex] = leftSibling.Children[i];

                newIndex++;
            }

            //copy last child
            newNode.Children[newIndex] = leftSibling.Children[leftSibling.KeyCount];

            newNode.Keys[newIndex] = parent.Keys[separatorIndex];
            newIndex++;


            for (int i = 0; i < rightSibling.KeyCount; i++)
            {
                newNode.Keys[newIndex] = rightSibling.Keys[i];
                newNode.Children[newIndex] = rightSibling.Children[i];

                newIndex++;
            }

            //copy last child
            newNode.Children[newIndex] = rightSibling.Children[rightSibling.KeyCount];

            parent.Children[separatorIndex] = newNode;

            RemoveAt(parent.Keys, separatorIndex);
            RemoveAt(parent.Children, separatorIndex + 1);

            if (parent.KeyCount == 0)
            {
                Root = newNode;
                return;
            }

            if (parent.KeyCount < minKeysPerNode)
            {
                Balance(parent);
            }
        }

        private void RightRotate(AsBTreeNode<T> node, AsBTreeNode<T> leftSibling)
        {
            var separatorIndex = GetSeparatorIndex(node);

            InsertAt(node.Keys, 0, node.Parent.Keys[separatorIndex]);
            InsertAt(node.Children, 0, leftSibling.Children[leftSibling.KeyCount]);

            node.Parent.Keys[separatorIndex] = leftSibling.Keys[leftSibling.KeyCount - 1];

            RemoveAt(leftSibling.Keys, leftSibling.KeyCount - 1);
            RemoveAt(leftSibling.Children, leftSibling.KeyCount);
        }

        private void LeftRotate(AsBTreeNode<T> node, AsBTreeNode<T> rightSibling)
        {
            var separatorIndex = GetSeparatorIndex(node);

            node.Keys[node.KeyCount] = node.Parent.Keys[separatorIndex];
            node.Children[node.KeyCount + 1] = rightSibling.Children[0];

            node.Parent.Keys[separatorIndex] = rightSibling.Keys[0];

            RemoveAt(rightSibling.Keys, 0);
            RemoveAt(rightSibling.Children, 0);
        }

        private int GetSeparatorIndex(AsBTreeNode<T> node)
        {
            var parent = node.Parent;

            for (int i = 0; i <= parent.KeyCount; i++)
            {
                if (parent.Children[i] == node)
                {
                    if (i == 0)
                    {
                        return 0;
                    }
                    else if (i == parent.KeyCount)
                    {
                        return i - 1;
                    }
                    else
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// get the right sibling node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private AsBTreeNode<T> GetRightSibling(AsBTreeNode<T> node)
        {
            var parent = node.Parent;

            for (int i = 0; i < parent.KeyCount; i++)
            {
                if (parent.Children[i] == node)
                {
                    return parent.Children[i + 1];
                }
            }

            return null;
        }

        /// <summary>
        /// get left sibling node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private AsBTreeNode<T> GetLeftSibling(AsBTreeNode<T> node)
        {
            var parent = node.Parent;

            for (int i = 1; i <= parent.KeyCount; i++)
            {
                if (parent.Children[i] == node)
                {
                    return parent.Children[i - 1];
                }
            }

            return null;
        }

        /// <summary>
        /// Locate the node in which the item to delete exist
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private AsBTreeNode<T> FindDeletionNode(AsBTreeNode<T> node, T value)
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
                    return FindInsertionLeaf(node.Children[i], value);
                }
                //current value is grearer than new value
                //and current value is last element 
                else if (node.KeyCount == i + 1)
                {
                    return FindInsertionLeaf(node.Children[i + 1], value);
                }

            }

            return null;
        }

    }
}