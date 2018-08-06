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

    //  implement IEnumerator.
    internal class BSTNodeLookUpEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly Dictionary<T, BSTNodeBase<T>> nodeLookUp;
        private Dictionary<T, BSTNodeBase<T>>.Enumerator enumerator;
        private T current;

        internal BSTNodeLookUpEnumerator(Dictionary<T, BSTNodeBase<T>> nodeLookUp)
        {
            this.nodeLookUp = nodeLookUp;
            enumerator = nodeLookUp.GetEnumerator();
        }

        public bool MoveNext()
        {
            if (nodeLookUp == null)
            {
                return false;
            }

            if (enumerator.MoveNext())
            {
                current = enumerator.Current.Key;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            enumerator = nodeLookUp.GetEnumerator();
        }

        public T Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            enumerator.Dispose();
        }
    }
}
