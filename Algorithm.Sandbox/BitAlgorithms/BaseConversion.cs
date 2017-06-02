using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    public class BaseConversion
    {
        public static string Convert(string srcNumber, 
                    char[] srcBaseChars,
                    char[] dstBaseChars)
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

            throw new NotImplementedException();
        }
    }
}
