namespace Algorithm.Sandbox.DynamicProgramming
{
    public class LongestCommonSubSequence
    {
        public static string FindSequence(string a, string b)
        {
            return FindSequence(a, b, a.Length - 1, b.Length - 1);
        }

        /// <summary>
        /// DP top down
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static string FindSequence(string a, string b, int i, int j)
        {
            if (i < 0 || j < 0)
            {
                return string.Empty;
            }

            if (a[i] == b[j])
            {
                return FindSequence(a, b, i - 1, j - 1) + a[i];
            }

            var result1 = FindSequence(a, b, i, j - 1);
            var result2 = FindSequence(a, b, i - 1, j);

            return result1.Length > result2.Length ? result1 : result2;

        }
    }
}
