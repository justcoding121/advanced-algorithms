using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Distributed
{
    /// <summary>
    ///     A simple asynchronous multi-thread supporting producer/consumer FIFO queue with minimal locking.
    /// </summary>
    public class AsyncQueue<T>
    {
        //data queue.
        private readonly Queue<T> queue = new Queue<T>();

        //consumer task queue and lock.
        private readonly Queue<TaskCompletionSource<T>> consumerQueue = new Queue<TaskCompletionSource<T>>();
        private SemaphoreSlim consumerQueueLock = new SemaphoreSlim(1);

        public int Count => queue.Count;

        /// <summary>
        ///     Supports multi-threaded producers.
        ///     Time complexity: O(1).
        /// </summary>
        public async Task EnqueueAsync(T value, int millisecondsTimeout = int.MaxValue, CancellationToken taskCancellationToken = default(CancellationToken))
        {
            await consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

            if(consumerQueue.Count > 0)
            {
                var consumer = consumerQueue.Dequeue();
                consumer.TrySetResult(value);
            }
            else
            {
                queue.Enqueue(value);
            }

            consumerQueueLock.Release();
        }

        /// <summary>
        ///      Supports multi-threaded consumers.
        ///      Time complexity: O(1).
        /// </summary>
        public async Task<T> DequeueAsync(int millisecondsTimeout = int.MaxValue, CancellationToken taskCancellationToken = default(CancellationToken))
        {
            await consumerQueueLock.WaitAsync(millisecondsTimeout, taskCancellationToken);

            TaskCompletionSource<T> consumer;

            try
            {
                if (queue.Count > 0)
                {
                    var result = queue.Dequeue();
                    return result;
                }

                consumer = new TaskCompletionSource<T>();
                taskCancellationToken.Register(() => consumer.TrySetCanceled());
                consumerQueue.Enqueue(consumer);
            }
            finally
            {
                consumerQueueLock.Release();
            }

            return await consumer.Task;

        }
    }
}
