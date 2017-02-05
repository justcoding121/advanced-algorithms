using System;
using System.Collections.Generic;
using System.Linq;

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

        internal int Count { get; private set; }
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

            Count++;

            return resultNode.Data;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest and returns the min Pointer
        /// </summary>
        private AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> Meld()
        {
            AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> minNode = null;

            if (heapForest.Head == null)
                return null;

            var hashTable = new AsHashSet<int, AsFibornacciTreeNode<T>>((int)Math.Log10(Count));

            var current = heapForest.Head;
            minNode = current;

            while (current != null)
            {
                //add to hash table
                if (!hashTable.ContainsKey(current.Data.Degree))
                {
                    var next = current.Next;

                    hashTable.Add(current.Data.Degree, current.Data);
                  
                    if (minNode == current)
                    {
                        minNode = null;
                    }

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

                    if (minNode == null
                        || minNode.Data.Value.CompareTo(current.Data.Value) > 0)
                    {
                        minNode = current;
                    }

                    hashTable.Remove(currentDegree);
                  
                }

            }

            //copy back trees with unique degrees
            if (hashTable.Count > 0)
            {
                foreach(var node in hashTable) 
                {
                    var newNode = heapForest.InsertLast(node.Value);

                    if (minNode == null
                        || minNode.Data.Value.CompareTo(newNode.Data.Value) > 0)
                    {
                        minNode = newNode;
                    }
                }

                hashTable.Clear();
            }

            return minNode;
        }

        /// <summary>
        /// O(log(n)) complexity
        /// </summary>
        /// <returns></returns>
        public T ExtractMin()
        {
            if (heapForest.Head == null)
                throw new Exception("Empty heap");

            var minNode = Meld();

            //remove tree root
            heapForest.Delete(minNode);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            //add removed roots children as new trees to forest
            for (int i = 0; i < minNode.Data.Children.Length; i++)
            {
                minNode.Data.Children[i].Parent = null;
                newHeapForest.InsertLast(minNode.Data.Children[i]);
            }

            MergeForests(newHeapForest);

            var minValue = minNode.Data.Value;
            minNode = null;

            return minValue;
        }

        /// <summary>
        /// Update the Heap with new value for this node pointer
        /// O(log(n)) complexity
        /// </summary>
        /// <param name="key"></param>
        public void DecrementKey(AsFibornacciTreeNode<T> node)
        {
            //need extra property to keep track or marked nodes
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unions this heap with another
        /// O(log(n)) complexity
        /// </summary>
        /// <param name="FibornacciHeap"></param>
        public void Union(AsFibornacciMinHeap<T> FibornacciHeap)
        {
            MergeForests(FibornacciHeap.heapForest);
            Count = Count + FibornacciHeap.Count;
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
                return heapForest.Head != null ? heapForest.Head : null;
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

            var minNode = Meld();

            return minNode.Data.Value;
        }
    }
}
