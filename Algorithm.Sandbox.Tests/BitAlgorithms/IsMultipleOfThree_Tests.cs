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
    /// http://www.geeksforgeeks.org/write-an-efficient-method-to-check-if-a-number-is-multiple-of-3/
    /// </summary>
    [TestClass]
    public class IsMultipleOfThree_Tests
    {
        [TestMethod]
        public void IsMultipleOfThree_Test()
        {
            Assert.IsTrue(IsMultipleOfThree.IsTrue(39));
            Assert.IsFalse(IsMultipleOfThree.IsTrue(35));

            Assert.IsTrue(IsMultipleOfThree.IsTrue(3));
            Assert.IsFalse(IsMultipleOfThree.IsTrue(5));
        }
    }
}
