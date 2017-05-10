using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class Suffix_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void Suffix_Smoke_Test()
        {
            var suffixTree = new AsSuffixTree<char>();

            suffixTree.Insert("bananaa".ToCharArray());
            Assert.IsTrue(suffixTree.Count == 1);

            Assert.IsTrue(suffixTree.ContainsPattern("aa".ToCharArray()));
            Assert.IsFalse(suffixTree.ContainsPattern("ab".ToCharArray()));

            var matches = suffixTree.StartsWithPattern("na".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            matches = suffixTree.StartsWithPattern("an".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            suffixTree.Delete("bananaa".ToCharArray());
            Assert.IsTrue(suffixTree.Count == 0);

        }
    }
}
