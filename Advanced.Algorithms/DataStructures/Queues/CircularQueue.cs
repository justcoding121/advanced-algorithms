using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced.Algorithms.DataStructures.Queues
{
    /// <summary>
    /// Cicular queue aka Ring Buffer using fixed size array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularQueue<T>
    {
        private T[] queue;

        //points to the index of next element to be deleted
        private int start = 0;

        //points to the index new element should be inserted
        private int end = 0;

        public int Count { get; private set; }

        public CircularQueue(int size)
        {
            queue = new T[size];
        }

        /// <summary>
        /// Note: When buffer overflows oldest data will be erased
        /// </summary>
        /// <param name="data"></param>
        public void Enqueue(T data)
        {
            //wrap around removing oldest element
            if (end > queue.Length - 1)
            {
                end = 0;

                if(start == 0)
                {
                    start++;
                }
            }

            //when end meets start after wraping around
            if (end == start && Count > 1)
            {
                start++;
            }

            queue[end] = data;
            end++;

            if (Count < queue.Length)
            {
                Count++;
            }
        }


        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new Exception("Empty queue.");
            }

            var element = queue[start];
            start++;

            //wrap around 
            if (start > queue.Length - 1)
            {
                start = 0;

                if (end == 0)
                {
                    end++;
                }
            }

            Count--;

            if (start == end && Count > 1)
            {
                end++;
            }

            //reset
            if (Count == 0)
            {
                start = end = 0;
            }

            return element;
        }
    }
}
