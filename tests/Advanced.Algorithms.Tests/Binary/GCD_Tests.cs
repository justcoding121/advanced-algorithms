using System;
using Advanced.Algorithms.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Binary
{
    [TestClass]
    public class GcdTests
    {
        private static int Euclidean(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        [TestMethod]
        public void GCD_Smoke_Test()
        {
            Assert.AreEqual(3, Gcd.Find(-9, 3));
            Assert.AreEqual(15, Gcd.Find(45, 30));

            Assert.AreEqual(1, Gcd.Find(3, 5));
        }

        [TestMethod]
        public void GCD_Edge_Cases()
        {
            Assert.AreEqual(0, Gcd.Find(0, 0));
            Assert.AreEqual(5, Gcd.Find(0, 5));
            Assert.AreEqual(5, Gcd.Find(5, 0));
            Assert.AreEqual(1, Gcd.Find(1, 1));
            Assert.AreEqual(7, Gcd.Find(7, 7));
            Assert.AreEqual(1, Gcd.Find(1, 100));
            Assert.AreEqual(25, Gcd.Find(-50, -25));
            Assert.AreEqual(6, Gcd.Find(-18, 24));
            Assert.AreEqual(1, Gcd.Find(17, 19));
            Assert.AreEqual(1024, Gcd.Find(1024, 2048));
        }

        [TestMethod]
        public void GCD_Oracle_Vs_Euclidean()
        {
            var pairs = new[]
            {
                (0, 0), (0, 1), (1, 0), (1, 1), (2, 2),
                (45, 30), (1071, 462), (1024, 2048), (17, 19),
                (-9, 3), (-50, -25), (-18, 24), (270, 192),
                (1, int.MaxValue), (int.MaxValue, int.MaxValue - 1)
            };

            foreach (var (a, b) in pairs)
                Assert.AreEqual(Euclidean(a, b), Gcd.Find(a, b), $"gcd({a},{b})");

            for (var a = -40; a <= 40; a++)
            for (var b = -40; b <= 40; b++)
                Assert.AreEqual(Euclidean(a, b), Gcd.Find(a, b), $"gcd({a},{b})");
        }
    }
}
