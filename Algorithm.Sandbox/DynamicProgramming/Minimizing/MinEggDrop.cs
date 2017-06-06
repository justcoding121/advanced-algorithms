using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming.Minimizing
{
    /// <summary>
    /// Problem statement below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-11-egg-dropping-puzzle/
    /// </summary>
    public class MinEggDrop
    {
        public static int GetMinDrops(int floors, int eggs)
        {
            return GetMinDrops(floors, eggs, 
                new Dictionary<string, int>());
        }
        public static int GetMinDrops(int floors, int eggs,
            Dictionary<string, int> cache)
        {
            throw new NotImplementedException();
        }
    }
}
