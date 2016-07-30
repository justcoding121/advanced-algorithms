namespace Algorithm.Sandbox.DataStructures
{
    public class AsQueue<T>
    {
        private AsDoublyLinkedList<T> list = new AsDoublyLinkedList<T>();

        //O(1)
        public void Enqueue(T item)
        {
            list.InsertFirst(item);

        }
        //O(1)
        public T Dequeue()
        {
            if (list.Head == null)
            {
                throw new System.Exception("Empty Queue");
            }

            return list.DeleteLast();
        }

        //O(n)
        public int Count()
        {
            return list.Count();
        }
    }

}
