namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsCircularDoublyLinkedListNode<T>
    {
        public AsCircularDoublyLinkedListNode<T> Previous;
        public AsCircularDoublyLinkedListNode<T> Next;
        public T Data;

        public AsCircularDoublyLinkedListNode(T data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// A singly linked list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsCircularDoublyLinkedList<T>
    {
        public AsCircularDoublyLinkedListNode<T> ReferenceNode;

        //marks this data as the new head
        //cost O(1)
        public void Add(T data)
        {
            var newNode = new AsCircularDoublyLinkedListNode<T>(data);

            if (ReferenceNode != null)
            {
                ReferenceNode.Previous = newNode;
            }

            newNode.Next = ReferenceNode;

            ReferenceNode = newNode;

        }

        //cost O(n) in worst case O(nlogn) average?
        public void Remove(T data)
        {
            if (ReferenceNode == null)
            {
                throw new System.Exception("Empty list");
            }

            //from here logic assumes atleast two elements in list

            var current = ReferenceNode;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    //current is the first element
                    if (current.Previous == null)
                    {
                        current.Next.Previous = null;
                        ReferenceNode = current.Next;
                    }
                    //current is the last element
                    else if (current.Next == null)
                    {
                        current.Previous.Next = null;
                      
                    }
                    //current is somewhere in the middle
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }
                }

                current = current.Next;
            }


        }

        //O(n) always
        public int Count()
        {
            var i = 0;
            var current = ReferenceNode;
            while (current != null)
            {
                i++;
                current = current.Next;
            }

            return i;
        }

        //O(1) always
        public bool IsEmpty()
        {
            return ReferenceNode == null;
        }

        //O(1) always
        public void RemoveAll()
        {
            if (ReferenceNode == null)
            {
                throw new System.Exception("Empty list");
            }

            ReferenceNode = null;
        
        }

        //O(n) time complexity
        public AsArrayList<T> GetAllNodes()
        {
            var result = new AsArrayList<T>();

            var current = ReferenceNode;
            while (current != null)
            {
                result.AddItem(current.Data);
                current = current.Next;
            }

            return result;
        }
    }
}
