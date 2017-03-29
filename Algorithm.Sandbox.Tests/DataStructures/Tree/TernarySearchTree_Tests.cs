using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class TernarySearchTree_Tests
    {
        [TestMethod]
        public void TernarySearchTree_Smoke_Test()
        {
            var searchTree = new AsTernarySearchTree<char>();

            searchTree.Insert("cat".ToCharArray());
            searchTree.Insert("cats".ToCharArray());
            searchTree.Insert("cut".ToCharArray());
            searchTree.Insert("cuts".ToCharArray());
            searchTree.Insert("up".ToCharArray());
            searchTree.Insert("bug".ToCharArray());
            searchTree.Insert("bugs".ToCharArray());

            Assert.IsTrue(searchTree.Contains("cat".ToCharArray()));
            Assert.IsTrue(searchTree.Contains("cut".ToCharArray()));
            Assert.IsFalse(searchTree.Contains("bu".ToCharArray()));

            searchTree.Delete("cuts".ToCharArray());
            Assert.IsFalse(searchTree.Contains("cuts".ToCharArray()));

            var matches = searchTree.StartsWith("u".ToCharArray());
            Assert.IsTrue(matches.Length == 1);

            matches = searchTree.StartsWith("cu".ToCharArray());
            Assert.IsTrue(matches.Length == 1);

            matches = searchTree.StartsWith("bu".ToCharArray());
            Assert.IsTrue(matches.Length == 2);

            matches = searchTree.StartsWith("c".ToCharArray());
            Assert.IsTrue(matches.Length == 3);

            matches = searchTree.StartsWith("ca".ToCharArray());
            Assert.IsTrue(matches.Length == 2);

            searchTree.Delete("cats".ToCharArray());
            searchTree.Delete("up".ToCharArray());
            searchTree.Delete("bug".ToCharArray());
            searchTree.Delete("bugs".ToCharArray());
            searchTree.Delete("cat".ToCharArray());
            searchTree.Delete("cut".ToCharArray());
        }
    }
}
