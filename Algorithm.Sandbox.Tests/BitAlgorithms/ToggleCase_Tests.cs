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
    public class ToggleCase_Tests
    {
        //[TestMethod]
        public void ToggleCase_Smoke_Test()
        {
            Assert.AreEqual("HELLO", ToggleCase.Toggle("hello"));
            Assert.AreEqual("hello", ToggleCase.Toggle("HELLO"));
            Assert.AreEqual("Hello", ToggleCase.Toggle("hELLO"));
        }
    }
}
