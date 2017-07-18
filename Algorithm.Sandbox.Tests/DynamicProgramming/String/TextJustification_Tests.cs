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
            var length = TextJustification.GetJustification(new List<string>()
                { "This", "is", "an", "example", "of", "text", "justification." }, 16);

            //CollectionAssert.AreEqual(
            //    new List<string>() { "This    is    an",
            //                        "example  of text",
            //                        "justification.  "},
            //   );
        }
    }
}
