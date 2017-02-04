using System;
using System.Collections.Generic;

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

        internal AsDoublyLinkedList<AsFibornacciTreeNode<T>> heapForest
            = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();

        internal AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> minNode;

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
            var resultNode = MergeForests(newHeapForest);

            if(minNode == null)
            {
                minNode = resultNode;
            }
            else if (minNode.Data.Value.CompareTo(resultNode.Data.Value) < 0)
            {
                minNode = resultNode;
            }

            return resultNode.Data;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {
            if (heapForest.Head == null)
                return;

            var hashTable = new Dictionary<int, AsFibornacciTreeNode<T>>();

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
                    var existing = hashTable[currentDegree];

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

                    if (minNode.Data.Value.CompareTo(current.Data.Value) < 0)
                    {
                        minNode = current;
                    }

                    hashTable.Remove(currentDegree);

                }

            }



            //copy back trees with unique degrees
            if (hashTable.Count > 0)
            {
                //var allNodes = hashTable.GetAll();

                foreach (var item in hashTable)
                {
                    var newNode = heapForest.InsertLast(item.Value);

                    if (minNode.Data.Value.CompareTo(newNode.Data.Value) < 0)
                    {
                        minNode = newNode;
                    }
                }

                hashTable.Clear();
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

            //remove tree root
            heapForest.Delete(minNode);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            //add removed roots children as new trees to forest
            for (int i = 0; i < minNode.Data.Children.Length; i++)
            {
                minNode.Data.Children.ItemAt(i).Parent = null;
                newHeapForest.InsertLast(minNode.Data.Children.ItemAt(i));
            }

            MergeForests(newHeapForest);

            return minNode.Data.Value;
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
        private AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> MergeForests(AsDoublyLinkedList<AsFibornacciTreeNode<T>> newHeapForest)
        {
            AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> lastInserted = null;

            var @new = newHeapForest.Head;

            if (heapForest.Head == null)
            {
                heapForest = newHeapForest;
                return heapForest.Head != null ? heapForest.Head: null;
            }
            //copy 
            while (@new != null)
            {
                lastInserted = heapForest.InsertAfter(heapForest.Head, new AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>(@new.Data));
                @new = @new.Next;
            }

            return lastInserted == null ? null : lastInserted;
        }

        /// <summary>
        /// O(log(n)) complexity
        /// </summary>
        /// <returns></returns>
        public T PeekMin()
        {
            if (heapForest.Head == null)
                throw new Exception("Empty heap");

            return minNode.Data.Value;
        }
    }
}
