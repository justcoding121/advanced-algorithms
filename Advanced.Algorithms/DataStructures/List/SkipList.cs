using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A skip list implementation with IEnumerable support.
    /// </summary>
    /// <typeparam name="T">The data type of thi skip list.</typeparam>
    public class SkipList<T> : IEnumerable<T> where T : IComparable
    {
        private readonly Random coinFlipper = new Random();

        internal SkipListNode<T> Head { get; set; }

        /// <summary>
        /// The number of elements in this skip list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// The maximum height of this skip list with which it was initialized.
        /// </summary>
        public readonly int MaxHeight;

        /// <param name="maxHeight">The maximum height.</param>
        public SkipList(int maxHeight = 32)
        {
            MaxHeight = maxHeight;
            Head = new SkipListNode<T>()
            {
                Prev = null,
                Next = new SkipListNode<T>[maxHeight],
                value = default(T)
            };
        }

        /// <summary>
        /// Finds the given value in this skip list.
        /// If item is not found default value of T will be returned.
        /// Time complexity: O(log(n)).
        /// </summary>
        public T Find(T value)
        {
            var current = Head;
            //for each level of linked list from Top
            for (int i = MaxHeight - 1; i >= 0; i--)
            {
                //iterate through the list until match
                //or with an itemless that match
                while (true)
                {
                    if (current.Next[i] != null
                        && current.Next[i].value.CompareTo(value) == 0)
                    {
                        return current.Next[i].value;
                    }

                    if (current.Next[i] == null
                        || current.Next[i].value.CompareTo(value) > 0)
                    {
                        break;
                    }

                    //update current
                    //so that in next level we can start search from here
                    current = current.Next[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// Inserts the given value to this skip list.
        /// Will throw exception if the value already exists.
        /// Time complexity: O(log(n))
        /// </summary>
        /// <param name="value">The value to insert.</param>
        public void Insert(T value)
        {
            if(!Find(value).Equals(default(T)))
            {
                throw new Exception("Cannot insert duplicate values.");
            }

            //find the random level up to which we link the new node
            var level = 0;
            for (int i = 0; i < MaxHeight
                && coinFlipper.Next(0, 2) == 1; i++)
            {
                level++;
            }

            var newNode = new SkipListNode<T>()
            {
                value = value,
                //only level + 1 number of links (level is zero index based)
                Next = new SkipListNode<T>[level + 1]
            };

            //init current to head before insertion
            var current = Head;

            //go down from top level
            for (int i = MaxHeight - 1; i >= 0; i--)
            {
                //move on current level of linked list
                //until next element is less than new value to insert
                while (true)
                {
                    if (current.Next[i] == null
                        || current.Next[i].value.CompareTo(value) > 0)
                    {

                        break;
                    }

                    current = current.Next[i];
                }

                //if this level is greater than 
                //maximum levels of link for new node
                //then jump to next level
                if (i > level)
                {
                    continue;
                }

                //insert and update pointers
                newNode.Next[i] = current.Next[i];
                current.Next[i] = newNode;

                //for base level set previous node
                if (i == 0)
                {
                    newNode.Prev = current;
                }
            }

            Count++;
        }

        /// <summary>
        /// Deletes the given value from this skip list.
        /// Will throw exception if the value does'nt exist in this skip list.
        /// Time complexity: O(log(n))
        /// </summary>
        /// <param name="value"> The value to delete.</param>
        public void Delete(T value)
        {
            //init current to head before insertion
            var current = Head;

            //go down from top level
            for (var i = MaxHeight - 1; i >= 0; i--)
            {
                //move on current level of linked list
                //until next element is less than new value to delete
                while (true)
                {
                    if (current.Next[i] == null
                        || current.Next[i].value.CompareTo(value) >= 0)
                    {
                        break;
                    }

                    current = current.Next[i];
                }

                //item not found
                if (i == 0 && current.Next[i].value.CompareTo(value) != 0)
                {
                    throw new Exception("Item to delete was not found in this skip list.");
                }

                if (current.Next[i] != null
                        && current.Next[i].value.CompareTo(value) == 0)
                {
                    //for base level set previous node
                    if (i == 0 && current.Next[i].Next[i] != null)
                    {
                        current.Next[i].Next[i].Prev = current;
                    }

                    //insert and update pointers
                    current.Next[i] = current.Next[i].Next[i];
                }
            }

            Count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SkipListEnumerator<T>(Head);
        }
    }

    internal class SkipListNode<T> where T : IComparable
    {
        internal SkipListNode<T> Prev { get; set; }
        internal SkipListNode<T>[] Next { get; set; }

        internal T value { get; set; }
    }

    internal class SkipListEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private SkipListNode<T> head;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private SkipListNode<T> current;

        internal SkipListEnumerator(SkipListNode<T> head)
        {
            this.head = head;
            this.current = head;
        }

        public bool MoveNext()
        {
            if (current.Next[0] != null)
            {
                current = current.Next[0];
                return true;
            }

            return false;

        }

        public void Reset()
        {
            current = head;
        }

        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                return current.value;
            }
        }

        public void Dispose()
        {
            head = null;
            current = null;
        }
    }
}
