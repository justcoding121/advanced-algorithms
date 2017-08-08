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
    /// http://www.geeksforgeeks.org/find-the-element-that-appears-once/
    /// </summary>
    [TestClass]
    public class FindUniqueElement_Tests
    {
        [TestMethod]
        public void FindUniqueElement_Smoke_Test()
        {
            Assert.AreEqual(2, 
                FindUniqueElement
                 .Find(new int[] { 12, 1, 12, 3, 12, 1, 1, 2, 3, 3 }));
        }
    }
}
