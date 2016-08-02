using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class Tree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void Tree_Test()
        {
            
            var tree = new AsTree<int>(0);
            Assert.AreEqual(tree.GetHeight(), 0);

            tree.Add(tree.Root, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            var child = tree.Find(1);
            tree.Add(child, 2);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Remove(tree.Root, 0);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Remove(tree.Root, 1);
            Assert.AreEqual(tree.GetHeight(), 0);
        }
    }
}
