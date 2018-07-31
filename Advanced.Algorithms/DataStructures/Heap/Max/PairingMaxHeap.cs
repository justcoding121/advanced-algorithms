using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    public class PairingMaxHeap<T> where T : IComparable
    {
        private Dictionary<T, List<PairingHeapNode<T>>> heapMapping
         = new Dictionary<T, List<PairingHeapNode<T>>>();

        private PairingHeapNode<T> Root;

        public int Count { get; private set; }

        /// <summary>
        /// Insert a new Node
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public void Insert(T newItem)
        {
            var newNode = new PairingHeapNode<T>(newItem);
            Root = meld(Root, newNode);
            addMapping(newItem, newNode);
            Count++;
        }

        /// <summary>
        /// Returns the max
        /// </summary>
        /// <returns></returns>
        public T ExtractMax()
        {
            var max = Root;
            removeMapping(max.Value, max);
            meld(Root.ChildrenHead);
            Count--;
            return max.Value;
        }

        /// <summary>
        /// Update heap after a node value was decremented
        /// </summary>
        /// <param name="node"></param>
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

            if (node == Root)
            {
                return;
            }

            deleteChild(node);

            Root = meld(Root, node);
        }

        /// <summary>
        /// Merge another heap with this heap
        /// </summary>
        /// <param name="PairingHeap"></param>
        public void Merge(PairingMaxHeap<T> PairingHeap)
        {
            Root = meld(Root, PairingHeap.Root);
            Count = Count + PairingHeap.Count;
        }

        /// <summary>
        /// O(1) time complexity
        /// </summary>
        /// <returns></returns>
        public T PeekMax()
        {
            if (Root == null)
                throw new Exception("Empty heap");

            return Root.Value;
        }

        /// <summary>
        ///  O(n), Amortized O(log(n))
        ///  Melds all the nodes to one single Root Node
        /// </summary>
        /// <param name="headNode"></param>
        private void meld(PairingHeapNode<T> headNode)
        {
            if (headNode == null)
                return;

            var passOneResult = new List<PairingHeapNode<T>>();

            var current = headNode;

            if (current.Next == null)
            {
                headNode.Next = null;
                headNode.Previous = null;
                passOneResult.Add(headNode);
            }
            else
            {
                while (true)
                {
                    if (current == null)
                    {
                        break;
                    }

                    if (current.Next != null)
                    {
                        var next = current.Next;
                        var nextNext = next.Next;
                        passOneResult.Add(meld(current, next));
                        current = nextNext;
                    }
                    else
                    {
                        var lastInserted = passOneResult[passOneResult.Count - 1];
                        passOneResult[passOneResult.Count - 1] = meld(lastInserted, current);
                        break;

                    }
                }

            }

            var passTwoResult = passOneResult[passOneResult.Count - 1];

            if (passOneResult.Count == 1)
            {
                Root = passTwoResult;
                return;
            }


            for (var i = passOneResult.Count - 2; i >= 0; i--)
            {
                current = passOneResult[i];
                passTwoResult = meld(passTwoResult, current);
            }

            Root = passTwoResult;
        }

        /// <summary>
        /// makes the smaller node parent of other and returns the Parent
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        private PairingHeapNode<T> meld(PairingHeapNode<T> node1,
            PairingHeapNode<T> node2)
        {
            if (node2 != null)
            {
                node2.Previous = null;
                node2.Next = null;
            }

            if (node1 == null)
            {
                return node2;
            }

            node1.Previous = null;
            node1.Next = null;

            if (node2 != null && node1.Value.CompareTo(node2.Value) >= 0)
            {

                addChild(ref node1, node2);
                return node1;
            }

            addChild(ref node2, node1);
            return node2;
        }

        /// <summary>
        /// Add new child to parent node
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        private void addChild(ref PairingHeapNode<T> parent, PairingHeapNode<T> child)
        {
            if (parent.ChildrenHead == null)
            {
                parent.ChildrenHead = child;
                child.Previous = parent;
                return;
            }

            var head = parent.ChildrenHead;

            child.Previous = head;
            child.Next = head.Next;

            if (head.Next != null)
            {
                head.Next.Previous = child;
            }

            head.Next = child;

        }

        /// <summary>
        /// delete node from parent
        /// </summary>
        /// <param name="node"></param>
        private void deleteChild(PairingHeapNode<T> node)
        {
            //if this node is the child head pointer of parent
            if (node.IsHeadChild)
            {
                var parent = node.Previous;

                //use close sibling as new parent child pointer
                if (node.Next != null)
                {
                    node.Next.Previous = parent;
                }

                parent.ChildrenHead = node.Next;
            }
            else
            {
                //just do regular deletion from linked list
                node.Previous.Next = node.Next;

                if (node.Next != null)
                {
                    node.Next.Previous = node.Previous;
                }
            }
        }

        private void addMapping(T newItem, PairingHeapNode<T> newNode)
        {
            if (heapMapping.ContainsKey(newItem))
            {
                heapMapping[newItem].Add(newNode);
            }
            else
            {
                heapMapping[newItem] = new List<PairingHeapNode<T>>(new[] { newNode });
            }
        }

        private void updateNodeValue(T currentValue, T newValue, PairingHeapNode<T> node)
        {
            removeMapping(currentValue, node);
            node.Value = newValue;
            addMapping(newValue, node);
        }

        private void removeMapping(T currentValue, PairingHeapNode<T> node)
        {
            heapMapping[currentValue].Remove(node);
            if (heapMapping[currentValue].Count == 0)
            {
                heapMapping.Remove(currentValue);
            }
        }
    }

}
