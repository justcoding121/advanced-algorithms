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
    /// http://www.geeksforgeeks.org/divide-and-conquer-set-2-karatsuba-algorithm-for-fast-multiplication/
    /// </summary>
    [TestClass]
    public class KaratsubaFastMultiplication_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            Assert.AreEqual(120,
                KaratsubaFastMultiplication.Multiply("1100", "1010"));
        }
    }
}
