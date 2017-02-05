using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class ArrayList_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void ArrayList_Test()
        {
            var arrayList = new AsArrayList<int>();
            int nodeCount = 1000 * 10;

            for (int i = 0; i <= nodeCount; i++)
            {
                arrayList.AddItem(i);
                Assert.AreEqual(true, arrayList.Contains(i));
            }


            for (int i = 0; i <= nodeCount; i++)
            {
                arrayList.RemoveItem(0);
                Assert.AreEqual(false, arrayList.Contains(i));
            }


            var rnd = new Random();
            var testSeries = Enumerable.Range(1, nodeCount).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                arrayList.AddItem(item);
                Assert.AreEqual(true, arrayList.Contains(item));
            }

            for (int i = 1; i <= nodeCount; i++)
            {
                arrayList.RemoveItem(0);
  
            }

        }
    }
}
