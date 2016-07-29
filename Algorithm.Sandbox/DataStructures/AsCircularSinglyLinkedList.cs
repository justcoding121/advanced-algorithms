using System;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsCircularSinglyLinkedListNode<T>
    {
        public AsCircularSinglyLinkedListNode<T> Next;
        public T Data;

        public AsCircularSinglyLinkedListNode(T data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// A singly linked list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsCircularSinglyLinkedList<T>
    {
        public AsCircularSinglyLinkedListNode<T> ReferenceNode;

        //marks this data as the new head
        //cost O(1)
        public void Insert(T data)
        {
            var newNode = new AsCircularSinglyLinkedListNode<T>(data);

            newNode.Next = ReferenceNode;
            ReferenceNode.Next = newNode;

            ReferenceNode = newNode;
        }


        //cost O(n) in worst case O(nlogn) average?
        public void Remove(T data)
        {
            if (ReferenceNode == null)
            {
                throw new Exception("Empty list");
            }

            var current = ReferenceNode;
            AsCircularSinglyLinkedListNode<T> prev = null;

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
                            ReferenceNode = null;
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
                            ReferenceNode = current.Next;
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
