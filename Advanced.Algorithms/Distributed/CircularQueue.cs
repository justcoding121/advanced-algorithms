using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Distributed
{
    /// <summary>
    /// Cicular queue aka Ring Buffer using fixed size array.
    /// </summary>
    public class CircularQueue<T>
    {
        private T[] queue;

        //points to the index of next element to be deleted
        private int start;

        //points to the index new element should be inserted
        private int end;

        public int Count { get; private set; }

        public CircularQueue(int size)
        {
            queue = new T[size];
        }

        /// <summary>
        /// Note: When buffer overflows oldest data will be erased.
        /// Time complexity: O(1) 
        /// </summary>
        public T Enqueue(T data)
        {
            var deleted = default(T);

            //wrap around removing oldest element
            if (end > queue.Length - 1)
            {
                end = 0;

                if (start == 0)
                {
                    deleted = queue[start];
                    start++;
                }
            }

            //when end meets start after wraping around
            if (end == start && Count > 1)
            {
                deleted = queue[start];
                start++;
            }

            queue[end] = data;
            end++;

            if (Count < queue.Length)
            {
                Count++;
            }

            return deleted;
        }

        /// <summary>
        /// Time complexity: O(n). 
        /// </summary>
        /// <returns>Deleted items.</returns>
        public IEnumerable<T> Enqueue(T[] bulk)
        {
            return bulk.Select(item => Enqueue(item))
                .Where(deleted => !deleted.Equals(default(T))).ToList();
        }

        /// <summary>
        /// O(1) Time complexity.
        /// </summary>
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

        /// <summary>
        /// Time complexity: O(n). 
        /// </summary>
        public IEnumerable<T> Dequeue(int bulkNumber)
        {
            var deletedList = new List<T>();
            while (bulkNumber > 0 && Count > 0)
            {
                var deleted = Dequeue();

                if (!deleted.Equals(default(T)))
                {
                    deletedList.Add(deleted);
                }

                bulkNumber--;
            }

            return deletedList;
        }

    }

}