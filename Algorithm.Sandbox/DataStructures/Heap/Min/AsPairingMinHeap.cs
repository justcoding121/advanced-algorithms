using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsPairingTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal AsPairingTreeNode<T> ChildrenHead { get; set; }
        internal bool IsHeadChild => Previous != null && Previous.ChildrenHead == this;

        public AsPairingTreeNode(T value)
        {
            this.Value = value;
        }

        public AsPairingTreeNode<T> Previous;
        public AsPairingTreeNode<T> Next;

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsPairingTreeNode<T>).Value);
        }
    }


    public class AsPairingMinHeap<T> where T : IComparable
    {
        internal AsPairingTreeNode<T> Root;
        internal int Count { get; private set; }

        public AsPairingTreeNode<T> Insert(T newItem)
        {
            var newNode = new AsPairingTreeNode<T>(newItem);
            Root = Meld(Root, newNode);
            Count++;

            return newNode;

        }

        /// <summary>
        ///  O(n), Amortized O(log(n))
        ///  Melds all the nodes to one single Root Node
        /// </summary>
        /// <param name="headNode"></param>
        private void Meld(AsPairingTreeNode<T> headNode)
        {
            if (headNode == null)
                return;

            var passOneResult = new AsArrayList<AsPairingTreeNode<T>>();

            var current = headNode;

            if (current.Next == null)
            {
                headNode.Next = null;
                headNode.Previous = null;
                passOneResult.AddItem(headNode);
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
                        passOneResult.AddItem(Meld(current, next));
                        current = nextNext;
                    }
                    else
                    {
                        var lastInserted = passOneResult[passOneResult.Length - 1];
                        passOneResult[passOneResult.Length - 1] = Meld(lastInserted, current);
                        break;

                    }

                }

            }

            var passTwoResult = passOneResult[passOneResult.Length - 1];

            if (passOneResult.Length == 1)
            {
                Root = passTwoResult;
                return;
            }


            for (int i = passOneResult.Length - 2; i >= 0; i--)
            {
                current = passOneResult[i];
                passTwoResult = Meld(passTwoResult, current);
            }

            Root = passTwoResult;
        }

        /// <summary>
        /// makes the smaller node parent of other and returns the Parent
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        private AsPairingTreeNode<T> Meld(AsPairingTreeNode<T> node1,
            AsPairingTreeNode<T> node2)
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

            if (node1.Value.CompareTo(node2.Value) <= 0)
            {
                if (node1.ChildrenHead == null)
                {
                    node1.ChildrenHead = node2;
                    node2.Previous = node1;
                }
                else
                {
                    InsertNode(node1, node2);
                }

                return node1;
            }
            else
            {
                if (node2.ChildrenHead == null)
                {
                    node2.ChildrenHead = node1;
                    node1.Previous = node2;
                }
                else
                {
                    InsertNode(node2, node1);
                }

                return node2;
            }
        }

        private void InsertNode(AsPairingTreeNode<T> parent, AsPairingTreeNode<T> child)
        {
            var head = parent.ChildrenHead;

            child.Previous = head;
            child.Next = head.Next;

            if (head.Next != null)
            {
                head.Next.Previous = child;
            }

            head.Next = child;

        }


        public T ExtractMin()
        {
            var min = Root;
            Meld(Root.ChildrenHead);
            Count--;
            return min.Value;
        }


        public void DecrementKey(AsPairingTreeNode<T> node)
        {
            if (node == Root)
                return;

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

            Root = Meld(Root, node);
        }

        public void Union(AsPairingMinHeap<T> PairingHeap)
        {
            Root = Meld(Root, PairingHeap.Root);
            Count = Count + PairingHeap.Count;
        }

        /// <summary>
        /// O(1) time complexity
        /// </summary>
        /// <returns></returns>
        public T PeekMin()
        {
            if (Root == null)
                throw new Exception("Empty heap");

            return Root.Value;
        }
    }


}
