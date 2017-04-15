using Algorithm.Sandbox.DataStructures.Graph.AdjacencyList;
using Algorithm.Sandbox.GraphAlgorithms.Coloring;
using System;
using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.GraphAlgorithms.Flow;

namespace Algorithm.Sandbox.GraphAlgorithms.Matching
{

    /// <summary>
    ///  Compute Max BiParitite Edges using Ford-Fukerson algorithm
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
        public AsArrayList<MatchEdge<T>> GetMaxBiPartiteMatching(AsGraph<T> graph)
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
        private AsArrayList<MatchEdge<T>> GetMaxBiPartiteMatching(AsGraph<T> graph,
            AsDictionary<int, AsArrayList<T>> partitions)
        {
            var leftMatch = new AsDictionary<T, T>();
            var rightMatch = new AsDictionary<T, T>();

            //while there is an augmenting Path
            while (BFS(graph, partitions, leftMatch, rightMatch))
            {
                foreach (var vertex in partitions[2])
                {
                    if (!rightMatch.ContainsKey(vertex))
                    {
                        var visited = new AsHashSet<T>();
                        visited.Add(vertex);

                        var pathResult = DFS(graph.Vertices[vertex],
                          leftMatch, rightMatch, visited, true);

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

            //now gather all group1 to group 2 edges in residual graph with positive flow
            var result = new AsArrayList<MatchEdge<T>>();

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
        private AsArrayList<PathResult> DFS(AsGraphVertex<T> current,
            AsDictionary<T, T> leftMatch, AsDictionary<T, T> rightMatch,
            AsHashSet<T> visitPath,
            bool isRightSide)
        {
            if (!leftMatch.ContainsKey(current.Value)
                && !isRightSide)
            {
                return new AsArrayList<PathResult>();
            }

            foreach (var edge in current.Edges)
            {
                //do not re-visit ancestors in current DFS tree
                if (visitPath.Contains(edge.Value.Value))
                {
                    continue;
                }

                if (!visitPath.Contains(edge.Value.Value))
                {
                    visitPath.Add(edge.Value.Value);
                }
                var pathResult = DFS(edge.Value, leftMatch, rightMatch, visitPath, !isRightSide);
                if (pathResult!=null)
                {
                    //XOR (partially done here by removing same edges)
                    //other part of XOR (adding new ones) is done after this method is finished
                    if (leftMatch.ContainsKey(current.Value)
                        && leftMatch[current.Value].Equals(edge.Value.Value))
                    {
                        leftMatch.Remove(current.Value);
                        rightMatch.Remove(edge.Value.Value);
                    }
                    else if (rightMatch.ContainsKey(current.Value)
                        && rightMatch[current.Value].Equals(edge.Value.Value))
                    {
                        rightMatch.Remove(current.Value);
                        leftMatch.Remove(edge.Value.Value);
                    }
                    else
                    {
                        pathResult.Add(new PathResult(current.Value, edge.Value.Value, isRightSide));
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
            AsDictionary<int, AsArrayList<T>> partitions,
            AsDictionary<T, T> leftMatch, AsDictionary<T, T> rightMatch)
        {
            var queue = new AsQueue<T>();
            var visited = new AsHashSet<T>();

            var leftGroup = new AsHashSet<T>();

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
                    if (!visited.Contains(edge.Value.Value))
                    {
                        queue.Enqueue(edge.Value.Value);
                        visited.Add(edge.Value.Value);
                    }

                }

            }

            return false;
        }
    }
}
