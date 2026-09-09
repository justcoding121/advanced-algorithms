using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class DisJointSetTests
    {
        [TestMethod]
        public void Smoke_Test_DisJointSet()
        {
            var disjointSet = new DisJointSet<int>();

            for (var i = 1; i <= 7; i++) disjointSet.MakeSet(i);

            //IEnumerable test
            Assert.AreEqual(disjointSet.Count, disjointSet.Count());

            disjointSet.Union(1, 2);
            Assert.AreEqual(1, disjointSet.FindSet(2));

            disjointSet.Union(2, 3);
            Assert.AreEqual(1, disjointSet.FindSet(3));

            disjointSet.Union(4, 5);
            Assert.AreEqual(4, disjointSet.FindSet(4));

            disjointSet.Union(5, 6);
            Assert.AreEqual(4, disjointSet.FindSet(5));

            disjointSet.Union(6, 7);
            Assert.AreEqual(4, disjointSet.FindSet(6));

            Assert.AreEqual(4, disjointSet.FindSet(4));
            disjointSet.Union(3, 4);
            Assert.AreEqual(1, disjointSet.FindSet(4));

            //IEnumerable test
            Assert.AreEqual(disjointSet.Count, disjointSet.Count());
        }

        [TestMethod]
        public void DisJointSet_Connectivity_Oracle()
        {
            var ds = new DisJointSet<int>();
            var parent = new System.Collections.Generic.Dictionary<int, int>();

            int Find(int x)
            {
                while (parent[x] != x) x = parent[x];
                return x;
            }

            void Union(int a, int b)
            {
                a = Find(a);
                b = Find(b);
                if (a != b) parent[b] = a;
            }

            for (var i = 0; i < 50; i++)
            {
                ds.MakeSet(i);
                parent[i] = i;
            }

            var rnd = new Random(1);
            for (var t = 0; t < 200; t++)
            {
                var a = rnd.Next(50);
                var b = rnd.Next(50);
                ds.Union(a, b);
                Union(a, b);
            }

            for (var i = 0; i < 50; i++)
            for (var j = 0; j < 50; j++)
            {
                var sameAa = ds.FindSet(i).Equals(ds.FindSet(j));
                var sameOr = Find(i) == Find(j);
                Assert.AreEqual(sameOr, sameAa);
            }
        }
    }
}
