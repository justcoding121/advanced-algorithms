namespace Algorithm.Sandbox.Sorting
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/convert-array-into-zig-zag-fashion/
    /// </summary>
    public class ZigZagOrderer
    {
        public static int[] Order(int[] input)
        {
            var currentOrderIsAsc = true;

            for (int i = 0; i < input.Length - 2; i++)
            {
                if(currentOrderIsAsc)
                {
                    if(input[i] > input[i+1])
                    {
                        swap(input, i, i+1);
                    }     
                }
                else
                {
                    if (input[i] < input[i+1])
                    {
                        swap(input, i, i+1);
                    }
                    
                }

                currentOrderIsAsc = !currentOrderIsAsc;

            }

            return input;
        }

        private static void swap(int[] input, int i, int j)
        {
            var tmp = input[i];
            input[i] = input[j];
            input[j] = tmp;
        }
    }
}
