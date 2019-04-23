using System;
using System.Text;

namespace Advanced.Algorithms.Binary
{ 
    /// <summary>
    /// Base conversion implementation.
    /// </summary>
    public class BaseConversion
    {
        /// <summary>
        /// Converts base of given number to the target base.
        /// </summary>
        /// <param name="srcNumber">Input number in source base system.</param>
        /// <param name="srcBaseChars">Source base system characters in increasing order. For example 0123456789 for base 10.</param>
        /// <param name="dstBaseChars">Destination base system characters in increasing order. For example 01 for base 2.</param>
        /// <param name="precision">Required precision when dealing with fractions. Defaults to 32 places.</param>
        /// <returns>The result in target base as a string.</returns>
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

                return convertWhole(whole, srcBaseChars, dstBaseChars) +
                   "." + convertFraction(fraction, srcBaseChars, dstBaseChars, precision);
            }

            return convertWhole(srcNumber, srcBaseChars, dstBaseChars);
        }
        /// <summary>
        /// Converts the whole part of source number.
        /// </summary>
        private static string convertWhole(string srcNumber,
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

        /// <summary>
        /// Converts the fractional part of source number.
        /// </summary>
        private static string convertFraction(string srcNumber,
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
