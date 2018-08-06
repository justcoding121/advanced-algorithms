using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
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
    }
}
