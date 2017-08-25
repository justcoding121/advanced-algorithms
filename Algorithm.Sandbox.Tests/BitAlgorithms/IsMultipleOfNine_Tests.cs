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
    /// http://www.geeksforgeeks.org/divisibility-9-using-bitwise-operators/
    /// </summary>
    [TestClass]
    public class IsMultipleOfNine_Tests
    {
        [TestMethod]
        public void IsMultipleOfNine_Smoke_Test()
        {
            Assert.AreEqual(true, IsMultipleOfNine.IsTrue(81));
            Assert.AreEqual(true, IsMultipleOfNine.IsTrue(90));

            Assert.AreEqual(false, IsMultipleOfNine.IsTrue(91));
            Assert.AreEqual(false, IsMultipleOfNine.IsTrue(102));
        }
    }
}
