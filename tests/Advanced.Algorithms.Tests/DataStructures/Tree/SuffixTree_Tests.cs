using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SuffixTests
    {
        /// <summary>
        ///     A tree test
        /// </summary>
        [TestMethod]
        public void Suffix_Smoke_Test()
        {
            var tree = new SuffixTree<char>();

            tree.Insert("bananaa".ToCharArray());
            Assert.IsTrue(tree.Count == 1);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.IsTrue(tree.Contains("aa".ToCharArray()));
            Assert.IsFalse(tree.Contains("ab".ToCharArray()));

            var matches = tree.StartsWith("na".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            matches = tree.StartsWith("an".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            tree.Delete("bananaa".ToCharArray());
            Assert.IsTrue(tree.Count == 0);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());
        }

        [TestMethod]
        public void Suffix_Corner_Cases()
        {
            var tree = new SuffixTree<char>();

            Assert.ThrowsException<ArgumentNullException>(() => tree.Insert(null));
            Assert.ThrowsException<ArgumentNullException>(() => tree.Delete(null));

            tree.Insert("ab".ToCharArray());
            Assert.ThrowsException<ArgumentException>(() => tree.Insert("ab".ToCharArray()));
            Assert.ThrowsException<ArgumentException>(() => tree.Delete("zz".ToCharArray()));

            Assert.IsTrue(tree.Contains("b".ToCharArray()));
            Assert.AreEqual(0, tree.StartsWith("z".ToCharArray()).Count);

            tree.Delete("ab".ToCharArray());
            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());
        }
    }
}