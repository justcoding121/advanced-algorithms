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
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-37-boolean-parenthesization-problem/
    /// </summary>
    [TestClass]
    public class BoolParenthesis_Tests
    {
        [TestMethod]
        public void BoolParenthesis_Smoke_Test()
        {
            //T|F^F&T|F^F^F^T|T&T^T|F^T^F&F^T|T^F
            var result = CountBoolParenthesization.CountPositiveCombinations(
                new bool[] {
               true, false, false, true, false, false, false, true,
               true, true, true, false, true, false, false, true, true, false
                },
                new char[] { '|', '^', '&', '|', '^', '^', '^', '|', '&', '^', '|', '^', '^', '&', '^', '|', '^' });

            Assert.AreEqual(99632640, result);
        }
    }
}
