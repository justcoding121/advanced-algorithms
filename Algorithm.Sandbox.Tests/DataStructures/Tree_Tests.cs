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
            var tree = new AsTree<int, object>(5, 15);

            tree.AddToRoot(11, null);
            tree.AddToRoot(10, null);

            var result = tree.Find(10);

            Assert.IsNotNull(result);
            Assert.IsTrue(tree.HasItem(result.Identifier));

        }
    }
}
