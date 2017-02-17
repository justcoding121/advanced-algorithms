using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsPairingTreeNode<T> : IComparable where T : IComparable
    {
        internal T Value { get; set; }

        internal AsArrayList<AsPairingTreeNode<T>> Children { get; set; }

        public AsPairingTreeNode(T value)
        {
            this.Value = value;
            Children = new AsArrayList<AsPairingTreeNode<T>>();
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
            return Insert(ref Root, newItem);
        }

        private AsPairingTreeNode<T> Insert(ref AsPairingTreeNode<T> Parent, T newItem)
        {
            var newNode = new AsPairingTreeNode<T>(newItem);

            if (Parent == null)
            {
                Parent = newNode;
                Count++;

                return newNode;
            }

            if (Parent.Value.CompareTo(newNode.Value) <= 0)
            {
                Parent.Children.AddItem(newNode);
            }
            else
            {
                newNode.Children.AddItem(Parent);
                Parent = newNode;
            }

            Count++;

            return newNode;
        }

        /// <summary>
        ///  O(n), Amortized O(log(n))
        /// </summary>
        /// <param name="nodes"></param>
        private void Meld(AsArrayList<AsPairingTreeNode<T>> nodes)
        {
            if (nodes.Length == 0)
                return;

            var passOneResult = new AsArrayList<AsPairingTreeNode<T>>();

            if (nodes.Length == 1)
            {
                passOneResult.AddItem(nodes[0]);
            }
            else
            {

                for (int i = 0; i < nodes.Length; i = i + 2)
                {
                    var current = nodes[i];

                    if (i == nodes.Length - 1)
                    {
                        var lastInserted = passOneResult[passOneResult.Length - 1];
                        Insert(ref lastInserted, current.Value);
                        passOneResult[passOneResult.Length - 1] = lastInserted;

                        break;
                    }

                    var next = nodes[i + 1];

                    if (current.Value.CompareTo(next.Value) <= 0)
                    {
                        current.Children.AddItem(next);
                        passOneResult.AddItem(current);
                    }
                    else
                    {
                        next.Children.AddItem(current);
                        passOneResult.AddItem(next);
                    }
                }

            }

            Root = null;
            var passTwoResult = Insert(passOneResult[passOneResult.Length - 1].Value);
            
            if(passOneResult.Length == 1)
            {
                Root = passTwoResult;
                return;
            }

            for (int i = passOneResult.Length - 2; i >= 0; i--)
            {
                Insert(ref passTwoResult, passOneResult[i].Value);
            }

            Root = passTwoResult;
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
            throw new NotImplementedException();
        }


        private void MergeForests(AsDoublyLinkedList<AsPairingTreeNode<T>> newHeapForest)
        {

            throw new NotImplementedException();

        }

        public T PeekMin()
        {
            throw new NotImplementedException();
        }
    }


}
