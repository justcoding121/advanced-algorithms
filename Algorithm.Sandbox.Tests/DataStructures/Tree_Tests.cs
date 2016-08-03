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

            tree.Add(0, 1);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Add(1, 2);
            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Remove(0);
            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Remove(1);
            Assert.AreEqual(tree.GetHeight(), 0);
        }
    }
}
