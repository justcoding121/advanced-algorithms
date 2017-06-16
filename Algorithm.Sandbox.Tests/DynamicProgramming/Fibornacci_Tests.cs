using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/program-for-nth-fibonacci-number/
    /// </summary>
    [TestClass]
    public class Fibornacci_Tests
    {
        [TestMethod]
        public void Fibornacci_Smoke_Test()
        {
            var numbers = FibornacciGenerator.GetFibornacciNumbers(10);

            Assert.AreEqual(55, numbers[9]);
        }
    }
}
