namespace Algorithm.Sandbox.DataStructures
{
    public class AsRedBlackTreeNode<T>
    {
        public T data { get; set; }

        public AsRedBlackTree<T> Left { get; set; }
        public AsRedBlackTree<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsRedBlackTreeNode(T data)
        {
            this.data = data;
        }

    }

    public class AsRedBlackTree<T>
    {
        public AsRedBlackTreeNode<T> Root { get; set; }

        public AsRedBlackTree(T rootData)
        {
            Root = new AsRedBlackTreeNode<T>(rootData);
        }

    }
}
