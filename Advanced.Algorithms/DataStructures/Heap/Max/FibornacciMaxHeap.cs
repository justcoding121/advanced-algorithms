using System;

namespace Advanced.Algorithms.DataStructures.Heap.Max
{
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    public class FibornacciMaxHeap<T> where T : IComparable
    {
        internal FibornacciHeapNode<T> HeapForestHead;

        //holds the maximum node at any given time
        private FibornacciHeapNode<T> maxNode;

        public int Count { get; private set; }

        /// <summary>
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="newItem"></param>
        public FibornacciHeapNode<T> Insert(T newItem)
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

            Count++;

            return newNode;
        }

        /// <summary>
        /// Merge roots with same degrees in Forest 
        /// </summary>
        private void Meld()
        {

            if (HeapForestHead == null)
            {
                maxNode = null;
                return;
            }

            //degree - node dictionary
            var mergeDictionary = new System.Collections.Generic.Dictionary<int, FibornacciHeapNode<T>>();

            var current = HeapForestHead;
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

                    deleteNode(ref HeapForestHead, current);

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

                        deleteNode(ref HeapForestHead, current);

                        var childHead = existing.ChildrenHead;
                        insertNode(ref childHead, current);
                        existing.ChildrenHead = childHead;

                        existing.Degree++;

                        insertNode(ref HeapForestHead, existing);
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
                    insertNode(ref HeapForestHead, node.Value);

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
        /// O(log(n)) complexity
        /// </summary>
        /// <returns></returns>
        public T ExtractMax()
        {
            if (HeapForestHead == null)
                throw new Exception("Empty heap");

            var maxValue = maxNode.Value;

            //remove tree root
            deleteNode(ref HeapForestHead, maxNode);

            mergeForests(maxNode.ChildrenHead);
            Meld();

            Count--;

            return maxValue;
        }


        /// <summary>
        /// Update the Heap with new value for this node pointer
        /// O(1) complexity amortized
        /// </summary>
        /// <param name="node"></param>
        public void IncrementKey(FibornacciHeapNode<T> node)
        {

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
        /// Delete this node from Heap Tree and adds it to forest as a new tree 
        /// </summary>
        /// <param name="node"></param>
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

            insertNode(ref HeapForestHead, node);

            //update max
            if (maxNode.Value.CompareTo(node.Value) < 0)
            {
                maxNode = node;
            }

        }

        /// <summary>
        /// Unions this heap with another
        /// O(k) complexity where K is the FibornacciHeap Forest Length 
        /// </summary>
        /// <param name="fibornacciHeap"></param>
        public void Union(FibornacciMaxHeap<T> fibornacciHeap)
        {
            mergeForests(fibornacciHeap.HeapForestHead);
            Count = Count + fibornacciHeap.Count;
        }

        /// <summary>
        /// Merges the given fibornacci node list to current Forest 
        /// </summary>
        /// <param name="headPointer"></param>
        private void mergeForests(FibornacciHeapNode<T> headPointer)
        {
            var current = headPointer;
            while (current != null)
            {
                var next = current.Next;
                insertNode(ref HeapForestHead, current);
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

        /// <summary/>
        ///  O(1) complexity 
        /// <returns></returns>
        public T PeekMax()
        {
            if (HeapForestHead == null)
                throw new Exception("Empty heap");

            return maxNode.Value;
        }
    }


}
