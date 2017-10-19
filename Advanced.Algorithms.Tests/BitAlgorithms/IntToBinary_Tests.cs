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
    public class IntToBinary_Tests
    {
        [TestMethod]
        public void IntToBinary_Smoke_Test()
        {
          Assert.AreEqual("00000001", IntToBinary.GetBinary(1, 8));
          Assert.AreEqual("11111111", IntToBinary.GetBinary(-1, 8));
        }
    }
}
