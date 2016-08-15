using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class AVLTree_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void AVLTree_Test()
        {
            //insert test
            var tree = new AsAVLTree<int>();
            Assert.AreEqual(-1, tree.GetHeight());

            tree.Insert(1);
            Assert.AreEqual(0, tree.GetHeight());

            tree.Insert(2);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(3);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(4);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(5);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(6);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(7);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(8);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(9);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(10);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(11);
            Assert.AreEqual(3, tree.GetHeight());

            //delete
            tree.Delete(1);
            tree.Delete(2);
            tree.Delete(3);
            tree.Delete(4);
            tree.Delete(5);
            tree.Delete(6);
            tree.Delete(7);
            tree.Delete(8);
            tree.Delete(9);
            tree.Delete(10);
            tree.Delete(11);

            Assert.AreEqual(tree.GetHeight(), -1);
            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }
    }
}
