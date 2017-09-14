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
    /// Find the subset of all set bits in given integer 
    /// </summary>
    [TestClass]
    public class BinarySubsets_Tests
    {
        [TestMethod]
        public void BinarySubsets_SmokeTest()
        {

            CollectionAssert.AreEqual(new List<int>() {
                Convert.ToInt32("1011",2),
                Convert.ToInt32("1010",2),
                Convert.ToInt32("1001",2),
                Convert.ToInt32("1000",2),
                Convert.ToInt32("0011",2),
                Convert.ToInt32("0010",2),
                Convert.ToInt32("0001",2),
                Convert.ToInt32("0000",2) },
                BinarySubsets.GetSubsets(Convert.ToInt32("1011", 2)));

            CollectionAssert.AreEqual(new List<int>() {
                Convert.ToInt32("10000",2),
                Convert.ToInt32("0000",2) },
                BinarySubsets.GetSubsets(Convert.ToInt32("10000", 2)));
        }
    }
}
