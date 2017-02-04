using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsBinomialTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree => Children.Length;

        internal AsArrayList<AsBinomialTreeNode<T>> Children { get; set; }

        public AsBinomialTreeNode(T value)
        {
            this.Value = value;
            Children = new AsArrayList<AsBinomialTreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsBinomialTreeNode<T>).Value);
        }
    }

    public class AsBinomialMinHeap<T> where T : IComparable
    {
        //TODO use a linked list in future to improve memory complexity
        internal AsDoublyLinkedList<AsBinomialTreeNode<T>> heapForest
            = new AsDoublyLinkedList<AsBinomialTreeNode<T>>();

        public void Insert(T newItem)
        {
            var newNode = new AsBinomialTreeNode<T>(newItem);

            var newHeapForest = new AsDoublyLinkedList<AsBinomialTreeNode<T>>();
            newHeapForest.InsertFirst(newNode);

            MergeSortedForests(newHeapForest);

            Meld();
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {
            if (heapForest.Head == null)
                return;

            int i = 0;
            var cur = heapForest.Head;
            var next = heapForest.Head.Next != null ? heapForest.Head.Next : null;

            //TODO
            while (next != null)
            {
                //case 1
                //degrees are differant 
                //we are good to move ahead
                if (cur.Data.Degree != next.Data.Degree)
                {
                    i++;

                    cur = next;

                    if (cur.Next != null)
                    {
                        next = cur.Next;
                    }
                    else
                    {
                        next = null;
                    }

                    continue;
                }
                //degress of cur & next are same
                else
                {
                    //case 2 next degree equals next-next degree
                    if (next.Next != null &&
                        cur.Data.Degree == next.Next.Data.Degree)
                    {
                        i++;
                        cur = next;
                        next = cur.Next;
                        continue;
                    }

                    //case 3 cur value is less than next
                    if (cur.Data.Value.CompareTo(next.Data.Value) < 0)
                    {
                        //add next as child of current
                        cur.Data.Children.AddItem(next.Data);

                        heapForest.Delete(next);

                        if (cur.Next != null)
                        {
                            next = cur.Next;
                        }
                        else
                        {
                            next = null;
                        }

                        continue;
                    }

                    //case 4 cur value is greater than next
                    if (cur.Data.Value.CompareTo(next.Data.Value) > 0)
                    {
                        //add current as child of next
                        next.Data.Children.AddItem(cur.Data);

                        heapForest.Delete(cur);

                        cur = next;

                        if (cur.Next != null)
                        {
                            next = cur.Next;
                        }
                        else
                        {
                            next = null;
                        }

                        continue;
                    }

                }

            }
        }


        public T ExtractMin()
        {
            if (heapForest.Head == null)
                throw new Exception("Empty heap");

            var minTree = heapForest.Head;
            var current = heapForest.Head;

            //find minimum tree
            while (current.Next != null)
            {
                current = current.Next;

                if (minTree.Data.Value.CompareTo(current.Data.Value) > 0)
                {
                    minTree = current;
                }
            }

            //remove tree root
            heapForest.Delete(minTree);

            var newHeapForest = new AsDoublyLinkedList<AsBinomialTreeNode<T>>();
            //add removed roots children as new trees to forest
            for (int i = 0; i < minTree.Data.Children.Length; i++)
            {
                newHeapForest.InsertLast(minTree.Data.Children.ItemAt(i));
            }

            MergeSortedForests(newHeapForest);

            Meld();

            return minTree.Data.Value;
        }

        /// <summary>
        /// Merges the given sorted forest to current sorted Forest 
        /// </summary>
        /// <param name="newHeapForest"></param>
        private void MergeSortedForests(AsDoublyLinkedList<AsBinomialTreeNode<T>> newHeapForest)
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
                    heapForest.InsertBefore(current, new AsDoublyLinkedListNode<AsBinomialTreeNode<T>>(@new.Data));
                    @new = @new.Next;
                }
                else
                {
                    //equal
                    heapForest.InsertAfter(current, new AsDoublyLinkedListNode<AsBinomialTreeNode<T>>(@new.Data));
                    current = current.Next;
                    @new = @new.Next;
                }

            }

            //copy left overs
            while (@new != null)
            {
                heapForest.InsertAfter(heapForest.Tail, new AsDoublyLinkedListNode<AsBinomialTreeNode<T>>(@new.Data));
                @new = @new.Next;
            }


        }

        public T PeekMin()
        {
            if (heapForest.Head == null)
                throw new Exception("Empty heap");

            var minTree = heapForest.Head;
            var current = heapForest.Head;

            //find minimum tree
            while (current.Next != null)
            {
                current = current.Next;

                if (minTree.Data.Value.CompareTo(current.Data.Value) > 0)
                {
                    minTree = current;
                }
            }

            return minTree.Data.Value;
        }
    }
}
