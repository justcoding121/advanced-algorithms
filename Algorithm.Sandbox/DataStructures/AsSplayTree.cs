namespace Algorithm.Sandbox.DataStructures
{
    public class AsSplayTreeNode<T>
    {
        public T data { get; set; }

        public AsSplayTree<T> Left { get; set; }
        public AsSplayTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsSplayTreeNode(T data)
        {
            this.data = data;
        }

    }

    public class AsSplayTree<T>
    {
        public AsSplayTreeNode<T> Root { get; set; }

        public AsSplayTree(T rootData)
        {
            Root = new AsSplayTreeNode<T>(rootData);
        }

    }
}
