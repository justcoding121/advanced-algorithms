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

            tree.Root.AddAsDirectChild(tree.Root, 1);
            Assert.IsNotNull(tree.Find(1));

            Assert.AreEqual(tree.GetHeight(), 1);

            var element = tree.Find(1);

            element.AddAsDirectChild(tree.Root, 2);
            Assert.IsNotNull(tree.Find(2));

            Assert.AreEqual(tree.GetHeight(), 2);

            element.AddAsDirectChild(tree.Root, 3);
            Assert.IsNotNull(tree.Find(3));

            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Root.RemoveFromDescendents(tree.Root, 3);
            Assert.IsNull(tree.Find(3));

            Assert.AreEqual(tree.GetHeight(), 2);

            tree.Root.RemoveFromDescendents(tree.Root, 1);
            Assert.IsNull(tree.Find(1));
            Assert.IsNotNull(tree.Find(2));

            Assert.AreEqual(tree.GetHeight(), 1);

            tree.Root.RemoveFromDescendents(tree.Root, 0);
            Assert.AreEqual(tree.GetHeight(), 0);
        }
    }
}
