using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class BentleyOttmannTests
    {
        private static readonly Random Random = new Random();

        [TestMethod]
        public void BentleyOttmann_Smoke_Test_1()
        {
            var lines = new List<Line>();

            var s1 = new Line(new Point(0, 200), new Point(100, 300));
            var s2 = new Line(new Point(20, 350), new Point(110, 150));
            var s3 = new Line(new Point(30, 250), new Point(80, 120));
            var s4 = new Line(new Point(50, 100), new Point(120, 300));

            lines.AddRange(new[] { s1, s2, s3, s4 });

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Smoke_Test_2()
        {
            var lines = new List<Line>();

            var s1 = new Line(new Point(100, 0), new Point(150, 130));
            var s2 = new Line(new Point(20, 80), new Point(80, 70));
            var s3 = new Line(new Point(80, 70), new Point(50, 100));

            lines.AddRange(new[] { s1, s2, s3 });

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Vertical_Lines_Test()
        {
            var lines = new List<Line>();

            lines.AddRange(VerticalLines());

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Horizontal_Lines_Test()
        {
            var lines = new List<Line>();

            lines.AddRange(HorizontalLines());

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Vertical_Horizontal_Lines_Test()
        {
            var lines = new List<Line>();

            //vertical
            lines.Add(new Line(new Point(100, 100), new Point(100, 200)));
            lines.Add(new Line(new Point(125, 100), new Point(125, 200)));
            lines.Add(new Line(new Point(150, 100), new Point(150, 200)));
            lines.Add(new Line(new Point(175, 100), new Point(175, 200)));
            lines.Add(new Line(new Point(200, 100), new Point(200, 200)));

            //horizontal
            lines.Add(new Line(new Point(100, 100), new Point(200, 100)));
            lines.Add(new Line(new Point(100, 125), new Point(200, 125)));
            lines.Add(new Line(new Point(100, 150), new Point(200, 150)));
            lines.Add(new Line(new Point(100, 175), new Point(200, 175)));
            lines.Add(new Line(new Point(100, 200), new Point(200, 200)));

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Vertical_Horizontal_Other_Lines_Test_1()
        {
            var lines = new List<Line>();

            //vertical
            lines.Add(new Line(new Point(100, 100), new Point(100, 200)));
            lines.Add(new Line(new Point(200, 100), new Point(200, 200)));

            //horizontal
            lines.Add(new Line(new Point(100, 100), new Point(200, 100)));
            lines.Add(new Line(new Point(100, 150), new Point(200, 150)));
            lines.Add(new Line(new Point(100, 200), new Point(200, 200)));

            //other lines
            lines.Add(new Line(new Point(100, 100), new Point(200, 200)));
            lines.Add(new Line(new Point(100, 200), new Point(200, 100)));

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }


        [TestMethod]
        public void BentleyOttmann_Vertical_Horizontal_Other_Lines_Test_2()
        {
            var lines = new List<Line>();

            //vertical
            lines.Add(new Line(new Point(100, 100), new Point(100, 200)));
            lines.Add(new Line(new Point(200, 100), new Point(200, 200)));

            //horizontal
            lines.Add(new Line(new Point(100, 100), new Point(200, 100)));
            lines.Add(new Line(new Point(100, 150), new Point(200, 150)));
            lines.Add(new Line(new Point(100, 200), new Point(200, 200)));

            //other lines
            lines.Add(new Line(new Point(110, 100), new Point(210, 200)));
            lines.Add(new Line(new Point(90, 200), new Point(250, 100)));

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann();

            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        /// <summary>
        ///     Regression for https://github.com/justcoding121/advanced-algorithms/issues/45
        /// </summary>
        [TestMethod]
        public void BentleyOttmann_Issue45_All_Intersections_Reported()
        {
            var lines = new List<Line>
            {
                new Line(new Point(0.307769600445297, 0.496908523373729),
                    new Point(0.752325133305194, 0.247345594804429)),
                new Line(new Point(0.59643911784349, 0.212231021938953),
                    new Point(0.936056684207198, 0.558365820701404)),
                new Line(new Point(0.362570754886871, 0.145859633640321),
                    new Point(0.837961102294718, 0.571770970510212)),
                new Line(new Point(0.415960443865489, 0.216604132306112),
                    new Point(0.746803699409032, 0.5559045125525))
            };

            var expectedIntersections = GetExpectedIntersections(lines);

            var bentleyOttmannAlgorithm = new BentleyOttmann(7);
            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);

            Assert.AreEqual(3, expectedIntersections.Count);
            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Corner_Cases_Test()
        {
            var algorithm = new BentleyOttmann();

            var empty = algorithm.FindIntersections(new List<Line>());
            Assert.AreEqual(0, empty.Count);

            var single = algorithm.FindIntersections(new List<Line>
            {
                new Line(new Point(0, 0), new Point(10, 10))
            });
            Assert.AreEqual(0, single.Count);

            Assert.ThrowsException<ArgumentException>(() =>
                algorithm.FindIntersections(new List<Line>
                {
                    new Line(new Point(-1, 0), new Point(1, 1))
                }));

            var vertical = new Line(new Point(1, 0), new Point(1, 5));
            var horizontal = new Line(new Point(0, 2), new Point(5, 2));
            Assert.IsTrue(vertical.IsVertical);
            Assert.IsTrue(horizontal.IsHorizontal);
            Assert.AreEqual(0, horizontal.Slope, 1e-9);
            Assert.AreEqual(double.MaxValue, vertical.Slope);

            var crossing = algorithm.FindIntersections(new List<Line> { vertical, horizontal });
            Assert.AreEqual(1, crossing.Count);
        }

        [TestMethod]
        public void BentleyOttmann_Stress_Test()
        {
            var lines = new List<Line>();

            lines.AddRange(GetRandomLines(1000));

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var expectedIntersections = GetExpectedIntersections(lines);
            stopWatch.Stop();

            var naiveElapsedTime = stopWatch.ElapsedMilliseconds;

            var bentleyOttmannAlgorithm = new BentleyOttmann();
            stopWatch.Reset();

            stopWatch.Start();
            var actualIntersections = bentleyOttmannAlgorithm.FindIntersections(lines);
            stopWatch.Stop();

            var actualElapsedTime = stopWatch.ElapsedMilliseconds;

            Assert.IsTrue(actualElapsedTime <= naiveElapsedTime);
            Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);
        }

        private static Dictionary<Point, List<Line>> GetExpectedIntersections(List<Line> lines)
        {
            var result = new Dictionary<Point, HashSet<Line>>(new PointComparer());

            for (var i = 0; i < lines.Count; i++)
            for (var j = i + 1; j < lines.Count; j++)
            {
                var intersection = LineIntersection.Find(lines[i], lines[j]);

                if (intersection != null)
                {
                    var existing = result.ContainsKey(intersection) ? result[intersection] : new HashSet<Line>();

                    if (!existing.Contains(lines[i])) existing.Add(lines[i]);

                    if (!existing.Contains(lines[j])) existing.Add(lines[j]);

                    result[intersection] = existing;
                }
            }

            return result.ToDictionary(x => x.Key, x => x.Value.ToList());
        }

        private static List<Line> GetRandomLines(int lineCount)
        {
            var lines = new List<Line>();

            while (lineCount > 0)
            {
                lines.Add(GetRandomLine());
                lineCount--;
            }

            return lines;
        }

        private static List<Line> VerticalLines()
        {
            var lines = new List<Line>();

            var s1 = new Line(new Point(100, 200), new Point(100, 600));
            var s2 = new Line(new Point(100, 225), new Point(100, 625));
            var s3 = new Line(new Point(100, 250), new Point(100, 475));
            var s4 = new Line(new Point(100, 290), new Point(100, 675));

            lines.AddRange(new[] { s1, s2, s3, s4 });

            return lines;
        }

        private static List<Line> HorizontalLines()
        {
            var lines = new List<Line>();

            var s1 = new Line(new Point(100, 100), new Point(600, 100));
            var s2 = new Line(new Point(225, 100), new Point(625, 100));
            var s3 = new Line(new Point(250, 100), new Point(475, 100));
            var s4 = new Line(new Point(290, 100), new Point(675, 100));

            lines.AddRange(new[] { s1, s2, s3, s4 });

            return lines;
        }

        private static Line GetRandomLine()
        {
            var leftX = Random.Next(0, 1000000) * Random.NextDouble();
            var leftY = Random.Next(0, 1000000) * Random.NextDouble();

            var rightX = leftX + Random.Next(0, 10000);
            var rightY = leftY + Random.Next(0, 10000);

            return new Line(new Point(leftX, leftY), new Point(rightX, rightY));
        }
    }
}