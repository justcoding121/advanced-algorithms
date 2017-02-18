using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsPairingTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal AsDoublyLinkedList<AsPairingTreeNode<T>> Children { get; set; }

        public AsPairingTreeNode(T value)
        {
            this.Value = value;
            Children = new AsDoublyLinkedList<AsPairingTreeNode<T>>();
        }

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
        /// <param name="nodes"></param>
        private void Meld(AsDoublyLinkedList<AsPairingTreeNode<T>> nodes)
        {
            if (nodes.Head == null)
                return;

            var passOneResult = new AsDoublyLinkedList<AsPairingTreeNode<T>>();

            var current = nodes.Head;

            if (current.Next == null)
            {
                passOneResult.InsertFirst(nodes.Head);
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
                node1.Children.InsertFirst(node2);
                return node1;
            }
            else
            {
                node2.Children.InsertFirst(node1);
                return node2;
            }
        }

        public T ExtractMin()
        {
            var min = Root;
            Meld(Root.Children);
            Count--;
            return min.Value;
        }


        public void DecrementKey(AsPairingTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        public void Union(AsPairingMinHeap<T> PairingHeap)
        {
            var tmpList = new AsDoublyLinkedList<AsPairingTreeNode<T>>();
            tmpList.InsertFirst(PairingHeap.Root);

            Meld(tmpList);

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
