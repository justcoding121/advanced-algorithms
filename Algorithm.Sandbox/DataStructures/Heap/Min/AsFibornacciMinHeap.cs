using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsFibornacciTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree;

        internal AsFibornacciTreeNode<T> Parent { get; set; }

        //TODO use a circular linked list to improve performance
        internal AsCircularLinkedList<AsFibornacciTreeNode<T>> Children { get; set; }
        public bool LostChild { get; internal set; }

        public AsFibornacciTreeNode(T value)
        {
            this.Value = value;

            Children = new AsCircularLinkedList<AsFibornacciTreeNode<T>>();
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

        //holds the minimum node at any given time
        private AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> minNode = null;


        //keep track of node reference for Circular List Node for each of our Fibornacci Node
        //This is so that we don't need to search whole heap to find the node during decrement key operation
        private Dictionary<AsFibornacciTreeNode<T>, AsCircularLinkedListNode<AsFibornacciTreeNode<T>>> childrenIndex
            = new Dictionary<AsFibornacciTreeNode<T>, AsCircularLinkedListNode<AsFibornacciTreeNode<T>>>();

        internal int Count { get; private set; }

        /// <summary>
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="newItem"></param>
        public AsFibornacciTreeNode<T> Insert(T newItem)
        {
            var newNode = new AsFibornacciTreeNode<T>(newItem);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            newHeapForest.InsertFirst(newNode);

            //return pointer to new Node
            var resultNode = MergeForests(newHeapForest);

            if (minNode == null)
            {
                minNode = resultNode;
            }
            else
            {
                if (minNode.Data.Value.CompareTo(resultNode.Data.Value) > 0)
                {
                    minNode = resultNode;
                }
            }

            Count++;

            return resultNode.Data;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {

            if (heapForest.Head == null)
                return;

            var hashTable = new Dictionary<int, AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>>();

            var current = heapForest.Head;
            minNode = current;

            while (current != null)
            {
                //add to hash table
                if (!hashTable.ContainsKey(current.Data.Degree))
                {
                    var next = current.Next;

                    hashTable.Add(current.Data.Degree, current);

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

                    AsCircularLinkedListNode<AsFibornacciTreeNode<T>> newCLNode;
                    if (existing.Data.Value.CompareTo(current.Data.Value) < 0)
                    {
                        current.Data.Parent = existing.Data;
                        newCLNode = existing.Data.Children.Insert(current.Data);


                        //add to index for retrieving the Circular List Node during Decrement Key Operation
                        if (childrenIndex.ContainsKey(current.Data))
                        {
                            childrenIndex[current.Data] = newCLNode;
                        }
                        else
                        {
                            childrenIndex.Add(current.Data, newCLNode);
                        }

                        existing.Data.Degree++;

                        var newNode = heapForest.InsertBefore(current, existing);


                        heapForest.Delete(current);

                        current = newNode;

                    }
                    else
                    {
                        existing.Data.Parent = current.Data;
                        newCLNode = current.Data.Children.Insert(existing.Data);

                        //add to index for retrieving the Circular List Node during Decrement Key Operation
                        if (childrenIndex.ContainsKey(existing.Data))
                        {
                            childrenIndex[existing.Data] = newCLNode;
                        }
                        else
                        {
                            childrenIndex.Add(existing.Data, newCLNode);
                        }

                        current.Data.Degree++;
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
                foreach (var node in hashTable)
                {
                    var newNode = heapForest.InsertLast(node.Value.Data);

                    if (minNode == null
                        || minNode.Data.Value.CompareTo(newNode.Data.Value) > 0)
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

            var minValue = minNode.Data.Value;

            //remove tree root
            heapForest.Delete(minNode);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            //add removed roots children as new trees to forest
            foreach (var child in minNode.Data.Children)
            {
                child.Parent = null;
                newHeapForest.InsertLast(child);
            }

            MergeForests(newHeapForest);
            Meld();


            return minValue;
        }

        /// <summary>
        /// Update the Heap with new value for this node pointer
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="key"></param>
        public void DecrementKey(AsFibornacciTreeNode<T> node)
        {
            var current = node;

            if (current.Parent != null
                && current.Value.CompareTo(current.Parent.Value) < 0)
            {

                var parent = current.Parent;
                if (parent.LostChild)
                {
                    parent.LostChild = false;

                    var grandParent = parent.Parent;
                    if (grandParent != null)
                    {

                        grandParent.LostChild = true;

                        heapForest.InsertFirst(parent);

                        var parentNode = childrenIndex[parent];
                        grandParent.Children.Delete(parentNode);

                        if (childrenIndex.ContainsKey(parent))
                        {
                            childrenIndex.Remove(parent);
                        }

                        grandParent.LostChild = true;
                        parent.Parent = null;

                    }
                }
                else
                {
                    heapForest.InsertFirst(current);

                    var currentNode = childrenIndex[current];
                    current.Parent.Children.Delete(currentNode);

                    if (childrenIndex.ContainsKey(current))
                    {
                        childrenIndex.Remove(current);
                    }

                    current.Parent.LostChild = true;
                    current.Parent = null;
                }

            }


        }

        /// <summary>
        /// Unions this heap with another
        /// O(k) complexity where K is the FibornacciHeap Forest Length 
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
        ///  O(1) complexity 
        /// <returns></returns>
        public T PeekMin()
        {
            if (heapForest.Head == null)
                throw new Exception("Empty heap");

            return minNode.Data.Value;
        }
    }


}
