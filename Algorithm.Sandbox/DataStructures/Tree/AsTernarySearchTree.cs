using System;

namespace Algorithm.Sandbox.DataStructures
{
    internal class AsTernarySearchTreeNode<T> where T : IComparable
    {
        internal bool IsEnd { get; set; }
        internal T Value { get; set; }
        internal bool HasChildren => !(Left == null && Middle == null && Right == null);
        internal AsTernarySearchTreeNode<T> Left { get; set; }
        internal AsTernarySearchTreeNode<T> Middle { get; set; }
        internal AsTernarySearchTreeNode<T> Right { get; set; }

        internal AsTernarySearchTreeNode(T value)
        {
            this.Value = value;
        }
    }

    public class AsTernarySearchTree<T> where T : IComparable
    {
        internal AsTernarySearchTreeNode<T> Root;
        public int Count { get; private set; }

        public AsTernarySearchTree()
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
            Insert(ref Root, entry, 0);
            Count++;
        }

        /// <summary>
        /// Insert a new record to this TernarySearchTree after finding the end recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void Insert(ref AsTernarySearchTreeNode<T> currentNode,
            T[] entry, int currentIndex)
        {
            //create new node if empty
            if (currentNode == null)
            {
                currentNode = new AsTernarySearchTreeNode<T>(entry[currentIndex]);
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
                Insert(ref left, entry, currentIndex);
                currentNode.Left = left;
            }
            else if (compareResult < 0)
            {
                //move right
                var right = currentNode.Right;
                Insert(ref right, entry, currentIndex);
                currentNode.Right = right;
            }
            else
            {
                //if equal we just skip to next element
                var middle = currentNode.Middle;
                Insert(ref middle, entry, currentIndex + 1);
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
            Delete(Root, entry, 0);
            Count--;
        }

        /// <summary>
        /// deletes a record from this TernarySearchTree after finding it recursively
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="entry"></param>
        /// <param name="currentIndex"></param>
        private void Delete(AsTernarySearchTreeNode<T> currentNode,
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
            AsTernarySearchTreeNode<T> child;
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                child = currentNode.Left;

                Delete(child, entry, currentIndex);
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
                Delete(child, entry, currentIndex);
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
                Delete(child, entry, currentIndex + 1);
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
        public AsArrayList<T[]> StartsWith(T[] prefix)
        {
            return StartsWith(Root, prefix, 0);
        }

        /// <summary>
        /// recursively visit until end of prefix 
        /// and then gather all suffixes under it
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private AsArrayList<T[]> StartsWith(AsTernarySearchTreeNode<T> currentNode,
            T[] searchPrefix, int currentIndex)
        {

            if (currentNode == null)
            {
                return new AsArrayList<T[]>();
            }

            var compareResult = currentNode.Value.CompareTo(searchPrefix[currentIndex]);
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                return StartsWith(currentNode.Left, searchPrefix, currentIndex);
            }
            else if (compareResult < 0)
            {
                //move right
                return StartsWith(currentNode.Right, searchPrefix, currentIndex);
            }
            else
            {
                //end of search Prefix, so gather all words under it
                if (currentIndex == searchPrefix.Length - 1)
                {
                    var result = new AsArrayList<T[]>();

                    GatherStartsWith(result,
                        searchPrefix, currentNode.Middle);

                    return result;
                }
                //if equal we just skip to next element
                return StartsWith(currentNode.Middle, searchPrefix, currentIndex + 1);
            }
        }

        /// <summary>
        /// Gathers all suffixes under this node appending with the given prefix
        /// </summary>
        /// <param name="result"></param>
        /// <param name="searchPrefix"></param>
        /// <param name="prefix"></param>
        /// <param name="node"></param>
        private void GatherStartsWith(AsArrayList<T[]> result, T[] prefix,
            AsTernarySearchTreeNode<T> node)
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
                GatherStartsWith(result, prefix, node.Left);
            }

            if (node.Middle != null)
            {
                //append to end of prefix for new prefix
                var newPrefix = new T[prefix.Length + 1];
                Array.Copy(prefix, newPrefix, prefix.Length);
                newPrefix[newPrefix.Length - 1] = node.Value;

                GatherStartsWith(result, newPrefix, node.Middle);
            }

            if (node.Right != null)
            {
                GatherStartsWith(result, prefix, node.Right);
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
        private bool Contains(AsTernarySearchTreeNode<T> currentNode, T[] entry, int currentIndex)
        {
            //create new node if empty
            if (currentNode == null)
            {
                return false;
            }

            //end of word, so return
            if (currentIndex == entry.Length - 1)
            {
                if (currentNode.IsEnd)
                {
                    return true;
                }

                return false;
            }

            var compareResult = currentNode.Value.CompareTo(entry[currentIndex]);
            //current is greater? move left, move right otherwise
            //if current is equal then move center
            if (compareResult > 0)
            {
                //move left
                return Contains(currentNode.Left, entry, currentIndex);
            }
            else if (compareResult < 0)
            {
                //move right
                return Contains(currentNode.Right, entry, currentIndex);
            }
            else
            {
                //if equal we just skip to next element
                return Contains(currentNode.Middle, entry, currentIndex + 1);
            }

        }
    }
}
