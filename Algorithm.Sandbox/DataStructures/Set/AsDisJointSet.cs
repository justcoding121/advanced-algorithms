using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures.Set
{
    internal class AsDisJointSetNode<T>
    {
        internal T Data { get; set; }
        internal int Rank { get; set; }

        internal AsDisJointSetNode<T> Parent { get; set; }
    }

    /// <summary>
    /// A disjoint set implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsDisJointSet<T>
    {
        /// <summary>
        /// A Map for faster access for members
        /// </summary>
        private Dictionary<T, AsDisJointSetNode<T>> set 
            = new Dictionary<T, AsDisJointSetNode<T>>();

        /// <summary>
        /// Creates a new set with given member
        /// </summary>
        /// <param name="member"></param>
        public void MakeSet(T member)
        {
            var newSet = new AsDisJointSetNode<T>()
            {
                Data = member,
                Rank = 0
            };

            //Root's Parent is Root itself
            newSet.Parent = newSet;

            set.Add(member, newSet);
        }


        /// <summary>
        /// Returns the reference member of the set where this member is part of
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public T FindSet(T member)
        {
            if(!set.ContainsKey(member))
            {
                throw new Exception("No such set with given member.");
            }

            return FindSet(set[member]).Data;
        }

        /// <summary>
        /// Recursively move up in the set tree till root
        /// And returns the Root (reference member)
        /// Do Path Compression on all visited members on way to root
        /// By pointing their parent to Root
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private AsDisJointSetNode<T> FindSet(AsDisJointSetNode<T> node)
        {
            var parent = node.Parent;

            if(node !=parent)
            {
                //compress path by setting parent to Root
                node.Parent = FindSet(node.Parent);
                return node.Parent;
            }
            else
            {
                //reached root so return the Root (reference Member)
                return parent;
            }
        }

        /// <summary>
        /// Union's their sets if given members are in differant sets
        /// Otherwise do nothing
        /// </summary>
        /// <param name="setAMember"></param>
        /// <param name="setBMember"></param>
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

    }
}
