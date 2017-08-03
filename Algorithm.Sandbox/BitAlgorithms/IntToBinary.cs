using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    public class IntToBinary
    {
        public static string GetBinary(int integer)
        {
            var stringBuilder = new StringBuilder();

            for(int i = 7;i >=0; i--)
            {
                stringBuilder.Insert(0, (integer & 1) == 0 ? "0" : "1");
                integer >>= integer;
            }

            return stringBuilder.ToString();
        }
    }
}
