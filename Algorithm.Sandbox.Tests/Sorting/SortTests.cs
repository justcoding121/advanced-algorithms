using Algorithm.Sandbox.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.Sorting
{
    [TestClass]
    public class Sort_Tests
    {
        private static int[] TestArray = new int[] { 7, 8, 3, 2, 1, 5, 4, 6, 0 };

        /// <summary>
        /// </summary>
        [TestMethod]
        public void BubbleSort_Test()
        {
            var result = BubbleSort<int>.Sort(TestArray);

            for (int i = 0; i <= 8; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }


        /// <summary>
        /// </summary>
        [TestMethod]
        public void SelectionSort_Test()
        {
            var result = SelectionSort<int>.Sort(TestArray);

            for (int i = 0; i <= 8; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void InsertionSort_Test()
        {
            var result = InsertionSort<int>.Sort(TestArray);

            for (int i = 0; i <= 8; i++)
            {
                Assert.AreEqual(i, result[i]);
            }
        }
    }
}
