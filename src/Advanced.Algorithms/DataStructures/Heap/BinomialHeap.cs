using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A binomial minMax heap implementation.
    /// </summary>
    public class BinomialHeap<T> : IEnumerable<T> where T : IComparable
    {
        private readonly bool isMaxHeap;
        private readonly IComparer<T> comparer;

        private DoublyLinkedList<BinomialHeapNode<T>> heapForest
            = new DoublyLinkedList<BinomialHeapNode<T>>();

        private Dictionary<T, List<BinomialHeapNode<T>>> heapMapping
            = new Dictionary<T, List<BinomialHeapNode<T>>>();

        public int Count { get; private set; }

        public BinomialHeap(SortDirection sortDirection = SortDirection.Ascending)
        {
            this.isMaxHeap = sortDirection == SortDirection.Descending;
            comparer = new CustomComparer<T>(sortDirection, Comparer<T>.Default);
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public void Insert(T newItem)
        {
            var newNode = new BinomialHeapNode<T>(newItem);

            var newHeapForest = new DoublyLinkedList<BinomialHeapNode<T>>();
            newHeapForest.InsertFirst(newNode);

            //updated pointer
            mergeSortedForests(newHeapForest);

            meld();

            addMapping(newItem, newNode);

            Count++;
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T Extract()
        {
            if (heapForest.Head == null)
            {
                throw new Exception("Empty heap");
            }

            var minMaxTree = heapForest.Head;
            var current = heapForest.Head;

            //find minMaximum tree
            while (current.Next != null)
            {
                current = current.Next;

                if (comparer.Compare(minMaxTree.Data.Value, current.Data.Value) > 0)
                {
                    minMaxTree = current;
                }
            }

            //remove tree root
            heapForest.Delete(minMaxTree);

            var newHeapForest = new DoublyLinkedList<BinomialHeapNode<T>>();
            //add removed roots children as new trees to forest
            foreach (var child in minMaxTree.Data.Children)
            {
                child.Parent = null;
                newHeapForest.InsertLast(child);
            }

            mergeSortedForests(newHeapForest);

            meld();

            removeMapping(minMaxTree.Data.Value, minMaxTree.Data);

            Count--;

            return minMaxTree.Data.Value;
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="currentValue">The value to update.</param>
        /// <param name="newValue">The updated new value.</param>
        public void UpdateKey(T currentValue, T newValue)
        {
            var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

            if (node == null)
            {
                throw new Exception("Current value is not present in this heap.");
            }

            if (comparer.Compare(newValue, node.Value) > 0)
            {
                throw new Exception($"New value is not {(!isMaxHeap ? "less" : "greater")} than old value.");
            }

            updateNodeValue(currentValue, newValue, node);

            var current = node;

            while (current.Parent != null
                && comparer.Compare(current.Value, current.Parent.Value) < 0)
            {
                //swap parent with child
                var tmp = current.Value;
                updateNodeValue(tmp, current.Parent.Value, current);
                updateNodeValue(current.Parent.Value, tmp, current.Parent);

                current = current.Parent;
            }
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        /// <param name="binomialHeap">The heap to union with.</param>
        public void Merge(BinomialHeap<T> binomialHeap)
        {
            mergeSortedForests(binomialHeap.heapForest);

            meld();

            Count += binomialHeap.Count;
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T Peek()
        {
            if (heapForest.Head == null)
            {
                throw new Exception("Empty heap");
            }

            var minMaxTree = heapForest.Head;
            var current = heapForest.Head;

            //find  tree
            while (current.Next != null)
            {
                current = current.Next;

                if (comparer.Compare(minMaxTree.Data.Value, current.Data.Value) > 0)
                {
                    minMaxTree = current;
                }
            }

            return minMaxTree.Data.Value;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void meld()
        {
            if (heapForest.Head == null)
            {
                return;
            }


            var cur = heapForest.Head;
            var next = heapForest.Head.Next;

            while (next != null)
            {
                //case 1
                //degrees are differant 
                //we are good to move ahead
                if (cur.Data.Degree != next.Data.Degree)
                {
                    cur = next;
                    next = cur.Next;
                }
                //degress of cur and next are same
                else
                {
                    //case 2 next degree equals next-next degree
                    if (next.Next != null &&
                        cur.Data.Degree == next.Next.Data.Degree)
                    {
                        cur = next;
                        next = cur.Next;
                        continue;
                    }

                    //case 3 cur value is less than next
                    if (comparer.Compare(cur.Data.Value, next.Data.Value) <= 0)
                    {
                        //add next as child of current
                        cur.Data.Children.Add(next.Data);
                        next.Data.Parent = cur.Data;
                        heapForest.Delete(next);

                        next = cur.Next;
                        continue;
                    }

                    //case 4 cur value is greater than next
                    if (comparer.Compare(cur.Data.Value, next.Data.Value) > 0)
                    {
                        //add current as child of next
                        next.Data.Children.Add(cur.Data);
                        cur.Data.Parent = next.Data;

                        heapForest.Delete(cur);

                        cur = next;
                        next = cur.Next;
                    }

                }

            }
        }

        /// <summary>
        /// Merges the given sorted forest to current sorted Forest 
        /// and returns the last inserted node (pointer required for update-key)
        /// </summary>
        private void mergeSortedForests(DoublyLinkedList<BinomialHeapNode<T>> newHeapForest)
        {
            var @new = newHeapForest.Head;

            if (heapForest.Head == null)
            {
                heapForest = newHeapForest;
                return;
            }

            var current = heapForest.Head;

            //insert at right spot and move forward
            while (@new != null && current != null)
            {
                if (current.Data.Degree < @new.Data.Degree)
                {
                    current = current.Next;
                }
                else if (current.Data.Degree > @new.Data.Degree)
                {
                    heapForest.InsertBefore(current, new DoublyLinkedListNode<BinomialHeapNode<T>>(@new.Data));
                    @new = @new.Next;
                }
                else
                {
                    //equal
                    heapForest.InsertAfter(current, new DoublyLinkedListNode<BinomialHeapNode<T>>(@new.Data));
                    current = current.Next;
                    @new = @new.Next;
                }

            }

            //copy left overs
            while (@new != null)
            {
                heapForest.InsertAfter(heapForest.Tail, new DoublyLinkedListNode<BinomialHeapNode<T>>(@new.Data));
                @new = @new.Next;
            }

        }

        private void addMapping(T newItem, BinomialHeapNode<T> newNode)
        {
            if (heapMapping.ContainsKey(newItem))
            {
                heapMapping[newItem].Add(newNode);
            }
            else
            {
                heapMapping[newItem] = new List<BinomialHeapNode<T>>(new[] { newNode });
            }
        }

        private void updateNodeValue(T currentValue, T newValue, BinomialHeapNode<T> node)
        {
            removeMapping(currentValue, node);
            node.Value = newValue;
            addMapping(newValue, node);
        }

        private void removeMapping(T currentValue, BinomialHeapNode<T> node)
        {
            heapMapping[currentValue].Remove(node);
            if (heapMapping[currentValue].Count == 0)
            {
                heapMapping.Remove(currentValue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return heapMapping.SelectMany(x => x.Value).Select(x => x.Value).GetEnumerator();
        }
    }
}
