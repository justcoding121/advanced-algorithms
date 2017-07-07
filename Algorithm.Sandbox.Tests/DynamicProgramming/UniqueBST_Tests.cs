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
    /// http://www.geeksforgeeks.org/construct-all-possible-bsts-for-keys-1-to-n/
    /// </summary>
    public class UniqueBST_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            var tree = new UniqueBST();

            CollectionAssert.AreEqual(new List<int[]>
            {
                new int[] { 1,2,3 },
                new int[] { 1,3,2 },
                new int[] { 2,1,3 },
                new int[] { 3,1,2 },
                new int[] { 3,2,1 },

            }, tree.GetAll(1, 3));
        }
    }
}
