using Advanced.Algorithms.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Algorithms.Tests.Geometry
{
    [TestClass]
    public class SweepLine_Tests
    {
        [TestMethod]
        public void SweepLine_Smoke_Test()
        {
            while (true)
            {
                var lines = getRandomLines(3);

                var expectedIntersections = getExpectedIntersections(lines);
                var actualIntersections = SweepLineIntersection.FindIntersections(lines).ToList();

                if (expectedIntersections.Count != actualIntersections.Count)
                {

                }
                // Assert.AreEqual(expectedIntersections.Count, actualIntersections.Count);


            }
        }


        private static Random random = new Random();

        private static List<Point> getExpectedIntersections(List<Line> lines)
        {
            var result = new List<Point>();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    var intersection = LineIntersection.FindIntersection(lines[i], lines[j]);

                    if (intersection != null)
                    {
                        result.Add(intersection);
                    }
                }
            }

            return result;
        }

        private static List<Line> getRandomLines(int lineCount)
        {
            var lines = new List<Line>();

            while (lineCount > 0)
            {
                lines.Add(getRandomLine());
                lineCount--;
            }

            return lines;
        }
        private static Line getRandomLine()
        {
            return new Line(new Point(random.Next(0, 100) * random.NextDouble(), random.Next(0, 100) * random.NextDouble()),
                new Point(random.Next(0, 100) * random.NextDouble(), random.Next(0, 100) * random.NextDouble()));
        }
    }
}
