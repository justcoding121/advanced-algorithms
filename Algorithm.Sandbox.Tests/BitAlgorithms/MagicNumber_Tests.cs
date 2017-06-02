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
    /// http://www.geeksforgeeks.org/find-nth-magic-number/
    /// </summary>
    [TestClass]
    public class MagicNumber_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(130, MagicNumber.Find(5));
        }
    }
}
