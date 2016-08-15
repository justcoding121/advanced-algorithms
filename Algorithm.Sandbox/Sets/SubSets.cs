using System.Diagnostics;

namespace Algorithm.Sandbox.Sets
{
    public class SubSets
    {
        public static void PrintSubSets()
        {
            int c = 0;
            PrintSubSets(string.Empty, "abc", ref c);
        }

        private static void PrintSubSets(string prefix, string array, ref int count)
        {
            count++;
            Debug.WriteLine(prefix.ToString());
            //print prefix + array
            for (int i = 0; i < array.Length; i++)
            {
                PrintSubSets(prefix + array[i].ToString(), array.Substring(i + 1), ref count);
            }
        }

        public static void PrintSubSets(int size)
        {
            int c = 0;
            PrintSubSets(size, string.Empty, "abcde", ref c);
        }

        private static void PrintSubSets(int size, string prefix, string array, ref int count)
        {
            count++;
            if (prefix.Length > size)
            {
                return;
            }

            if (prefix.Length == size)
            {
                Debug.WriteLine(prefix.ToString());
                return;
            }
            //print prefix + array
            for (int i = 0; i < array.Length; i++)
            {
                PrintSubSets(size, prefix + array.Substring(i, 1), array.Substring(i + 1), ref count);
            }
        }
    }
}
