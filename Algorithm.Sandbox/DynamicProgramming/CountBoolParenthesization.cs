using Algorithm.Sandbox.DataStructures;
using System;
using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class CountBoolParenthesization
    {
        public static int CountPositiveCombinations
            (bool[] symbols, char[] operators)
        {
            if (symbols.Length <= 1 || operators.Length == 0)
            {
                throw new Exception("Invalid symbol or operator lengths.");
            }

            if (symbols.Length != operators.Length + 1)
            {
                throw new System.Exception("Operator count should be one less than symbol length.");
            }

            var possibleOperators = new char[] { '&', '^', '|' };

            if (operators.Any(x => !possibleOperators.Contains(x)))
            {
                throw new Exception("Not all operators are supported.");
            }

            return True(symbols, operators, 0, symbols.Length - 1, 
                        new AsDictionary<string, int>(), 
                        new AsDictionary<string, int>());

        }

        /// <summary>
        /// Returns total number of true result paranthesis combinations between indices i & j
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="operators"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static int True(bool[] symbols,
            char[] operators,
            int i, int j, 
            AsDictionary<string, int> trueCache,
            AsDictionary<string, int> falseCache)
        {
            if (j < i)
            {
                return 0;
            }

            if (j == i)
            {
                if (symbols[i])
                {
                    return 1;
                }

                return 0;
            }

            var cacheKey = string.Concat(i, j);

            if (trueCache.ContainsKey(cacheKey))
            {
                return trueCache[cacheKey];
            }

            var result = 0;

            //k is the separator between i & j
            for (int k = i; k < j; k++)
            {
                switch (operators[k])
                {
                    case '&':
                        result += True(symbols, operators, i, k, trueCache, falseCache)
                            * True(symbols, operators, k + 1, j, trueCache, falseCache);
                        break;

                    case '|':
                        result += Both(symbols, operators, i, k, trueCache, falseCache)
                            * Both(symbols, operators, k + 1, j, trueCache, falseCache)
                            - False(symbols, operators, i, k, trueCache, falseCache) 
                            * False(symbols, operators, k + 1, j, trueCache, falseCache);
                        break;

                    case '^':
                        result += True(symbols, operators, i, k, trueCache, falseCache)
                            * False(symbols, operators, k + 1, j, trueCache, falseCache)
                            + False(symbols, operators, i, k, trueCache, falseCache) 
                            * True(symbols, operators, k + 1, j, trueCache, falseCache);
                        break;
                }
            }

            trueCache.Add(cacheKey, result);

            return result;

        }


        /// <summary>
        /// Returns total number of false result paranthesis combinations between indices i & j
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="operators"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static int False(bool[] symbols,
           char[] operators,
           int i, int j,
           AsDictionary<string, int> trueCache,
           AsDictionary<string, int> falseCache)
        {
            if (j < i)
            {
                return 0;
            }

            if (j == i)
            {
                if (!symbols[i])
                {
                    return 1;
                }

                return 0;
            }

            var cacheKey = string.Concat(i, j);

            if (falseCache.ContainsKey(cacheKey))
            {
                return falseCache[cacheKey];
            }

            var result = 0;

            //k is the separator between i & j
            for (int k = i; k < j; k++)
            {
                switch (operators[k])
                {
                    case '&':
                        result += Both(symbols, operators, i, k, trueCache, falseCache) 
                            * Both(symbols, operators, k + 1, j, trueCache, falseCache)
                             - True(symbols, operators, i, k, trueCache, falseCache) 
                             * True(symbols, operators, k + 1, j, trueCache, falseCache);
                        break;

                    case '|':
                        result += False(symbols, operators, i, k, trueCache, falseCache)
                            * False(symbols, operators, k + 1, j, trueCache, falseCache);
                        break;

                    case '^':
                        result += True(symbols, operators, i, k, trueCache, falseCache) 
                            * True(symbols, operators, k + 1, j, trueCache, falseCache)
                            + False(symbols, operators, i, k, trueCache, falseCache) 
                            * False(symbols, operators, k + 1, j, trueCache, falseCache);
                        break;
                }
            }

            falseCache.Add(cacheKey, result);

            return result;

        }

        /// <summary>
        /// Returns total number of true & false result paranthesis combinations between indices i & j
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="operators"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private static int Both(bool[] symbols,
           char[] operators,
           int i, int j,
           AsDictionary<string, int> trueCache,
           AsDictionary<string, int> falseCache)
        {
      
            var result = True(symbols, operators, i, j, trueCache, falseCache)
                + False(symbols, operators, i, j, trueCache, falseCache);

            return result;
        }
    }
}
