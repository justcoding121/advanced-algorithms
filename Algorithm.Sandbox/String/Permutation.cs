using System.Diagnostics;

namespace Algorithm.Sandbox.String
{
    /// <summary>
    /// String permutation implementation
    /// </summary>
    public class Permutation
    {
        /// <summary>
        /// Prints all permutation of given input string to debug output
        /// </summary>
        /// <param name="input"></param>
        public static void Permutate(string input)
        {
            Permutate(string.Empty, input);
        }

        private static void Permutate(string prefix, string input)
        {
            Debug.WriteLine(prefix);

            for (int i = 0; i < input.Length; i++)
            {
                Permutate(prefix + input.Substring(i, 1), input.Substring(0, i) + input.Substring(i + 1));
            }
        }

       
    }

}

