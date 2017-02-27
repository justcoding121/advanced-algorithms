using Algorithm.Sandbox.DataStructures.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class BTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void BST_Test()
        {
            //insert test
            var tree = new AsBTree<int>(3);
           

            tree.Insert(11);
            tree.Insert(6);
            tree.Insert(8);
            tree.Insert(19);
            tree.Insert(4);
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(17);
            tree.Insert(43);
            tree.Insert(49);
            tree.Insert(31);


            //delete
            tree.Delete(43);
            tree.Delete(11);
            tree.Delete(6);
            tree.Delete(8);
            tree.Delete(19);
            tree.Delete(4);
            tree.Delete(10);
            tree.Delete(5);
            tree.Delete(17);
            tree.Delete(49);
            tree.Delete(31);

            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }
    }
}
