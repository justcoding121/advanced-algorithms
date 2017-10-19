namespace Advanced.Algorithms.DataStructures.Queues
{

    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    internal class LinkedListQueue<T> : IQueue<T>
    {
        private DoublyLinkedList<T> list = new DoublyLinkedList<T>();

        public int Count { get; private set; }
        //O(1)
        public void Enqueue(T item)
        {
            list.InsertFirst(item);
            Count++;
        }

        //O(1)
        public T Dequeue()
        {
            if (list.Head == null)
            {
                throw new System.Exception("Empty Queue");
            }

            var result = list.DeleteLast();
            Count--;
            return result;
        }

     
    }

}
