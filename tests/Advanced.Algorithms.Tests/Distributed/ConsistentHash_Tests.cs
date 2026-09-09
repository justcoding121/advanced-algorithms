using System;
using System.Collections.Generic;
using Advanced.Algorithms.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests
{
    [TestClass]
    public class ConsistentHashTests
    {
        [TestMethod]
        public void ConsistantHash_Smoke_Test()
        {
            var hash = new ConsistentHash<int>();

            hash.AddNode(15);
            hash.AddNode(25);
            hash.AddNode(172);

            for (var i = 200; i < 300; i++) hash.AddNode(i);

            hash.RemoveNode(15);
            hash.RemoveNode(172);
            hash.RemoveNode(25);

            var rnd = new Random();
            for (var i = 0; i < 1000; i++)
            {
                Assert.AreNotEqual(15, hash.GetNode(rnd.Next().ToString()));
                Assert.AreNotEqual(25, hash.GetNode(rnd.Next().ToString()));
                Assert.AreNotEqual(172, hash.GetNode(rnd.Next().ToString()));

                var t = hash.GetNode(rnd.Next().ToString());
                Assert.IsTrue(t >= 200 && t < 300);
            }
        }

        [TestMethod]
        public void ConsistentHash_Single_Node_And_Remove_Unknown()
        {
            var hash = new ConsistentHash<string>(new[] { "a" }, 10);

            Assert.AreEqual("a", hash.GetNode("any-key"));
            Assert.AreEqual("a", hash.GetNode("another"));

            Assert.ThrowsException<ArgumentException>(() => hash.RemoveNode("missing"));

            hash.AddNode("b");
            hash.RemoveNode("a");
            Assert.AreEqual("b", hash.GetNode("any-key"));
        }

        [TestMethod]
        public void ConsistentHash_Adversarial_Minimal_Remap_On_Add_Remove()
        {
            var hash = new ConsistentHash<string>(new[] { "n1", "n2", "n3" }, 50);
            var before = new Dictionary<string, string>();
            for (var i = 0; i < 300; i++)
            {
                var key = "k" + i;
                before[key] = hash.GetNode(key);
            }

            hash.AddNode("n4");
            for (var i = 0; i < 300; i++)
            {
                var key = "k" + i;
                var after = hash.GetNode(key);
                //keys may only move to the newly added node
                Assert.IsTrue(after == before[key] || after == "n4", key);
            }

            hash.RemoveNode("n4");
            for (var i = 0; i < 300; i++)
            {
                var key = "k" + i;
                Assert.AreEqual(before[key], hash.GetNode(key), key);
            }
        }
    }
}