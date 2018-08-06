using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A trie (prefix tree) implementation.
    /// </summary>
    public class Trie<T> : IEnumerable<T[]>
    {
        private TrieNode<T> root { get; set; }

        public int Count { get; private set; }

        public Trie()
        {
            root = new TrieNode<T>(null, default(T));
            Count = 0;
        }

        /// <summary>
        /// Insert a new record to this trie.
        /// Time complexity: O(m) where m is the length of entry.
        /// </summary>
        public void Insert(T[] entry)
        {
            insert(root, entry, 0);
            Count++;
        }

        /// <summary>
        /// Insert a new record to this trie after finding the end recursively.
        /// </summary>
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
                    var newNode = new TrieNode<T>(currentNode, entry[currentIndex]);
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
        /// Deletes a record from this trie.
        /// Time complexity: O(m) where m is the length of entry.
        /// </summary>
        public void Delete(T[] entry)
        {
            delete(root, entry, 0);
            Count--;
        }

        /// <summary>
        /// Deletes a record from this trie after finding it recursively.
        /// </summary>
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
        /// Returns a list of records matching this prefix.
        /// Time complexity: O(rm) where r is the number of results and m is the average length of each entry.
        /// </summary>
        public List<T[]> StartsWith(T[] prefix)
        {
            return startsWith(root, prefix, 0);
        }

        /// <summary>
        /// Recursively visit until end of prefix 
        /// and then gather all sub entries under it.
        /// </summary>
        private List<T[]> startsWith(TrieNode<T> currentNode, T[] searchPrefix, int currentIndex)
        {
            while (true)
            {
                if (currentIndex == searchPrefix.Length)
                {
                    var result = new List<T[]>();

                    //gather sub entries and prefix them with search entry prefix
                    gatherStartsWith(result, searchPrefix, new List<T>(), currentNode);

                    return result;
                }

                if (currentNode.Children.ContainsKey(searchPrefix[currentIndex]) == false)
                {
                    return new List<T[]>();
                }

                currentNode = currentNode.Children[searchPrefix[currentIndex]];
                currentIndex = currentIndex + 1;
            }
        }

        /// <summary>
        /// Gathers all suffixes under this node appending with the given prefix.
        /// </summary>
        private void gatherStartsWith(List<T[]> result, T[] searchPrefix, List<T> suffix,
            TrieNode<T> node)
        {
            //end of word
            if (node.IsEnd)
            {
                if (suffix != null)
                {
                    result.Add(searchPrefix.Concat(suffix).ToArray());
                }
                else
                {
                    result.Add(searchPrefix);
                }
            }

            //visit all children
            foreach (var child in node.Children)
            {
                //append to end of prefix for new prefix
                suffix.Add(child.Key);
                gatherStartsWith(result, searchPrefix, suffix, child.Value);
                suffix.RemoveAt(suffix.Count - 1);
            }
        }

        /// <summary>
        /// Returns true if the entry exist.
        /// Time complexity: O(e) where e is the length of the given entry.
        /// </summary>
        public bool Contains(T[] entry)
        {
            return contains(root, entry, 0, false);
        }

        /// <summary>
        /// Returns true if any records match this prefix.
        /// Time complexity: O(e) where e is the length of the given entry.
        /// </summary>
        public bool ContainsPrefix(T[] prefix)
        {
            return contains(root, prefix, 0, true);
        }

        /// <summary>
        /// Find if the record exist recursively.
        /// </summary>
        private bool contains(TrieNode<T> currentNode, T[] entry, int currentIndex, bool isPrefixSearch)
        {
            while (true)
            {
                if (currentIndex == entry.Length)
                {
                    return isPrefixSearch || currentNode.IsEnd;
                }

                if (currentNode.Children.ContainsKey(entry[currentIndex]) == false)
                {
                    return false;
                }

                currentNode = currentNode.Children[entry[currentIndex]];
                currentIndex = currentIndex + 1;
            }
        }
      
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return new TrieEnumerator<T>(root);
        }

    }

    internal class TrieNode<T>
    {
        internal bool IsEmpty => Children.Count == 0;
        internal bool IsEnd { get; set; }
        internal TrieNode<T> Parent { get; set; }
        internal Dictionary<T, TrieNode<T>> Children { get; set; }
        internal T Value { get; set; }

        internal TrieNode(TrieNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            Children = new Dictionary<T, TrieNode<T>>();
        }
    }

    internal class TrieEnumerator<T> : IEnumerator<T[]> 
    {
        private readonly TrieNode<T> root;
        private Stack<TrieNode<T>> progress;

        internal TrieEnumerator(TrieNode<T> root)
        {
            this.root = root;
        }

        public bool MoveNext()
        {
            if (root == null)
            {
                return false;
            }

            if (progress == null)
            {
                progress = new Stack<TrieNode<T>>(root.Children.Select(x => x.Value));
            }

            while (progress.Count > 0)
            {
                var next = progress.Pop();

                foreach (var child in next.Children)
                {
                    progress.Push(child.Value);
                }

                if (next.IsEnd)
                {
                    Current = getValue(next);
                    return true;
                }
            }

            return false;
        }

        private T[] getValue(TrieNode<T> next)
        {
            var result = new Stack<T>();
            result.Push(next.Value);

            while (next.Parent!=null && !next.Parent.Value.Equals(default(T)))
            {
                next = next.Parent;
                result.Push(next.Value);
            }

            return result.ToArray();
        }

        public void Reset()
        {
            progress = null;
            Current = null;
        }

        public T[] Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            progress = null;
        }
    }
}
