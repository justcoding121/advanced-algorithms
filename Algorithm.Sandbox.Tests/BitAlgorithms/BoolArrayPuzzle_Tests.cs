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
    /// http://www.geeksforgeeks.org/a-boolean-array-puzzle/
    /// </summary>
    [TestClass]
    public class BoolArrayPuzzle_Tests
    {
        //[TestMethod]
        public void SmokeTest()
        {
            CollectionAssert.AreEqual(new int[] { 0, 0 },
                BoolArrayPuzzle.ChangeToZero(new int[] { 0, 1 }));
        }
    }
}
