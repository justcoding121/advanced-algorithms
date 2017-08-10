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
    /// http://www.geeksforgeeks.org/find-the-two-repeating-elements-in-a-given-array/
    /// </summary>
    [TestClass]
    public class TwoRepeatingNums_Tests
    {
        [TestMethod]
        public void TwoRepeatingNums_Smoke_Test()
        {
            CollectionAssert.AreEqual(new int[] { 2, 4 },
                TwoRepeatingNums.Find(new int[] { 4, 2, 4, 5, 2, 3, 1 }, 5));
        }
    }
}
