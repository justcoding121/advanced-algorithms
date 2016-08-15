using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTrieNode<T> where T : IComparable
    {
        public T Value { get; set; }

        public AsTreeHashSet<T, AsTrieNode<T>> Children { get; set; }

        public AsTrieNode(T value)
        {
            this.Value = value;
            Children = new AsTreeHashSet<T, AsTrieNode<T>>();
        }

    }

    public class AsTrie<T> where T : IComparable
    {
        public AsTrieNode<T> Root { get; set; }

    }
}
