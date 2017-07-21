using Algorithm.Sandbox.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    [TestClass]
    public class TextJustification_Tests
    {
        [TestMethod]
        public void Smoke_Text_Justification()
        {
            var sumOfSquaresOfBlankSpaceEndingsInEachLine = TextJustification.GetJustification(new List<string>()
                { "Ramanu", "Dog", "likes", "to", "code"}, 10);

            Assert.AreEqual(26, sumOfSquaresOfBlankSpaceEndingsInEachLine);
        }
    }
}
