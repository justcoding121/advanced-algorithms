using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Advanced.Algorithms.DataStructures;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class SparseSet_Tests
    {
        [TestMethod]
        public void SparseSet_Smoke_Test()
        {
            var set = new SparseSet(15, 10);

            set.Add(6);
            set.Add(15);
            set.Add(0);

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());

            set.Remove(15);

            Assert.IsTrue(set.HasItem(6));
            Assert.AreEqual(2, set.Count);

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());
        }

        [TestMethod]
        public void SparseSet_Stress_Test()
        {
            var set = new SparseSet(1000, 1000);

            var random = new Random();
            var testCollection = Enumerable.Range(0, 1000)
                .OrderBy(x => random.Next())
                .ToList();

            foreach(var element in testCollection)
            {
                set.Add(element);
            }

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());

            foreach (var element in testCollection)
            {
                Assert.IsTrue(set.HasItem(element));
            }

            foreach (var element in testCollection)
            {
                Assert.IsTrue(set.HasItem(element));
                set.Remove(element);
                Assert.IsFalse(set.HasItem(element));
            }

            //IEnumerable test
            Assert.AreEqual(set.Count, set.Count());
        }
    }
}
