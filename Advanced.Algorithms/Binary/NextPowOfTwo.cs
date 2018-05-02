namespace Advanced.Algorithms.Binary
{

    public class NextPowOfTwo
    {
        internal static int Next(int v)
        {
            if (v == 0)
            {
                return 1;
            }

            //is a power of two already
            if ((v & (v - 1)) == 0)
            {
                return v;
            }

            var result = 1;

            //shift result one left until y is 0
            var y = v;
            while (y > 0)
            {
                result = result << 1;
                y = y >> 1;
            }

            return result;
        }
    }
}
