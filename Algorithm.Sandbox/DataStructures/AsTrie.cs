using System;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsTrieNode<I> where I : IComparable
    {
        public I Identifier { get; set; }

        public AsTreeHashSet<I, AsTrieNode<I>> Children { get; set; }

        public AsTrieNode(I identifier)
        {
            this.Identifier = identifier;
            Children = new AsTreeHashSet<I, AsTrieNode<I>>();
        }

    }

    public class AsTrie<I> where I : IComparable
    {
        public AsTrieNode<I> Root { get; set; }

    }
}
