using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Sandbox.Combinatorics;

namespace Algorithm.Sandbox.Tests.Combinatorics
{
    [TestClass]
    public class Subset_Tests
    {

        [TestMethod]
        public void Subset_Smoke_Test()
        {
            var input = "".ToCharArray().ToList();
            var combinations = Subset.Find<char>(input);
            Assert.AreEqual(Math.Pow(2, input.Count), combinations.Count);

            input = "cookie".ToCharArray().ToList();
            combinations = Subset.Find<char>(input);
            Assert.AreEqual(Math.Pow(2, input.Count), combinations.Count);

            input = "monster".ToCharArray().ToList();
            combinations = Subset.Find<char>(input);
            Assert.AreEqual(Math.Pow(2, input.Count), combinations.Count);
        }


    }
}
