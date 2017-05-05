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
    public class BalancedPartition_Tests
    {
        [TestMethod]
        public void Smoke_Test_Partition()
        {
            var input = new int[] { 3, 1, 1, 2, 2, 1, 4 , 2};

            var partitionA = BalancedPartition.FindPartition(input);

            Assert.AreEqual(8, partitionA
                .Select(i => input[i])
                .Sum());

            var partitionB = input.Where((x, i) => !partitionA.Contains(i)).ToList();

            Assert.AreEqual(8, 
                partitionB.Sum());
        }
    }
}
