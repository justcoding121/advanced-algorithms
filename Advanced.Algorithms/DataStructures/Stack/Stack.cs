using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures.Foundation
{
    /// <summary>
    /// A stack implementation.
    /// </summary>
    public class Stack<T> : IEnumerable<T>
    {
        private readonly IStack<T> stack;

        /// <summary>
        /// The total number of items in this stack.
        /// </summary>
        public int Count => stack.Count;

        /// <param name="type">The stack type to use.</param>
        public Stack(StackType type = StackType.Array)
        {
            if (type == StackType.Array)
            {
                stack = new ArrayStack<T>();
            }
            else
            {
               stack = new LinkedListStack<T>();
            }
        }

        /// <summary>
        /// Time complexity:O(1).
        /// </summary>
        /// <returns>The item popped.</returns>
        public T Pop()
        {
            return stack.Pop();
        }

        /// <summary>
        /// Time complexity:O(1).
        /// </summary>
        /// <param name="item">The item to push.</param>
        public void Push(T item)
        {
            stack.Push(item);
        }

        /// <summary>
        /// Peek from stack.
        /// Time complexity:O(1).
        /// </summary>
        /// <returns>The item peeked.</returns>
        public T Peek()
        {
            return stack.Peek();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return stack.GetEnumerator();
        }
    }

    internal interface IStack<T> : IEnumerable<T>
    {
        int Count { get; }
        T Pop();
        void Push(T item);

        T Peek();
    }

    /// <summary>
    /// The stack implementation types.
    /// </summary>
    public enum StackType
    {
        Array = 0,
        LinkedList = 1
    }

}
