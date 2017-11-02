using Advanced.Algorithms.DynamicProgramming.Count;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.DynamicProgramming.Count
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class DigitCounter_Tests
    {
        [TestMethod]
        public void DigitCount_Smoke_Test()
        {
            Assert.AreEqual(0, DigitCounter.Count(5, 9));
            Assert.AreEqual(1, DigitCounter.Count(9, 9));

            Assert.AreEqual(9, DigitCounter.Count(25, 2));
            Assert.AreEqual(3, DigitCounter.Count(26, 6));
            Assert.AreEqual(4, DigitCounter.Count(30, 3));

            Assert.AreEqual(280, DigitCounter.Count(898, 6));

            Assert.AreEqual(910116681, DigitCounter.Count(1015242410, 6));
        }
    }
}
