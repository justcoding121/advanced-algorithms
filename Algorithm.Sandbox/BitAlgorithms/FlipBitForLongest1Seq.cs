using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    /// <summary>
    /// You hvae an integer and you can flip exactly one bit from 0 to 1. Write code  
    /// to find the length of longest sequence of 1s you can create
    /// Example: 1775 (11011101111) => 8
    /// </summary>
    public class FlipBitForLongest1Seq
    {
        internal static int Find(int x)
        {
            if(x == 0)
            {
                return 1;
            }

            var bestCount = 0;
            var currentCount = 0;
            var lastConsecutiveOnesCount = 0;

            var oneBitUsed = false;

            while (x > 0)
            {
                //if current bit is one
                if ((x & 1) == 1)
                {
                    currentCount++;
                    lastConsecutiveOnesCount++;
                }
                //current bit is zero
                else
                {
                    //if next bit is one
                    if((x & (1 << 1)) > 0)
                    {
                        //if 1-bit was not used previously
                        //use it
                        if (!oneBitUsed)
                        {
                            currentCount++;
                            oneBitUsed = true;
                        }
                        //if 1-bit was used previously
                        else
                        {
                            //reset & update best
                            oneBitUsed = false;
                            bestCount = Math.Max(bestCount, currentCount);
                            currentCount = lastConsecutiveOnesCount;

                            //fill current zero with out 1-bit and start new sequence
                            currentCount++;
                            oneBitUsed = true;
                        }
                    }
                    //if next bit is zero
                    else
                    {
                        if (!oneBitUsed)
                        {
                            bestCount = Math.Max(bestCount, currentCount + 1);
                        }

                        oneBitUsed = false;
                        currentCount = 0;
                    }

                    lastConsecutiveOnesCount = 0;
                }

                bestCount = Math.Max(bestCount, currentCount);
                x >>= 1;
            }

            //edge case
            if(bestCount == lastConsecutiveOnesCount)
            {
                return bestCount + 1;
            }

            return bestCount;
        }
    }
}
