using Algorithm.Sandbox.BitAlgorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.BitAlgorithms
{
    // Interleave bits of x and y, so that all of the
    // bits of x are in the even positions and y in the odd;
    // z gets the resulting Morton Number
    [TestClass]
    public class InterleaveBits_Test
    {
        [TestMethod]
        public void InterleaveBits_Smoke_Test()
        {

            string x  = "1001", y = "1100";

            Assert.AreEqual("11100001", Convert.ToString(
                InterleaveBits.Interleave(Convert.ToInt32(x, 2),
                Convert.ToInt32(y, 2)), 2)
                ) ;
        }
    }
}
