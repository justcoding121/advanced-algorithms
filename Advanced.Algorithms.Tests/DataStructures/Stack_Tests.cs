using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class Stack_Tests
    {

        [TestMethod]
        public void ArrayStack_Test()
        {
            var stack = new Stack<string>();

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

        [TestMethod]
        public void LinkedListStack_Test()
        {
            var stack = new Stack<string>(StackType.LinkedList);

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
