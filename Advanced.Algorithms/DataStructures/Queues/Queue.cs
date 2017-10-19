namespace Advanced.Algorithms.DataStructures.Queues
{
    internal interface IQueue<T>
    {
        int Count { get; }
        void Enqueue(T item);
        T Dequeue();
    }

    public enum QueueType
    {
        Array = 0,
        LinkedList = 1
    }

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    public class AsQueue<T> 
    {
        private IQueue<T> queue;

        public int Count => queue.Count;

        public AsQueue(QueueType type = QueueType.Array)
        {
            if (type == QueueType.Array)
            {
                queue = new ArrayQueue<T>();
            }
            else
            {
                queue = new LinkedListQueue<T>();
            }
        }
        //O(1)
        public void Enqueue(T item)
        {
           queue.Enqueue(item);
        }
        //O(1)
        public T Dequeue()
        {
           return queue.Dequeue();
        }

    }

}
