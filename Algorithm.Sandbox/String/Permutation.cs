using System.Diagnostics;

namespace Algorithm.Sandbox.String
{
    public class Permutation
    {
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

        private static void Permutate(string prefix, string input, int size)
        {
            if (prefix.Length > size)
            {
                return;
            }

            if (prefix.Length == size)
            {
                Debug.WriteLine(prefix);
            }


            for (int i = 0; i < input.Length; i++)
            {
                Permutate(prefix + input.Substring(i, 1), input.Substring(0, i) + input.Substring(i + 1));
            }
        }
    }

}

