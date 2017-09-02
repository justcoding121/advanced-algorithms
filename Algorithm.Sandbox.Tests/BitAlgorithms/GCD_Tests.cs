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
        //[TestMethod]
        public void GCD_Smoke_Test()
        {
            Assert.AreEqual(15, GCD.Find(30, 45));
        }
    }
}
