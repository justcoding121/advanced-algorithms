using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/swap-two-numbers-without-using-temporary-variable/
    /// </summary>
    [TestClass]
    public class SwapWithoutTemp_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            int x = 10, y = 5;

            SwapWithoutTemp.Swap(ref x, ref y);

            Assert.AreEqual(5, x);
            Assert.AreEqual(10, y);
        }
    }
}
