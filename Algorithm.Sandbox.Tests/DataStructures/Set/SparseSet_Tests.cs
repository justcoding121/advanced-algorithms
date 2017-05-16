using Algorithm.Sandbox.DataStructures.Set;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Set
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

            set.Remove(15);

            Assert.IsTrue(set.HasItem(6));
            Assert.AreEqual(2, set.Length);
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
        }
    }
}
