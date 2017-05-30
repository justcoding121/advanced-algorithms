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
    /// https://leetcode.com/problems/wildcard-matching/#/description
    /// </summary>
    [TestClass]
    public class WildCardMatching_Tests
    {
        [TestMethod]
        public void SmokeTest()
        {
            Assert.IsFalse(WildCardMatching.IsMatch("aa", "a"));
            Assert.IsTrue(WildCardMatching.IsMatch("aa", "aa"));           
        }
    }
}
