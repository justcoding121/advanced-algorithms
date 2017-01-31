using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsBinomialTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree { get; set; }

        internal AsArrayList<AsBinomialTreeNode<T>> Children { get; set; }

        public AsBinomialTreeNode(T value)
        {
            this.Value = value;
            Degree = 0;
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsBinomialTreeNode<T>).Value);
        }
    }

    public class AsBinomialMinHeap<T> where T : IComparable
    {
        private AsArrayList<AsBinomialTreeNode<T>> heapForest
            = new AsArrayList<AsBinomialTreeNode<T>>();

        public void Insert(T newItem)
        {
            var newNode = new AsBinomialTreeNode<T>(newItem);
            heapForest.AddItem(newNode);
            Meld();
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {
            //Sort in increasing order
            SortForest();

            AsSinglyLinkedListNode<AsBinaryTreeNode<T>> prev;
            var cur = heapForest.ItemAt(0);
            var next = heapForest.ItemAt(1);

            //TODO

        }

        /// <summary>
        /// Simple heap sort on Forest in increasing order
        /// </summary>
        private void SortForest()
        {
            //heapify
            var heap = new AsBMinHeap<AsBinomialTreeNode<T>>();
            for (int i = 0; i < heapForest.Length; i++)
            {
                heap.Insert(heapForest.ItemAt(i));
            }

            //now extract min until empty and return them as sorted array
            heapForest = new AsArrayList<AsBinomialTreeNode<T>>();
            while (heap.Count > 0)
            {
                heapForest.AddItem(heap.ExtractMin());
            }
        }


        public T ExtractMin()
        {
            var minTree = heapForest.ItemAt(0);
            var current = heapForest.ItemAt(0);

            //find minimum tree
            for (int i = 0; i < heapForest.Length; i++)
            {
                current = heapForest.ItemAt(i);

                if (minTree.Value.CompareTo(current.Value) > 0)
                {
                    minTree = current;
                }
            }

            //remove tree root
            heapForest.RemoveItem(minTree);
            
            //add removed roots children as new trees to forest
            for (int i = 0; i < minTree.Children.Length; i++)
            {
                heapForest.AddItem(minTree.Children.ItemAt(i));
            }

            //now union
            Meld();

            return minTree.Value;
        }

        public T PeekMin()
        {
            throw new NotImplementedException();
        }
    }
}
