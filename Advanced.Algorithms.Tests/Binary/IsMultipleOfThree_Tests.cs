using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Binary
{

    [TestClass]
    public class IsMultipleOfThree_Tests
    {
        [TestMethod]
        public void IsMultipleOfThree_Test()
        {
            Assert.IsTrue(IsMultipleOfThree.IsTrue(39));
            Assert.IsFalse(IsMultipleOfThree.IsTrue(35));

            Assert.IsTrue(IsMultipleOfThree.IsTrue(3));
            Assert.IsFalse(IsMultipleOfThree.IsTrue(5));
        }
    }
}
