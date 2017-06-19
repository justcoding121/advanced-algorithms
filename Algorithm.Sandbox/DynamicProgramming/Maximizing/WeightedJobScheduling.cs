using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    public class WeightedJob
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Weight { get; set; }

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
            var netMax = 0;

            GetMaxProfit(jobs.OrderBy(x => x.Start).ToList(),
                jobs.Count - 1, ref netMax, new Dictionary<int, int>());

            return netMax;
        }

        /// <summary>
        /// Just a regular LIS problem 
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="j"></param>
        /// <param name="netMax"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static int GetMaxProfit(List<WeightedJob> jobs, int j,
            ref int netMax, Dictionary<int, int> cache)
        {
            if (j == 0)
            {
                return jobs[j].Weight;
            }

            if(cache.ContainsKey(j))
            {
                return cache[j];
            }

            var localMax = 0;

            for (int i = 0; i < j; i++)
            {
                var subMax = GetMaxProfit(jobs, i, ref netMax, cache);

                if (jobs[i].End <= jobs[j].Start
                    && subMax + jobs[j].Weight > localMax)
                {
                    localMax = subMax + jobs[j].Weight;
                }
            }

            netMax = Math.Max(netMax, localMax);

            cache.Add(j, localMax);

            return localMax;
        }
    }
}
