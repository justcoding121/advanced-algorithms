using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class CountDecodings_Test
    {
        [TestMethod]
        public void Count_Decoding_Smoke_Test()
        {
            var result = CountDecodings.Count("121");

            Assert.AreEqual(3, result);

            result = CountDecodings.Count("1234");

            Assert.AreEqual(3, result);
        }
    }
}
