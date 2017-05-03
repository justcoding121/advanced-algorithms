using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class Fibornacci_Tests
    {
        [TestMethod]
        public void Smoke_Test_Fibornacci()
        {
            var numbers = FibornacciGenerator.GetFibornacciNumbers(10);

            Assert.AreEqual(55, numbers[9]);
        }
    }
}
