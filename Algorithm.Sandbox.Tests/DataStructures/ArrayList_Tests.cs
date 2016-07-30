using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class ArrayList_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void ArrayList_Test()
        {
            var arrayList = new AsArrayList<string>();

            arrayList.AddItem("a");
            arrayList.AddItem("b");
            arrayList.AddItem("c");

            Assert.AreEqual(arrayList.Length, 3);

            arrayList.RemoveItem(0);

            Assert.AreEqual(arrayList.Length, 2);

            arrayList.SetItem(0, "a");
            Assert.AreEqual(arrayList.ItemAt(0), "a");

        }
    }
}
