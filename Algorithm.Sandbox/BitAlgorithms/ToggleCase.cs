using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.BitAlgorithms
{
    public class ToggleCase
    {
        public static string Toggle(string s)
        {
            var result = new char[s.Length];

            //based on the observation that char xor space char 
            //(ie " " => 32 in decimal => 0100000 in binary => 1<<5)
            //will flip caps on ASCII encoding
            //example A => 65 => 1000001 => 1100001 => 97 => a
            for (int i = 0; i < s.Length; i++)
            {
                result[i] = (char)(s[i] ^ (1 << 5));
            }

            return new string(result);
        }
    }
}
