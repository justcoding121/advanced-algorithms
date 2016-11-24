using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class HashSet_Tests
    {
        /// <summary>
        /// key value dictionary tests 
        /// </summary>
        [TestMethod]
        public void HashSet_Test()
        {
            var hashSet = new AsHashSet<string, int>(100);

            hashSet.Add("a", 1);
            hashSet.Add("b", 2);
            hashSet.Add("c", 3);

            Assert.AreEqual(hashSet.GetValue("a"), 1);
            Assert.AreEqual(hashSet.GetValue("b"), 2);

            Assert.IsTrue(hashSet.ContainsKey("a"));
            Assert.IsTrue(hashSet.ContainsKey("b"));

            hashSet.Remove("a");
            Assert.IsFalse(hashSet.ContainsKey("a"));
            Assert.IsTrue(hashSet.ContainsKey("b"));

        }
    }
}
