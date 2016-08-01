namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMaxHeapNode<T>
    {
        public T data { get; set; }

        public AsBMaxHeap<T> Left { get; set; }
        public AsBMaxHeap<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBMaxHeapNode(T data)
        {
            this.data = data;
        }

    }

    public class AsBMaxHeap<T>
    {
        public AsBMaxHeapNode<T> Root { get; set; }

        public AsBMaxHeap(T rootData)
        {
            Root = new AsBMaxHeapNode<T>(rootData);
        }

    }
}
