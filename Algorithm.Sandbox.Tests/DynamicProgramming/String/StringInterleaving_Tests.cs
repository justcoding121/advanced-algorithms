using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DynamicProgramming
{
    public class StringInterleaving_Tests
    {
        //[TestMethod]
        public void Smoke_Test()
        {
            Assert.IsFalse(StringInterleaving.IsInterleaved("XXY", "XXZ", "XXZXXXY"));
            Assert.IsTrue(StringInterleaving.IsInterleaved("WZXY", "XY", "WZ"));

        }
    }
}
