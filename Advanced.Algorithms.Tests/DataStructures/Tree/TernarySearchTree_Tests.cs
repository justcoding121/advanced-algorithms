using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class TernarySearchTree_Tests
    {
        [TestMethod]
        public void TernarySearchTree_Smoke_Test()
        {
            var tree = new TernarySearchTree<char>();

            tree.Insert("cat".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Insert("cats".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Insert("cut".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Insert("cuts".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Insert("up".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Insert("bug".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Insert("bugs".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            Assert.IsTrue(tree.Contains("cat".ToCharArray()));
            Assert.IsTrue(tree.Contains("cut".ToCharArray()));
            Assert.IsFalse(tree.Contains("bu".ToCharArray()));
            Assert.IsTrue(tree.ContainsPrefix("bu".ToCharArray()));

            tree.Delete("cuts".ToCharArray());
            Assert.IsFalse(tree.Contains("cuts".ToCharArray()));

            var matches = tree.StartsWith("u".ToCharArray());
            Assert.IsTrue(matches.Count == 1);

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            matches = tree.StartsWith("cu".ToCharArray());
            Assert.IsTrue(matches.Count == 1);

            matches = tree.StartsWith("bu".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            matches = tree.StartsWith("c".ToCharArray());
            Assert.IsTrue(matches.Count == 3);

            matches = tree.StartsWith("ca".ToCharArray());
            Assert.IsTrue(matches.Count == 2);

            tree.Delete("cats".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());


            tree.Delete("up".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete("bug".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete("bugs".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete("cat".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete("cut".ToCharArray());

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());
        }

        [TestMethod]
        public void TernarySearchTree_Test()
        {
            var tree = new TernarySearchTree<char>();

            var testCount = 1000;

            var testStrings = new List<string>();

            while(testCount > 0)
            {
                var testString = randomString(3);
                testStrings.Add(testString);
                testCount--;
            }

            testStrings = new List<string>(testStrings.Distinct());

            foreach(var testString in testStrings)
            {
                tree.Insert(testString.ToArray());
            }

            //IEnumerable test
            Assert.AreEqual(tree.Count, tree.Count());

            foreach(var item in tree)
            {
                var existing = new string(item);
                Assert.IsTrue(testStrings.Contains(existing));
            }

            foreach (var item in testStrings)
            {
                Assert.IsTrue(tree.Contains(item.ToArray()));
            }

            foreach (var testString in testStrings)
            {
                tree.Delete(testString.ToArray());
            }
           
        }

        private static Random random = new Random();

        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
