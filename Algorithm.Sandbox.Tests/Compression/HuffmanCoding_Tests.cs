using Algorithm.Sandbox.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.Compression
{
    [TestClass]
    public class HuffmanCoding_Tests
    {
        [TestMethod]
        public void HuffmanCoding_Test()
        {
            var encoder = new HuffmanCoding<char>();

            var compressed = encoder
                .Compress("abcasdasdasdcaaaaaadqwerdasd".ToCharArray());

            Assert.AreEqual(compressed['a'].Length, 1);
        }
    }
}
