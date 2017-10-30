using Advanced.Algorithms.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-18-partition-problem/
    /// </summary>
    [TestClass]
    public class BalancedPartition_Tests
    {
        [TestMethod]
        public void Partition_Smoke_Test()
        {
            var input = new int[] { 3, 1, 1, 2, 2, 1, 4 , 2};

            Assert.IsTrue(BalancedPartition.CanPartition(input));
        }
    }
}
