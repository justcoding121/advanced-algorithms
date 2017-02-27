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
            Keys = new T[maxKeysPerNode + 1];
            Children = new AsBTreeNode<T>[maxKeysPerNode + 2];

        }

        internal int GetMedianIndex()
        {
            return (KeyCount / 2);
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

                //else move next
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
            }

            var inserted = false;

            //insert in sorted order
            for (int i = 0; i < node.KeyCount; i++)
            {
                if (newValue.CompareTo(node.Keys[i]) < 0)
                {
                    InsertAt(node.Keys, i, newValue);
                    node.KeyCount++;

                    //Insert children if any
                    InsertAt(node.Children, i, newValueLeft);
                    InsertAt(node.Children, i + 1, newValueRight);

                    inserted = true;
                    break;
                }
            }

            //element should be inserted at the end then
            if(!inserted)
            {
                node.Keys[node.KeyCount] = newValue;
                node.Children[node.KeyCount] = newValueLeft;
                node.Children[node.KeyCount + 1] = newValueRight;
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