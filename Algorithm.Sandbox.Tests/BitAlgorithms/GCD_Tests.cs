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
    public class GCD_Tests
    {
        [TestMethod]
        public void GCD_Smoke_Test()
        {
            Assert.AreEqual(3, GCD.Find(-9, 3));
            Assert.AreEqual(15, GCD.Find(45, 30));

            Assert.AreEqual(1, GCD.Find(3, 5));

        }
    }
}
