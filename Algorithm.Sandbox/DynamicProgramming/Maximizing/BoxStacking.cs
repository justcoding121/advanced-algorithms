using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
 
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-21-box-stacking-problem/
    /// </summary>
    public class BoxStacking
    {
        public static int GetMaxHeight(List<int[]> dimensions)
        {
            var allDims = new List<Box>();

            //each box can be stacked in six differant ways (six sides)
            foreach (var dim in dimensions)
            {
                allDims.Add(new Box(dim[0], dim[1], dim[2]));
                allDims.Add(new Box(dim[2], dim[1], dim[0]));

                allDims.Add(new Box(dim[1], dim[2], dim[0]));
                allDims.Add(new Box(dim[0], dim[2], dim[1]));

                allDims.Add(new Box(dim[2], dim[0], dim[1]));
                allDims.Add(new Box(dim[1], dim[0], dim[2]));
            }

            //stable quick sort by decreasing order of Base Size
            allDims = allDims.OrderByDescending(x => x).ToList();

            var maxHeight = -1;
            //now lets find the longest increasing sub seq 
            //with max height
            FindMaxHeight(allDims, allDims.Count - 1, ref maxHeight, new Dictionary<int, int>());

            return maxHeight;
        }

        /// <summary>
        /// Return the longest height possible 
        /// from sequence such that box base size (0,1,..j-1) < box size j
        /// </summary>
        /// <param name="allDims"></param>
        private static int FindMaxHeight(List<Box> input,
             int j, ref int netMax, Dictionary<int, int> cache)
        {
            if (j == 0)
            {
                return input[0].Height;
            }

            if (cache.ContainsKey(j))
            {
                return cache[j];
            }

            var currentMax = input[j].Height;

            for (int i = 0; i < j; i++)
            {
                var subMax = FindMaxHeight(input, i, ref netMax, cache);

                //check for box size
                //And if subMax of values from (0, 1, .., i) + value at j is better
                if (input[i].Length > input[j].Length
                    && input[i].Breadth > input[j].Breadth
                    && input[j].Height + subMax > currentMax)
                {
                    currentMax = input[j].Height + subMax;
                }
            }

            netMax = Math.Max(netMax, currentMax);

            cache.Add(j, currentMax);

            return currentMax;
        }

        internal class Box : IComparable
        {
            public int Height { get; set; }
            public int Breadth { get; set; }
            public int Length { get; set; }

            public Box(int height, int breadth, int length)
            {
                Height = height;
                Breadth = breadth;
                Length = length;
            }

            /// <summary>
            /// Compare Box base size
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                var box = obj as Box;

                if (Breadth < box.Breadth
                    && Length < box.Length)
                {
                    return -1;
                }
                else if (Breadth == box.Breadth
                    && Length == box.Length)
                {
                    return 0;
                }

                return 1;
            }
        }
    }
}
