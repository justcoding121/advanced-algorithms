using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advanced.Algorithms.DataStructures.Graph.AdjacencyMatrix
{
    /// <summary>
    /// A directed graph implementation using dynamically growing/shrinking adjacency matrix array.
    /// IEnumerable enumerates all vertices.
    /// </summary>
    public class Graph<T> : IGraph<T>, IEnumerable<T>
    {
        private BitArray[] matrix;

        private Dictionary<T, int> vertexIndices;
        private Dictionary<int, T> reverseVertexIndices;
        private Dictionary<T, GraphVertex<T>> vertexObjects;

        private int maxSize => matrix.Length;
        private int usedSize;
        private int nextAvailableIndex;

        public int VerticesCount => usedSize;
        public bool IsWeightedGraph => false;

        public Graph()
        {
            vertexIndices = new Dictionary<T, int>();
            reverseVertexIndices = new Dictionary<int, T>();
            vertexObjects = new Dictionary<T, GraphVertex<T>>();

            matrix = new BitArray[1];

            for (int i = 0; i < maxSize; i++)
            {
                matrix[i] = new BitArray(maxSize);
            }
        }

        public IGraphVertex<T> ReferenceVertex => getReferenceVertex();
        private GraphVertex<T> getReferenceVertex()
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
            vertexObjects.Add(value, new GraphVertex<T>(this, value));

            nextAvailableIndex++;
            usedSize++;

        }


        /// <summary>
        /// Remove an existing vertex from graph.
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
                matrix[i].Set(index, false);
                matrix[index].Set(i, false);
            }

            reverseVertexIndices.Remove(index);
            vertexIndices.Remove(value);
            vertexObjects.Remove(value);

            usedSize--;

        }

        /// <summary>
        /// Add an edge from source to destination vertex.
        /// Time complexity: O(1).
        /// </summary>
        public void AddEdge(T source, T dest)
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
            if (matrix[sourceIndex].Get(destIndex) && matrix[destIndex].Get(sourceIndex))
            {
                throw new Exception("Edge already exists.");
            }

            matrix[sourceIndex].Set(destIndex, true);
            matrix[destIndex].Set(sourceIndex, true);
        }

        /// <summary>
        /// Remove an existing edge between source and destination.
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
            if (!matrix[sourceIndex].Get(destIndex) || !matrix[destIndex].Get(sourceIndex))
            {
                throw new Exception("Edge do not exists.");
            }

            matrix[sourceIndex].Set(destIndex, false);
            matrix[destIndex].Set(sourceIndex, false);

        }

        /// <summary>
        /// Do we have an edge between the given source and destination?
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
            if (matrix[sourceIndex].Get(destIndex) && matrix[destIndex].Get(sourceIndex))
            {
                return true;
            }

            return false;
        }


        public IEnumerable<T> Edges(T vertex)
        {
            if (!vertexIndices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            var index = vertexIndices[vertex];

            for (int i = 0; i < maxSize; i++)
            {
                if (matrix[i].Get(index))
                {
                    yield return reverseVertexIndices[i];
                }
            }
        }

        public int EdgesCount(T vertex)
        {
            if (!vertexIndices.ContainsKey(vertex))
            {
                throw new ArgumentException("vertex is not in this graph.");
            }

            var count = 0;
            var index = vertexIndices[vertex];

            for (int i = 0; i < maxSize; i++)
            {
                if (matrix[i].Get(index))
                {
                    count++;
                }
            }

            return count;
        }


        private void doubleMatrixSize()
        {
            var newMatrix = new BitArray[maxSize * 2];
            for (int i = 0; i < maxSize * 2; i++)
            {
                newMatrix[i] = new BitArray(maxSize * 2);
            }

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
                    if (matrix[i].Get(j) && matrix[j].Get(i)
                        && reverseVertexIndices.ContainsKey(i)
                        && reverseVertexIndices.ContainsKey(j))
                    {
                        var newI = newVertexIndices[reverseVertexIndices[i]];
                        var newJ = newVertexIndices[reverseVertexIndices[j]];

                        newMatrix[newI].Set(newJ, true);
                        newMatrix[newJ].Set(newI, true);
                    }
                }
            }

            matrix = newMatrix;
            vertexIndices = newVertexIndices;
            reverseVertexIndices = newReverseIndices;
        }

        private void halfMatrixSize()
        {
            var newMatrix = new BitArray[maxSize / 2];
            for (int i = 0; i < maxSize / 2; i++)
            {
                newMatrix[i] = new BitArray(maxSize / 2);
            }

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
                    if (matrix[i].Get(j) && matrix[j].Get(i)
                        && reverseVertexIndices.ContainsKey(i)
                        && reverseVertexIndices.ContainsKey(j))
                    {
                        var newI = newVertexIndices[reverseVertexIndices[i]];
                        var newJ = newVertexIndices[reverseVertexIndices[j]];

                        newMatrix[newI].Set(newJ, true);
                        newMatrix[newJ].Set(newI, true);
                    }
                }
            }

            matrix = newMatrix;
            vertexIndices = newVertexIndices;
            reverseVertexIndices = newReverseIndices;
        }

        public IEnumerable<IGraphVertex<T>> VerticesAsEnumberable => vertexObjects.Select(x => x.Value);

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

        public Graph<T> Clone()
        {
            var graph = new Graph<T>();

            foreach (var vertex in this)
            {
                graph.AddVertex(vertex);
            }

            foreach (var vertex in this)
            {
                foreach (var edge in Edges(vertex))
                {
                    graph.AddEdge(vertex, edge);
                }
            }

            return graph;
        }

        private class GraphVertex<T> : IGraphVertex<T>
        {
            Graph<T> graph;
            private int vertexIndex;
            private T vertexKey;

            private int maxSize => graph.maxSize;
            private BitArray[] matrix => graph.matrix;

            private Dictionary<T, int> vertexIndices => graph.vertexIndices;
            private Dictionary<int, T> reverseVertexIndices => graph.reverseVertexIndices;

            internal GraphVertex(Graph<T> graph, T vertexKey)
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
              .Select(x => new Edge<T, int>(graph.vertexObjects[x], 1));

            public IEdge<T> GetOutEdge(IGraphVertex<T> targetVertex)
            {
                if (!vertexIndices.ContainsKey(targetVertex.Key))
                {
                    throw new ArgumentException("vertex is not in this graph.");
                }

                var index = vertexIndices[targetVertex.Key];
                var key = targetVertex as GraphVertex<T>;
                return new Edge<T, int>(targetVertex, 1);
            }

            public IEdge<T> GetEdge(IGraphVertex<T> targetVertex)
            {
                if (!vertexIndices.ContainsKey(targetVertex.Key))
                {
                    throw new ArgumentException("vertex is not in this graph.");
                }

                var index = vertexIndices[targetVertex.Key];
                var key = targetVertex as GraphVertex<T>;
                return new Edge<T, int>(targetVertex, 1);
            }
        }
    }
}
