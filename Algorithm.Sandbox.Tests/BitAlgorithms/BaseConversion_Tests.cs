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
    public class BaseConversion_Tests
    {
        [TestMethod]
        public void BaseConversion_Smoke_Test()
        {
            Assert.AreEqual("Foo",
                BaseConversion.Convert("9", "0123456789", 
                    "oF8"));

            Assert.AreEqual("9",
                BaseConversion.Convert("Foo", "oF8",
                    "0123456789"));

            Assert.AreEqual("10011",
                BaseConversion.Convert("13", "0123456789abcdef",
                    "01"));

            Assert.AreEqual("JAM!",
                BaseConversion.Convert("CODE", "O!CDE?",
                    "A?JM!."));

        }
    }
}
