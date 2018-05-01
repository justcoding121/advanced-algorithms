using System;
using System.Text;

namespace Advanced.Algorithms.BitAlgorithms
{
    public class BaseConversion
    {
        /// <summary>
        /// Converts base of given number
        /// </summary>
        /// <param name="srcNumber">input number in source base system</param>
        /// <param name="srcBaseChars">Should be in correct order => eg. 0123456789 for decimal</param>
        /// <param name="dstBaseChars">>Should be in correct order => eg. 01 for binary</param>
        /// <param name="precision">Precision.</param>
        /// <returns></returns>
        public static string Convert(string srcNumber,
                    string srcBaseChars,
                    string dstBaseChars, int precision = 32)
        {
            srcNumber = srcNumber.Trim();
            if (srcNumber.Contains("."))
            {
                var tmp = srcNumber.Split('.');
                var whole = tmp[0].TrimEnd();
                var fraction = tmp[1].TrimStart();

                return ConvertWhole(whole, srcBaseChars, dstBaseChars) +
                   "." + ConvertFraction(fraction, srcBaseChars, dstBaseChars, precision);
            }

            return ConvertWhole(srcNumber, srcBaseChars, dstBaseChars);
        }
        /// <summary>
        /// Converts base of given number
        /// </summary>
        /// <param name="srcNumber">input number in source base system</param>
        /// <param name="srcBaseChars">Should be in correct order => eg. 0123456789 for decimal</param>
        /// <param name="dstBaseChars">>Should be in correct order => eg. 01 for binary</param>
        /// <returns></returns>
        private static string ConvertWhole(string srcNumber,
                string srcBaseChars,
                string dstBaseChars)
        {
            if (string.IsNullOrEmpty(srcNumber))
            {
                return string.Empty;
            }

            var srcBase = srcBaseChars.Length;
            var dstBase = dstBaseChars.Length;

            if (srcBase <= 1)
            {
                throw new Exception("Invalid source base length.");
            }

            if (dstBase <= 1)
            {
                throw new Exception("Invalid destination base length.");
            }

            long base10Result = 0;
            var j = 0;
            //convert to base 10
            //move from least to most significant numbers
            for (int i = srcNumber.Length - 1; i >= 0; i--)
            {
                //eg. 1 * 2^0 
                base10Result += (srcBaseChars.IndexOf(srcNumber[i]))
                    * (long)(Math.Pow(srcBase, j));
                j++;
            }

            var result = new StringBuilder();
            //now convert to target base
            while (base10Result != 0)
            {
                var rem = (int)base10Result % dstBase;
                result.Insert(0, dstBaseChars[rem]);
                base10Result = base10Result / dstBase;
            }

            return result.ToString();

        }

        private static string ConvertFraction(string srcNumber,
           string srcBaseChars,
           string dstBaseChars, int maxPrecision)
        {
            if (string.IsNullOrEmpty(srcNumber))
            {
                return string.Empty;
            }

            var srcBase = srcBaseChars.Length;
            var dstBase = dstBaseChars.Length;

            if (srcBase <= 1)
            {
                throw new Exception("Invalid source base length.");
            }

            if (dstBase <= 1)
            {
                throw new Exception("Invalid destination base length.");
            }

            decimal base10Result = 0;
            //convert to base 10
            //move from most significant numbers to least
            for (int i = 0; i < srcNumber.Length; i++)
            {
                //eg. 1 * 1/(2^1) 
                base10Result += (srcBaseChars.IndexOf(srcNumber[i]))
                    * (decimal)(1 / Math.Pow(srcBase, i + 1));
            }

            var result = new StringBuilder();
            //now convert to target base
            while (base10Result != 0 && maxPrecision > 0)
            {
                base10Result = base10Result * dstBase;
                result.Append(dstBaseChars[(int)Math.Floor(base10Result)]);
                base10Result -= Math.Floor(base10Result);
                maxPrecision--;
            }

            return result.ToString();

        }
    }
}
