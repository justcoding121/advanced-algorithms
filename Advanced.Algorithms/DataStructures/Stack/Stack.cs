using System;

namespace Advanced.Algorithms.DataStructures
{
    internal interface IStack<T>
    {
        int Count { get; }
        T Pop();
        void Push(T item);

        T Peek();
    }

    public enum StackType
    {
        Array = 0,
        LinkedList = 1
    }
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    public class AsStack<T>
    {
        private readonly IStack<T> stack;

        public int Count => stack.Count;

        public AsStack(StackType type = StackType.Array)
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

        //O(1)
        public T Pop()
        {
            return stack.Pop();
        }

        //O(1)
        public void Push(T item)
        {
            stack.Push(item);
        }

        //O(1)
        public T Peek()
        {
            return stack.Peek();
        }

    }
}
