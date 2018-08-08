using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix
{
    /// <summary>
    /// A weighted graph implementation using dynamically growinng/shrinking adjacency matrix array.
    /// IEnumerable enumerates all vertices.
    /// </summary>
    public class WeightedDiGraph<T, TW> : IEnumerable<T> where TW : IComparable
    {
        public int VerticesCount => usedSize;

        private Dictionary<T, int> vertexIndices;
        private Dictionary<int, T> reverseVertexIndices;

        private TW[,] matrix;

        private int maxSize;
        private int usedSize;
        private int nextAvailableIndex;

        public WeightedDiGraph()
        {
            maxSize = 1;
            vertexIndices = new Dictionary<T, int>();
            reverseVertexIndices = new Dictionary<int, T>();
            matrix = new TW[maxSize, maxSize];
        }

        /// <summary>
        /// Add a new vertex to this graph.
        /// Time complexity: O(1).
        /// </summary>
        public void AddVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (vertexIndices.ContainsKey(value))
            {
                throw new Exception("Vertex exists.");
            }

            if (usedSize < maxSize / 2)
            {
                halfMatrixSize();
            }

            if (nextAvailableIndex == maxSize)
            {
                doubleMatrixSize();
            }

            vertexIndices.Add(value, nextAvailableIndex);
            reverseVertexIndices.Add(nextAvailableIndex, value);
            nextAvailableIndex++;
            usedSize++;

        }

        /// <summary>
        /// Remove the given vertex.
        /// Time complexity: O(V) where V is the number of vertices.
        /// </summary>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (!vertexIndices.ContainsKey(value))
            {
                throw new Exception("Vertex does'nt exist.");
            }

            if (usedSize <= maxSize / 2)
            {
                halfMatrixSize();
            }

            var index = vertexIndices[value];
           
            //clear edges
            for (int i = 0; i < maxSize; i++)
            {
                matrix[i, index] = default(TW);
                matrix[index, i] = default(TW);
            }

            reverseVertexIndices.Remove(index);
            vertexIndices.Remove(value);

            usedSize--;

        }

        /// <summary>
        /// Add a new edge to this graph.
        /// Time complexity: O(1).
        /// </summary>
        public void AddEdge(T source, T dest, TW weight)
        {
            if (weight.Equals(default(TW)))
            {
                throw new Exception("Cannot add default edge weight.");
            }

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            {
                throw new Exception("Source or destination vertex does'nt exist.");
            }

            var sourceIndex = vertexIndices[source];
            var destIndex = vertexIndices[dest];
            if (!matrix[sourceIndex, destIndex].Equals(default(TW)))
            {
                throw new Exception("Edge already exists.");
            }

            matrix[sourceIndex, destIndex] = weight;
        }

        /// <summary>
        /// Remove the given edge from this graph.
        /// Time complexity: O(1).
        /// </summary>
        public void RemoveEdge(T source, T dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            {
                throw new Exception("Source or destination vertex does'nt exist.");
            }

            var sourceIndex = vertexIndices[source];
            var destIndex = vertexIndices[dest];
            if (matrix[sourceIndex, destIndex].Equals(default(TW)))
            {
                throw new Exception("Edge do not exists.");
            }

            matrix[sourceIndex, destIndex] = default(TW);
        }

        /// <summary>
        /// Do we have an edge between given source and destination?
        /// Time complexity: O(1).
        /// </summary>
        public bool HasEdge(T source, T dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

            if (!vertexIndices.ContainsKey(source) || !vertexIndices.ContainsKey(dest))
            {
                throw new Exception("Source or destination vertex does'nt exist.");
            }

            var sourceIndex = vertexIndices[source];
            var destIndex = vertexIndices[dest];

            if (!matrix[sourceIndex, destIndex].Equals(default(TW)))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Tuple<T, TW>> OutEdges(T vertex)
        {
            if (!vertexIndices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            var index = vertexIndices[vertex];

            var result = new List<Tuple<T, TW>>();

            for (int i = 0; i < maxSize; i++)
            {
                if (!matrix[index,i].Equals(default(TW)))
                {
                    result.Add(new Tuple<T,TW>(reverseVertexIndices[i], matrix[index, i]));
                }
            }

            return result;
        }

        public IEnumerable<Tuple<T, TW>> InEdges(T vertex)
        {
            if (!vertexIndices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            var index = vertexIndices[vertex];

            var result = new List<Tuple<T, TW>>();

            for (int i = 0; i < maxSize; i++)
            {
                if (!matrix[i, index].Equals(default(TW)))
                {
                    result.Add(new Tuple<T, TW>(reverseVertexIndices[i], matrix[i, index]));
                }
            }

            return result;
        }

        private void doubleMatrixSize()
        {
            var newMatrix = new TW[maxSize * 2, maxSize * 2];

            var newVertexIndices = new Dictionary<T, int>();
            var newReverseIndices = new Dictionary<int, T>();

            int k = 0;
            foreach (var vertex in vertexIndices)
            {
                newVertexIndices.Add(vertex.Key, k);
                newReverseIndices.Add(k, vertex.Key);
                k++;
            }
            nextAvailableIndex = k;

            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    if (!matrix[i, j].Equals(default(TW)) 
                        && reverseVertexIndices.ContainsKey(i)
                        && reverseVertexIndices.ContainsKey(j))
                    {
                        var newI = newVertexIndices[reverseVertexIndices[i]];
                        var newJ = newVertexIndices[reverseVertexIndices[j]];

                        newMatrix[newI, newJ] = matrix[i, j];
                    }
                }
            }

            matrix = newMatrix;
            vertexIndices = newVertexIndices;
            reverseVertexIndices = newReverseIndices;
            maxSize *= 2;
        }

        private void halfMatrixSize()
        {
            var newMatrix = new TW[maxSize / 2, maxSize / 2];

            var newVertexIndices = new Dictionary<T, int>();
            var newReverseIndices = new Dictionary<int, T>();

            int k = 0;
            foreach (var vertex in vertexIndices)
            {
                newVertexIndices.Add(vertex.Key, k);
                newReverseIndices.Add(k, vertex.Key);
                k++;
            }
            nextAvailableIndex = k;

            for (int i = 0; i < maxSize; i++)
            {
                for (int j = 0; j < maxSize; j++)
                {
                    if (!matrix[i, j].Equals(default(TW)) 
                        && reverseVertexIndices.ContainsKey(i)
                        && reverseVertexIndices.ContainsKey(j))
                    {
                        var newI = newVertexIndices[reverseVertexIndices[i]];
                        var newJ = newVertexIndices[reverseVertexIndices[j]];

                        newMatrix[newI, newJ] = matrix[i, j];
                    }
                }
            }

            matrix = newMatrix;
            vertexIndices = newVertexIndices;
            reverseVertexIndices = newReverseIndices;
            maxSize /= 2;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return vertexIndices.Select(x => x.Key).GetEnumerator();
        }
    }
}
