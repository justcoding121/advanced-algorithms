using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/sum-of-bit-differences-among-all-pairs/
    /// </summary>
    public class SumBitDiff
    {
        public static int Sum(int[] x)
        {
            var sum = 0;
            var mask = 1;

            for (int i = 31; i >= 0; i--)
            {
                var setBitCount = 0;
                var unSetBitCount = 0;

                for (int j = 0; j < x.Length; j++)
                {
                    //Lets fist count the set bit count in each number (setBitCount)
                    if (((x[j] >> i) & mask) > 0)
                    {
                        setBitCount++;
                    }
                    else
                    {
                        //So there will be n-count number on elements with unset bits (unSetBitCount)
                        unSetBitCount++;
                    }
                   
                }

                //now the total number of differences b/w the sets will be 
                //setBitCount * unSetBitCount ie. the number of pair combinations
                //to include symmetric duplications x by 2
                sum += setBitCount * unSetBitCount * 2;
            }

            return sum;
        }
    }
}
