using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class Stack_Tests
    {
        /// <summary>
        /// A dynamic array test
        /// </summary>
        [TestMethod]
        public void Stack_Test()
        {
            var stack = new AsStack<string>();

            stack.Push("a");
            stack.Push("b");

            Assert.AreEqual(stack.Count, 2);
            Assert.AreEqual(stack.Peek(), "b");

            stack.Pop();

            Assert.AreEqual(stack.Count, 1);
            Assert.AreEqual(stack.Peek(), "a");

            stack.Pop();

            Assert.AreEqual(stack.Count, 0);

            stack.Push("a");
            Assert.AreEqual(stack.Count, 1);
            Assert.AreEqual(stack.Peek(), "a");
        }
    }
}
