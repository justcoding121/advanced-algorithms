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
        public void WildCardMatching_Smoke_Test()
        {
            Assert.IsFalse(WildCardMatching.IsMatch("aa", "a"));
            Assert.IsTrue(WildCardMatching.IsMatch("aa", "aa"));

            Assert.IsFalse(WildCardMatching.IsMatch("aaa", "aa"));
            Assert.IsTrue(WildCardMatching.IsMatch("aa", "*"));

            Assert.IsFalse(WildCardMatching.IsMatch("aasdfsdfs dfsdfs df", "*?dfsdfs d"));
            Assert.IsTrue(WildCardMatching.IsMatch("aasdfsdfs dfsdfs df", "*?dfsdfs df"));

            Assert.IsTrue(WildCardMatching.IsMatch("ab", "?*"));

            Assert.IsFalse(WildCardMatching.IsMatch("aab", "c*a*b"));

        }
    }
}
