using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.NumericalMethods
{
    /// <summary>
    /// Keeps median of given stream 
    /// An online algorithm
    /// </summary>
    public class MedianStream
    {
        private BMaxHeap<int> leftHeap = new BMaxHeap<int>();
        private BMinHeap<int> rightHeap = new BMinHeap<int>();

        /// <summary>
        /// Add a new item to stream
        /// </summary>
        /// <param name="newValue"></param>
        public void Add(int newValue)
        {
            var median = GetMedian();

            //our goal is to keep the balance factor atmost to be <=1
            if (leftHeap.Count == rightHeap.Count)
            {
                if (newValue < median)
                {
                    leftHeap.Insert(newValue);
                }
                else
                {
                    rightHeap.Insert(newValue);
                }

                return;
            }
            //left has more elements
            else if (leftHeap.Count > rightHeap.Count)
            {
                //we can't increase left count > 1
                //So borrow top to right first
                if (newValue < median)
                {
                    rightHeap.Insert(leftHeap.ExtractMax());
                    leftHeap.Insert(newValue);
                }
                else
                {
                    rightHeap.Insert(newValue);
                }

            }
            //right has more elements
            else if(rightHeap.Count > leftHeap.Count)
            {
                //we can't increase right count > 1
                //So borrow top to left first
                if (newValue > median)
                {
                    leftHeap.Insert(rightHeap.ExtractMin());
                    rightHeap.Insert(newValue);
                }
                else
                {
                    leftHeap.Insert(newValue);
                }
            }
        }

        /// <summary>
        /// Returns the current median
        /// </summary>
        /// <returns></returns>
        public int GetMedian()
        {
            //default
            if (leftHeap.Count == 0
                && rightHeap.Count == 0)
            {
                return 0;
            }

            //take average of tops if equal count
            if (leftHeap.Count == rightHeap.Count)
            {
                return (leftHeap.PeekMax() + rightHeap.PeekMin()) / 2;
            }

            //pick left top if left heap has greater count
            if (leftHeap.Count > rightHeap.Count)
            {
                return leftHeap.PeekMax();
            }

            //pick right top otherwise
            return rightHeap.PeekMin();
        }
    }
}
