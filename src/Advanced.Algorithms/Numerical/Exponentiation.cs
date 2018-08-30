namespace Advanced.Algorithms.Numerical
{
    /// <summary>
    /// A fast exponentiation algorithm implementation.
    /// </summary>
    public class FastExponentiation
    {
        /// <summary>
        /// Computes exponentiation using squaring.
        /// </summary>
        public static int BySquaring(int @base, int power)
        {
            while (true)
            {
                //using the algebraic result
                //a^-n  = (1/a)^n
                if (power < 0)
                {
                    @base = 1 / @base;
                    power = -power;
                    continue;
                }

                switch (power)
                {
                    case 0:
                        return 1;
                    case 1:
                        return @base;
                    default:
                        if (power % 2 == 0)
                        {
                            @base = @base * @base;
                            power = power / 2;
                            continue;
                        }
                        //power is odd
                        else
                        {
                            return @base * BySquaring(@base * @base, (power - 1) / 2);
                        }
                }
            }
        }
    }
}
