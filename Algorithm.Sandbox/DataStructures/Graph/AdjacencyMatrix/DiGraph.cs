using System;
using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures.Graph.AdjacencyMatrix
{


    //TODO implement IEnumerable & make sure duplicates are handled correctly if its not already
    /// <summary>
    /// A directed graph implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DiGraph<T>
    {
        public int VerticesCount;
        

        /// <summary>
        /// Constructor
        /// </summary>
        public DiGraph()
        {
           
        }

        /// <summary>
        /// return a reference vertex  to start traversing Vertices
        /// O(1) complexity
        /// </summary>
        public T ReferenceVertex { get; }
 


        /// <summary>
        /// add a new vertex to this graph
        /// O(1) complexity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void AddVertex(T value)
        {
            if ( value == null)
            {
                throw new ArgumentNullException();
            }

           
        }

        /// <summary>
        /// remove an existing vertex frm graph
        /// O(V) complexity
        /// </summary>
        /// <param name="value"></param>
        public void RemoveVertex(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

          
        }

        /// <summary>
        /// add an edge from source to destination vertex
        /// O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void AddEdge(T source, T dest)
        {
            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

        }

        /// <summary>
        /// remove an existing edge between source & destination
        ///  O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public void RemoveEdge(T source, T dest)
        {

            if (source == null || dest == null)
            {
                throw new ArgumentException();
            }

         
        }

        /// <summary>
        /// do we have an edge between the given source and destination?
        /// O(1) complexity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool HasEdge(T source, T dest)
        {
          throw new NotImplementedException();
        }

    }
}
