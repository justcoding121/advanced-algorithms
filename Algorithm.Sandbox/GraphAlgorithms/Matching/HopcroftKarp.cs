using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Coloring;
using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.GraphAlgorithms.Matching
{

    /// <summary>
    ///  Compute Max BiParitite Edges using Hopcroft Karp algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HopcroftKarpMatching<T>
    {
        IBiPartiteMatchOperators<T> operators;
        public HopcroftKarpMatching(IBiPartiteMatchOperators<T> operators)
        {
            this.operators = operators;
        }

        /// <summary>
        /// Returns a list of Max BiPartite Match Edges
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<MatchEdge<T>> GetMaxBiPartiteMatching(AsGraph<T> graph)
        {
            //check if the graph is BiPartite by coloring 2 colors
            var mColorer = new MColorer<T, int>();
            var colorResult = mColorer.Color(graph, new int[] { 1, 2 });

            if (colorResult.CanColor == false)
            {
                throw new Exception("Graph is not BiPartite.");
            }

            return GetMaxBiPartiteMatching(graph, colorResult.Partitions);

        }

        /// <summary>
        /// Get Max Match from Given BiPartitioned Graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="partitions"></param>
        /// <returns></returns>
        private List<MatchEdge<T>> GetMaxBiPartiteMatching(AsGraph<T> graph,
            Dictionary<int, List<T>> partitions)
        {
            var leftMatch = new Dictionary<T, T>();
            var rightMatch = new Dictionary<T, T>();

            //while there is an augmenting Path
            while (BFS(graph, partitions, leftMatch, rightMatch))
            {
                foreach (var vertex in partitions[2])
                {
                    if (!rightMatch.ContainsKey(vertex))
                    {
                        var visited = new HashSet<T>();
                        visited.Add(vertex);

                        var pathResult = DFS(graph.Vertices[vertex],
                          leftMatch, rightMatch, visited, true);
                        
                        //XOR remaining done here (partially done inside DFS)
                        foreach(var pair in pathResult)
                        {
                            if(pair.isRight)
                            {
                                rightMatch.Add(pair.A, pair.B);
                                leftMatch.Add(pair.B, pair.A);
                            }
                            else
                            {
                                leftMatch.Add(pair.A, pair.B);
                                rightMatch.Add(pair.B, pair.A);
                            }
                            
                        }
                    }

                }

            }

            //now gather all 
            var result = new List<MatchEdge<T>>();

            foreach (var item in leftMatch)
            {
                result.Add(new MatchEdge<T>(item.Key, item.Value));
            }
            return result;
        }

        /// <summary>
        /// Trace Path for DFS
        /// </summary>
        private class PathResult
        {
            public T A { get; }
            public T B { get; }
            public bool isRight { get; }

            public PathResult(T a, T b, bool isRight)
            {
                A = a;
                B = b;
                this.isRight = isRight;
            }
        }

        /// <summary>
        /// Find a Path from free vertex on left to another free vertex on right
        /// <param name="graph"></param>
        /// <param name="partitions"></param>
        /// <param name="leftMatch"></param>
        /// <param name="rightMatch"></param>
        private List<PathResult> DFS(AsGraphVertex<T> current,
            Dictionary<T, T> leftMatch, Dictionary<T, T> rightMatch,
            HashSet<T> visitPath,
            bool isRightSide)
        {
            if (!leftMatch.ContainsKey(current.Value)
                && !isRightSide)
            {
                return new List<PathResult>();
            }

            foreach (var edge in current.Edges)
            {
                //do not re-visit ancestors in current DFS tree
                if (visitPath.Contains(edge.Value))
                {
                    continue;
                }

                if (!visitPath.Contains(edge.Value))
                {
                    visitPath.Add(edge.Value);
                }
                var pathResult = DFS(edge, leftMatch, rightMatch, visitPath, !isRightSide);
                if (pathResult!=null)
                {
                    //XOR (partially done here by removing same edges)
                    //other part of XOR (adding new ones) is done after DFS method is finished
                    if (leftMatch.ContainsKey(current.Value)
                        && leftMatch[current.Value].Equals(edge.Value))
                    {
                        leftMatch.Remove(current.Value);
                        rightMatch.Remove(edge.Value);
                    }
                    else if (rightMatch.ContainsKey(current.Value)
                        && rightMatch[current.Value].Equals(edge.Value))
                    {
                        rightMatch.Remove(current.Value);
                        leftMatch.Remove(edge.Value);
                    }
                    else
                    {
                        pathResult.Add(new PathResult(current.Value, edge.Value, isRightSide));
                    }

                    return pathResult;
                }

            }

            return null;
        }

        /// <summary>
        /// Returns true if there is an augmenting Path from left to right
        /// An augmenting path is a path which starts from a free vertex 
        /// and ends at a free vertex via Matched/UnMatched edges alternatively
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="partitions"></param>
        /// <param name="leftMatch"></param>
        /// <param name="rightMatch"></param>
        private bool BFS(AsGraph<T> graph,
            Dictionary<int, List<T>> partitions,
            Dictionary<T, T> leftMatch, Dictionary<T, T> rightMatch)
        {
            var queue = new Queue<T>();
            var visited = new HashSet<T>();

            var leftGroup = new HashSet<T>();

            foreach (var vertex in partitions[1])
            {
                leftGroup.Add(vertex);
                //if vertex is free
                if (!leftMatch.ContainsKey(vertex))
                {
                    queue.Enqueue(vertex);
                    visited.Add(vertex);
                }

            }


            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                //if vertex is free
                if (!leftGroup.Contains(current) &&
                    !rightMatch.ContainsKey(current))
                {
                    return true;
                }

                foreach (var edge in graph.Vertices[current].Edges)
                {
                    if (!visited.Contains(edge.Value))
                    {
                        queue.Enqueue(edge.Value);
                        visited.Add(edge.Value);
                    }

                }

            }

            return false;
        }
    }
}
