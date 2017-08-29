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
    public class FlipBitForLongest1Seq_Tests
    {
        //[TestMethod]
        public void FlipBitForLongest1Seq_Smoke_Test()
        {
            //1775
            var testNumber = Convert.ToInt32("11011101111", 2);
            Assert.AreEqual(8, FlipBitForLongest1Seq.Find(testNumber));
        }
    }
}
