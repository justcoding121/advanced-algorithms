using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class StackTests
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

        [TestMethod]
        public void ArrayStack_Empty_Pop_Throws()
        {
            var stack = new Stack<int>();
            Assert.ThrowsException<InvalidOperationException>(() => stack.Pop());
        }

        [TestMethod]
        public void LinkedListStack_Empty_Pop_Throws()
        {
            var stack = new Stack<int>(StackType.LinkedList);
            Assert.ThrowsException<InvalidOperationException>(() => stack.Pop());
        }

        [TestMethod]
        public void ArrayStack_Empty_Peek_Returns_Default()
        {
            var stack = new Stack<int>();
            Assert.AreEqual(0, stack.Peek());
        }

        [TestMethod]
        public void ArrayStack_Enumerate_Preserves_Push_Order()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, stack.ToArray());
        }

        [TestMethod]
        public void LinkedListStack_Enumerate_Preserves_LIFO_Order()
        {
            var stack = new Stack<int>(StackType.LinkedList);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, stack.ToArray());
        }

        [TestMethod]
        public void ArrayStack_Single_Element()
        {
            var stack = new Stack<string>();
            stack.Push("only");
            Assert.AreEqual("only", stack.Peek());
            Assert.AreEqual("only", stack.Pop());
            Assert.AreEqual(0, stack.Count);
        }
    }
}
