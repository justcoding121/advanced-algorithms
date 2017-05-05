using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class DistinctBinaryString_Tests
    {
        [TestMethod]
        public void Smoke_Test_DistinctBinaryString()
        {
           var result = DistinctBinaryString.Count(3);
        }
    }
}
