using System;
using Advanced.Algorithms.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Compression
{
    [TestClass]
    public class HuffmanCodingTests
    {
        [TestMethod]
        public void HuffmanCoding_Test()
        {
            var encoder = new HuffmanCoding<char>();

            var compressed = encoder
                .Compress("abcasdasdasdcaaaaaadqwerdasd".ToCharArray());

            Assert.AreEqual(compressed['a'].Length, 1);
        }

        [TestMethod]
        public void HuffmanCoding_Single_Symbol()
        {
            var encoder = new HuffmanCoding<char>();
            var compressed = encoder.Compress(new[] { 'a' });

            Assert.AreEqual(1, compressed.Count);
            Assert.AreEqual(0, compressed['a'].Length);
        }

        [TestMethod]
        public void HuffmanCoding_All_Identical()
        {
            var encoder = new HuffmanCoding<char>();
            var compressed = encoder.Compress(new[] { 'x', 'x', 'x', 'x' });

            Assert.AreEqual(1, compressed.Count);
            Assert.AreEqual(0, compressed['x'].Length);
        }

        [TestMethod]
        public void HuffmanCoding_Two_Symbols()
        {
            var encoder = new HuffmanCoding<char>();
            var compressed = encoder.Compress(new[] { 'a', 'b', 'a', 'a' });

            Assert.AreEqual(2, compressed.Count);
            Assert.AreEqual(1, compressed['a'].Length);
            Assert.AreEqual(1, compressed['b'].Length);
            Assert.AreNotEqual(compressed['a'][0], compressed['b'][0]);
        }

        [TestMethod]
        public void HuffmanCoding_Empty_Throws()
        {
            var encoder = new HuffmanCoding<char>();

            Assert.ThrowsException<Exception>(
                () => encoder.Compress(Array.Empty<char>()));
        }
    }
}