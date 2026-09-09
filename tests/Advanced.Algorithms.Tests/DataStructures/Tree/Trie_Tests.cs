using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void Trie_Smoke_Test_Banana()
        {
            var trie = new Trie<char>();

            trie.Insert("banana".ToCharArray());
            Assert.IsTrue(trie.Contains("banana".ToCharArray()));

            trie.Insert("anana".ToCharArray());
            Assert.IsTrue(trie.Contains("anana".ToCharArray()));

            trie.Insert("nana".ToCharArray());
            Assert.IsTrue(trie.Contains("nana".ToCharArray()));
            Assert.IsFalse(trie.Contains("banan".ToCharArray()));
            Assert.IsTrue(trie.ContainsPrefix("banan".ToCharArray()));

            trie.Insert("ana".ToCharArray());
            Assert.IsTrue(trie.Contains("ana".ToCharArray()));

            trie.Insert("na".ToCharArray());
            Assert.IsTrue(trie.Contains("na".ToCharArray()));

            trie.Insert("a".ToCharArray());
            Assert.IsTrue(trie.Contains("a".ToCharArray()));
            Assert.IsTrue(trie.Count == 6);

            //IEnumerable test
            Assert.AreEqual(trie.Count, trie.Count());

            Assert.IsTrue(trie.Contains("banana".ToCharArray()));
            trie.Delete("banana".ToCharArray());
            Assert.IsFalse(trie.Contains("banana".ToCharArray()));

            Assert.IsTrue(trie.Contains("anana".ToCharArray()));
            trie.Delete("anana".ToCharArray());
            Assert.IsFalse(trie.Contains("anana".ToCharArray()));

            Assert.IsTrue(trie.Contains("nana".ToCharArray()));
            trie.Delete("nana".ToCharArray());
            Assert.IsFalse(trie.Contains("nana".ToCharArray()));

            Assert.IsTrue(trie.Contains("ana".ToCharArray()));
            trie.Delete("ana".ToCharArray());
            Assert.IsFalse(trie.Contains("ana".ToCharArray()));

            Assert.IsTrue(trie.Contains("na".ToCharArray()));
            trie.Delete("na".ToCharArray());
            Assert.IsFalse(trie.Contains("na".ToCharArray()));

            Assert.IsTrue(trie.Contains("a".ToCharArray()));
            trie.Delete("a".ToCharArray());
            Assert.IsFalse(trie.Contains("a".ToCharArray()));
            Assert.IsTrue(trie.Count == 0);

            //IEnumerable test
            Assert.AreEqual(trie.Count, trie.Count());
        }

        /// <summary>
        ///     A tree test
        /// </summary>
        [TestMethod]
        public void Trie_Smoke_Test()
        {
            var trie = new Trie<char>();

            trie.Insert("abcd".ToCharArray());
            trie.Insert("abcde".ToCharArray());
            trie.Insert("bcde".ToCharArray());
            trie.Insert("cdab".ToCharArray());
            trie.Insert("efghi".ToCharArray());

            Assert.IsTrue(trie.Contains("cdab".ToCharArray()));
            Assert.IsFalse(trie.Contains("ab".ToCharArray()));

            trie.Delete("cdab".ToCharArray());
            Assert.IsFalse(trie.Contains("cdab".ToCharArray()));

            //IEnumerable test
            Assert.AreEqual(trie.Count, trie.Count());

            var matches = trie.StartsWith("b".ToCharArray());
            Assert.IsTrue(matches.Count == 1);

            matches = trie.StartsWith("abcd".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            trie.Delete("abcd".ToCharArray());
            trie.Delete("abcde".ToCharArray());
            trie.Delete("bcde".ToCharArray());
            trie.Delete("efghi".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(trie.Count, trie.Count());
        }

        [TestMethod]
        public void Trie_Corner_Cases()
        {
            var trie = new Trie<char>();

            Assert.AreEqual(0, trie.Count);
            Assert.AreEqual(0, trie.Count());
            Assert.IsFalse(trie.Contains("a".ToCharArray()));
            Assert.AreEqual(0, trie.StartsWith("a".ToCharArray()).Count);

            trie.Insert("a".ToCharArray());
            trie.Insert("ab".ToCharArray());
            Assert.AreEqual(2, trie.StartsWith("a".ToCharArray()).Count);
            Assert.IsTrue(trie.ContainsPrefix("a".ToCharArray()));

            Assert.ThrowsException<ArgumentException>(() => trie.Delete("abc".ToCharArray()));
            Assert.ThrowsException<ArgumentException>(() => trie.Delete("z".ToCharArray()));

            trie.Delete("a".ToCharArray());
            Assert.IsFalse(trie.Contains("a".ToCharArray()));
            Assert.IsTrue(trie.Contains("ab".ToCharArray()));
            Assert.AreEqual(1, trie.StartsWith("a".ToCharArray()).Count);
        }
    }
}