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
        //[TestMethod]
        public void NextSparseNumber_Smoke_Test()
        {
            Assert.AreEqual(8, NextSparseNumber.Next(6));
        }
    }
}
