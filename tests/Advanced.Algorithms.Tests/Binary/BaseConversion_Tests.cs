using System;
using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Binary
{
    [TestClass]
    public class BaseConversionTests
    {
        [TestMethod]
        public void BaseConversion_Smoke_Test()
        {
            //decimal to binary
            Assert.AreEqual("11",
                BaseConversion.Convert("1011", "01",
                    "0123456789"));

            //binary to decimal
            Assert.AreEqual("11.5",
                BaseConversion.Convert("1011.10", "01",
                    "0123456789"));

            //decimal to base3
            Assert.AreEqual("Foo",
                BaseConversion.Convert("9", "0123456789",
                    "oF8"));

            //base3 to decimal 
            Assert.AreEqual("9",
                BaseConversion.Convert("Foo", "oF8",
                    "0123456789"));

            //hex to binary
            Assert.AreEqual("10011",
                BaseConversion.Convert("13", "0123456789abcdef",
                    "01"));

            //decimal to hex
            Assert.AreEqual("5.0e631f8a0902de00d1b71758e219652b",
                BaseConversion.Convert("5.05620", "0123456789",
                    "0123456789abcdef"));

            //hex to decimal with precision 5
            Assert.AreEqual("5.05619",
                BaseConversion.Convert("5.0e631f8a0902de00d1b71758e219652b", "0123456789abcdef",
                    "0123456789", 5));
        }

        [TestMethod]
        public void BaseConversion_Edge_Cases()
        {
            Assert.AreEqual("0",
                BaseConversion.Convert("0", "0123456789", "0123456789"));

            Assert.AreEqual("0",
                BaseConversion.Convert("0", "0123456789", "01"));

            Assert.AreEqual("1",
                BaseConversion.Convert("1", "01", "0123456789"));

            Assert.AreEqual("",
                BaseConversion.Convert("", "0123456789", "01"));

            Assert.AreEqual(".5",
                BaseConversion.Convert(".1", "01", "0123456789"));

            Assert.AreEqual("0.1",
                BaseConversion.Convert("0.5", "0123456789", "01"));

            Assert.AreEqual("10.",
                BaseConversion.Convert("2.", "0123456789", "01"));

            Assert.ThrowsException<ArgumentException>(() =>
                BaseConversion.Convert("1", "0", "01"));

            Assert.ThrowsException<ArgumentException>(() =>
                BaseConversion.Convert("1", "01", "0"));

            Assert.ThrowsException<ArgumentException>(() =>
                BaseConversion.Convert("0.1", "0", "01"));

            Assert.ThrowsException<ArgumentException>(() =>
                BaseConversion.Convert("0.1", "01", "0"));
        }
    }
}
