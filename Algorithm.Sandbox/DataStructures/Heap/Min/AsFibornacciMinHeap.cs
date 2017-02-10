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

        //keep track of node reference for tree root items in forest
        //This is so that we can directly set new min node during decrement key without iterating roots
        private Dictionary<AsFibornacciTreeNode<T>, AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>> rootIndex
          = new Dictionary<AsFibornacciTreeNode<T>, AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>>();

        //keep track of node reference for Circular List Node for each of our Fibornacci Node in tree (excluding roots)
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
            var newDllNode = newHeapForest.InsertFirst(newNode);

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
                    rootIndex.Remove(current.Data);

                    current = next;
                    continue;
                }
                //insert back to forest by merging current tree 
                //with existing tree in merge dictionary
                else
                {
                    var currentDegree = current.Data.Degree;
                    var existing = mergeDictionary[currentDegree];

                    AsCircularLinkedListNode<AsFibornacciTreeNode<T>> newCLNode;
                    if (existing.Data.Value.CompareTo(current.Data.Value) < 0)
                    {
                        current.Data.Parent = existing.Data;
                        newCLNode = existing.Data.Children.Insert(current.Data);
                        existing.Data.Degree++;

                        //add to index for retrieving the Circular List Node during Decrement Key Operation
                        childrenIndex.Add(newCLNode.Data, newCLNode);

                        var newNode = heapForest.InsertBefore(current, existing);
                        rootIndex.Add(newNode.Data, newNode);

                        heapForest.Delete(current);
                        rootIndex.Remove(current.Data);

                        current = newNode;

                    }
                    else
                    {
                        existing.Data.Parent = current.Data;
                        newCLNode = current.Data.Children.Insert(existing.Data);
                        current.Data.Degree++;

                        //add to index for retrieving the Circular List Node during Decrement Key Operation
                        childrenIndex.Add(newCLNode.Data, newCLNode);

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
                    var newNode = heapForest.InsertLast(node.Value.Data);
                    rootIndex.Add(newNode.Data, newNode);

                    if (minNode == null
                        || minNode.Data.Value.CompareTo(newNode.Data.Value) > 0)
                    {
                        minNode = newNode;
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
            rootIndex.Remove(minNode.Data);

            var newHeapForest = new AsDoublyLinkedList<AsFibornacciTreeNode<T>>();
            //add removed roots children as new trees to forest
            foreach (var child in minNode.Data.Children)
            {
                child.Parent = null;

                var newNode = newHeapForest.InsertLast(child);
                rootIndex.Add(newNode.Data, newNode);

                childrenIndex.Remove(child);

            }

            MergeForests(newHeapForest);
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
                minNode = rootIndex[node];
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

            //cut child and attach to heap Forest
            //and mark parent for lost child
            var newNode = heapForest.InsertFirst(node);

            rootIndex.Add(newNode.Data, newNode);

            //update min
            if (minNode.Data.Value.CompareTo(newNode.Data.Value) > 0)
            {
                minNode = rootIndex[newNode.Data];
            }

            var currentNode = childrenIndex[node];
            node.Parent.Children.Delete(currentNode);
            node.Parent.Degree--;
            childrenIndex.Remove(node);

            if (parent.Parent != null)
            {
                parent.LostChild = true;
            }
            node.LostChild = false;
            node.Parent = null;
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

                var current = heapForest.Head;
                while (current != null)
                {
                    if (rootIndex.ContainsKey(current.Data))
                    {
                        rootIndex[current.Data] = current;
                    }
                    else
                    {
                        rootIndex.Add(current.Data, current);
                    }

                    current = current.Next;
                }

                return heapForest.Head != null ? heapForest.Head : null;
            }
            //copy 
            while (@new != null)
            {
                lastInserted = heapForest.InsertAfter(heapForest.Head, new AsDoublyLinkedListNode<AsFibornacciTreeNode<T>>(@new.Data));

                if (rootIndex.ContainsKey(lastInserted.Data))
                {
                    rootIndex[lastInserted.Data] = lastInserted;
                }
                else
                {
                    rootIndex.Add(lastInserted.Data, lastInserted);
                }

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
