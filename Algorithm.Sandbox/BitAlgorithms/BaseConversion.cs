using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    public class BaseConversion
    {
        /// <summary>
        /// Converts base of given number
        /// </summary>
        /// <param name="srcNumber">input number in source base system</param>
        /// <param name="srcBaseChars">Should be in correct order => eg. 0123456789 for decimal</param>
        /// <param name="dstBaseChars">>Should be in correct order => eg. 01 for binary</param>
        /// <returns></returns>
        public static string Convert(string srcNumber, 
                    string srcBaseChars,
                    string dstBaseChars)
        {
            var srcBase = srcBaseChars.Length;
            var dstBase = dstBaseChars.Length;

            if (srcBase <= 1)
            {
                throw new Exception("Invalid source base length.");
            }

            if (dstBase <=1)
            {
                throw new Exception("Invalid destination base length.");
            }

            long base10Result = 0;
            var j = 0;
            //convert to base 10
            //move from least to most significant numbers
            for(int i=srcNumber.Length-1;i >= 0;i--)
            {
                //eg. 1 * 2^0 
                base10Result += (long)((srcBaseChars.IndexOf(srcNumber[i])) * Math.Pow(srcBase, j));
                j++;
            }

            var result = new StringBuilder();
            //now convert to target base
            while(base10Result!=0)
            {
                var rem = (int)base10Result % dstBase;
                result.Insert(0, dstBaseChars.ElementAt(rem));
                base10Result = base10Result / dstBase;
            }

            return result.ToString();

        }
    }
}
