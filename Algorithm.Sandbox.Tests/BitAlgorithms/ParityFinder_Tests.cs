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
    /// </summary>
    [TestClass]
    public class ParityBit_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            Assert.AreEqual(0, ParityFinder.Find(13));
        }
    }
}
