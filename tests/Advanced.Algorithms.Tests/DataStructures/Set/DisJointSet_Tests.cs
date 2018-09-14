using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class DisJointSet_Tests
    {
        [TestMethod]
        public void Smoke_Test_DisJointSet()
        {
            var disjointSet = new DisJointSet<int>();

            for (int i = 1; i <= 7; i++)
            {
                disjointSet.MakeSet(i);
            }

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
    }
}
