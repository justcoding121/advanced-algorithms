using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    //define the generic node
    public class AsCircularLinkedListNode<T>
    {
        public AsCircularLinkedListNode<T> Prev;
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
    public class AsCircularLinkedList<T> : IEnumerable<T>
    {
        public AsCircularLinkedListNode<T> ReferenceNode;

        //marks this data as the new head (kinda insert first assuming current reference node as head)
        //cost O(1)
        public AsCircularLinkedListNode<T> Insert(T data)
        {
            var newNode = new AsCircularLinkedListNode<T>(data);

            //if no item exist
            if (ReferenceNode == null)
            {
                //attach the item after reference node
                newNode.Next = newNode;
                newNode.Prev = newNode;

            }
            else
            {
                //attach the item after reference node
                newNode.Prev = ReferenceNode;
                newNode.Next = ReferenceNode.Next;

                ReferenceNode.Next.Prev = newNode;
                ReferenceNode.Next = newNode;

            }

            ReferenceNode = newNode;

            return newNode;
        }



        //O(1) delete this item
        public void Delete(AsCircularLinkedListNode<T> current)
        {
            if (ReferenceNode.Next == ReferenceNode)
            {
                if (ReferenceNode == current)
                {
                    ReferenceNode = null;
                    return;
                }
                throw new Exception("Not found");
            }

            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;

            //match is a reference node
            if (current == ReferenceNode)
            {
                ReferenceNode = current.Next;
            }
        }

        //search and delete
        //cost O(n) in worst case O(nlog(n) average?
        public void Delete(T data)
        {
            if (ReferenceNode == null)
            {
                throw new Exception("Empty list");
            }

            //only one element on list
            if (ReferenceNode.Next == ReferenceNode)
            {
                if (ReferenceNode.Data.Equals(data))
                {
                    ReferenceNode = null;
                    return;
                }
                throw new Exception("Not found");
            }

            //atleast two elements from here
            var current = ReferenceNode;
            var found = false;
            while (true)
            {
                if (current.Data.Equals(data))
                {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;

                    //match is a reference node
                    if (current == ReferenceNode)
                    {
                        ReferenceNode = current.Next;
                    }

                    found = true;
                    break;
                }

                //terminate loop if we are about to cycle
                if (current.Next == ReferenceNode)
                {
                    break;
                }

                ///move to next item
                current = current.Next;
            }

            if (found == false)
            {
                throw new Exception("Not found");
            }
        }

        //O(n) always
        public int Count()
        {
            //re-implement
            //no need to iterate
            var i = 0;

            var current = ReferenceNode;

            if (current == null)
            {
                return 0;
            }
            else
            {
                i++;
            }

            while (current.Next != ReferenceNode)
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
        public void DeleteAll()
        {
            if (ReferenceNode == null)
            {
                throw new Exception("Empty list");
            }

            ReferenceNode = null;

        }

        //O(n) time complexity
        public List<T> GetAllNodes()
        {
            var result = new List<T>();

            var current = ReferenceNode;

            if (current == null)
            {
                return result;
            }
            else
            {
                result.Add(current.Data);
            }

            while (current.Next != ReferenceNode)
            {
                result.Add(current.Data);
                current = current.Next;
            }

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new AsCircularLinkedListEnumerator<T>(ref ReferenceNode);
        }

        /// <summary>
        /// O(1) time complexity
        /// </summary>
        /// <param name="newList"></param>
        internal void Union(AsCircularLinkedList<T> newList)
        {
           
            ReferenceNode.Prev.Next = newList.ReferenceNode;
            ReferenceNode.Prev = newList.ReferenceNode.Prev;

            newList.ReferenceNode.Prev.Next = ReferenceNode;
            newList.ReferenceNode.Prev = ReferenceNode.Prev;


        }
    }

    //  implement IEnumerator.
    public class AsCircularLinkedListEnumerator<T> : IEnumerator<T> 
    {
        internal AsCircularLinkedListNode<T> referenceNode;
        internal AsCircularLinkedListNode<T> currentNode;

        internal AsCircularLinkedListEnumerator(ref AsCircularLinkedListNode<T> referenceNode)
        {
            this.referenceNode = referenceNode;
        }

        public bool MoveNext()
        {
            if (referenceNode == null)
                return false;

            if (currentNode == null)
            {
                currentNode = referenceNode;
                return true;
            }

            if (currentNode.Next != null && currentNode.Next != referenceNode)
            {
                currentNode = currentNode.Next;
                return true;
            }

            return false;

        }

        public void Reset()
        {
            currentNode = referenceNode;
        }


        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return currentNode.Data;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public void Dispose()
        {
        }

    }
}
