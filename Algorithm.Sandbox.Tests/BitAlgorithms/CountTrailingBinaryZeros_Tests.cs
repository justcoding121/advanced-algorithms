using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    [TestClass]
    public class CountTrailingBinaryZeros_Tests
    {
        //[TestMethod]
        public void CountTrailingBinaryZeros_Smoke_Test()
        {
            Assert.AreEqual(3, CountTrailingBinaryZeros.Count(Convert.ToInt32("1101000", 2)));
        }
    }
}
