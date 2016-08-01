namespace Algorithm.Sandbox.DataStructures
{
    public class AsDiGraphVertex<T>
    {
        public T data { get; set; }
        public AsSinglyLinkedList<AsDiGraphVertex<T>> OutNeighbours { get; set; }
        public AsSinglyLinkedList<AsDiGraphVertex<T>> InNeighbours { get; set; }

        public AsDiGraphVertex(T data)
        {
            this.data = data;
            OutNeighbours = new AsSinglyLinkedList<AsDiGraphVertex<T>>();
            InNeighbours = new AsSinglyLinkedList<AsDiGraphVertex<T>>();
        }

    }

    public class AsDiGraph<T>
    {
        public AsDiGraphVertex<T> ReferenceVertex { get; set; }

        public AsDiGraph(T referenceNodeData)
        {
            ReferenceVertex = new AsDiGraphVertex<T>(referenceNodeData);
        }
    }
}
