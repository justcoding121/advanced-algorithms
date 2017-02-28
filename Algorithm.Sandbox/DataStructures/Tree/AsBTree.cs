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


        public void Insert(T newValue)
        {
            if (Root == null)
            {
                Root = new AsBTreeNode<T>(maxKeysPerNode, null);
                Root.Keys[0] = newValue;
                Root.KeyCount++;
                return;
            }

            var leafToInsert = FindInsertionLeaf(Root, newValue);

            SplitInsert(ref leafToInsert, newValue, null, null);

        }

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
        private void SplitInsert(ref AsBTreeNode<T> node, T newValue,
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
                var left = new AsBTreeNode<T>(maxKeysPerNode, null);
                var right = new AsBTreeNode<T>(maxKeysPerNode, null);

                var medianIndex = node.GetMedianIndex();
                var currentNode = left;
                var currentNodeIndex = 0;

                var newMedian = default(T);
                var medianSet = false;
                var valueTaken = false;

                int j = 0;
                //insert in sorted order
                for (int i = 0; i < node.KeyCount; i++)
                {
                    if (i == medianIndex)
                    {
                        currentNode = right;
                        currentNodeIndex = 0;
                    }

                    if (!medianSet && j == medianIndex)
                    {
                        medianSet = true;

                        if (!valueTaken && newValue.CompareTo(node.Keys[i]) < 0)
                        {
                            newMedian = newValue;
                            valueTaken = true;
                            i--;
                            j++;
                            continue;
                        }
                        else
                        {
                            newMedian = node.Keys[i];
                            continue;
                        }

                    }

                    if (valueTaken || node.Keys[i].CompareTo(newValue) < 0)
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
                        valueTaken = true;
                    }

                    currentNodeIndex++;
                    j++;
                }

                if(!valueTaken)
                {
                    currentNode.Keys[currentNodeIndex] = newValue;
                    currentNode.Children[currentNodeIndex] = newValueLeft;
                    currentNode.Children[currentNodeIndex + 1] = newValueRight;
                    currentNode.KeyCount++;
                }
                //insert overflow to parent
                var parent = node.Parent;
                SplitInsert(ref parent, newMedian, left, right);

            }
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
                        InsertAt(node.Children, i+1, newValueRight);


                        node.KeyCount++;

                        inserted = true;
                        break;
                    }
                }

                //element should be inserted at the end then
                if (!inserted)
                {
                    node.Keys[node.KeyCount] = newValue;

                    node.Children[node.KeyCount] = newValueLeft;
                    node.Children[node.KeyCount + 1] = newValueRight;

                    node.KeyCount++;
                }
            }

            //if node keys exceed size
            //split and create a new node


        }

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