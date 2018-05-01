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
