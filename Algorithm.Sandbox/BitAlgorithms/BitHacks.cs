using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    public class BitHacks
    {
        /// <summary>
        /// Checks if given number is even
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsEven(int x)
        {
            return (x & 1) == 0;
        }

        /// <summary>
        /// Checks if given number is a power of 2
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPowerOf2(int x)
        {
            return (x & (x - 1)) == 0;
        }

        /// <summary>
        /// Checks if given numbers are of opposite signs
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool HasOppositeSigns(int x, int y)
        {
            var mask = 1 << 31;

            return ((x & mask) == 0 && (y & mask) != 0)
                 || ((x & mask) != 0 && (y & mask) == 0);
        }

        /// <summary>
        /// Checks if nth bit from right is set, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsSet(int x, int n)
        {
            var mask = 1 << n;
            return (x & mask) > 0;
        }

        /// <summary>
        /// Sets nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SetBit(int x, int n)
        {
            var mask = 1 << n;

            return x | mask;
        }

        /// <summary>
        /// Unsets nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int UnsetBit(int x, int n)
        {
            var mask = ~(1 << n);
            return x & mask;
        }

        /// <summary>
        /// Toggles nth bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ToggleBit(int x, int n)
        {
            return IsSet(x, n) ? UnsetBit(x, n) : SetBit(x, n);
        }

        /// <summary>
        ///  Turns On first Unset bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int TurnOnBitAfterRightmostSetBit(int x)
        {
            //1100 => 1100 & ~(1011) >> 1 => 1100 & 0100 >> 1 => 0100 >>1 => 0010
            var mask = (x & ~(x - 1)) >> 1;

            // 1100|0010 => 1110
            return x | mask;
        }

        /// <summary>
        /// Turns Off first set bit from right, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int TurnOffRightmostSetBit(int x)
        {
            //1100 => ~(1100 & ~(1011)) => ~(1100 & 0100) >> ~(0100) => 1011
            var mask = ~(x & ~(x - 1));

            //1100 & 1011 => 1000
            return x & mask;
        }

        /// <summary>
        /// Gets the first right most sub bits starting with a set bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GetRightmostSubBitsStartingWithASetBit(int x)
        {
            //1100 => ~(1011) => 0100
            var mask = ~(x - 1);

            //1100 & 0100 => 0100
            return x & mask;
        }

        /// <summary>
        ///  Gets the first right most sub bits starting with a Unset bit, with rightmost being 0th bit
        ///  eg. 1011 => 0011
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GetRightmostSubBitsStartingWithAnUnsetBit(int x)
        {

            //make it the same problem as above
            //1011=> 0100
            var y = ~x;

            //0100
            var z = y & ~(y - 1);

            //count the number of zero bits after the last one
            //for example 0100 has 2 zero bits to the right of last one (n = 3)
            var n = 0;
            while (z != 0)
            {
                z = z >> 1;
                n++;
            }

            //to compensate the extra one added in above while loop
            n = n - 1;

            //0001
            var result = 1;
            //since result initiated with 1 at the end 
            //we just need to do n>1
            while (n > 1)
            {
                result = (result << 1) | 1;
                n--;
            }

            //0011
            return result;
        }

        /// <summary>
        ///  Sets all the first right most sub bits starting with a set bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int RightPropogateRightmostSetBit(int x)
        {
            //~1100 => method call (0011) => 0011
            var mask = GetRightmostSubBitsStartingWithAnUnsetBit(~x);

            //1100 | 0011 => 1111
            return mask | x;
        }

        /// <summary>
        ///  UnSets all the first right most sub bits starting with a unset bit, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int RightPropogateRightmostUnsetBit(int x)
        {
            //1011 => 0011
            var mask = GetRightmostSubBitsStartingWithAnUnsetBit(x);

            return x & ~mask;
        }

        /// <summary> 
        /// Update the nth bit from right with given boolean value, with rightmost being 0th bit
        /// </summary>
        /// <param name="number"></param>
        /// <param name="n"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int UpdateBitToValue(int x, int n, bool value)
        {
            if(value)
            {
                //1011 (n=2) => 1111
                var mask = 1;
                return x | (mask << n);
            }
            else
            {
                //1111 (n=2) => 1011
                var mask = 1;
                return x & ~(mask << n);
            }
          
        }

    }
}
