using Algorithm.Sandbox.DynamicProgramming.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Matrix
{
    /// <summary>
    /// Problem statement here
    /// http://www.geeksforgeeks.org/dynamic-programming-set-8-matrix-chain-multiplication/
    /// </summary>
    [TestClass]
    public class ChainMultiplication_Tests
    {
        [TestMethod]
        public void Smoke_Test_ChainMultiplication()
        {
            var result = ChainMultiplication
                .FindMinMultiplications(new int[] { 1, 2, 3, 4 });
        }
    }
}
