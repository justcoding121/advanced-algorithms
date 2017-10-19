using Advanced.Algorithms.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Compression
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
