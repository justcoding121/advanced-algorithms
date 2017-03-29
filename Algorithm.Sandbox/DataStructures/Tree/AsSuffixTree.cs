using System;

namespace Algorithm.Sandbox.DataStructures
{
    /// <summary>
    /// A simple suffix tree implementation using a trie
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsSuffixTree<T>
    {
        private AsTrie<T> trie;
        public int Count { private set; get; }

        public AsSuffixTree()
        {
            trie = new AsTrie<T>();
            Count = 0;
        }

        /// <summary>
        /// insert a new entry to suffix tree
        /// O(m^2) complexity if m is the length of entry array
        /// </summary>
        /// <param name="entry"></param>
        public void Insert(T[] entry)
        {
            if (entry == null)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < entry.Length; i++)
            {
                var suffix = new T[entry.Length - i];
                Array.Copy(entry, i, suffix, 0, entry.Length - i);

                trie.Insert(suffix);
            }

            Count++;
        }

        /// <summary>
        /// deletes an entry from this suffix tree
        /// O(m^2) complexity if m is the length of the entry to be deleted
        /// </summary>
        /// <param name="entry"></param>
        public void Delete(T[] entry)
        {
            if (entry == null)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < entry.Length; i++)
            {
                var suffix = new T[entry.Length - i];
                Array.Copy(entry, i, suffix, 0, entry.Length - i);

                trie.Delete(suffix);
            }
            Count--;
        }

        /// <summary>
        /// returns if the entry pattern is in this suffix tree
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool ContainsPattern(T[] entry)
        {
            return trie.StartsWith(entry).Length > 0;
        }

        /// <summary>
        /// returns all sub entries that starts with this search pattern
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public AsArrayList<T[]> StartsWithPattern(T[] entry)
        {
            return trie.StartsWith(entry);
        }
    }
}
