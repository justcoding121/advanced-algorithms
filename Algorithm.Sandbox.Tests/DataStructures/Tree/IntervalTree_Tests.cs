using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class IntervalTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void IntervalTree_Smoke_Test()
        {
            var intTree = new AsIntervalTree<int>();

            intTree.Insert(new AsInterval<int>(1, 2));
            intTree.Insert(new AsInterval<int>(2, 3));
            intTree.Insert(new AsInterval<int>(3, 4));
            intTree.Insert(new AsInterval<int>(4, 5));
            intTree.Insert(new AsInterval<int>(5, 6));
            intTree.Insert(new AsInterval<int>(6, 7));

            Assert.AreEqual(intTree.Count, 6);

            Assert.IsNotNull(intTree.GetOverlap(new AsInterval<int>(0, 1)));
            intTree.Delete(new AsInterval<int>(6, 7));
            Assert.IsNull(intTree.GetOverlap(new AsInterval<int>(7, 8)));

            intTree.Delete(new AsInterval<int>(1, 2));
            intTree.Delete(new AsInterval<int>(2, 3));
            intTree.Delete(new AsInterval<int>(3, 4));
            intTree.Delete(new AsInterval<int>(4, 5));

            Assert.IsNotNull(intTree.GetOverlap(new AsInterval<int>(6, 7)));
            intTree.Delete(new AsInterval<int>(5, 6));
            Assert.IsNull(intTree.GetOverlap(new AsInterval<int>(6, 7)));


            Assert.AreEqual(intTree.Count, 0);
        }

        /// </summary>
        [TestMethod]
        public void IntervalTree_Accuracy_Test()
        {
            var nodeCount = 1000;
            var rnd = new Random();
            var intTree = new AsIntervalTree<int>();

            for (int i = 0; i < nodeCount; i++)
            {
                intTree.Insert(new AsInterval<int>(i, i + 1));

                for (int j = i; j >= 0; j--)
                {
                    Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(j - rnd.Next(1, 10), j + rnd.Next(1,10))));
                }
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(i - rnd.Next(1, 10), i + rnd.Next(1, 10))));
            }

            for (int i = 0; i < nodeCount; i++)
            {
                intTree.Delete(new AsInterval<int>(i, i + 1));

                for (int j = i + 1; j < nodeCount; j++)
                {
                    Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(j - rnd.Next(1, 10), j + rnd.Next(1, 10))));
                }
            }
        }

        /// <summary>
        /// stress test
        /// </summary>
        [TestMethod]
        public void IntervalTree_Stress_Test()
        {
            var nodeCount = 1000 * 10;
            var rnd = new Random();
            var intTree = new AsIntervalTree<int>();

            for (int i = 0; i < nodeCount; i++)
            {
                intTree.Insert(new AsInterval<int>(i, i + 1));
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(i - rnd.Next(1, 10), i + rnd.Next(1, 10))));
            }

            for (int i = 0; i < nodeCount; i++)
            {
                intTree.Delete(new AsInterval<int>(i, i + 1));

            }
        }
    }
}
