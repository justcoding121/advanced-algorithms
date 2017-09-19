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
        [TestMethod]
        public void FlipBitForLongest1Seq_Smoke_Test()
        {
            //1775
            var testNumber = Convert.ToInt32("11011101111", 2);
            Assert.AreEqual(8, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("11111001111", 2);
            Assert.AreEqual(6, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("1001001000", 2);
            Assert.AreEqual(2, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("1111001011", 2);
            Assert.AreEqual(5, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("00001000", 2);
            Assert.AreEqual(2, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("10000000", 2);
            Assert.AreEqual(2, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("0000001", 2);
            Assert.AreEqual(2, FlipBitForLongest1Seq.Find(testNumber));

            testNumber = Convert.ToInt32("0000000", 2);
            Assert.AreEqual(1, FlipBitForLongest1Seq.Find(testNumber));
        }
    }
}
