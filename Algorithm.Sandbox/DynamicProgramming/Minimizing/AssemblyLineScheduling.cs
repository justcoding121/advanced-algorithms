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
        public static int GetMinTime(int[][] stationTime, int[][] crossingTime, int[] entryTime, int[] exitTime)
        {
            var stations = stationTime[0].Length;
            var cache = new Dictionary<int, int>();
            return Math.Min(GetMinTimeA(stationTime, crossingTime, entryTime, exitTime, stations - 1, stations, cache),
                            GetMinTimeB(stationTime, crossingTime, entryTime, exitTime, stations - 1, stations, cache));
        }

        public static int GetMinTimeA(int[][] stationTime, int[][] crossingTime,
            int[] entryTime, int[] exitTime,
            int i, int totalStations, Dictionary<int, int> cache)
        {
            //first station
            if (i == 0)
            {
                return entryTime[0] + stationTime[0][0];
            }

            if(cache.ContainsKey(i))
            {
                return cache[i];
            }

            var prevMinA = GetMinTimeA(stationTime, crossingTime, entryTime, exitTime, i - 1, totalStations, cache) + stationTime[0][i];
            var prevMinB = GetMinTimeB(stationTime, crossingTime, entryTime, exitTime, i - 1, totalStations, cache) + crossingTime[1][i] + stationTime[0][i];

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

        public static int GetMinTimeB(int[][] stationTime, int[][] crossingTime,
            int[] entryTime, int[] exitTime,
            int i, int totalStations, Dictionary<int, int> cache)
        {
            //first station
            if (i == 0)
            {
                return entryTime[1] + stationTime[1][0];
            }

            if (cache.ContainsKey(i))
            {
                return cache[i];
            }

            var prevMinB = GetMinTimeB(stationTime, crossingTime, entryTime, exitTime, i - 1, totalStations, cache) + stationTime[1][i];
            var prevMinA = GetMinTimeA(stationTime, crossingTime, entryTime, exitTime, i - 1, totalStations, cache) + crossingTime[0][i] + stationTime[1][i];

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
