using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Binary
{

    [TestClass]
    public class IsMultipleOfNine_Tests
    {
        [TestMethod]
        public void IsMultipleOfNine_Smoke_Test()
        {
            Assert.AreEqual(true, IsMultipleOfNine.IsTrue(81));
            Assert.AreEqual(true, IsMultipleOfNine.IsTrue(90));

            Assert.AreEqual(false, IsMultipleOfNine.IsTrue(91));
            Assert.AreEqual(false, IsMultipleOfNine.IsTrue(102));
        }
    }
}
