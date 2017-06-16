using Algorithm.Sandbox.NumericalMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.NumericalMethods
{
    [TestClass]
    public class PrimeGenerator_Tests
    {
        [TestMethod]
        public void Prime_Generation_Smoke_Test()
        {
            Assert.AreEqual(5, PrimeGenerator.GetAllPrimes(11).Count);
            Assert.AreEqual(8, PrimeGenerator.GetAllPrimes(20).Count);
        }
    }
}
