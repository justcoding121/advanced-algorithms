using System;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsDoublyLinkedListNode<T>
    {
        public AsDoublyLinkedListNode<T> Previous;
        public AsDoublyLinkedListNode<T> Next;
        public T Data;

        public AsDoublyLinkedListNode(T data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// A singly linked list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsDoublyLinkedList<T>
    {
        public AsDoublyLinkedListNode<T> Head;
        public AsDoublyLinkedListNode<T> Tail;
        //marks this data as the new head
        //cost O(1)
        public void InsertFirst(T data)
        {
            var newNode = new AsDoublyLinkedListNode<T>(data);

            if (Head != null)
            {
                Head.Previous = newNode;
            }

            newNode.Next = Head;

            Head = newNode;

            if (Tail == null)
            {
                Tail = Head;
            }

        }

        //insert at the end
        //costs O(1)
        public void InsertLast(T data)
        {
            var newNode = new AsDoublyLinkedListNode<T>(data);

            if (Tail == null)
            {
                InsertFirst(data);
                return;
            }

            Tail.Next = newNode;
            newNode.Previous = Tail;

            Tail = newNode;

        }

        //cost O(1)
        public T DeleteFirst()
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            var firstData = Head.Data;

            if (Head == Tail)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Head.Next.Previous = null;
                Head = Head.Next;
            }

            return firstData;
        }

        //cost O(1)
        public T DeleteLast()
        {
            if (Tail == null)
            {
                throw new Exception("Empty list");
            }

            var lastData = Tail.Data;

            if (Tail == Head)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                Tail.Previous.Next = null;
                Tail = Tail.Previous;
            }

            return lastData;
        }

        //cost O(n) in worst case O(nlogn) average?
        public void Delete(T data)
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            //eliminate single element list possibility
            if (Head == Tail)
            {
                if (Head.Data.Equals(data))
                {
                    DeleteFirst();
                }

                return;
            }

            //from here logic assumes atleast two elements in list

            var current = Head;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    //current is the first element
                    if (current.Previous == null)
                    {
                        current.Next.Previous = null;
                        Head = current.Next;
                    }
                    //current is the last element
                    else if (current.Next == null)
                    {
                        current.Previous.Next = null;
                        Tail = current.Previous;
                    }
                    //current is somewhere in the middle
                    else
                    {
                        current.Previous.Next = current.Next;
                        current.Next.Previous = current.Previous;
                    }
                    break;
                }

                current = current.Next;
            }
            
        }

        //O(n) always
        public int Count()
        {
            var i = 0;
            var current = Head;
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
            return Head == null;
        }

        //O(1) always
        public void DeleteAll()
        {
            if (Head == null)
            {
                throw new System.Exception("Empty list");
            }

            Head = null;
            Tail = null;
        }

        //O(n) time complexity
        public AsArrayList<T> GetAllNodes()
        {
            var result = new AsArrayList<T>();

            var current = Head;
            while (current != null)
            {
                result.AddItem(current.Data);
                current = current.Next;
            }

            return result;
        }
    }  
}
