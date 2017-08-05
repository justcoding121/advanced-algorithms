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
    /// Generating all the binary subsets of an integer
    /// </summary>
    [TestClass]
    public class BinarySubsets_Tests
    {
        [TestMethod]
        public void BinarySubsets_SmokeTest()
        {
            CollectionAssert.AreEqual(new List<string>() {
                "1011", "1010", "1001",
                "1000", "0011", "0010",
                "0001", "0000" },
                BinarySubsets.GetSubsets(Convert.ToInt32("1011", 2)));
        }
    }
}
