using System;
using System.Linq;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class CountBoolParanthesization
    {
        public int CountPositiveCombinations
            (bool[] symbols, char[] operators)
        {
            if(symbols.Length <=1 || operators.Length == 0)
            {
                throw new Exception("Invalid symbol or operator lengths.");
            }

            if(symbols.Length != operators.Length  + 1)
            {
                throw new System.Exception("Operator count should be one less than symbol length.");
            }

            var possibleOperators = new char[] { '&', '^', '|' };

            if (operators.Any(x=> !possibleOperators.Contains(x)))
            {
                throw new Exception("Not all operators are supported.");
            }

            CountPositiveCombinations(symbols, operators, 0, symbols.Length - 1);

            throw new NotImplementedException();
        }

        private bool CountPositiveCombinations(bool[] symbols, 
            char[] operators,
            int i, int j)
        {
            //base case
            if(j - i == 1)
            {
                if(operators[j-1] == '&')
                {
                    return symbols[i] & symbols[j];

                } 
                else if(operators[j-1] == '|')
                {
                    return symbols[i] | symbols[j];
                }
                else
                {
                    return symbols[i] ^ symbols[j];
                }

            }

            if (operators[j - 1] == '&' && symbols[j])
            {
                return CountPositiveCombinations(symbols, operators, i, j - 1)
                    & symbols[j];

            }
            else if (operators[j - 1] == '|')
            {
                if (symbols[i] | symbols[j])
                {
                    return 1;
                }

                return 0;
            }
            else
            {
                if (symbols[i] ^ symbols[j])
                {
                    return 1;
                }

                return 0;
            }

        }
    }
}
