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
        [TestMethod]
        public void RegularExpressionMatching_Smoke_Test()
        {
            Assert.IsFalse(RegularExpressionMatching.IsMatch("aa", "a"));
            Assert.IsTrue(RegularExpressionMatching.IsMatch("aa", "aa"));

            Assert.IsFalse(RegularExpressionMatching.IsMatch("aaa", "aa"));
            Assert.IsTrue(RegularExpressionMatching.IsMatch("aa", "a*"));

            Assert.IsTrue(RegularExpressionMatching.IsMatch("aa", ".*"));
            Assert.IsTrue(RegularExpressionMatching.IsMatch("ab", ".*"));

            Assert.IsFalse(RegularExpressionMatching.IsMatch("aab", "c*a*b"));
            Assert.IsTrue(RegularExpressionMatching.IsMatch("aab", "*a*b"));
        }
    }
}
