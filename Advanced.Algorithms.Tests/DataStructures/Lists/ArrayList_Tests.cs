using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class ArrayList_Tests
    {
        [TestMethod]
        public void ArrayList_Test()
        {
            var arrayList = new ArrayList<int>();
            int nodeCount = 1000;

            for (int i = 0; i <= nodeCount; i++)
            {
                arrayList.Add(i);
                Assert.AreEqual(true, arrayList.Contains(i));
            }

            //IEnumerable test using linq
            Assert.AreEqual(arrayList.Length, arrayList.Count());

            for (int i = 0; i <= nodeCount; i++)
            {
                arrayList.RemoveAt(0);
                Assert.AreEqual(false, arrayList.Contains(i));
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                arrayList.Add(item);
                Assert.AreEqual(true, arrayList.Contains(item));
            }

            for (int i = 1; i <= nodeCount; i++)
            {
                arrayList.RemoveAt(0);
            }

        }

        [TestMethod]
        public void ArrayList_InsertAt_Test()
        {
            var arrayList = new ArrayList<int>();
            int nodeCount = 10;

            for (int i = 0; i <= nodeCount; i++)
            {
                arrayList.InsertAt(i, i);
                Assert.AreEqual(true, arrayList.Contains(i));
            }

            arrayList.InsertAt(5, 50000);

            //IEnumerable test using linq
            Assert.AreEqual(arrayList.Length, arrayList.Count());

            Assert.AreEqual(true, arrayList.Contains(50000));
            Assert.AreEqual(nodeCount + 2, arrayList.Length);
        }
    }
}
