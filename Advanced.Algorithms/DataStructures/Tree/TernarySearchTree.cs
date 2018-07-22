using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.DataStructures
{
    internal class TernarySearchTreeNode<T> where T : IComparable
    {
        internal bool IsEnd { get; set; }
        internal T Value { get; set; }
        internal bool HasChildren => !(Left == null && Middle == null && Right == null);
        internal TernarySearchTreeNode<T> Left { get; set; }
        internal TernarySearchTreeNode<T> Middle { get; set; }
        internal TernarySearchTreeNode<T> Right { get; set; }

        internal TernarySearchTreeNode(T value)
        {
            this.Value = value;
        }
    }

    public class TernarySearchTree<T> where T : IComparable
    {
        internal TernarySearchTreeNode<T> Root;
        public int Count { get; private set; }

        public TernarySearchTree()
        {
            Count = 0;
        }

        /// <summary>
        /// Insert a new record to this TernarySearchTree
        /// O(m) time complexity where m is the length of entry
        /// </summary>
        /// <param name="entry"></param>
        public void Insert(T[] entry)
        {
            insert(ref Root, entry, 0);
            Count++;
        }

        /// <summary>
        /// Insert a new record to this TernarySearchTree after finding the end recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void insert(ref TernarySearchTreeNode<T> currentNode,
            T[] entry, int currentIndex)
        {
            //create new node if empty
            if (currentNode == null)
            {
                currentNode = new TernarySearchTreeNode<T>(entry[currentIndex]);
            }

            //end of word, so return
            if (currentIndex == entry.Length - 1)
            {
                currentNode.IsEnd = true;
                return;
            }

            var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                var left = currentNode.Left;
                insert(ref left, entry, currentIndex);
                currentNode.Left = left;
            }
            else if (compareResult < 0)
            {
                //move right
                var right = currentNode.Right;
                insert(ref right, entry, currentIndex);
                currentNode.Right = right;
            }
            else
            {
                //if equal we just skip to next element
                var middle = currentNode.Middle;
                insert(ref middle, entry, currentIndex + 1);
                currentNode.Middle = middle;
            }
        }

        /// <summary>
        /// deletes a record from this TernarySearchTree
        /// O(m) where m is the length of entry
        /// </summary>
        /// <param name="entry"></param>
        public void Delete(T[] entry)
        {
            delete(Root, entry, 0);
            Count--;
        }

        /// <summary>
        /// deletes a record from this TernarySearchTree after finding it recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void delete(TernarySearchTreeNode<T> currentNode,
            T[] entry, int currentIndex)
        {
            //empty node
            if (currentNode == null)
            {
                throw new Exception("Item not found.");
            }

            //end of word, so return
            if (currentIndex == entry.Length - 1)
            {
                if (!currentNode.IsEnd)
                {
                    throw new Exception("Item not found.");
                }

                //remove this end flag
                currentNode.IsEnd = false;

                return;
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
        /// and then gather all suffixes under it
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
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

                gatherStartsWith(result, searchPrefix, currentNode.Middle);

                return result;
                
            }
        }

        /// <summary>
        /// Gathers all suffixes under this node appending with the given prefix
        /// </summary>
        private void gatherStartsWith(List<T[]> result, T[] prefix, TernarySearchTreeNode<T> node)
        {
            while (true)
            {
                if (node == null)
                {
                    result.Add(prefix);
                    return;
                }

                //end of word
                if (node.IsEnd)
                {
                    //append to end of prefix for new prefix
                    var newPrefix = new T[prefix.Length + 1];
                    Array.Copy(prefix, newPrefix, prefix.Length);
                    newPrefix[newPrefix.Length - 1] = node.Value;
                    result.Add(newPrefix);
                }

                if (node.Left != null)
                {
                    gatherStartsWith(result, prefix, node.Left);
                }

                if (node.Middle != null)
                {
                    //append to end of prefix for new prefix
                    var newPrefix = new T[prefix.Length + 1];
                    Array.Copy(prefix, newPrefix, prefix.Length);
                    newPrefix[newPrefix.Length - 1] = node.Value;

                    gatherStartsWith(result, newPrefix, node.Middle);
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
        private bool contains(TernarySearchTreeNode<T> currentNode, T[] entry, int currentIndex)
        {
            while (true)
            {
                //create new node if empty
                if (currentNode == null)
                {
                    return false;
                }

                //end of word, so return
                if (currentIndex == entry.Length - 1)
                {
                    return currentNode.IsEnd;
                }

                var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);
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
    }
}
