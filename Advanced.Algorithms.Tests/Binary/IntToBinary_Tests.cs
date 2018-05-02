using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Advanced.Algorithms.Tests.Binary
{
    [TestClass]
    public class IntToBinary_Tests
    {
        [TestMethod]
        public void IntToBinary_Smoke_Test()
        {
          Assert.AreEqual("00000001", IntToBinary.GetBinary(1, 8));
          Assert.AreEqual("11111111", IntToBinary.GetBinary(-1, 8));
        }
    }
}
