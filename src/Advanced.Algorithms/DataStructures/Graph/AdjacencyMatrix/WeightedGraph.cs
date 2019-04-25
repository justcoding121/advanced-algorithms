using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix
{
    /// <summary>
    /// A weighted graph implementation using dynamically growing/shrinking adjacency matrix array.
    /// IEnumerable enumerates all vertices.
    /// </summary>
    public class WeightedGraph<T, TW> : IGraph<T>, IEnumerable<T> where TW : IComparable
    {
        private TW[,] matrix;

        private int maxSize => matrix.GetLength(0);
        private int usedSize;
        private int nextAvailableIndex;

        private Dictionary<T, int> vertexIndices;
        private Dictionary<int, T> reverseVertexIndices;
        private Dictionary<T, WeightedGraphVertex<T, TW>> vertexObjects;

        public int VerticesCount => usedSize;
        public bool IsWeightedGraph => true; 

        public WeightedGraph()
        {
            vertexIndices = new Dictionary<T, int>();
            reverseVertexIndices = new Dictionary<int, T>();
            vertexObjects = new Dictionary<T, WeightedGraphVertex<T, TW>>();
            matrix = new TW[1, 1];
        }

        public IGraphVertex<T> ReferenceVertex => getReferenceVertex();
        private WeightedGraphVertex<T, TW> getReferenceVertex()
        {
            if (this.VerticesCount == 0)
            {
                throw new Exception("Empty graph.");
            }

            return vertexObjects[this.First()];
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
            vertexObjects.Add(value, new WeightedGraphVertex<T, TW>(this, value));
            nextAvailableIndex++;
            usedSize++;
        }

        /// <summary>
        /// Remove given vertex from this graph.
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
            vertexObjects.Remove(value);

            usedSize--;
        }

        /// <summary>
        /// Add a new edge to this graph with given weight 
        /// and between given source and destination vertex.
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
            if (!matrix[sourceIndex, destIndex].Equals(default(TW))
                && !matrix[destIndex, sourceIndex].Equals(default(TW)))
            {
                throw new Exception("Edge already exists.");
            }

            matrix[sourceIndex, destIndex] = weight;
            matrix[destIndex, sourceIndex] = weight;

        }

        /// <summary>
        /// Remove given edge.
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
            if (matrix[sourceIndex, destIndex].Equals(default(TW))
                && matrix[destIndex, sourceIndex].Equals(default(TW)))
            {
                throw new Exception("Edge do not exists.");
            }

            matrix[sourceIndex, destIndex] = default(TW);
            matrix[destIndex, sourceIndex] = default(TW);
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

            if (!matrix[sourceIndex, destIndex].Equals(default(TW))
                && !matrix[destIndex, sourceIndex].Equals(default(TW)))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<KeyValuePair<T,TW>> Edges(T vertex)
        {
            if (!vertexIndices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            var index = vertexIndices[vertex];

            for (int i = 0; i < maxSize; i++)
            {
                if (!matrix[i,index].Equals(default(TW)))
                {
                    yield return new KeyValuePair<T, TW>(reverseVertexIndices[i], matrix[i, index]);
                }
            }
        }

        public int EdgeCount(T vertex)
        {
            if (!vertexIndices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            var index = vertexIndices[vertex];

            int count = 0;

            for (int i = 0; i < maxSize; i++)
            {
                if (!matrix[i, index].Equals(default(TW)))
                {
                    count++;
                }
            }

            return count;
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

                for (int j = i; j < maxSize; j++)
                {
                    if (!matrix[i, j].Equals(default(TW)) && !matrix[j, i].Equals(default(TW))
                        && reverseVertexIndices.ContainsKey(i)
                        && reverseVertexIndices.ContainsKey(j))
                    {
                        var newI = newVertexIndices[reverseVertexIndices[i]];
                        var newJ = newVertexIndices[reverseVertexIndices[j]];

                        newMatrix[newI, newJ] = matrix[i, j];
                        newMatrix[newJ, newI] = matrix[j, i];
                    }
                }
            }

            matrix = newMatrix;
            vertexIndices = newVertexIndices;
            reverseVertexIndices = newReverseIndices;
        }

        private void halfMatrixSize()
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
                for (int j = i; j < maxSize; j++)
                {
                    if (!matrix[i, j].Equals(default(TW)) && !matrix[j,i].Equals(default(TW))
                        && reverseVertexIndices.ContainsKey(i)
                        && reverseVertexIndices.ContainsKey(j))
                    {
                        var newI = newVertexIndices[reverseVertexIndices[i]];
                        var newJ = newVertexIndices[reverseVertexIndices[j]];

                        newMatrix[newI, newJ] = matrix[i, j];
                        newMatrix[newJ, newI] = matrix[j, i];
                    }
                }
            }

            matrix = newMatrix;
            vertexIndices = newVertexIndices;
            reverseVertexIndices = newReverseIndices;
        }

        public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertexObjects.Select(x=>x.Value);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return vertexIndices.Select(x => x.Key).GetEnumerator();
        }

        public bool ContainsVertex(T key)
        {
            return vertexObjects.ContainsKey(key);
        }

        public IGraphVertex<T> GetVertex(T key)
        {
            return vertexObjects[key];
        }

        IGraph<T> IGraph<T>.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Clones this graph.
        /// </summary>
        public WeightedGraph<T, TW> Clone()
        {
            var graph = new WeightedGraph<T, TW>();

            foreach (var vertex in this)
            {
                graph.AddVertex(vertex);
            }

            foreach (var vertex in this)
            {
                foreach (var edge in Edges(vertex))
                {
                    graph.AddEdge(vertex, edge.Key, edge.Value);
                }
            }

            return graph;
        }

        private class WeightedGraphVertex<T, TW> : IGraphVertex<T> where TW : IComparable
        {
            WeightedGraph<T, TW> graph;
            private int vertexIndex;
            private T vertexKey;

            private int maxSize => graph.maxSize;
            private TW[,] matrix => graph.matrix;

            private Dictionary<T, int> vertexIndices => graph.vertexIndices;
            private Dictionary<int, T> reverseVertexIndices => graph.reverseVertexIndices;

            internal WeightedGraphVertex(WeightedGraph<T, TW> graph, T vertexKey)
            {
                if (!graph.vertexIndices.ContainsKey(vertexKey))
                {
                    throw new ArgumentException("vertex is not in this graph.");
                }

                this.graph = graph;
                this.vertexKey = vertexKey;
                this.vertexIndex = graph.vertexIndices[vertexKey];
            }

            public T Key => vertexKey;


            IEnumerable<IEdge<T>> IGraphVertex<T>.Edges => graph.Edges(vertexKey)
              .Select(x => new Edge<T, TW>(graph.vertexObjects[x.Key], x.Value));

            public IEdge<T> GetOutEdge(IGraphVertex<T> targetVertex)
            {
                if (!vertexIndices.ContainsKey(targetVertex.Key))
                {
                    throw new ArgumentException("vertex is not in this graph.");
                }

                var index = vertexIndices[targetVertex.Key];
                var key = targetVertex as WeightedGraphVertex<T, TW>;
                return new Edge<T, TW>(targetVertex, matrix[vertexIndex, index]);
            }

            public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
            {
                if (!vertexIndices.ContainsKey(targetVertex.Key))
                {
                    throw new ArgumentException("vertex is not in this graph.");
                }

                var index = vertexIndices[targetVertex.Key];
                var key = targetVertex as WeightedGraphVertex<T, TW>;
                return new Edge<T, TW>(targetVertex, matrix[vertexIndex, index]);
            }
        }
    }
}
