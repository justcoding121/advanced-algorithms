using Algorithm.Sandbox.DataStructures;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// Problem statement in detail below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-18-partition-problem/
    /// </summary>
    public class BalancedPartition
    {
        /// <summary>
        /// Returns list of indices for chosen partition half
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static AsArrayList<int>
            FindPartition(int[] input)
        {
            var sum = FindSum(input);

            if (sum % 2 == 1)
            {
                //cannot partition
                return new AsArrayList<int>();
            }

            var result = new AsArrayList<int>();

            var canPartition = Partition(sum / 2, input, 0, 0, result);

            return result;

        }

        /// <summary>
        /// Dynamic recursion
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <param name="progress"></param>
        /// <param name="pickedIndices"></param>
        /// <returns></returns>
        private static bool Partition(int sum,
            int[] input, int index,
            int progress, 
            AsArrayList<int> pickedIndices)
        {
            //found result
            if (sum == 0)
            {
                return true;
            }

            //cannot pick this value
            //which is greater than current sum
            if (input[index] > sum)
            {
                return Partition(sum, input, index + 1, progress, pickedIndices);
            }

            //pick current element
            var canPartition = Partition(sum - input[index], input,
                index + 1, progress + 1, pickedIndices);

            if (canPartition)
            {
                pickedIndices.Add(index);
                return canPartition;
            }

            //skip current element
            canPartition = Partition(sum, input, index + 1, progress, pickedIndices);

            return canPartition;
        }

        private static int FindSum(int[] input)
        {
            var sum = 0;

            foreach(var item in input)
            {
                sum += item;
            }

            return sum;
        }
    }
}
