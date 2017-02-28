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
        public void BTree_Smoke_Test()
        {
            //insert test
            var tree = new AsBTree<int>(3);

            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(21);
            tree.Insert(9);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(10);
            tree.Insert(12);
            tree.Insert(4);
            tree.Insert(8);


            ////delete
            //tree.Delete(43);
            //tree.Delete(11);
            //tree.Delete(6);
            //tree.Delete(8);
            //tree.Delete(19);
            //tree.Delete(4);
            //tree.Delete(10);
            //tree.Delete(5);
            //tree.Delete(17);
            //tree.Delete(49);
            //tree.Delete(31);

            //Assert.AreEqual(tree.Count, 0);

            //tree.Insert(31);
        }

        [TestMethod]
        public void BTree_AccuracyTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new AsBTree<int>(4);

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);

                var actualHeight = tree.GetHeight();

                //http://cs.stackexchange.com/questions/31990/min-max-height-of-b-tree
                var maxHeight = Math.Log(nodeCount + 1, 3) + 1;

                Assert.IsTrue(actualHeight <= maxHeight);
                Assert.IsTrue(tree.Count == i + 1);
            }


            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                                   .OrderBy(x => rnd.Next())
                                   .ToList();

            //for (int i = 0; i < nodeCount; i++)
            //{
            //    tree.Delete(randomNumbers[i]);
             

            //    var actualHeight = tree.GetHeight();

            //    var maxHeight = 2 * Math.Log(nodeCount + 1, 2);

            //    Assert.IsTrue(actualHeight < maxHeight);
            //    Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            //}

            //Assert.IsTrue(tree.Count == 0);
        }


        [TestMethod]
        public void BTree_StressTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new AsBTree<int>(3);

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                Assert.IsTrue(tree.Count == i + 1);
            }


            ////shuffle again before deletion tests
            //randomNumbers = Enumerable.Range(1, nodeCount)
            //                       .OrderBy(x => rnd.Next())
            //                       .ToList();


            //for (int i = 0; i < nodeCount; i++)
            //{
            //    tree.Delete(randomNumbers[i]);
            //    Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            //}

            //Assert.IsTrue(tree.Count == 0);
        }
    }
}
