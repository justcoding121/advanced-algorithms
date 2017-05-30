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
        public static void Smoke_Text_Justification()
        {
            CollectionAssert.AreEqual(
                new List<string>() { "This    is    an",
                                    "example  of text",
                                    "justification.  "},
                TextJustification.GetJustification(new List<string>()
                { "This", "is", "an", "example", "of", "text", "justification." }, 16));
        }
    }
}
