using Algorithm.Sandbox.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.String
{
    [TestClass]
    public class SubSet_Tests
    {
        [TestMethod]
        public void String_Subset_Test()
        {
            SubSets.PrintSubSets();
            SubSets.PrintSubSets(2);
        }
    }
}
