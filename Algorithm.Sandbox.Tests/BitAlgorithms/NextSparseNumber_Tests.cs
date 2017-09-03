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
    /// http://www.geeksforgeeks.org/given-a-number-find-next-sparse-number/
    /// </summary>
    [TestClass]
    public class NextSparseNumber_Tests
    {
        [TestMethod]
        public void NextSparseNumber_Smoke_Test()
        {
            //already sparse
            Assert.AreEqual(0, NextSparseNumber.Next(0));

            Assert.AreEqual(8, NextSparseNumber.Next(6));
            Assert.AreEqual(4, NextSparseNumber.Next(4));
            Assert.AreEqual(40, NextSparseNumber.Next(38));
            Assert.AreEqual(64, NextSparseNumber.Next(44));
        }
    }
}
