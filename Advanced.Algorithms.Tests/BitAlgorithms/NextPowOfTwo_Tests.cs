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
    public class NextPowOfTwo_Tests
    {
        [TestMethod]
        public void NextPowOfTwo_Smoke_Test()
        {
            Assert.AreEqual(8, NextPowOfTwo.Next(5));
        }
    }
}
