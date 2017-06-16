using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Matrix
{
    [TestClass]
    public class MatrixMinCost_Tests
    {
        [TestMethod]
        public void MinCost_Smoke_Test()
        {
            var matrix = new int[,] { 
                      {1, 2, 3},
                      {4, 8, 2},
                      {1, 5, 3}
                    };

           Assert.AreEqual(8, MinCostMatrixPath.FindPath(matrix));
        }
    }
}
