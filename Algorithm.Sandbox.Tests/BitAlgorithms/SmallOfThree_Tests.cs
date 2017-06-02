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
    /// http://www.geeksforgeeks.org/smallest-of-three-integers-without-comparison-operators/
    /// </summary>
    [TestClass]
    public class SmallOfThree_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(5, SmallOfThree.GetSmallest(12, 15, 5));
        }
    }
}
