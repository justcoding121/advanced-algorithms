using System;
using System.Collections;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A suffix tree implementation using a trie.
    /// </summary>
    public class SuffixTree<T> : IEnumerable<T[]>
    {
        private Trie<T> trie;
        public int Count { private set; get; }
        private HashSet<T[]> items = new HashSet<T[]>(new ArrayComparer<T>());

        public SuffixTree()
        {
            trie = new Trie<T>();
            Count = 0;
        }

        /// <summary>
        /// Insert a new entry to this suffix tree.
        /// Time complexity: O(m^2) where m is the length of entry array.
        /// </summary>
        public void Insert(T[] entry)
        {
            if (entry == null)
            {
                throw new ArgumentException();
            }

            if (items.Contains(entry))
            {
                throw new Exception("Item exists.");
            }

            for (var i = 0; i < entry.Length; i++)
            {
                var suffix = new T[entry.Length - i];
                Array.Copy(entry, i, suffix, 0, entry.Length - i);

                trie.Insert(suffix);
            }

            items.Add(entry);

            Count++;
        }

        /// <summary>
        /// Deletes an existing entry from this suffix tree.
        /// Time complexity: O(m^2) where m is the length of entry array.
        /// </summary>
        public void Delete(T[] entry)
        {
            if (entry == null)
            {
                throw new ArgumentException();
            }

            if (!items.Contains(entry))
            {
                throw new Exception("Item does'nt exist.");
            }

            for (var i = 0; i < entry.Length; i++)
            {
                var suffix = new T[entry.Length - i];
                Array.Copy(entry, i, suffix, 0, entry.Length - i);

                trie.Delete(suffix);
            }

            items.Remove(entry);

            Count--;
        }

        /// <summary>
        /// Returns true if the given entry pattern is in this suffix tree.
        /// Time complexity: O(e) where e is the length of the given entry.
        /// </summary>
        public bool Contains(T[] pattern)
        {
            return trie.ContainsPrefix(pattern);
        }

        /// <summary>
        /// Returns all sub-entries that starts with this search pattern.
        /// Time complexity: O(rm) where r is the number of results and m is the average length of each entry.
        /// </summary>
        public List<T[]> StartsWith(T[] pattern)
        {
            return trie.StartsWith(pattern);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
