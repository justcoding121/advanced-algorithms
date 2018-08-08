using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A ternary search tree implementation.
    /// </summary>
    public class TernarySearchTree<T> : IEnumerable<T[]> where T : IComparable
    {
        private TernarySearchTreeNode<T> root;

        public int Count { get; private set; }

        public TernarySearchTree()
        {
            Count = 0;
        }

        /// <summary>
        /// Time complexity: O(m) where m is the length of entry.
        /// </summary>
        public void Insert(T[] entry)
        {
            insert(ref root, null, entry, 0);
            Count++;
        }

        /// <summary>
        /// Insert a new record to this ternary search tree after finding the end recursively.
        /// </summary>
        private void insert(ref TernarySearchTreeNode<T> currentNode,
            TernarySearchTreeNode<T> parent,
            T[] entry, int currentIndex)
        {

            //create new node if empty
            if (currentNode == null)
            {
                currentNode = new TernarySearchTreeNode<T>(parent, entry[currentIndex]);
            }

            var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);

            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                var left = currentNode.Left;
                insert(ref left, parent, entry, currentIndex);
                currentNode.Left = left;
            }
            else if (compareResult < 0)
            {
                //move right
                var right = currentNode.Right;
                insert(ref right, parent, entry, currentIndex);
                currentNode.Right = right;
            }
            else
            {
                if (currentIndex != entry.Length - 1)
                {
                    //if equal we just skip to next element
                    var middle = currentNode.Middle;
                    insert(ref middle, currentNode, entry, currentIndex + 1);
                    currentNode.Middle = middle;
                }
                //end of word
                else
                {
                    if (currentNode.IsEnd)
                    {
                        throw new Exception("Item exists.");
                    }

                    currentNode.IsEnd = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Deletes a record from this ternary search tree.
        /// Time complexity: O(m) where m is the length of entry.
        /// </summary>
        public void Delete(T[] entry)
        {
            delete(root, entry, 0);
            Count--;
        }

        /// <summary>
        /// Deletes a record from this TernarySearchTree after finding it recursively.
        /// </summary>
        private void delete(TernarySearchTreeNode<T> currentNode,
            T[] entry, int currentIndex)
        {
            //empty node
            if (currentNode == null)
            {
                throw new Exception("Item not found.");
            }

            var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);
            TernarySearchTreeNode<T> child;
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                child = currentNode.Left;

                delete(child, entry, currentIndex);
                //delete if middle is not end
                //and we if have'nt deleted the node yet
                if (child.HasChildren == false
                    && !child.IsEnd)
                {
                    currentNode.Left = null;
                }

            }
            else if (compareResult < 0)
            {
                //move right
                child = currentNode.Right;
                delete(child, entry, currentIndex);
                //delete if middle is not end
                //and we if have'nt deleted the node yet
                if (child.HasChildren == false
                    && !child.IsEnd)
                {
                    currentNode.Right = null;
                }

            }
            else
            {
                if (currentIndex != entry.Length - 1)
                {
                    //if equal we just skip to next element
                    child = currentNode.Middle;
                    delete(child, entry, currentIndex + 1);
                    //delete if middle is not end
                    //and we if have'nt deleted the node yet
                    if (child.HasChildren == false
                        && !child.IsEnd)
                    {
                        currentNode.Middle = null;
                    }
                }
                //end of word
                else
                {
                    if (!currentNode.IsEnd)
                    {
                        throw new Exception("Item not found.");
                    }

                    //remove this end flag
                    currentNode.IsEnd = false;
                    return;
                }

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
        /// Recursively visit until end of prefix and then gather all suffixes under it.

        /// </summary>
        private List<T[]> startsWith(TernarySearchTreeNode<T> currentNode, T[] searchPrefix, int currentIndex)
        {
            while (true)
            {
                if (currentNode == null)
                {
                    return new List<T[]>();
                }

                var compareResult = currentNode.Value.CompareTo(searchPrefix[currentIndex]);
                //current is greater? move left, move right otherwise
                //if current is equal then move center
                if (compareResult > 0)
                {
                    //move left
                    currentNode = currentNode.Left;
                    continue;
                }

                if (compareResult < 0)
                {
                    //move right
                    currentNode = currentNode.Right;
                    continue;
                }

                //end of search Prefix, so gather all words under it
                if (currentIndex != searchPrefix.Length - 1)
                {
                    currentNode = currentNode.Middle;
                    currentIndex = currentIndex + 1;
                    continue;
                }

                var result = new List<T[]>();

                gatherStartsWith(result, searchPrefix.ToList(), currentNode.Middle);

                return result;

            }
        }

        /// <summary>
        /// Gathers all suffixes under this node appending with the given prefix
        /// </summary>
        private void gatherStartsWith(List<T[]> result, List<T> prefix, TernarySearchTreeNode<T> node)
        {
            while (true)
            {
                if (node == null)
                {
                    result.Add(prefix.ToArray());
                    return;
                }

                //end of word
                if (node.IsEnd)
                {
                    //append to end of prefix for new prefix
                    result.Add(prefix.Concat(new[] { node.Value }).ToArray());
                }

                if (node.Left != null)
                {
                    gatherStartsWith(result, prefix, node.Left);
                }

                if (node.Middle != null)
                {
                    //append to end of prefix for new prefix
                    prefix.Add(node.Value);
                    gatherStartsWith(result, prefix, node.Middle);
                    prefix.RemoveAt(prefix.Count - 1);
                }

                if (node.Right != null)
                {
                    node = node.Right;
                    continue;
                }

                break;
            }
        }

        /// <summary>
        /// Returns true if the entry exist.
        /// Time complexity: O(e) where e is the length of the given entry.
        /// </summary>
        public bool Contains(T[] entry)
        {
            return search(root, entry, 0, false);
        }


        /// <summary>
        /// Returns true if the entry prefix exist.
        /// Time complexity: O(e) where e is the length of the given entry.
        /// </summary>
        public bool ContainsPrefix(T[] entry)
        {
            return search(root, entry, 0, true);
        }

        /// <summary>
        /// Find if the record exist recursively.
        /// </summary>
        private bool search(TernarySearchTreeNode<T> currentNode, T[] searchEntry, int currentIndex, bool isPrefixSearch)
        {
            while (true)
            {
                //create new node if empty
                if (currentNode == null)
                {
                    return false;
                }

                //end of word, so return
                if (currentIndex == searchEntry.Length - 1)
                {
                    return isPrefixSearch || currentNode.IsEnd;
                }

                var compareResult = currentNode.Value.CompareTo(searchEntry[currentIndex]);
                //current is greater? move left, move right otherwise
                //if current is equal then move center
                if (compareResult > 0)
                {
                    //move left
                    currentNode = currentNode.Left;
                    continue;
                }

                if (compareResult < 0)
                {
                    //move right
                    currentNode = currentNode.Right;
                    continue;
                }

                //if equal we just skip to next element
                currentNode = currentNode.Middle;
                currentIndex = currentIndex + 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T[]> GetEnumerator()
        {
            return new TernarySearchTreeEnumerator<T>(root);
        }
    }

    internal class TernarySearchTreeNode<T> where T : IComparable
    {
        internal bool IsEnd { get; set; }
        internal T Value { get; set; }
        internal bool HasChildren => !(Left == null && Middle == null && Right == null);

        internal TernarySearchTreeNode<T> Parent { get; set; }

        internal TernarySearchTreeNode<T> Left { get; set; }
        internal TernarySearchTreeNode<T> Middle { get; set; }
        internal TernarySearchTreeNode<T> Right { get; set; }

        internal TernarySearchTreeNode(TernarySearchTreeNode<T> parent, T value)
        {
            Parent = parent;
            this.Value = value;
        }
    }

    internal class TernarySearchTreeEnumerator<T> : IEnumerator<T[]> where T : IComparable
    {
        private readonly TernarySearchTreeNode<T> root;
        private Stack<TernarySearchTreeNode<T>> progress;

        internal TernarySearchTreeEnumerator(TernarySearchTreeNode<T> root)
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
                progress = new Stack<TernarySearchTreeNode<T>>(new[] { root });
            }

            while (progress.Count > 0)
            {
                var next = progress.Pop();

                foreach (var child in new[] { next.Left, next.Middle, next.Right }.Where(x => x != null))
                {
                    progress.Push(child);
                }

                if (next.IsEnd)
                {
                    Current = getValue(next);
                    return true;
                }
            }

            return false;
        }

        private T[] getValue(TernarySearchTreeNode<T> next)
        {
            var result = new Stack<T>();
            result.Push(next.Value);

            while (next.Parent != null && !next.Parent.Value.Equals(default(T)))
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
