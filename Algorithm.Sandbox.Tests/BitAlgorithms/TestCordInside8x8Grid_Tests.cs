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
    public class TestCordInside8x8Grid_Tests
    {
        //[TestMethod]
        public void TestCordInside8x8Grid_Smoke_Test()
        {
            Assert.IsTrue(TestCordInside8x8Grid.IsInside(3, 4));
        }
    }
}
