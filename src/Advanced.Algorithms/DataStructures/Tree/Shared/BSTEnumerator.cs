using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    //  implement IEnumerator.
    internal class BSTEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly BSTNodeBase<T> root;
        private BSTNodeBase<T> current;

        internal BSTEnumerator(BSTNodeBase<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null)
            {
                return false;
            }

            if (current == null)
            {
                current = root.FindMin();
                return true;
            }

            var next = current.NextHigher();
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
