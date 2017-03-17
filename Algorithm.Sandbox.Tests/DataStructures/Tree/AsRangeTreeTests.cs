using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class AsRangeTreeTests
    {
        /// <summary>
        /// Smoke test
        /// </summary>
        [TestMethod]
        public void AsRangeTree_Smoke_Test()
        {
            var tree = new AsRangeTree<int>();
            tree.Insert(0);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);

        }
    }
}
