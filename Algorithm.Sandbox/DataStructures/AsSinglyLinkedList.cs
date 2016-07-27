using System;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsSinglyLinkedListNode<T>
    {
        public AsSinglyLinkedListNode<T> next;
        public T data;

        public AsSinglyLinkedListNode(T data)
        {
            this.data = data;
        }
    }

    //wrap the node inside a generic class
    public class AsSinglyLinkedList<T>
    {
        public AsSinglyLinkedListNode<T> Head;

        //marks this data as the new head
        //cost O(1)
        public void AddFirst(T data)
        {
            var newNode = new AsSinglyLinkedListNode<T>(data);

            newNode.next = Head;

            Head = newNode;
        }

        //insert at the end
        //costs O(n)
        public void AddLast(T data)
        {
            var newNode = new AsSinglyLinkedListNode<T>(data);

            if (Head == null)
            {
                Head = new AsSinglyLinkedListNode<T>(data);
            }
            else
            {
                var current = Head;

                while (current.next != null)
                {
                    current = current.next;
                }

                current.next = newNode;
            }

        }

        //cost O(1)
        public void RemoveFirst()
        {
            if (Head == null)
                throw new Exception("Nothing to remove");

            Head = Head.next;
        }

        //cost O(n)
        public void RemoveLast()
        {
            if (Head == null)
                throw new Exception("Nothing to remove");

            var current = Head;
            AsSinglyLinkedListNode<T> prev = null;
            while (current.next != null)
            {
                prev = current;
                current = current.next;
            }

            prev.next = null;
        }

        //cost O(n) in worst case O(nlogn) average?
        public void Remove(T data)
        {
            if (Head == null)
                throw new System.Exception("Empty list");

            var current = Head;
            AsSinglyLinkedListNode<T> prev = null;

            do
            {
                if (current.data.Equals(data))
                {
                    //last element
                    if (current.next == null)
                    {
                        //head is the only node
                        if (prev == null)
                        {
                            Head = null;
                        }
                        else
                        {
                            //last element
                            prev.next = null;
                        }
                    }
                    else
                    {
                        //current is head
                        if (prev == null)
                        {
                            Head = current.next;
                        }
                        else
                        {
                            //delete
                            prev.next = current.next;
                        }
                    }

                    break;
                }

                prev = current;
                current = current.next;
            }
            while (current != null);
        }

        //O(n) always
        public int Count()
        {
            var i = 0;
            var current = Head;
            while (current != null)
            {
                i++;
                current = current.next;
            }

            return i;
        }

        //O(1) always
        public bool IsEmpty()
        {
            return Head == null;
        }

        //O(1) always
        public void RemoveAll()
        {
            if (Head == null)
                throw new System.Exception("Empty list");

            Head = null;
        }

        //O(n) time complexity
        public void PrintAllNodes()
        {
            var current = Head;
            while (current != null)
            {
                Console.WriteLine(current.data);
                current = current.next;
            }
        }
    }


}
