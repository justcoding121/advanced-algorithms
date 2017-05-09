using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/iterative-tower-of-hanoi/
    /// </summary>
    [TestClass]
    public class TowerOfHanoi_Tests
    {
        [TestMethod]
        public void Hanoi_Test()
        {
            var moves = TowerOfHanoi.Tower(1);
            Assert.AreEqual(1, moves);

            moves = TowerOfHanoi.Tower(2);
            Assert.AreEqual(3, moves);

            moves = TowerOfHanoi.Tower(3);
            Assert.AreEqual(7, moves);

            moves = TowerOfHanoi.Tower(4);
            Assert.AreEqual(15, moves);

            moves = TowerOfHanoi.Tower(5);
            Assert.AreEqual(31, moves);

            moves = TowerOfHanoi.Tower(6);
            Assert.AreEqual(63, moves);

            moves = TowerOfHanoi.Tower(7);
            Assert.AreEqual(127, moves);
        }
    }
}
