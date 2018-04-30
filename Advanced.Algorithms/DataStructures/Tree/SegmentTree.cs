using System;

namespace Advanced.Algorithms.DataStructures
{
    public class SegmentTree<T>
    {
        private readonly int length;
        private readonly T[] segmentTree;

        /// <summary>
        /// Example operations Sum, Min, Max
        /// </summary>
        private readonly Func<T, T, T> operation;

        /// <summary>
        /// default value to eliminate node during range search
        /// default value for Sum operation is 0
        /// default value for Min operation is Max Value (int.Max if T is int)
        /// default value for Max operation is Min Value(int.Min if T is int) 
        /// </summary>
        private readonly Func<T> defaultValue;

        /// <summary>
        /// constructs a segment tree using the specified operation function
        /// Operation function is the criteria for range queries
        /// For example operation function can return Max, Min or Sum of the two input elements
        /// Default value is a void value that will eliminate a node during operation comparisons
        /// For example if operation return min value default value will be largest value (int.Max for if T is int)
        /// or default value will be 0 if operation is sum
        /// </summary>
        /// <param name="input"></param>
        /// <param name="operation"></param>
        /// <param name="defaultValue"></param>
        public SegmentTree(T[] input, Func<T, T, T> operation, Func<T> defaultValue)
        {
            if (input == null || operation == null)
            {
                throw new ArgumentNullException();
            }

            var maxHeight = Math.Ceiling(Math.Log(input.Length, 2));
            var maxTreeNodes = 2 * (int)(Math.Pow(2, maxHeight)) - 1;
            segmentTree = new T[maxTreeNodes];
            this.operation = operation;
            this.defaultValue = defaultValue;

            length = input.Length;

            construct(input, 0, input.Length - 1, 0);
        }

        private T construct(T[] input, int left, int right, int currentIndex)
        {
            if (left == right)
            {
                segmentTree[currentIndex] = input[left];
                return segmentTree[currentIndex];
            }

            var midIndex = getMidIndex(left, right);

            segmentTree[currentIndex] = operation(construct(input, left, midIndex, 2 * currentIndex + 1),
                 construct(input, midIndex + 1, right, 2 * currentIndex + 2));

            return segmentTree[currentIndex];
        }

        public T GetRangeResult(int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > length - 1
                 || endIndex < startIndex)
            {
                throw new ArgumentException();
            }

            return getRangeResult(startIndex, endIndex, 0, length - 1, 0);

        }

        private T getRangeResult(int start, int end, int left, int right, int currentIndex)
        {
            //total overlap so return the value
            if (left >= start && right <= end)
            {
                return segmentTree[currentIndex];
            }

            //no overlap, so return default
            if (right < start || left > end)
            {
                return defaultValue();
            }

            //partial overlap so dig in
            var midIndex = getMidIndex(left, right);
            return operation(getRangeResult(start, end, left, midIndex, 2 * currentIndex + 1),
                             getRangeResult(start, end, midIndex + 1, right, 2 * currentIndex + 2));

        }


        private int getMidIndex(int left, int right)
        {
            return left + ((right - left) / 2);
        }

    }
}
