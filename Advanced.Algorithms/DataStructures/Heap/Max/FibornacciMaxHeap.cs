using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A fibornacci max heap implementation.
    /// </summary>
    public class FibornacciMaxHeap<T> : IEnumerable<T> where T : IComparable
    {
        private FibornacciHeapNode<T> maxNode = null;

        //holds the maximum node at any given time
        private FibornacciHeapNode<T> heapForestHead;

        private Dictionary<T, List<FibornacciHeapNode<T>>> heapMapping
            = new Dictionary<T, List<FibornacciHeapNode<T>>>();

        public int Count { get; private set; }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public void Insert(T newItem)
        {
            var newNode = new FibornacciHeapNode<T>(newItem);

            //return pointer to new Node
            mergeForests(newNode);

            if (maxNode == null)
            {
                maxNode = newNode;
            }
            else
            {
                if (maxNode.Value.CompareTo(newNode.Value) < 0)
                {
                    maxNode = newNode;
                }
            }

            addMapping(newItem, newNode);

            Count++;
        }

        /// <summary>
        /// Time complexity: O(log(n)).
        /// </summary>
        public T ExtractMax()
        {
            if (heapForestHead == null)
            {
                throw new Exception("Empty heap");
            }

            var maxValue = maxNode.Value;

            removeMapping(maxValue, maxNode);

            //remove tree root
            deleteNode(ref heapForestHead, maxNode);

            mergeForests(maxNode.ChildrenHead);
            Meld();

            Count--;

            return maxValue;
        }


        /// <summary>
        /// Update the Heap with new value for this node pointer.
        /// Time complexity: O(1).
        /// </summary>
        public void IncrementKey(T currentValue, T newValue)
        {
            var node = heapMapping[currentValue]?.Where(x => x.Value.Equals(currentValue)).FirstOrDefault();

            if (node == null)
            {
                throw new Exception("Current value is not present in this heap.");
            }

            if (newValue.CompareTo(node.Value) < 0)
            {
                throw new Exception("New value is not greater than old value.");
            }

            updateNodeValue(currentValue, newValue, node);

            if (node.Parent == null
                && maxNode.Value.CompareTo(node.Value) < 0)
            {
                maxNode = node;
            }

            var current = node;

            if (current.Parent == null || current.Value.CompareTo(current.Parent.Value) <= 0)
            {
                return;
            }

            var parent = current.Parent;

            //if parent already lost one child
            //then cut current and parent
            if (parent.LostChild)
            {
                parent.LostChild = false;

                var grandParent = parent.Parent;

                //mark grand parent
                if (grandParent == null)
                {
                    return;
                }

                cut(parent);
                cut(current);
            }
            else
            {
                cut(current);
            }

        }

        /// <summary>
        /// Unions this heap with another.
        /// Time complexity: O(1).
        /// </summary>
        public void Merge(FibornacciMaxHeap<T> FibornacciHeap)
        {
            mergeForests(FibornacciHeap.heapForestHead);
            Count = Count + FibornacciHeap.Count;
        }

        /// <summary>
        /// Time complexity: O(1).
        /// </summary>
        public T PeekMax()
        {
            if (heapForestHead == null)
                throw new Exception("Empty heap");

            return maxNode.Value;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest.
        /// </summary>
        private void Meld()
        {

            if (heapForestHead == null)
            {
                maxNode = null;
                return;
            }

            //degree - node dictionary
            var mergeDictionary = new Dictionary<int, FibornacciHeapNode<T>>();

            var current = heapForestHead;
            maxNode = current;
            while (current != null)
            {
                current.Parent = null;
                var next = current.Next;
                //no same degree already in merge dictionary
                //add to hash table
                if (!mergeDictionary.ContainsKey(current.Degree))
                {
                    mergeDictionary.Add(current.Degree, current);

                    if (maxNode == current)
                    {
                        maxNode = null;
                    }

                    deleteNode(ref heapForestHead, current);

                    current = next;
                }
                //insert back to forest by merging current tree 
                //with existing tree in merge dictionary
                else
                {
                    var currentDegree = current.Degree;
                    var existing = mergeDictionary[currentDegree];

                    if (existing.Value.CompareTo(current.Value) > 0)
                    {
                        current.Parent = existing;

                        deleteNode(ref heapForestHead, current);

                        var childHead = existing.ChildrenHead;
                        insertNode(ref childHead, current);
                        existing.ChildrenHead = childHead;

                        existing.Degree++;

                        insertNode(ref heapForestHead, existing);
                        current = existing;
                        current.Next = next;

                    }
                    else
                    {
                        existing.Parent = current;

                        var childHead = current.ChildrenHead;
                        insertNode(ref childHead, existing);
                        current.ChildrenHead = childHead;

                        current.Degree++;
                    }


                    if (maxNode == null
                        || maxNode.Value.CompareTo(current.Value) < 0)
                    {
                        maxNode = current;
                    }

                    mergeDictionary.Remove(currentDegree);

                }

            }

            //insert back trees with unique degrees to forest
            if (mergeDictionary.Count > 0)
            {
                foreach (var node in mergeDictionary)
                {
                    insertNode(ref heapForestHead, node.Value);

                    if (maxNode == null
                        || maxNode.Value.CompareTo(node.Value.Value) < 0)
                    {
                        maxNode = node.Value;
                    }
                }

                mergeDictionary.Clear();
            }

        }


        /// <summary>
        /// Delete this node from Heap Tree and adds it to forest as a new tree 
        /// </summary>
        private void cut(FibornacciHeapNode<T> node)
        {
            var parent = node.Parent;

            //cut child and attach to heap Forest
            //and mark parent for lost child
            var childHead = node.Parent.ChildrenHead;
            deleteNode(ref childHead, node);
            node.Parent.ChildrenHead = childHead;

            node.Parent.Degree--;
            if (parent.Parent != null)
            {
                parent.LostChild = true;
            }
            node.LostChild = false;
            node.Parent = null;

            insertNode(ref heapForestHead, node);

            //update max
            if (maxNode.Value.CompareTo(node.Value) < 0)
            {
                maxNode = node;
            }

        }

        /// <summary>
        /// Merges the given fibornacci node list to current Forest 
        /// </summary>
        private void mergeForests(FibornacciHeapNode<T> headPointer)
        {
            var current = headPointer;
            while (current != null)
            {
                var next = current.Next;
                insertNode(ref heapForestHead, current);
                current = next;
            }

        }

        private void insertNode(ref FibornacciHeapNode<T> head, FibornacciHeapNode<T> newNode)
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

        private void deleteNode(ref FibornacciHeapNode<T> heapForestHead, FibornacciHeapNode<T> deletionNode)
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

        private void addMapping(T newItem, FibornacciHeapNode<T> newNode)
        {
            if (heapMapping.ContainsKey(newItem))
            {
                heapMapping[newItem].Add(newNode);
            }
            else
            {
                heapMapping[newItem] = new List<FibornacciHeapNode<T>>(new[] { newNode });
            }
        }

        private void updateNodeValue(T currentValue, T newValue, FibornacciHeapNode<T> node)
        {
            removeMapping(currentValue, node);
            node.Value = newValue;
            addMapping(newValue, node);
        }


        private void removeMapping(T currentValue, FibornacciHeapNode<T> node)
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
