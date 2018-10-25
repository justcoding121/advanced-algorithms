using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    //  implement IEnumerator.
    internal class BSTEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly bool asc;

        private readonly BSTNodeBase<T> root;
        private BSTNodeBase<T> current;

        internal BSTEnumerator(BSTNodeBase<T> root, bool asc = true)
        {
            this.root = root;
            this.asc = asc;
        }

        public bool MoveNext()
        {
            if (root == null)
            {
                return false;
            }

            if (current == null)
            {
                current = asc ? root.FindMin() : root.FindMax();
                return true;
            }

            var next = asc ? current.NextHigher() : current.NextLower();
            if (next != null)
            {
                current = next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            current = root;
        }

        public T Current
        {
            get
            {
                return current.Value;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            current = null;
        }
    }

}
