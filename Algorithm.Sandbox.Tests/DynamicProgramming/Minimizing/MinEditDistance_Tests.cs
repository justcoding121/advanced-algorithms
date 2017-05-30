using Algorithm.Sandbox.DynamicProgramming.Minimizing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming.Minimizing
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-5-edit-distance/
    /// </summary>
    public class MinEditDistance_Tests
    {
        public void Smoke_MinEditDistance()
        {
            Assert.AreEqual(1, MinEditDistance.GetMin("geek", "gesek"));
            Assert.AreEqual(1, MinEditDistance.GetMin("cat", "cut"));
            Assert.AreEqual(3, MinEditDistance.GetMin("sunday", "saturday"));
        }
    }
}
