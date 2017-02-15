using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsFibornacciTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }
        internal int Degree;

        internal AsFibornacciTreeNode<T> Parent { get; set; }
        internal AsDoublyLinkedList<AsFibornacciTreeNode<T>> Children { get; set; }
        public bool LostChild { get; internal set; }

        public AsFibornacciTreeNode(T value)
        {
            this.Value = value;

            Children = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsFibornacciTreeNode<T>).Value);
        }
    }

    public class AsFibornacciMinHeap<T> where T : IComparable
    {

        //TODO use a Doubly linked list so that when childrens become orphan
        //we can do a union of Children with Forest in constant time O(1) during ExtractMin
        //instead of creating new DoublyLinked List Nodes for each orphaned child
        internal AsDoublyLinkedList<AsFibornacciTreeNode<T>> heapForest
            = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();

        //holds the minimum node at any given time
        private AsDoublyLinkedListNode<AsFibornacciTreeNode<T>> minNode = null;

        //keep track of node reference for tree root items in forest
        //This is so that we can directly set new min node during decrement key without iterating roots
        private Dictionary<AsFibornacciTreeNode<T>, AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>> heapIndex
          = new Dictionary<AsFibornacciTreeNode<T>, AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>>();


        internal int Count { get; private set; }

        /// <summary>
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="newItem"></param>
        public AsFibornacciTreeNode<T> Insert(T newItem)
        {
            var newNode = new AsFibornacciTreeNode<T>(newItem);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            var newDllNode = newHeapForest.InsertFirst(newNode);

            //return pointer to new Node
            MergeForests(newHeapForest);

            if (minNode == null)
            {
                minNode = newDllNode;
            }
            else
            {
                if (minNode.Data.Value.CompareTo(newDllNode.Data.Value) > 0)
                {
                    minNode = newDllNode;
                }
            }

            Count++;

            heapIndex.Add(newDllNode.Data, newDllNode);

            return newDllNode.Data;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {

            if (heapForest.Head == null)
            {
                minNode = null;
                return;
            }

            //degree - node dictionary
            var mergeDictionary = new Dictionary<int, AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>>();

            var current = heapForest.Head;
            minNode = current;
            while (current != null)
            {
                current.Data.Parent = null;

                //no same degree already in merge dictionary
                //add to hash table
                if (!mergeDictionary.ContainsKey(current.Data.Degree))
                {
                    var next = current.Next;

                    mergeDictionary.Add(current.Data.Degree, current);

                    if (minNode == current)
                    {
                        minNode = null;
                    }

                    heapForest.Delete(current);

                    current = next;
                    continue;
                }
                //insert back to forest by merging current tree 
                //with existing tree in merge dictionary
                else
                {
                    var currentDegree = current.Data.Degree;
                    var existing = mergeDictionary[currentDegree];

                    if (existing.Data.Value.CompareTo(current.Data.Value) < 0)
                    {
                        current.Data.Parent = existing.Data;
                        heapForest.Delete(current);
                        existing.Data.Children.InsertFirst(current);
                        existing.Data.Degree++;

                        heapForest.InsertFirst(existing);

                        current = existing;

                    }
                    else
                    {
                        existing.Data.Parent = current.Data;
                        current.Data.Children.InsertFirst(existing);
                        current.Data.Degree++;

                    }


                    if (minNode == null
                        || minNode.Data.Value.CompareTo(current.Data.Value) > 0)
                    {
                        minNode = current;
                    }

                    mergeDictionary.Remove(currentDegree);

                }

            }

            //insert back trees with unique degrees to forest
            if (mergeDictionary.Count > 0)
            {
                foreach (var node in mergeDictionary)
                {
                  
                    heapForest.InsertFirst(node.Value);

                    if (minNode == null
                        || minNode.Data.Value.CompareTo(node.Value.Data.Value) > 0)
                    {
                        minNode = node.Value;
                    }
                }

                mergeDictionary.Clear();
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
            heapIndex.Remove(minNode.Data);

            MergeForests(minNode.Data.Children);
            Meld();

            Count--;

            return minValue;
        }

        /// <summary>
        /// Update the Heap with new value for this node pointer
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="key"></param>
        public void DecrementKey(AsFibornacciTreeNode<T> node)
        {

            if (node.Parent == null
                && minNode.Data.Value.CompareTo(node.Value) > 0)
            {
                minNode = heapIndex[node];
            }

            var current = node;

            if (current.Parent != null
                && current.Value.CompareTo(current.Parent.Value) < 0)
            {

                var parent = current.Parent;

                //if parent already lost one child
                //then cut current and parent
                if (parent.LostChild)
                {
                    parent.LostChild = false;

                    var grandParent = parent.Parent;

                    //mark grand parent
                    if (grandParent != null)
                    {
                        Cut(parent);
                        Cut(current);
                    }
                }
                else
                {
                    Cut(current);
                }
            }

        }
        /// <summary>
        /// Delete this node from Heap Tree and adds it to forest as a new tree 
        /// </summary>
        /// <param name="node"></param>
        private void Cut(AsFibornacciTreeNode<T> node)
        {
            var parent = node.Parent;

            var currentNode = heapIndex[node];

            //cut child and attach to heap Forest
            //and mark parent for lost child
            node.Parent.Children.Delete(currentNode);
            node.Parent.Degree--;
            if (parent.Parent != null)
            {
                parent.LostChild = true;
            }
            node.LostChild = false;
            node.Parent = null;

            heapForest.InsertFirst(currentNode);

            //update min
            if (minNode.Data.Value.CompareTo(currentNode.Data.Value) > 0)
            {
                minNode = heapIndex[currentNode.Data];
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
        private void MergeForests(AsDoublyLinkedList<AsFibornacciTreeNode<T>> newHeapForest)
        {

            var @new = newHeapForest.Head;

            if (heapForest.Head == null)
            {
                heapForest = newHeapForest;
                return;
            }

            heapForest.Union(newHeapForest);

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
