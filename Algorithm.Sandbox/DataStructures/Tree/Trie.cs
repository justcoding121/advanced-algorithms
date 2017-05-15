using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    internal class TrieNode<T>
    {
        internal bool IsEmpty => Children.Count == 0;
        internal bool IsEnd { get; set; }
        internal Dictionary<T, TrieNode<T>> Children { get; set; }

        internal TrieNode()
        {
            Children = new Dictionary<T, TrieNode<T>>();
        }

    }

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
            Insert(Root, entry, 0);
            Count++;
        }

        /// <summary>
        /// Insert a new record to this trie after finding the end recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void Insert(TrieNode<T> currentNode, T[] entry, int currentIndex)
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
                Insert(newNode, entry, currentIndex + 1);
            }
            else
            {
                Insert(currentNode.Children[entry[currentIndex]], entry, currentIndex + 1);
            }
        }

        /// <summary>
        /// deletes a record from this trie
        /// O(m) where m is the length of entry
        /// </summary>
        /// <param name="entry"></param>
        public void Delete(T[] entry)
        {
            Delete(Root, entry, 0);
            Count--;
        }

        /// <summary>
        /// deletes a record from this trie after finding it recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void Delete(TrieNode<T> currentNode, T[] entry, int currentIndex)
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
            else
            {
                Delete(currentNode.Children[entry[currentIndex]], entry, currentIndex + 1);
            }

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
            return StartsWith(Root, prefix, 0);
        }

        /// <summary>
        /// recursively visit until end of prefix 
        /// and then gather all sub entries under it
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private List<T[]> StartsWith(TrieNode<T> currentNode, T[] searchPrefix, int currentIndex)
        {
            if (currentIndex == searchPrefix.Length)
            {
                var result = new List<T[]>();

                //gather sub entries and prefix them with search entry prefix
                GatherStartsWith(result , searchPrefix, null, currentNode);

                return result;
            }

            if (currentNode.Children.ContainsKey(searchPrefix[currentIndex]) == false)
            {
                return new List<T[]>();
            }
            else
            {
                return StartsWith(currentNode.Children[searchPrefix[currentIndex]], searchPrefix, currentIndex + 1);
            }
        }

        /// <summary>
        /// Gathers all suffixes under this node appending with the given prefix
        /// </summary>
        /// <param name="result"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="suffix"></param>
        /// <param name="node"></param>
        private void GatherStartsWith(List<T[]> result, T[] searchPrefix, T[] suffix,
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
                    GatherStartsWith(result, searchPrefix, newPrefix, child.Value);
                }
                else
                {
                    var newPrefix = new T[1];
                    newPrefix[0] = child.Key;
                    GatherStartsWith(result, searchPrefix, newPrefix, child.Value);
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
            return Contains(Root, entry, 0);
        }

        /// <summary>
        /// Find if the record exist recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private bool Contains(TrieNode<T> currentNode, T[] entry, int currentIndex)
        {
            if (currentIndex == entry.Length)
            {
                if (!currentNode.IsEnd)
                {
                    return false;
                }

                return true;
            }

            if (currentNode.Children.ContainsKey(entry[currentIndex]) == false)
            {
                return false;
            }
            else
            {
                return Contains(currentNode.Children[entry[currentIndex]], entry, currentIndex + 1);
            }
        }
    }
}
