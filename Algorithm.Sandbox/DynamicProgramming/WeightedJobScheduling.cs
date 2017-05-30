using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class WeightedJob
    {
        int Start { get; set; }
        int End { get; set; }
        int Weight { get; set; }

        public WeightedJob(int[] values)
        {
            Start = values[0];
            End = values[1];
            Weight = values[2];
        }
    }

    /// <summary>
    /// Problem details below
    /// http://www.geeksforgeeks.org/weighted-job-scheduling/
    /// </summary>
    public class WeightedJobScheduling
    {
        public static int GetMaxProfit(List<WeightedJob> jobs)
        {
            throw new NotImplementedException();
        }
    }
}
