using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    /// Given a +ive integer, print the next smallest and the next largest number
    /// that have the same number of 1 bits in their binary representation
    [TestClass]
    public class NextSmallLargeNumber_Tests
    {
        //[TestMethod]
        public void NextSmallLargeNumber_Smoke_Test()
        {
            NextSmallLargeNumber.NextLarge(112);
            NextSmallLargeNumber.NextSmall(101);
        }
    }
}
