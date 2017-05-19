using Algorithm.Sandbox.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.Sorting
{
    [TestClass]
    public class ZigZag_Tests
    {
        private static int[] testArray =
            new int[] { 4, 3, 7, 8, 6, 2, 1 };

        [TestMethod]
        public void ZigZag_Smoke_Test()
        {
            var result = ZigZagOrderer.Order(testArray);
            CollectionAssert.AreEqual(new int[] { 3, 7, 4, 8, 2, 6, 1, }, result);
        }
    }
}
