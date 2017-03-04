using System;


namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// Interval object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsIntervalTreeNode<T> : IComparable where T : IComparable
    {
        /// <summary>
        /// Start of this interval range
        /// </summary>
        public T Start { get; set; }

        /// <summary>
        /// End of this interval range
        /// </summary>
        public T End { get; set; }

        /// <summary>
        /// Max End interval under this interval
        /// Which would be in the rightmost sub node of BST
        /// </summary>
        internal T MaxChild { get; set; }

        public int CompareTo(object obj)
        {
            return this.Start.CompareTo((obj as AsIntervalTreeNode<T>).Start);
        }
    }

    /// <summary>
    /// An interval tree implementation
    /// TODO support interval start range that collide
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsIntervalTree<T> where T : IComparable
    {
        internal AsRedBlackTree<AsIntervalTreeNode<T>> bst
            = new AsRedBlackTree<AsIntervalTreeNode<T>>();

        /// <summary>
        /// Insert a new Interval
        /// </summary>
        /// <param name="newInterval"></param>
        public void Insert(AsIntervalTreeNode<T> newInterval)
        {
            bst.Insert(newInterval);

            //TODO
            //update max nodes under this new node
            var newNode = bst.Find(newInterval);
        }

        /// <summary>
        /// Delete this interval
        /// </summary>
        /// <param name="interval"></param>
        public void Delete(AsIntervalTreeNode<T> interval)
        {
            bst.Delete(interval);
        
            //TODO
            //Update max nodes under the deleted node if it was replaced by a leaf in BST
        }

        /// <summary>
        /// Returns an interval that overlaps with this interval
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public AsIntervalTreeNode<T> GetOverlap(AsIntervalTreeNode<T> interval)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does this interval a overlap with b 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool doOverlap(AsIntervalTreeNode<T> a, AsIntervalTreeNode<T> b)
        {
            throw new NotImplementedException();
        }
    }
}
