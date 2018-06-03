using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.DataStructures.Tree
{
    [TestClass]
    public class RTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void RTree_Insertion_Test()
        {
            var nodeCount = 1000;
            var randomPolygons = new System.Collections.Generic.HashSet<Polygon>();
            for (int i = 0; i < nodeCount; i++)
            {
                randomPolygons.Add(getRandomPolygon());
            }
            var order = 5;
            var tree = new RTree(order);

            int j = 0;
            foreach (var polygon in randomPolygons)
            {
                tree.Insert(polygon);

                //height should be similar to that of B+ tree.
                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight = Math.Ceiling(Math.Log((j + 2) / 2, (int)Math.Ceiling((double)order / 2))) + 1;
                var actualMaxHeight = verifyHeightUniformity(tree.Root, order);
                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                j++;

                Assert.IsTrue(tree.Exists(polygon));
            }

            var leafs = tree.Root.GetLeafs();

            Assert.AreEqual(j, leafs.Count);
        }

        /// </summary>
        [TestMethod]
        public void RTree_Range_Search_Test()
        {
            var nodeCount = 1000;
            var randomPolygons = new System.Collections.Generic.HashSet<Polygon>();
            for (int i = 0; i < nodeCount; i++)
            {
                randomPolygons.Add(getRandomPolygon());
            }
            var order = 5;
            var tree = new RTree(order);

            foreach (var polygon in randomPolygons)
            {
                tree.Insert(polygon);
            }

            var searchRectangle = getRandomPolygon().GetContainingRectangle();

            var expectedIntersections = randomPolygons.Where(x => RectangleIntersection.FindIntersection(searchRectangle, x.GetContainingRectangle()) != null).ToList();
            var actualIntersections = tree.RangeSearch(searchRectangle);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        /// </summary>
        [TestMethod]
        public void RTree_Deletion_Test()
        {
            var nodeCount = 1000;
            var randomPolygons = new System.Collections.Generic.HashSet<Polygon>();
            for (int i = 0; i < nodeCount; i++)
            {
                randomPolygons.Add(getRandomPolygon());
            }
            var order = 5;
            var tree = new RTree(order);

            foreach (var polygon in randomPolygons)
            {
                tree.Insert(polygon);
            }

            int j = randomPolygons.Count;
            foreach (var polygon in randomPolygons)
            {
                tree.Delete(polygon);
                Assert.IsFalse(tree.Exists(polygon));
                j--;
                var polygons = tree.Root.GetLeafs();
                verifyHeightUniformity(tree.Root, order);
                Assert.AreEqual(j, polygons.Count);
            }
        }

        [TestMethod]
        public void RTree_Range_Stress_Test()
        {
            var nodeCount = 10000;
            var randomPolygons = new System.Collections.Generic.HashSet<Polygon>();
            for (int i = 0; i < nodeCount; i++)
            {
                randomPolygons.Add(getRandomPolygon());
            }

            var order = 12;
            var tree = new RTree(order);

            foreach (var polygon in randomPolygons)
            {
                tree.Insert(polygon);
            }

            foreach (var polygon in randomPolygons)
            {
                tree.Delete(polygon);
            }
        }
        /// <summary>
        ///     Verifies that all children have same height.
        /// </summary>
        /// <returns>Returns the height of given node</returns>
        private int verifyHeightUniformity(RTreeNode node, int order)
        {
            if (!node.IsLeaf)
            {
                Assert.IsTrue(node.KeyCount >= order / 2);
            }

            var heights = new List<int>();
            foreach (var child in node.Children.Take(node.KeyCount))
            {
                heights.Add(verifyHeightUniformity(child, order) + 1);
            }

            if (node.KeyCount > 0)
            {
                var height = heights.Distinct();
                Assert.AreEqual(1, height.Count());
                return height.First();
            }

            return 0;
        }

        private static Random random = new Random();

        private static Polygon getRandomPolygon()
        {
            var edgeLength = random.Next(2, 5);

            var edgePoints = new List<Point>();

            while (edgeLength > 0)
            {
                edgePoints.Add(new Point(random.Next(0, 100) * random.NextDouble(), random.Next(0, 100) * random.NextDouble()));
                edgeLength--;
            }

            return new Polygon(edgePoints);
        }
    }
}
