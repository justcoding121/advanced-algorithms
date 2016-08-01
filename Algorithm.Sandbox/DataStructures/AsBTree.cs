namespace Algorithm.Sandbox.DataStructures
{
    public class AsBTreeNode<T>
    {
        public T data { get; set; }
        
        public AsBTree<T> Left { get; set; }
        public AsBTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBTreeNode(T data)
        {
            this.data = data;
        }

    }

    public class AsBTree<T>
    {
        public AsBTreeNode<T> Root { get; set; }

        public AsBTree(T rootData)
        {
            Root = new AsBTreeNode<T>(rootData);
        }

    }
}
