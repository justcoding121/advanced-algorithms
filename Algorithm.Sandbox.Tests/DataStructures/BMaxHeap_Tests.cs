using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class BMaxHeap_Tests
    {
        /// <summary>
        /// A tree test
        /// </summary>
        [TestMethod]
        public void BMaxHeap_Test()
        {
            //insert test
            var tree = new AsBMaxHeap<int>();

            for (int i = 0; i < 14; i++)
            {
                tree.Insert(i);
            }

            for (int i = 13; i >= 0; i--)
            {
                var max = tree.ExtractMax();
                Assert.AreEqual(max, i);
            }


        }
    }
}
