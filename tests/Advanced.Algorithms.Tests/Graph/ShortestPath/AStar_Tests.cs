using Advanced.Algorithms.Geometry;
using Advanced.Algorithms.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Advanced.Algorithms.Tests.Graph
{
    [TestClass]
    public class AStar_Tests
    {
        //test using eucledian (straight line) distance to destination as heuristic.
        [TestMethod]
        public void AStar_AdjacencyListGraph_Smoke_Test()
        {
            var testLocations = @"A5 30 573
                            A4 30 483
                            A2 30 178
                            A1 30 48
                            B1 207 48
                            B2 207 161
                            B3 144 339
                            B4 129 443
                            B5 127 479
                            C2 258 162
                            C3 240 288
                            C4 225 443
                            C5 336 573
                            D1 438 48
                            D2 438 174
                            D3 438 335
                            D4 438 473
                            D5 438 573
                            E4 575 475
                            E5 684 493
                            F1 611 48
                            F2 600 173
                            F5 701 573
                            G1 797 48
                            G2 797 150
                            G2b 770 151
                            G4 797 382
                            G4b 770 382
                            G5 797 573";

            var locationMappings = new Dictionary<string, Location>();

            var graph = new Advanced.Algorithms.DataStructures.Graph.AdjacencyList.WeightedDiGraph<Location, double>();

            using (var reader = new StringReader(testLocations))
            {
                string line;
                while ((line = reader.ReadLine()?.Trim()) != null)
                {
                    var @params = line.Split(' ');

                    var location = new Location()
                    {
                        Point = new Point(double.Parse(@params[1]), double.Parse(@params[2])),
                        Name = @params[0]
                    };

                    locationMappings.Add(@params[0], location);
                    graph.AddVertex(location);
                }
            }

            var testConnections = @"A1 2 B1 A2
                                A2 3 A1 B2 A4
                                A4 3 A2 B5 A5
                                A5 2 A4 C5
                                B1 3 A1 D1 B2
                                B2 4 C2 C3 A2 B1
                                B3 1 B4
                                B4 3 B3 B5 C4
                                B5 3 A4 C5 B4
                                C2 3 D2 B2 C3
                                C3 3 C2 B2 C4
                                C4 3 B4 C3 D4
                                C5 3 D5 B5 A5
                                D1 3 B1 F1 D2
                                D2 3 C2 D1 F2
                                D3 1 D4
                                D4 4 D3 C4 E4 D5
                                D5 3 C5 D4 F5
                                E4 3 D4 E5 F2
                                F1 3 F2 D1 G1
                                F2 4 F1 D2 G2b E4
                                F5 3 E5 G5 D5
                                G1 2 G2 F1
                                G2 3 G1 G2b G4
                                G2b 3 G2 F2 G4b
                                G4 3 G4b G2 G5
                                G4b 3 G4 G2b E5
                                G5 2 F5 G4
                                E5 3 F5 E4 G4b";

            using (var reader = new StringReader(testConnections))
            {
                string line;
                while ((line = reader.ReadLine()?.Trim()) != null)
                {
                    var @params = line.Split(' ');

                    for (int i = 2; i < int.Parse(@params[1]) + 2; i++)
                    {
                        graph.AddEdge(locationMappings[@params[0]], locationMappings[@params[i]],
                                EucledianDistanceCalculator.GetEucledianDistance(locationMappings[@params[0]].Point,
                                                                                 locationMappings[@params[i]].Point));
                    }

                }
            }

            var algorithm = new AStarShortestPath<Location, double>(new AStarShortestPathOperators(), new AStarSearchHeuristic());

            var result = algorithm.FindShortestPath(graph, locationMappings["A1"], locationMappings["G5"]);

            Assert.AreEqual(10, result.Path.Count);
            Assert.AreEqual(1217.3209396395309, result.Length);

            var expectedPath = new string[] { "A1", "B1", "B2", "C3", "C4", "D4", "E4", "E5", "F5", "G5" };
            for (int i = 0; i < expectedPath.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], result.Path[i].Name);
            }
        }

        //test using eucledian (straight line) distance to destination as heuristic.
        [TestMethod]
        public void AStar_AdjacencyMatrixGraph_Smoke_Test()
        {
            var testLocations = @"A5 30 573
                            A4 30 483
                            A2 30 178
                            A1 30 48
                            B1 207 48
                            B2 207 161
                            B3 144 339
                            B4 129 443
                            B5 127 479
                            C2 258 162
                            C3 240 288
                            C4 225 443
                            C5 336 573
                            D1 438 48
                            D2 438 174
                            D3 438 335
                            D4 438 473
                            D5 438 573
                            E4 575 475
                            E5 684 493
                            F1 611 48
                            F2 600 173
                            F5 701 573
                            G1 797 48
                            G2 797 150
                            G2b 770 151
                            G4 797 382
                            G4b 770 382
                            G5 797 573";

            var locationMappings = new Dictionary<string, Location>();

            var graph = new Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix.WeightedDiGraph<Location, double>();

            using (var reader = new StringReader(testLocations))
            {
                string line;
                while ((line = reader.ReadLine()?.Trim()) != null)
                {
                    var @params = line.Split(' ');

                    var location = new Location()
                    {
                        Point = new Point(double.Parse(@params[1]), double.Parse(@params[2])),
                        Name = @params[0]
                    };

                    locationMappings.Add(@params[0], location);
                    graph.AddVertex(location);
                }
            }

            var testConnections = @"A1 2 B1 A2
                                A2 3 A1 B2 A4
                                A4 3 A2 B5 A5
                                A5 2 A4 C5
                                B1 3 A1 D1 B2
                                B2 4 C2 C3 A2 B1
                                B3 1 B4
                                B4 3 B3 B5 C4
                                B5 3 A4 C5 B4
                                C2 3 D2 B2 C3
                                C3 3 C2 B2 C4
                                C4 3 B4 C3 D4
                                C5 3 D5 B5 A5
                                D1 3 B1 F1 D2
                                D2 3 C2 D1 F2
                                D3 1 D4
                                D4 4 D3 C4 E4 D5
                                D5 3 C5 D4 F5
                                E4 3 D4 E5 F2
                                F1 3 F2 D1 G1
                                F2 4 F1 D2 G2b E4
                                F5 3 E5 G5 D5
                                G1 2 G2 F1
                                G2 3 G1 G2b G4
                                G2b 3 G2 F2 G4b
                                G4 3 G4b G2 G5
                                G4b 3 G4 G2b E5
                                G5 2 F5 G4
                                E5 3 F5 E4 G4b";

            using (var reader = new StringReader(testConnections))
            {
                string line;
                while ((line = reader.ReadLine()?.Trim()) != null)
                {
                    var @params = line.Split(' ');

                    for (int i = 2; i < int.Parse(@params[1]) + 2; i++)
                    {
                        graph.AddEdge(locationMappings[@params[0]], locationMappings[@params[i]],
                                EucledianDistanceCalculator.GetEucledianDistance(locationMappings[@params[0]].Point,
                                                                                 locationMappings[@params[i]].Point));
                    }

                }
            }

            var algorithm = new AStarShortestPath<Location, double>(new AStarShortestPathOperators(), new AStarSearchHeuristic());

            var result = algorithm.FindShortestPath(graph, locationMappings["A1"], locationMappings["G5"]);

            Assert.AreEqual(10, result.Path.Count);
            Assert.AreEqual(1217.3209396395309, result.Length);

            var expectedPath = new string[] { "A1", "B1", "B2", "C3", "C4", "D4", "E4", "E5", "F5", "G5" };
            for (int i = 0; i < expectedPath.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], result.Path[i].Name);
            }
        }

        public class Location
        {
            public Point Point { get; set; }
            public string Name { get; set; }
        }

        public class AStarSearchHeuristic : IAStarHeuristic<Location, double>
        {
            public double HueristicDistanceToTarget(Location sourceVertex, Location targetVertex)
            {
                return EucledianDistanceCalculator.GetEucledianDistance(sourceVertex.Point,
                                                                       targetVertex.Point);
            }
        }

        public class AStarShortestPathOperators : IShortestPathOperators<double>
        {
            public double DefaultValue
            {
                get
                {
                    return default(double);
                }

            }

            public double MaxValue
            {
                get
                {
                    return double.MaxValue;
                }
            }

            public double Sum(double a, double b)
            {
                return checked(a + b);
            }
        }
    }

    public class EucledianDistanceCalculator
    {
        /// <summary>
        /// returns the eucledian distance between given two points
        /// </summary>
        public static double GetEucledianDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(a.X - b.X), 2)
                + Math.Pow(Math.Abs(a.Y - b.Y), 2));
        }
    }

}
