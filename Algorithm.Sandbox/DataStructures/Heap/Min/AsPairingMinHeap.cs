using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsPairingTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree;

        internal AsDoublyLinkedList<AsPairingTreeNode<T>> Children { get; set; }

        public AsPairingTreeNode(T value)
        {
            this.Value = value;
            Children = new AsDoublyLinkedList<AsPairingTreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsPairingTreeNode<T>).Value);
        }
    }

    public class AsPairingMinHeap<T> where T : IComparable
    {
        internal AsPairingTreeNode<T> Root;

        internal int Count { get; private set; }

        public AsPairingTreeNode<T> Insert(T newItem)
        {
            var newNode = new AsPairingTreeNode<T>(newItem);

            throw new NotImplementedException();

        }
        private void Meld()
        {
            throw new NotImplementedException();
        }

        public T ExtractMin()
        {

            throw new NotImplementedException();
        }


        public void DecrementKey(AsPairingTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        public void Union(AsPairingMinHeap<T> PairingHeap)
        {
            throw new NotImplementedException();
        }


        private void MergeForests(AsDoublyLinkedList<AsPairingTreeNode<T>> newHeapForest)
        {

            throw new NotImplementedException();

        }

        public T PeekMin()
        {
            throw new NotImplementedException();
        }
    }


}
