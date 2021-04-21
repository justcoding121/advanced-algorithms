using Advanced.Algorithms.DataStructures.Graph;
using System;
using System.Collections.Generic;


namespace Advanced.Algorithms.Graph
{
    /// <summary>
    ///  Compute Max BiParitite Edges using Hopcroft Karp algorithm.
    /// </summary>
    public class HopcroftKarpMatching<T>
    {
        /// <summary>
        /// Returns a list of Max BiPartite Match Edges.
        /// </summary>
        public HashSet<MatchEdge<T>> GetMaxBiPartiteMatching(IGraph<T> graph)
        {
            //check if the graph is BiPartite by coloring 2 colors
            var mColorer = new MColorer<T, int>();
            var colorResult = mColorer.Color(graph, new int[] { 1, 2 });

            if (colorResult.CanColor == false)
            {
                throw new Exception("Graph is not BiPartite.");
            }

            return getMaxBiPartiteMatching(graph, colorResult.Partitions);
        }

        /// <summary>
        /// Get Max Match from Given BiPartitioned Graph.
        /// </summary>
        private HashSet<MatchEdge<T>> getMaxBiPartiteMatching(IGraph<T> graph,
            Dictionary<int, List<T>> partitions)
        {
            var matches = new HashSet<MatchEdge<T>>();

            var leftToRightMatchEdges = new Dictionary<T, T>();
            var rightToLeftMatchEdges = new Dictionary<T, T>();

            var freeVerticesOnRight = bfs(graph, partitions, leftToRightMatchEdges, rightToLeftMatchEdges);
            //while there is an augmenting Path
            while (freeVerticesOnRight.Count > 0)
            {
                var visited = new HashSet<T>();
                var path = new HashSet<MatchEdge<T>>();

                foreach (var vertex in freeVerticesOnRight)
                {
                    var currentPath = dfs(graph,
                      leftToRightMatchEdges, rightToLeftMatchEdges, vertex, default, visited, true);

                    if (currentPath != null)
                    {
                        union(path, currentPath);
                    }
                }

                xor(matches, path, leftToRightMatchEdges, rightToLeftMatchEdges);

                freeVerticesOnRight = bfs(graph, partitions, leftToRightMatchEdges, rightToLeftMatchEdges);
            }

            return matches;
        }

        /// <summary>
        /// Returns list of free vertices on right if there is an augmenting Path from left to right.
        /// An augmenting path is a path which starts from a free vertex 
        /// and ends at a free vertex via UnMatched (left -> right) and Matched (right -> left) edges alternatively.
        /// </summary>
        private List<T> bfs(IGraph<T> graph,
            Dictionary<int, List<T>> partitions,
            Dictionary<T, T> leftToRightMatchEdges, Dictionary<T, T> rightToLeftMatchEdges)
        {
            var queue = new Queue<T>();
            var visited = new HashSet<T>();

            var freeVerticesOnRight = new List<T>();

            foreach (var vertex in partitions[1])
            {
                //if this left vertex is free
                if (!leftToRightMatchEdges.ContainsKey(vertex) && !visited.Contains(vertex))
                {
                    queue.Enqueue(vertex);
                    
                    while (queue.Count > 0)
                    {
                        var current = queue.Dequeue();
                        visited.Add(vertex);

                        //unmatched edges left to right
                        foreach (var leftToRightEdge in graph.GetVertex(current).Edges)
                        {
                            if (visited.Contains(leftToRightEdge.TargetVertexKey))
                            {
                                continue;
                            }

                            //checking if this right vertex is free
                            if (!rightToLeftMatchEdges.ContainsKey(leftToRightEdge.TargetVertex.Key))
                            {
                                freeVerticesOnRight.Add(leftToRightEdge.TargetVertex.Key);
                            }
                            else
                            {
                                foreach (var rightToLeftEdge in leftToRightEdge.TargetVertex.Edges)
                                {
                                    //matched edge right to left
                                    if (leftToRightMatchEdges.ContainsKey(rightToLeftEdge.TargetVertexKey)
                                        && !visited.Contains(rightToLeftEdge.TargetVertexKey))
                                    {
                                        queue.Enqueue(rightToLeftEdge.TargetVertexKey);
                                    }
                                }
                            }

                            visited.Add(leftToRightEdge.TargetVertexKey);
                        }
                    }
                }
            }

            return freeVerticesOnRight;
        }

        /// <summary>
        /// Find an augmenting path that start from a given free vertex on right and ending
        /// at a free vertex on left, via Matched (right -> left) and UnMatched (left -> right) edges alternatively.  
        /// Return the matching edges along that path.
        /// </summary>
        private HashSet<MatchEdge<T>> dfs(IGraph<T> graph,
            Dictionary<T, T> leftToRightMatchEdges,
            Dictionary<T, T> rightToLeftMatchEdges,
            T current,
            T previous,
            HashSet<T> visited,
            bool currentIsRight)
        {
            var currentIsLeft = !currentIsRight;

            if (visited.Contains(current))
            {
                return null;
            }

            //free vertex on left found!
            if (currentIsLeft && !leftToRightMatchEdges.ContainsKey(current))
            {
                visited.Add(current);
                return new HashSet<MatchEdge<T>>() { new MatchEdge<T>(current, previous) };
            }

            //right to left should be unmatched edges
            if (currentIsRight && !rightToLeftMatchEdges.ContainsKey(current))
            {
                foreach (var edge in graph.GetVertex(current).Edges)
                {
                    var result = dfs(graph, leftToRightMatchEdges, rightToLeftMatchEdges, edge.TargetVertexKey, current, visited, !currentIsRight);
                    if (result != null)
                    {
                        result.Add(new MatchEdge<T>(edge.TargetVertexKey, current));
                        visited.Add(current);
                        return result;
                    }
                }
            }

            //left to right should be matched edges
            if (currentIsLeft && leftToRightMatchEdges.ContainsKey(current))
            {
                foreach (var edge in graph.GetVertex(current).Edges)
                {
                    var result = dfs(graph, leftToRightMatchEdges, rightToLeftMatchEdges, edge.TargetVertexKey, current, visited, !currentIsRight);
                    if (result != null)
                    {
                        result.Add(new MatchEdge<T>(current, edge.TargetVertexKey));
                        visited.Add(current);
                        return result;
                    }
                }
            }

            return null;
        }
        
        private void union(HashSet<MatchEdge<T>> paths, HashSet<MatchEdge<T>> path)
        {
            foreach (var item in path)
            {
                if (!paths.Contains(item))
                {
                    paths.Add(item);
                }
            }
        }

        private void xor(HashSet<MatchEdge<T>> matches, HashSet<MatchEdge<T>> paths,
            Dictionary<T, T> leftToRightMatchEdges, Dictionary<T, T> rightToLeftMatchEdges)
        {
            foreach (var item in paths)
            {
                if (matches.Contains(item))
                {
                    matches.Remove(item);
                    leftToRightMatchEdges.Remove(item.Source);
                    rightToLeftMatchEdges.Remove(item.Target);
                }
                else
                {
                    matches.Add(item);
                    leftToRightMatchEdges.Add(item.Source, item.Target);
                    rightToLeftMatchEdges.Add(item.Target, item.Source);
                }
            }
        }

    }
}
