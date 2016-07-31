using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class StringBuilder_Tests
    {
        /// <summary>
        /// A StringBuilder test
        /// </summary>
        [TestMethod]
        public void StringBuilder_Test()
        {
            var builder = new AsStringBuilder();

            builder.Append("Hello ");
            builder.Append("J");

            Assert.AreEqual(builder.ToString(), "Hello J");
        }
    }
}
