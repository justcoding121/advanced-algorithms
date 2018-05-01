using Advanced.Algorithms.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.BitAlgorithms
{
    [TestClass]
    public class GCD_Tests
    {
        [TestMethod]
        public void GCD_Smoke_Test()
        {
            Assert.AreEqual(3, Gcd.Find(-9, 3));
            Assert.AreEqual(15, Gcd.Find(45, 30));

            Assert.AreEqual(1, Gcd.Find(3, 5));

        }
    }
}
