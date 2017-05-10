using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsFibornacciTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal int Degree;
        internal AsFibornacciTreeNode<T> ChildrenHead { get; set; }

        internal AsFibornacciTreeNode<T> Parent { get; set; }
        internal bool LostChild { get; set; }

        internal AsFibornacciTreeNode<T> Previous;
        internal AsFibornacciTreeNode<T> Next;

        public AsFibornacciTreeNode(T value)
        {
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsFibornacciTreeNode<T>).Value);
        }
    }

    public class AsFibornacciMinHeap<T> where T : IComparable
    {
        internal AsFibornacciTreeNode<T> heapForestHead;

        //holds the minimum node at any given time
        private AsFibornacciTreeNode<T> minNode = null;

        public int Count { get; private set; }

        /// <summary>
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="newItem"></param>
        public AsFibornacciTreeNode<T> Insert(T newItem)
        {
            var newNode = new AsFibornacciTreeNode<T>(newItem);

            //return pointer to new Node
            MergeForests(newNode);

            if (minNode == null)
            {
                minNode = newNode;
            }
            else
            {
                if (minNode.Value.CompareTo(newNode.Value) > 0)
                {
                    minNode = newNode;
                }
            }

            Count++;

            return newNode;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {

            if (heapForestHead == null)
            {
                minNode = null;
                return;
            }

            //degree - node dictionary
            var mergeDictionary = new Dictionary<int, AsFibornacciTreeNode<T>>();

            var current = heapForestHead;
            minNode = current;
            while (current != null)
            {
                current.Parent = null;
                var next = current.Next;
                //no same degree already in merge dictionary
                //add to hash table
                if (!mergeDictionary.ContainsKey(current.Degree))
                {
                  

                    mergeDictionary.Add(current.Degree, current);

                    if (minNode == current)
                    {
                        minNode = null;
                    }

                    DeleteNode(ref heapForestHead, current);

                    current = next;
                    continue;
                }
                //insert back to forest by merging current tree 
                //with existing tree in merge dictionary
                else
                {
                    var currentDegree = current.Degree;
                    var existing = mergeDictionary[currentDegree];

                    if (existing.Value.CompareTo(current.Value) < 0)
                    {
                        current.Parent = existing;

                        DeleteNode(ref heapForestHead, current);

                        var childHead = existing.ChildrenHead;
                        InsertNode(ref childHead, current);
                        existing.ChildrenHead = childHead;

                        existing.Degree++;

                        InsertNode(ref heapForestHead, existing);
                        current = existing;
                        current.Next = next;

                    }
                    else
                    {
                        existing.Parent = current;

                        var childHead = current.ChildrenHead;
                        InsertNode(ref childHead, existing);
                        current.ChildrenHead = childHead;

                        current.Degree++;
                    }


                    if (minNode == null
                        || minNode.Value.CompareTo(current.Value) > 0)
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
                    InsertNode(ref heapForestHead, node.Value);

                    if (minNode == null
                        || minNode.Value.CompareTo(node.Value.Value) > 0)
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
            if (heapForestHead == null)
                throw new Exception("Empty heap");

            var minValue = minNode.Value;

            //remove tree root
            DeleteNode(ref heapForestHead, minNode);

            MergeForests(minNode.ChildrenHead);
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
                && minNode.Value.CompareTo(node.Value) > 0)
            {
                minNode = node;
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
            var childHead = node.Parent.ChildrenHead;
            DeleteNode(ref childHead, node);
            node.Parent.ChildrenHead = childHead;

            node.Parent.Degree--;
            if (parent.Parent != null)
            {
                parent.LostChild = true;
            }
            node.LostChild = false;
            node.Parent = null;

            InsertNode(ref heapForestHead, node);

            //update min
            if (minNode.Value.CompareTo(node.Value) > 0)
            {
                minNode = node;
            }

        }

        /// <summary>
        /// Unions this heap with another
        /// O(k) complexity where K is the FibornacciHeap Forest Length 
        /// </summary>
        /// <param name="FibornacciHeap"></param>
        public void Union(AsFibornacciMinHeap<T> FibornacciHeap)
        {
            MergeForests(FibornacciHeap.heapForestHead);
            Count = Count + FibornacciHeap.Count;
        }

        /// <summary>
        /// Merges the given fibornacci node list to current Forest 
        /// </summary>
        /// <param name="headPointer"></param>
        private void MergeForests(AsFibornacciTreeNode<T> headPointer)
        {
            var current = headPointer;
            while (current != null)
            {
                var next = current.Next;
                InsertNode(ref heapForestHead, current);
                current = next;
            }

        }

        private void InsertNode(ref AsFibornacciTreeNode<T> head, AsFibornacciTreeNode<T> newNode)
        {
            newNode.Next = newNode.Previous = null;

            if (head == null)
            {
                head = newNode;
                return;
            }

            head.Previous = newNode;
            newNode.Next = head;

            head = newNode;
        }

        private void DeleteNode(ref AsFibornacciTreeNode<T> heapForestHead, AsFibornacciTreeNode<T> deletionNode)
        {
            if (deletionNode == heapForestHead)
            {
                if (deletionNode.Next != null)
                {
                    deletionNode.Next.Previous = null;
                }

                heapForestHead = deletionNode.Next;
                deletionNode.Next = null;
                deletionNode.Previous = null;
                return;
            }

            deletionNode.Previous.Next = deletionNode.Next;

            if (deletionNode.Next != null)
            {
                deletionNode.Next.Previous = deletionNode.Previous;
            }

            deletionNode.Next = null;
            deletionNode.Previous = null;
        }

        /// <summary>
        ///  O(1) complexity 
        /// <returns></returns>
        public T PeekMin()
        {
            if (heapForestHead == null)
                throw new Exception("Empty heap");

            return minNode.Value;
        }
    }


}
