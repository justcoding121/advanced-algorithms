using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/how-to-print-maximum-number-of-a-using-given-four-keys/
    /// </summary>
    [TestClass]
    public class PrintMaxAs_Tests
    {
        [TestMethod]
        public void PrintMaxAs_Smoke_Test()
        {
            Assert.AreEqual(1, PrintMaxAs.GetCount(1));
            Assert.AreEqual(2, PrintMaxAs.GetCount(2));
            Assert.AreEqual(3, PrintMaxAs.GetCount(3));
            Assert.AreEqual(9, PrintMaxAs.GetCount(7));
            Assert.AreEqual(27, PrintMaxAs.GetCount(11));
            Assert.AreEqual(81, PrintMaxAs.GetCount(15));
            Assert.AreEqual(324, PrintMaxAs.GetCount(20));
        }
    }
}
