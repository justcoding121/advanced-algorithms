using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsFibornacciTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree => Children.Length;

        internal AsFibornacciTreeNode<T> Parent { get; set; }
        internal AsArrayList<AsFibornacciTreeNode<T>> Children { get; set; }

        public AsFibornacciTreeNode(T value)
        {
            this.Value = value;

            Children = new AsArrayList<AsFibornacciTreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsFibornacciTreeNode<T>).Value);
        }
    }

    public class AsFibornacciMinHeap<T> where T : IComparable
    {
        //TODO use a linked list in future to improve memory complexity
        internal AsDoublyLinkedList<AsFibornacciTreeNode<T>> heapForest
            = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();

        /// <summary>
        /// O(log(n)) complexity
        /// </summary>
        /// <param name="newItem"></param>
        public AsFibornacciTreeNode<T> Insert(T newItem)
        {
            var newNode = new AsFibornacciTreeNode<T>(newItem);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            newHeapForest.InsertFirst(newNode);

            //return pointer to new Node
            return MergeForests(newHeapForest);

        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {
            if (heapForest.Head == null)
                return;

            var hashTable = new AsTreeHashSet<int, AsFibornacciTreeNode<T>>();

            var current = heapForest.Head;

            while (current != null)
            {
                //add to hash table
                if (!hashTable.ContainsKey(current.Data.Degree))
                {
                    var next = current.Next;

                    hashTable.Add(current.Data.Degree, current.Data);
                    heapForest.Delete(current);

                    current = next;
                    continue;
                }
                else
                {
                    var currentDegree = current.Data.Degree;
                    var existing = hashTable.GetValue(currentDegree);

                    if (existing.Value.CompareTo(current.Data.Value) < 0)
                    {
                        existing.Children.AddItem(current.Data);

                        var newNode = heapForest.InsertBefore(current,
                            new AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>
                            (existing));

                        heapForest.Delete(current);

                        current = newNode;
                    }
                    else
                    {
                        current.Data.Children.AddItem(existing);
                    }

                    hashTable.Remove(currentDegree);

                }

            }

            //copy back trees with unique degrees
            if (hashTable.Count > 0)
            {
                var allNodes = hashTable.GetAll();

                for (int i = 0; i < allNodes.Length; i++)
                {
                    heapForest.InsertLast(allNodes.ItemAt(i).Value);
                }

                allNodes.Clear();
            }
        }

        /// <summary>
        /// O(log(n)) complexity
        /// </summary>
        /// <returns></returns>
        public T ExtractMin()
        {
            if (heapForest.Head == null)
                throw new Exception("Empty heap");

            Meld();

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

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            //add removed roots children as new trees to forest
            for (int i = 0; i < minTree.Data.Children.Length; i++)
            {
                minTree.Data.Children.ItemAt(i).Parent = null;
                newHeapForest.InsertLast(minTree.Data.Children.ItemAt(i));
            }

            MergeForests(newHeapForest);

            return minTree.Data.Value;
        }

        /// <summary>
        /// Update the Heap with new value for this node pointer
        /// O(log(n)) complexity
        /// </summary>
        /// <param name="key"></param>
        public void DecrementKey(AsFibornacciTreeNode<T> node)
        {
            var current = node;

            while (current.Parent != null
                && current.Value.CompareTo(current.Parent.Value) < 0)
            {
                var tmp = current.Value;
                current.Value = current.Parent.Value;
                current.Parent.Value = tmp;

                current = current.Parent;
            }
        }

        /// <summary>
        /// Unions this heap with another
        /// O(log(n)) complexity
        /// </summary>
        /// <param name="FibornacciHeap"></param>
        public void Union(AsFibornacciMinHeap<T> FibornacciHeap)
        {
            MergeForests(FibornacciHeap.heapForest);

        }
        /// <summary>
        /// Merges the given forest to current Forest 
        /// returns the last inserted node (pointer required for decrement-key)
        /// </summary>
        /// <param name="newHeapForest"></param>
        private AsFibornacciTreeNode<T> MergeForests(AsDoublyLinkedList<AsFibornacciTreeNode<T>> newHeapForest)
        {
            AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> lastInserted = null;

            var @new = newHeapForest.Head;

            if (heapForest.Head == null)
            {
                heapForest = newHeapForest;
                return heapForest.Tail != null ? heapForest.Tail.Data : null;
            }
            //copy 
            while (@new != null)
            {
                lastInserted = heapForest.InsertAfter(heapForest.Tail, new AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>(@new.Data));
                @new = @new.Next;
            }

            return lastInserted == null ? null : lastInserted.Data;
        }

        /// <summary>
        /// O(log(n)) complexity
        /// </summary>
        /// <returns></returns>
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
