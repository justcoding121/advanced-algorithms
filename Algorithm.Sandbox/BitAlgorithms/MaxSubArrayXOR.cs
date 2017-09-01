using Algorithm.Sandbox.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/find-the-maximum-subarray-xor-in-a-given-array/
    /// </summary>
    public class MaxSubArrayXOR
    {
        public static int FindMax(int[] x)
        {
            var trie = new Trie<bool>();

            //init with zero
            trie.Insert(ToBoolArray(0));

            var max = int.MinValue;
            var prefixMax = 0;

            for (int i = 0; i < x.Length; i++)
            {
                //update current prefixMax
                prefixMax = prefixMax ^ x[i];
                
                //insert prefix max to trie
                trie.Insert(ToBoolArray(prefixMax));

                //query max sub array from trie and update
                max = Math.Max(max, QueryMax(trie, prefixMax));
            }

            return max;
        }

        /// <summary>
        /// Returns the maximum sum with all sub array within the given prefix max
        /// </summary>
        /// <param name="trie"></param>
        /// <param name="prefixMax"></param>
        /// <returns></returns>
        private static int QueryMax(Trie<bool> trie, int prefixMax)
        {
            var prefixArray = ToBoolArray(prefixMax);
            var prevMax = new bool[32];

            var currentNode = trie.Root;

            for (int i = 0; i < 32; i++)
            {
                //our goal is to maximize sub array xor
                //xor is maxed when bits differ; so dig down to opposite bit
                if (currentNode.Children.ContainsKey(!prefixArray[i]))
                {
                    currentNode = currentNode.Children[!prefixArray[i]];
                    prevMax[i] = !prefixArray[i];
                }
                //else just dig down on same bit
                else
                {
                    currentNode = currentNode.Children[prefixArray[i]];
                    prevMax[i] = prefixArray[i];
                }
            }

            return prefixMax ^ ToInt(prevMax);
        }

        /// <summary>
        /// returns a bool array of size 32 (int to binary)
        /// corresponding to 32 bit length of x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static bool[] ToBoolArray(int x)
        {
            return Convert.ToString(x, 2).PadLeft(32, '0').Select(s => s.Equals('1')).ToArray();
        }

        /// <summary>
        /// returns int from a bool array of size 32
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int ToInt(bool[] x)
        {
            return Convert.ToInt32(new string(x.Select(y => y ? '1' : '0').ToArray()), 2);
        }
    }
}
