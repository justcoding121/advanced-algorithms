using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced.Algorithms.DynamicProgramming.Count
{
    public class DigitCounter
    {
        /// <summary>
        ///  Counts the appearences of given digit (0-9) in all numbers from 0 to given number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static int Count(int number, int digit)
        {
            if(digit < 0 || digit > 9)
            {
                throw new ArgumentException("Invalid digit.");
            }

            if(number < 0)
            {
                throw new ArgumentException("Invalid number.");
            }

            if (number < 10)
            {
                if(digit == number)
                {
                    return 1;
                }

                return 0;
            }

            return Count(number, digit, new Dictionary<int, int>());
        }
        /// <summary>
        /// Counts the appearences of given digit (0-9) in all numbers from 0 to number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="digit"></param>
        public static int Count(int number, int digit, Dictionary<int, int> cache)
        {
            //comments below explains each step with the assumption number = 898
            if (number < 10)
            {
                return 1;
            }

            if (cache.ContainsKey(number))
            {
                return cache[number];
            }

            //898 => 3
            var digits = (int)Math.Log10(number) + 1;

            //most significant 
            //898 => 8
            var msd = (int)(number / Math.Pow(10, digits - 1));

            //898 => count(0-99) * 8
            var result = Count((int)Math.Pow(10, digits - 1) - 1, digit, cache) * msd;

            //(for 600 - 699)
            //6 < 8
            if (digit < msd)
            {
                //+100 (for 600 - 699)
                result += (int)Math.Pow(10, digits - 1);
            }

            //898 => 98
            var remaining = number - msd * (int)Math.Pow(10, digits - 1);

            if(remaining > 0)
            {
               result += Count(remaining, digit, cache);
            }
          
            //6 == 8?
            // (for 800 - 898)
            if (digit == msd)
            {
                //+98 (for 800 - 898)
                //+1 for 800
                result += remaining + 1;
            }

            cache.Add(number, result);

            return result;
        }
    }
}
