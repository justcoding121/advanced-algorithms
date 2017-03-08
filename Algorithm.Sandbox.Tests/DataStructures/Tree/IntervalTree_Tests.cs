using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
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
            while (true)
            {
                var nodeCount = 1000;
                var intTree = new AsIntervalTree<int>();

                var rnd = new Random();
                var intervals = new List<AsInterval<int>>();

                for (int i = 1; i <= nodeCount; i++)
                {
                    var start = i + rnd.Next(0, 10);
                    var interval = new AsInterval<int>(start, start + rnd.Next(0, 10));
                    intervals.Add(interval);
                    intTree.Insert(new AsInterval<int>(interval.Start, interval.End[0]));

                    for (int j = i-1; j >= 0; j--)
                    {
                        Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(intervals[j].Start - rnd.Next(1, 10),
                            intervals[j].End[0] + rnd.Next(1, 10))));
                    }
                }

                for (int i = 0; i < nodeCount; i++)
                {
                    Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(intervals[i].Start,
                                                       intervals[i].End[0])));

                    Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(intervals[i].Start - rnd.Next(1, 10),
                                                        intervals[i].End[0] + rnd.Next(1, 10))));
                }
            }

            //for (int i = 0; i < intervals.Count; i++)
            //{
            //    intTree.Delete(new AsInterval<int>(intervals[i].Start, intervals[i].End[0]));

            //    for (int j = i + 1; j < nodeCount; j++)
            //    {
            //        Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(intervals[j].Start,
            //            intervals[j].End[0])));
            //    }

            
            //}
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
                Assert.IsTrue(intTree.DoOverlap(new AsInterval<int>(i - rnd.Next(1, 5), i + rnd.Next(1, 5))));
            }

            for (int i = 0; i < nodeCount; i++)
            {
                intTree.Delete(new AsInterval<int>(i, i + 1));
            }
        }
    }
}
