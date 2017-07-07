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
    /// https://leetcode.com/problems/regular-expression-matching/#/description
    /// </summary>
    [TestClass]
    public class RegularExpressionMatching_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            Assert.IsFalse(RegularExpressionMatching.IsMatch("aa", "a"));
            Assert.IsTrue(RegularExpressionMatching.IsMatch("aa", "aa"));           
        }
    }
}
