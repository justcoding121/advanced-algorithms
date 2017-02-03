using Algorithm.Sandbox.Sorting;
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
        internal AsArrayList<AsBinomialTreeNode<T>> heapForest
            = new AsArrayList<AsBinomialTreeNode<T>>();

        public void Insert(T newItem)
        {
            var newNode = new AsBinomialTreeNode<T>(newItem);

            var newHeapForest = new AsArrayList<AsBinomialTreeNode<T>>();
            newHeapForest.AddItem(newNode);

            MergeSortForest(newHeapForest);

            Meld();
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {
            if (heapForest.Length == 0)
                return;

            int i = 0;
            AsBinomialTreeNode<T> cur = heapForest.ItemAt(i);
            AsBinomialTreeNode<T> next = heapForest.Length > 1 ? heapForest.ItemAt(i + 1) : null;

            //TODO
            while (next != null)
            {
                //case 1
                //degrees are differant 
                //we are good to move ahead
                if (cur.Degree != next.Degree)
                {
                    i++;

                    cur = heapForest.ItemAt(i);

                    if (heapForest.Length > i + 1)
                    {
                        next = heapForest.ItemAt(i + 1);
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
                    if (heapForest.Length > i + 2 &&
                        cur.Degree == heapForest.ItemAt(i + 2).Degree)
                    {
                        i++;
                        cur = heapForest.ItemAt(i);
                        next = heapForest.ItemAt(i + 1);
                        continue;
                    }

                    //case 3 cur value is less than next
                    if (cur.Value.CompareTo(next.Value) < 0)
                    {
                        //add next as child of current
                        cur.Children.AddItem(next);
                        heapForest.RemoveItem(next);

                        if (heapForest.Length > i + 1)
                        {
                            next = heapForest.ItemAt(i + 1);
                        }
                        else
                        {
                            next = null;
                        }

                        continue;
                    }

                    //case 4 cur value is greater than next
                    if (cur.Value.CompareTo(next.Value) > 0)
                    {
                        //add current as child of next
                        next.Children.AddItem(cur);
                        heapForest.RemoveItem(cur);

                        cur = next;

                        if (heapForest.Length > i + 1)
                        {
                            next = heapForest.ItemAt(i + 1);
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
            if (heapForest.Length == 0)
                throw new Exception("Empty heap");

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

            var newHeapForest = new AsArrayList<AsBinomialTreeNode<T>>();
            //add removed roots children as new trees to forest
            for (int i = 0; i < minTree.Children.Length; i++)
            {
                newHeapForest.AddItem(minTree.Children.ItemAt(i));
            }

            MergeSortForest(newHeapForest);

            Meld();

            return minTree.Value;
        }

        /// <summary>
        /// Merges the given sorted forest to current sorted Forest 
        /// </summary>
        /// <param name="newHeapForest"></param>
        private void MergeSortForest(AsArrayList<AsBinomialTreeNode<T>> newHeapForest)
        {
            
            var mergedForest = new AsArrayList<AsBinomialTreeNode<T>>();

            //pick smallest and move to new merged Array
            while (heapForest.Length > 0 && newHeapForest.Length > 0)
            {
                if (heapForest.ItemAt(0).Degree < newHeapForest.ItemAt(0).Degree)
                {
                    mergedForest.AddItem(heapForest.ItemAt(0));
                    heapForest.RemoveItem(0);
                }
                else
                {
                    mergedForest.AddItem(newHeapForest.ItemAt(0));
                    newHeapForest.RemoveItem(0);
                }

            }

            //copy left overs
            while (heapForest.Length > 0)
            {
                mergedForest.AddItem(heapForest.ItemAt(0));
                heapForest.RemoveItem(0);
            }

            while (newHeapForest.Length > 0)
            {
                mergedForest.AddItem(newHeapForest.ItemAt(0));
                newHeapForest.RemoveItem(0);
            }

            heapForest = mergedForest;
        }

        public T PeekMin()
        {
            if (heapForest.Length == 0)
                throw new Exception("Empty heap");

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

            return minTree.Value;
        }
    }
}
