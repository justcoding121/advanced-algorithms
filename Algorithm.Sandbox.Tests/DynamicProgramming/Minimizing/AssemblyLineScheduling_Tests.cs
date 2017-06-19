using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-34-assembly-line-scheduling/
    /// </summary>
    [TestClass]
    public class AssemblyLineScheduling_Tests
    {
        [TestMethod]
        public void AssemblyLineScheduling_SmokeTest()
        {
            Assert.AreEqual(35, 
            AssemblyLineScheduling.GetMinTime(new int[2][]{
               new int[] {4, 5, 3, 2},
               new int[] {2, 10, 1, 4}
            }, new int[2][]{
               new int[] {0, 7, 4, 5},
               new int[] {0, 9, 2, 8}
            },
             new int[] { 10, 12 },
             new int[] { 18, 7 }));
        }
    }
}
