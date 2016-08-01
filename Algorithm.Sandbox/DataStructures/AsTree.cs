namespace Algorithm.Sandbox.DataStructures
{
    public class AsTreeNode<T>
    {
        public T data { get; set; }
        public AsSinglyLinkedList<AsTreeNode<T>> Children { get; set; }

        public bool IsLeaf => Children.Count() == 0;

        public AsTreeNode(T data)
        {
            this.data = data;
            Children = new AsSinglyLinkedList<AsTreeNode<T>>();
        } 

    }

    public class AsTree<T>
    {
        public AsTreeNode<T> Root { get; set; }

        public AsTree(T rootData)
        {
            Root = new AsTreeNode<T>(rootData);
        }
    }
}
