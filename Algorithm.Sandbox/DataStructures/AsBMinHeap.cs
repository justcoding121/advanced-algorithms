namespace Algorithm.Sandbox.DataStructures
{
    public class AsBMinHeapNode<T>
    {
        public T data { get; set; }

        public AsBMinHeap<T> Left { get; set; }
        public AsBMinHeap<T> Right { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public AsBMinHeapNode(T data)
        {
            this.data = data;
        }

    }

    public class AsBMinHeap<T>
    {
        public AsBMinHeapNode<T> Root { get; set; }

        public AsBMinHeap(T rootData)
        {
            Root = new AsBMinHeapNode<T>(rootData);
        }

    }
}
