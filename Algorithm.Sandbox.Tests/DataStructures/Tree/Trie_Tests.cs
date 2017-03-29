using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class Trie_Tests
    {
        [TestMethod]
        public void Smoke_Test_Using_Banana()
        {
            var trie = new AsTrie<char>();

            trie.Insert("banana".ToCharArray());
            Assert.IsTrue(trie.Contains("banana".ToCharArray()));

            trie.Insert("anana".ToCharArray());
            Assert.IsTrue(trie.Contains("anana".ToCharArray()));

            trie.Insert("nana".ToCharArray());
            Assert.IsTrue(trie.Contains("nana".ToCharArray()));

            trie.Insert("ana".ToCharArray());
            Assert.IsTrue(trie.Contains("ana".ToCharArray()));

            trie.Insert("na".ToCharArray());
            Assert.IsTrue(trie.Contains("na".ToCharArray()));

            trie.Insert("a".ToCharArray());
            Assert.IsTrue(trie.Contains("a".ToCharArray()));
            Assert.IsTrue(trie.Count == 6);

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
        }
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void Trie_Smoke_Test()
        {
            var trie = new AsTrie<char>();

            trie.Insert("abcd".ToCharArray());
            trie.Insert("abcde".ToCharArray());
            trie.Insert("bcde".ToCharArray());
            trie.Insert("cdab".ToCharArray());
            trie.Insert("efghi".ToCharArray());

            Assert.IsTrue(trie.Contains("cdab".ToCharArray()));
            Assert.IsFalse(trie.Contains("ab".ToCharArray()));

            trie.Delete("cdab".ToCharArray());
            Assert.IsFalse(trie.Contains("cdab".ToCharArray()));

            var matches = trie.StartsWith("b".ToCharArray());
            Assert.IsTrue(matches.Length == 1);

            matches = trie.StartsWith("abcd".ToCharArray());
            Assert.IsTrue(matches.Length == 2);

            trie.Delete("abcd".ToCharArray());
            trie.Delete("abcde".ToCharArray());
            trie.Delete("bcde".ToCharArray());
            trie.Delete("efghi".ToCharArray());


        }
    }
}
