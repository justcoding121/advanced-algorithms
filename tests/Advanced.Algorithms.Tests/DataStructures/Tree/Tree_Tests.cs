using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class TreeTests
    {
        /// <summary>
        ///     A tree test
        /// </summary>
        [TestMethod]
        public void Tree_Test()
        {
            var tree = new Tree<int>();
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(0, 0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(1, 2);
            Assert.AreEqual(tree.GetHeight(), 2);

            //IEnumerable test using linq count()
            Assert.AreEqual(tree.Count, tree.Count());

            tree.Delete(1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Delete(2);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Delete(0);
            Assert.AreEqual(tree.GetHeight(), -1);

            tree.Insert(0, 0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Insert(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Insert(1, 2);
            Assert.AreEqual(tree.GetHeight(), 2);

            //IEnumerable test using linq count()
            Assert.AreEqual(tree.Count, tree.Count());
        }

        [TestMethod]
        public void Tree_Empty_SingleNode_DeleteRoot_Enumerate()
        {
            var tree = new Tree<int>();

            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());
            Assert.IsFalse(tree.HasItem(1));
            Assert.ThrowsException<InvalidOperationException>(() => tree.Delete(1));

            tree.Insert(0, 1);
            Assert.AreEqual(1, tree.Count);
            Assert.IsTrue(tree.HasItem(1));
            CollectionAssert.AreEqual(new[] { 1 }, tree.ToList());

            tree.Delete(1);
            Assert.AreEqual(0, tree.Count);
            Assert.AreEqual(0, tree.Count());
            Assert.IsFalse(tree.HasItem(1));
        }
    }
}
