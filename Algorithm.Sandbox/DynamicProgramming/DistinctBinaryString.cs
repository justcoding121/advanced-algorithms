using System;
using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class DistinctBinaryString
    {

        public static int Count(int length)
        {
            if (length == 1)
            {
                return 1;
            }

            return Count(length - 1) + 1
                + Count(length - 1);

        }

    }
}
