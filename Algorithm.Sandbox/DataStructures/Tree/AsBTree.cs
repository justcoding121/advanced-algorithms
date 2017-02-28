using System;

namespace Algorithm.Sandbox.DataStructures.Tree
{
    internal class AsBTreeNode<T> where T : IComparable
    {
        internal T[] Keys { get; set; }
        internal int KeyCount;

        internal AsBTreeNode<T> Parent { get; set; }
        internal AsBTreeNode<T>[] Children { get; set; }

        internal bool IsLeaf;
        internal AsBTreeNode(int maxKeysPerNode, AsBTreeNode<T> parent)
        {
            IsLeaf = true;
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

        public AsBTree(int maxKeysPerNode)
        {
            this.maxKeysPerNode = maxKeysPerNode;
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
        /// gets the height of the tree
        /// usefull to verify accuracy of this BTree implementation
        /// </summary>
        /// <returns></returns>
        internal int GetHeight()
        {
            return GetHeight(Root);
        }

        /// <summary>
        /// find height by recursively visiting children
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetHeight(AsBTreeNode<T> node)
        {
            var max = 0;

            for (int i = 0; i <= node.KeyCount; i++)
            {
                if (node.Children[i] != null)
                {
                    max = Math.Max(GetHeight(node.Children[i]) + 1, max);
                }
            }

            return max;
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
                        currentNode.Children[currentNodeIndex] = node.Children[i];
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

                    node.IsLeaf = false;
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

        public void Delete(T value)
        {

        }

        public bool Exists(T value)
        {
            throw new NotImplementedException();
        }
    }
}