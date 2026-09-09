using System;
using System.Collections.Generic;
using System.Linq;
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

            Assert.ThrowsException<InvalidOperationException>(
                () => encoder.Compress(Array.Empty<char>()));
        }

        [TestMethod]
        public void HuffmanCoding_Oracle_Roundtrip_And_Prefix_Free()
        {
            var fixtures = new[]
            {
                "abcasdasdasdcaaaaaadqwerdasd".ToCharArray(),
                new[] { 'a', 'b', 'a', 'a' },
                new[] { 'x', 'x', 'x' },
                "hello huffman".ToCharArray(),
                Enumerable.Range(0, 50).Select(i => (char)('a' + i % 7)).ToArray()
            };

            foreach (var input in fixtures)
            {
                var codes = new HuffmanCoding<char>().Compress(input);

                Assert.AreEqual(input.Distinct().Count(), codes.Count);
                AssertPrefixFree(codes);
                AssertMoreFrequentHasShorterOrEqualCode(input, codes);
                CollectionAssert.AreEqual(input, Decode(input, codes));
            }
        }

        private static void AssertPrefixFree(Dictionary<char, byte[]> codes)
        {
            var codeStrings = codes.Values.Select(c => string.Concat(c)).ToList();

            for (var i = 0; i < codeStrings.Count; i++)
            for (var j = 0; j < codeStrings.Count; j++)
            {
                if (i == j || codeStrings[i].Length == 0) continue;
                Assert.IsFalse(codeStrings[j].StartsWith(codeStrings[i], StringComparison.Ordinal));
            }
        }

        private static void AssertMoreFrequentHasShorterOrEqualCode(char[] input, Dictionary<char, byte[]> codes)
        {
            var frequencies = input.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

            foreach (var left in frequencies)
            foreach (var right in frequencies)
            {
                if (left.Value <= right.Value) continue;
                Assert.IsTrue(codes[left.Key].Length <= codes[right.Key].Length);
            }
        }

        private static char[] Decode(char[] input, Dictionary<char, byte[]> codes)
        {
            if (codes.Count == 1 && codes.First().Value.Length == 0)
                return Enumerable.Repeat(codes.First().Key, input.Length).ToArray();

            var root = new DecodeNode();
            foreach (var pair in codes)
            {
                var node = root;
                foreach (var bit in pair.Value)
                {
                    if (bit == 0)
                    {
                        node.Left ??= new DecodeNode();
                        node = node.Left;
                    }
                    else
                    {
                        node.Right ??= new DecodeNode();
                        node = node.Right;
                    }
                }

                node.Symbol = pair.Key;
                node.HasSymbol = true;
            }

            var bits = new List<byte>();
            foreach (var ch in input) bits.AddRange(codes[ch]);

            var decoded = new List<char>();
            var current = root;
            foreach (var bit in bits)
            {
                current = bit == 0 ? current.Left : current.Right;
                if (!current.HasSymbol) continue;
                decoded.Add(current.Symbol);
                current = root;
            }

            return decoded.ToArray();
        }

        private sealed class DecodeNode
        {
            public DecodeNode Left { get; set; }
            public DecodeNode Right { get; set; }
            public char Symbol { get; set; }
            public bool HasSymbol { get; set; }
        }
    }
}