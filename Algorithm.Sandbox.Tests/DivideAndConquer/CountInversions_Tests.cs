using Algorithm.Sandbox.DivideAndConquer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DivideAndConquer
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/counting-inversions/
    /// </summary>
    [TestClass]
    public class CountInversions_Tests
    {
        //[TestMethod]
        public void CountInversions_Smoke_Test()
        {
            var arr = new int[] { 1, 20, 6, 4, 5 };
            Assert.AreEqual(5, CountInversions.Count(arr));
        }
    }
}
