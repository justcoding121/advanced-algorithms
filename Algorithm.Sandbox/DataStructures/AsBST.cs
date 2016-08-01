namespace Algorithm.Sandbox.DataStructures
{
    public class AsBSTNode<T, U>
    {
        public T Identifier { get; set; }
        public U Data { get; set; }

        public AsBST<T, U> Left { get; set; }
        public AsBST<T, U> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBSTNode(T identifier, U data)
        {
            this.Identifier = identifier;
            this.Data = data;
        }

    }

    public class AsBST<T, U>
    {
        public AsBSTNode<T, U> Root { get; set; }

    }
}
