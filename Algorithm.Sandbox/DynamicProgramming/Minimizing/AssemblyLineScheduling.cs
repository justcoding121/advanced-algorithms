using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.DynamicProgramming
{
    /// <summary>
    /// problem details below
    /// http://www.geeksforgeeks.org/dynamic-programming-set-34-assembly-line-scheduling/
    /// </summary>
    public class AssemblyLineScheduling
    {
        public static int GetMinTime(int[][] a, int[][] t, int[] entryTime, int[] exitTime)
        {
            var stations = a[0].Length;
            var cache = new Dictionary<int, int>();
            return Math.Min(GetMinTimeA(a, t, entryTime, exitTime, stations - 1, stations, cache),
                            GetMinTimeB(a, t, entryTime, exitTime, stations - 1, stations, cache));
        }

        public static int GetMinTimeA(int[][] a, int[][] t,
            int[] entryTime, int[] exitTime,
            int i, int totalStations, Dictionary<int, int> cache)
        {
            //first station
            if (i == 0)
            {
                return entryTime[0] + a[0][0];
            }

            if(cache.ContainsKey(i))
            {
                return cache[i];
            }

            var prevMinA = GetMinTimeA(a, t, entryTime, exitTime, i - 1, totalStations, cache) + a[0][i];
            var prevMinB = GetMinTimeB(a, t, entryTime, exitTime, i - 1, totalStations, cache) + t[1][i] + a[0][i];

            //last station
            if(i == totalStations - 1)
            {
                prevMinA += exitTime[0];
                prevMinB += exitTime[1];
            }

            var min = Math.Min(prevMinA, prevMinB);

            cache.Add(i, min);

            return min;
        }

        public static int GetMinTimeB(int[][] a, int[][] t,
            int[] entryTime, int[] exitTime,
            int i, int totalStations, Dictionary<int, int> cache)
        {
            //first station
            if (i == 0)
            {
                return entryTime[1] + a[1][0];
            }

            if (cache.ContainsKey(i))
            {
                return cache[i];
            }

            var prevMinB = GetMinTimeB(a, t, entryTime, exitTime, i - 1, totalStations, cache) + a[1][i];
            var prevMinA = GetMinTimeA(a, t, entryTime, exitTime, i - 1, totalStations, cache) + t[0][i] + a[1][i];

            //last station
            if (i == totalStations - 1)
            {
                prevMinA += exitTime[0];
                prevMinB += exitTime[1];
            }

            var min = Math.Min(prevMinA, prevMinB);

            cache.Add(i, min);

            return min;
        }
    }
}
