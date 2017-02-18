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
                        passOneResult[passOneResult.Length - 1] = Meld(lastInserted, current);
                        break;
                    }

                    var next = nodes[i + 1];
                    passOneResult.AddItem(Meld(current, next));

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
                var current = passOneResult[i];
                passTwoResult = Meld(passTwoResult, current);
            }

            Root = passTwoResult;
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
                node1 = node2;
                return node1;
            }

            if (node1.Value.CompareTo(node2.Value) <= 0)
            {
                node1.Children.AddItem(node2);
                return node1;
            }
            else
            {
                node2.Children.AddItem(node1);
                node1 = node2;
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
            var tmpList = new AsArrayList<AsPairingTreeNode<T>>();
            tmpList.AddItem(PairingHeap.Root);

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
