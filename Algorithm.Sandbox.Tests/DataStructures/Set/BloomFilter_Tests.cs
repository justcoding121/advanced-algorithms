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
    public class BloomFilter_Tests
    {
        [TestMethod]
        public void BloomFilter_Smoke_Test()
        {
            var filter = new BloomFilter<string>(100);

            filter.AddKey("cat");
            filter.AddKey("rat");

            Assert.IsTrue(filter.KeyExists("cat"));
            Assert.IsFalse(filter.KeyExists("bat"));


        }
    }
}
