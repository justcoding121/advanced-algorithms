using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsPairingTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal AsPairingLinkedListNode<AsPairingTreeNode<T>> headChild { get; set; }

        public AsPairingTreeNode(T value)
        {
            this.Value = value;
        }

        public int CompareTo(object obj)
        {
            return this.Value.CompareTo((obj as AsPairingTreeNode<T>).Value);
        }
    }

    public class AsPairingLinkedListNode<T> : AsDoublyLinkedListNode<T> where T : IComparable
    {
        public AsPairingLinkedListNode(T data) : base(data)
        {
        }

        public T Parent { get; internal set; }

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
        private void Meld(AsPairingLinkedListNode<AsPairingTreeNode<T>> headNode)
        {
            if (headNode == null)
                return;

            var passOneResult = new AsDoublyLinkedList<AsPairingTreeNode<T>>();

            var current = headNode as AsDoublyLinkedListNode<AsPairingTreeNode<T>>;

            if (current.Next == null)
            {
                passOneResult.InsertFirst(headNode.Data);
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
                        passOneResult.InsertFirst(Meld(current.Data, next.Data));
                        current = next.Next;
                    }
                    else
                    {
                        var lastInserted = passOneResult.Tail;
                        passOneResult.Tail.Data = Meld(lastInserted.Data, current.Data);
                        break;

                    }

                }

            }

            var passTwoResult = passOneResult.Tail;

            if (passOneResult.Head.Next == null)
            {
                Root = passTwoResult.Data;
                return;
            }

            current = passOneResult.Tail.Previous;
            while (current != null)
            {
                passTwoResult.Data = Meld(passTwoResult.Data, current.Data);
                current = current.Previous;
            }

            Root = passTwoResult.Data;
        }

        /// <summary>
        /// makes the smaller node parent of other and returns the smallest Node
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        private AsPairingTreeNode<T> Meld(AsPairingTreeNode<T> node1,
            AsPairingTreeNode<T> node2)
        {
            if (node1 == null)
            {
                return node2;
            }

            if (node1.Value.CompareTo(node2.Value) <= 0)
            {
                if (node1.headChild == null)
                {
                    node1.headChild = new AsPairingLinkedListNode<AsPairingTreeNode<T>>(node2);
                    node1.headChild.Parent = node1;
                }
                else
                {
                    InsertNode(node1, node2);
                }

                return node1;
            }
            else
            {
                if (node2.headChild == null)
                {
                    node2.headChild = new AsPairingLinkedListNode<AsPairingTreeNode<T>>(node1);
                    node2.headChild.Parent = node2;
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
            var head = parent.headChild;
            var newNode = new AsPairingLinkedListNode<AsPairingTreeNode<T>>(child);
          
                newNode.Previous = head;
                newNode.Next = head.Next;

                if (head.Next != null)
                {
                    head.Next.Previous = newNode;
                }

                head.Next = newNode;
            

        }

        public T ExtractMin()
        {
            var min = Root;
            Meld(Root.headChild);
            Count--;
            return min.Value;
        }


        public void DecrementKey(AsPairingTreeNode<T> node)
        {
            throw new NotImplementedException();
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
