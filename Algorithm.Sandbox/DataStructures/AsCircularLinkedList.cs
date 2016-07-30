using System;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsCircularLinkedListNode<T>
    {
        public AsCircularLinkedListNode<T> Next;
        public T Data;

        public AsCircularLinkedListNode(T data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// A singly linked list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsCircularLinkedList<T>
    {
        public AsCircularLinkedListNode<T> ReferenceNode;

        //marks this data as the new head (kinda insert first assuming current reference node as head)
        //cost O(1)
        public void Add(T data)
        {
            var newNode = new AsCircularLinkedListNode<T>(data);

            if (ReferenceNode != null)
            {
                newNode.Next = ReferenceNode;
               
            }
            else
            {
                newNode.Next = newNode;
            }

            ReferenceNode = newNode;

        }

        //cost O(n) in worst case O(nlogn) average?
        public void Remove(T data)
        {
            if (ReferenceNode == null)
            {
                throw new System.Exception("Empty list");
            }

            //reference node itself is the search term
            if (ReferenceNode.Next == ReferenceNode)
            {
                if (ReferenceNode.Data.Equals(data))
                {
                    ReferenceNode = null;
                    return;
                }
                throw new System.Exception("Item not in list");
            }

            //atleast two elements at this point
            var current = ReferenceNode;
            AsCircularLinkedListNode<T> prev = null;
            while (current.Next != ReferenceNode)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }

                prev = current;
                current = current.Next;
            }

            //current is node before reference node
            if (current.Next == ReferenceNode)
            {
                prev.Next = ReferenceNode;
            }
            //somewhere in between
            else
            {
                prev.Next = current.Next;
            }
        }

        //O(n) always
        public int Count()
        {
            var i = 0;
            var current = ReferenceNode;
            while (current != null && current.Next != ReferenceNode)
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

            if (ReferenceNode == null)
            {
                return result;
            }

            var current = ReferenceNode;
            result.AddItem(current.Data);

            while (current.Next != ReferenceNode)
            {
                result.AddItem(current.Data);
                current = current.Next;
            }

            return result;
        }
    }
}
