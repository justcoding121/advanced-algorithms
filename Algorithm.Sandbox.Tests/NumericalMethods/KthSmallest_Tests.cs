using Algorithm.Sandbox.NumericalMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.NumericalMethods
{
    [TestClass]
    public class KthSmallest_Tests
    {
        [TestMethod]
        public void KthSmallest_Smoke_Test()
        {
            var kthSmallest = new KthSmallest<int>();

            var k = kthSmallest
                .FindKthSmallest(new int[] { 12, 3, 5, 7, 4, 19, 26 }, 3);

            Assert.AreEqual(k, 5);
        }
    }
}
