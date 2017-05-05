using System.Diagnostics;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class TowerOfHanoi
    {
        public static int Tower(int numOfDisks)
        {
            var minMoves = 0;

            Tower(numOfDisks, "a", "b", "c", ref minMoves);

            return minMoves;
        }

        /// <summary>
        /// DP top down 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="source"></param>
        /// <param name="aux"></param>
        /// <param name="dest"></param>
        /// <param name="moveCount"></param>
        private static void Tower(int n, string source, string aux, string dest, ref int moveCount)
        {
            Debug.WriteLine("Moving disc {0} from pole {1} to {2}", n, source, dest);

            moveCount++;

            if (n > 1)
            {
                ////assume without last disc on top we would be moving a disc from source to dest
                //Tower(n - 1, source, aux, dest, ref moveCount);

                ////The last disc we just moved above to destination was in source
                ////that disc was definitely moved from aux to source (if it was in destination we would be not here)
                //Tower(n - 1, aux, dest, source, ref moveCount);

                //or alternatively
                //assume without last disc on top we would be moving a disc from aux to dest
                Tower(n - 1, aux, source, dest, ref moveCount);

                //The last disc we just moved above to destination was in aux
                //that disc was definitely moved from source to aux (if it was in destination we would be done by now)
                Tower(n - 1, source, dest, aux, ref moveCount);
            }
        }

    }
}
