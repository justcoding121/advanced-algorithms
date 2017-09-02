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
    /// Compute modulus division by 1 << s without a division operator.
    /// </summary>
    [TestClass]
    public class DivisionModulus_Tests
    {
        //[TestMethod]
        public void DivisionModulus_Smoke_Test()
        {
            Assert.AreEqual(1, DivisionModulus.GetModulus(5, 4));
        }
    }
}
