using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures
{
    /// <summary>
    /// A disjoint set implementation.
    /// </summary>
    public class DisJointSet<T> : IEnumerable<T>
    {
        /// <summary>
        /// A Map for faster access for members.
        /// </summary>
        private Dictionary<T, DisJointSetNode<T>> set 
            = new Dictionary<T, DisJointSetNode<T>>();

        public int Count { get; private set; }

        /// <summary>
        /// Creates a new set with given member.
        /// Time complexity: log(n).
        /// </summary>
        public void MakeSet(T member)
        {
            if (set.ContainsKey(member))
            {
                throw new Exception("A set with given member already exists.");
            }

            var newSet = new DisJointSetNode<T>()
            {
                Data = member,
                Rank = 0
            };

            //Root's Parent is Root itself
            newSet.Parent = newSet;
            set.Add(member, newSet);

            Count++;
        }


        /// <summary>
        /// Returns the reference member of the set where this member is part of.
        /// Time complexity: log(n).
        /// </summary>
        public T FindSet(T member)
        {
            if(!set.ContainsKey(member))
            {
                throw new Exception("No such set with given member.");
            }

            return findSet(set[member]).Data;
        }

        /// <summary>
        /// Recursively move up in the set tree till root
        /// and return the Root.
        /// Does path Compression on all visited members on way to root
        /// by pointing their parent to Root.
        /// </summary>
        private DisJointSetNode<T> findSet(DisJointSetNode<T> node)
        {
            var parent = node.Parent;

            if(node !=parent)
            {
                //compress path by setting parent to Root
                node.Parent = findSet(node.Parent);
                return node.Parent;
            }

            //reached root so return the Root (reference Member)
            return parent;
        }

        /// <summary>
        /// Union's given member's sets if given members are in differant sets.
        /// Otherwise does nothing.
        /// Time complexity: log(n).
        /// </summary>
        public void Union(T memberA, T memberB)
        {
            var rootA = FindSet(memberA);
            var rootB = FindSet(memberB);

            if(rootA.Equals(rootB))
            {
                return;
            }

            var nodeA = set[rootA];
            var nodeB = set[rootB];

            //equal rank so just pick any of two as Root
            //and increment rank
            if(nodeA.Rank == nodeB.Rank)
            {
                nodeB.Parent = nodeA;
                nodeA.Rank++;
            }
            else
            {
                //pick max Rank node as root
                if(nodeA.Rank < nodeB.Rank)
                {
                    nodeA.Parent = nodeB;
                }
                else
                {
                    nodeB.Parent = nodeA;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.Values.Select(x => x.Data).GetEnumerator();
        }
      
    }

    internal class DisJointSetNode<T>
    {
        internal T Data { get; set; }
        internal int Rank { get; set; }

        internal DisJointSetNode<T> Parent { get; set; }
    }

}
