using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class SinglyLinkedListNode<T>
    {
        public SinglyLinkedListNode<T> Next;
        public T Data;

        public SinglyLinkedListNode(T data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// A singly linked list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsSinglyLinkedList<T> 
    {
        public SinglyLinkedListNode<T> Head;

        //marks this data as the new head
        //cost O(1)
        public void InsertFirst(T data)
        {
            var newNode = new SinglyLinkedListNode<T>(data);

            newNode.Next = Head;

            Head = newNode;
        }

        //insert at the end
        //costs O(n)
        public void InsertLast(T data)
        {
            var newNode = new SinglyLinkedListNode<T>(data);

            if (Head == null)
            {
                Head = new SinglyLinkedListNode<T>(data);
            }
            else
            {
                var current = Head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
            }

        }

        //cost O(1)
        public T DeleteFirst()
        {
            if (Head == null)
            {
                throw new Exception("Nothing to remove");
            }

            var firstData = Head.Data;

            Head = Head.Next;

            return firstData;
        }

        //cost O(n)
        public T DeleteLast()
        {
            if (Head == null)
            {
                throw new Exception("Nothing to remove");
            }

            var current = Head;
            SinglyLinkedListNode<T> prev = null;
            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            var lastData = prev.Next.Data;

            prev.Next = null;

            return lastData;
        }

        //search and delete
        //cost O(n) in worst case O(nlog(n) average?
        public void Delete(T data)
        {
            if (Head == null)
            {
                throw new Exception("Empty list");
            }

            var current = Head;
            SinglyLinkedListNode<T> prev = null;

            do
            {
                if (current.Data.Equals(data))
                {
                    //last element
                    if (current.Next == null)
                    {
                        //head is the only node
                        if (prev == null)
                        {
                            Head = null;
                        }
                        else
                        {
                            //last element
                            prev.Next = null;
                        }
                    }
                    else
                    {
                        //current is head
                        if (prev == null)
                        {
                            Head = current.Next;
                        }
                        else
                        {
                            //delete
                            prev.Next = current.Next;
                        }
                    }

                    break;
                }

                prev = current;
                current = current.Next;
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
        }

        //O(n) time complexity
        public List<T> GetAllNodes()
        {
            var result = new List<T>();

            var current = Head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }

            return result;
        }

        /// <summary>
        /// Inserts this element to the begining
        /// </summary>
        /// <param name="current"></param>
        public void InsertFirst(SinglyLinkedListNode<T> current)
        {
            current.Next = Head;
            Head = current;
        }
    }


}
