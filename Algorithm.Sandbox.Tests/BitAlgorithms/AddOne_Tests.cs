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
    /// http://www.geeksforgeeks.org/add-1-to-a-given-number/
    /// </summary>
    [TestClass]
    public class AddOne_Tests
    {
        //[TestMethod]
        public static void SmokeTest()
        {
            Assert.AreEqual(13, AddOne.Find(12));
        }
    }
}
