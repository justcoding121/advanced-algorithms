using System;
using System.Linq;
using Advanced.Algorithms.DataStructures.Foundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SysStack = System.Collections.Generic.Stack<int>;

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

        /// <summary>
        ///     Push/Pop/Peek oracle vs System.Collections.Generic.Stack for both backends.
        /// </summary>
        [TestMethod]
        public void Stack_SystemStackOracle_RandomOps()
        {
            foreach (StackType type in new[] { StackType.Array, StackType.LinkedList })
            {
                var rng = new Random(23 + (int)type);
                var ours = new Stack<int>(type);
                var oracle = new SysStack();

                for (var step = 0; step < 500; step++)
                {
                    var op = rng.Next(3);

                    if (op == 0 || oracle.Count == 0)
                    {
                        var v = rng.Next(1000);
                        ours.Push(v);
                        oracle.Push(v);
                    }
                    else if (op == 1)
                    {
                        Assert.AreEqual(oracle.Pop(), ours.Pop());
                    }
                    else
                    {
                        Assert.AreEqual(oracle.Peek(), ours.Peek());
                    }

                    Assert.AreEqual(oracle.Count, ours.Count);
                    if (oracle.Count > 0)
                        Assert.AreEqual(oracle.Peek(), ours.Peek());
                }
            }
        }
    }
}
