using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Advanced.Algorithms.Distributed;

namespace Advanced.Algorithms.Tests
{
    [TestClass]
    public class ConsistentHash_Tests
    {

        [TestMethod]
        public void ConsistantHash_Smoke_Test()
        {
            var hash = new ConsistentHash<int>();

            hash.AddNode(15);
            hash.AddNode(25);
            hash.AddNode(172);

            for (int i = 200; i < 300; i++)
            {
                hash.AddNode(i);
            }

            hash.RemoveNode(15);
            hash.RemoveNode(172);
            hash.RemoveNode(25);

            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                Assert.AreNotEqual(15, hash.GetNode(rnd.Next().ToString()));
                Assert.AreNotEqual(25, hash.GetNode(rnd.Next().ToString()));
                Assert.AreNotEqual(172, hash.GetNode(rnd.Next().ToString()));

                var t = hash.GetNode(rnd.Next().ToString());
                Assert.IsTrue(t >= 200 && t < 300);
            }

        }

    }
}