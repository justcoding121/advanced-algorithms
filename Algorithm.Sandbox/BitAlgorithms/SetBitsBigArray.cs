using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/program-to-count-number-of-set-bits-in-an-big-array/
    /// Run time complexity is O(n) where n is the length of int array
    /// </summary>
    public class SetBitsBigArray
    {
        public static long CountSetBits(int[] array)
        {
            var lookUpTable = getLookUp256Bytes();

            long count = 0;

            foreach(var item in array)
            {
                byte[] intBytes = BitConverter.GetBytes(item);

                foreach(var @byte in intBytes)
                {
                    count += lookUpTable[@byte];
                }
                
            }

            return count;
        }

        /// <summary>
        /// returns the count of set bits in
        /// all numbers b/w 0 to 255
        /// </summary>
        /// <returns></returns>
        private static Dictionary<byte, int> getLookUp256Bytes()
        {
            var lookUpHashTable = new Dictionary<byte, int>();

            for(int i=0;i<256;i++)
            {

            }

            throw new NotImplementedException();        }
    }
}
