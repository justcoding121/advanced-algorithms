﻿using System.Linq;
using Advanced.Algorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.DataStructures
{
    [TestClass]
    public class DoublyLinkedListTests
    {
        /// <summary>
        ///     doubly linked list tests
        /// </summary>
        [TestMethod]
        public void DoublyLinkedList_Test()
        {
            var list = new DoublyLinkedList<string>();

            list.InsertFirst("a");
            list.InsertLast("b");
            list.InsertFirst("c");
            list.InsertLast("d");

            //{c,a,b,c}
            Assert.AreEqual(list.Count(), 4);
            Assert.AreEqual(list.Head.Data, "c");

            list.Delete("c");

            //{a,b,c}
            Assert.AreEqual(list.Count(), 3);
            Assert.AreEqual(list.Head.Data, "a");

            //{b}
            list.DeleteFirst();
            list.DeleteLast();

            Assert.AreEqual(list.Count(), 1);
            Assert.AreEqual(list.Head.Data, "b");

            list.Delete("b");
            Assert.AreEqual(list.Count(), 0);

            list.InsertFirst("a");
            list.InsertLast("a");
            list.InsertFirst("c");
            list.InsertLast("a");

            list.Delete("c");
            list.Delete("a");
            list.Delete("a");
            list.Delete("a");
            Assert.AreEqual(list.Count(), 0);
        }
    }
}