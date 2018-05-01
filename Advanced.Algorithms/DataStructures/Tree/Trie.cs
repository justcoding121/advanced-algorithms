using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    internal class TrieNode<T>
    {
        internal bool IsEmpty => Children.Count == 0;
        internal bool IsEnd { get; set; }
        internal System.Collections.Generic.Dictionary<T, TrieNode<T>> Children { get; set; }

        internal TrieNode()
        {
            Children = new System.Collections.Generic.Dictionary<T, TrieNode<T>>();
        }

    }

    //TODO support initial  bulk loading if possible
    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    public class Trie<T>
    {
        internal TrieNode<T> Root { get; set; }
        public int Count { get; private set; }

        public Trie()
        {
            Root = new TrieNode<T>();
            Count = 0;
        }

        /// <summary>
        /// Insert a new record to this trie
        /// O(m) time complexity where m is the length of entry
        /// </summary>
        /// <param name="entry"></param>
        public void Insert(T[] entry)
        {
            insert(Root, entry, 0);
            Count++;
        }

        /// <summary>
        /// Insert a new record to this trie after finding the end recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void insert(TrieNode<T> currentNode, T[] entry, int currentIndex)
        {
            while (true)
            {
                if (currentIndex == entry.Length)
                {
                    currentNode.IsEnd = true;
                    return;
                }

                if (currentNode.Children.ContainsKey(entry[currentIndex]) == false)
                {
                    var newNode = new TrieNode<T>();
                    currentNode.Children.Add(entry[currentIndex], newNode);
                    currentNode = newNode;
                    currentIndex = currentIndex + 1;
                }
                else
                {
                    currentNode = currentNode.Children[entry[currentIndex]];
                    currentIndex = currentIndex + 1;
                }
            }
        }

        /// <summary>
        /// deletes a record from this trie
        /// O(m) where m is the length of entry
        /// </summary>
        /// <param name="entry"></param>
        public void Delete(T[] entry)
        {
            delete(Root, entry, 0);
            Count--;
        }

        /// <summary>
        /// deletes a record from this trie after finding it recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void delete(TrieNode<T> currentNode, T[] entry, int currentIndex)
        {
            if (currentIndex == entry.Length)
            {
                if (!currentNode.IsEnd)
                {
                    throw new Exception("Item not in trie.");
                }

                currentNode.IsEnd = false;
                return;
            }

            if (currentNode.Children.ContainsKey(entry[currentIndex]) == false)
            {
                throw new Exception("Item not in trie.");
            }

            delete(currentNode.Children[entry[currentIndex]], entry, currentIndex + 1);

            if (currentNode.Children[entry[currentIndex]].IsEmpty 
                && !currentNode.IsEnd)
            {
                currentNode.Children.Remove(entry[currentIndex]);
            }
        }

        /// <summary>
        /// returns a list of records matching this prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<T[]> StartsWith(T[] prefix)
        {
            return startsWith(Root, prefix, 0);
        }

        /// <summary>
        /// recursively visit until end of prefix 
        /// and then gather all sub entries under it
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private List<T[]> startsWith(TrieNode<T> currentNode, T[] searchPrefix, int currentIndex)
        {
            while (true)
            {
                if (currentIndex == searchPrefix.Length)
                {
                    var result = new List<T[]>();

                    //gather sub entries and prefix them with search entry prefix
                    gatherStartsWith(result, searchPrefix, null, currentNode);

                    return result;
                }

                if (currentNode.Children.ContainsKey(searchPrefix[currentIndex]) == false)
                    return new List<T[]>();

                currentNode = currentNode.Children[searchPrefix[currentIndex]];
                currentIndex = currentIndex + 1;
            }
        }

        /// <summary>
        /// Gathers all suffixes under this node appending with the given prefix
        /// </summary>
        /// <param name="result"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="suffix"></param>
        /// <param name="node"></param>
        private void gatherStartsWith(List<T[]> result, T[] searchPrefix, T[] suffix,
            TrieNode<T> node)
        {
            //end of word
            if (node.IsEnd)
            {
                if(suffix !=null)
                {
                    //append to end of prefix for new prefix
                    var newPrefix = new T[searchPrefix.Length + suffix.Length];
                    Array.Copy(searchPrefix, newPrefix, searchPrefix.Length);
                    Array.Copy(suffix, 0, newPrefix, searchPrefix.Length, suffix.Length);

                    result.Add(newPrefix);
                }
                else
                {
                    result.Add(searchPrefix);
                }
             
            }

            //visit all children
            foreach (var child in node.Children)
            {
                if (suffix != null)
                {
                    //append to end of prefix for new prefix
                    var newPrefix = new T[suffix.Length + 1];
                    Array.Copy(suffix, newPrefix, suffix.Length);
                    newPrefix[newPrefix.Length - 1] = child.Key;
                    gatherStartsWith(result, searchPrefix, newPrefix, child.Value);
                }
                else
                {
                    var newPrefix = new T[1];
                    newPrefix[0] = child.Key;
                    gatherStartsWith(result, searchPrefix, newPrefix, child.Value);
                }
               
            }
        }

        /// <summary>
        /// returns true if the entry exist
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool Contains(T[] entry)
        {
            return contains(Root, entry, 0);
        }

        /// <summary>
        /// Find if the record exist recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private bool contains(TrieNode<T> currentNode, T[] entry, int currentIndex)
        {
            while (true)
            {
                if (currentIndex == entry.Length)
                {
                    return currentNode.IsEnd;
                }

                if (currentNode.Children.ContainsKey(entry[currentIndex]) == false)
                {
                    return false;
                }

                currentNode = currentNode.Children[entry[currentIndex]];
                currentIndex = currentIndex + 1;
            }
        }
    }
}
