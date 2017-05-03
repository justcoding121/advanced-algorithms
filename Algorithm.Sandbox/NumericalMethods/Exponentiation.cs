namespace Algorithm.Sandbox.NumericalMethods
{
    public class FastExponentiation
    {
        /// <summary>
        /// Computes exponentiation using squaring
        /// </summary>
        /// <param name="base"></param>
        /// <param name="power"></param>
        public static int BySquaring(int @base, int power)
        {
            //using the algebraic result
            //a^-n  = (1/a)^n
            if (power < 0)
            {
                return BySquaring(1 / @base, -power);
            }

            if (power == 0)
            {
                return 1;
            }
            else if (power == 1)
            {
                return @base;
            }
            //power is even
            else if (power % 2 == 0)
            {
                return BySquaring(@base * @base, power / 2);
            }
            //power is odd
            else
            {
                return @base * BySquaring(@base * @base, (power - 1) / 2);
            }
        }
    }
}
