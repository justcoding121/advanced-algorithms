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
    public class ModifyBits_Test
    {
        //[TestMethod]
        public void InsertBits_Smoke_Test()
        {
            var input = Convert.ToInt32("10000000000", 2);
            var toInsert = "10011";
            Assert.AreEqual(Convert.ToInt32("10001001100"), ModifyBits.Insert(input, toInsert, 2, 6));
        }
    }
}
