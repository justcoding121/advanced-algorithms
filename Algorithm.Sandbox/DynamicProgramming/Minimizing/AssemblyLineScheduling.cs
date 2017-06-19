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
        public static int GetMinTime(int[][] stationTime, int[][] crossingTime,
            int[] entryTime, int[] exitTime)
        {
            var stations = stationTime[0].Length;

            var cache = new Dictionary<string, int>();

            return Math.Min(MinTimeStationA(stationTime[0], stationTime[1],
                                            crossingTime[0], crossingTime[1],
                                            entryTime[0], entryTime[1],
                                            exitTime[0], exitTime[1],
                                            stations - 1, stations, cache),

                           MinTimeStationB(stationTime[0], stationTime[1],
                                            crossingTime[0], crossingTime[1],
                                             entryTime[0], entryTime[1],
                                            exitTime[0], exitTime[1]
                                            , stations - 1, stations, cache)
                                           );
        }


        private static int MinTimeStationA(int[] stationATime, int[] stationBTime,
            int[] AB_crossingTime, int[] BA_crossingTime,
            int entryTimeA, int exitTimeA,
            int entryTimeB, int exitTimeB,
            int currentStation, int totalStations, Dictionary<string, int> cache)
        {
            //first station
            if (currentStation == 0)
            {
                return entryTimeA + stationATime[0];
            }

            var cacheKey = $"A-{currentStation}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var prevMinA = MinTimeStationA(stationATime, stationBTime,
                                AB_crossingTime, BA_crossingTime,
                                entryTimeA, entryTimeB,
                                exitTimeA, exitTimeB,
                                currentStation - 1, totalStations, cache)
                                + stationATime[currentStation];

            var prevMinB = MinTimeStationB(stationATime, stationBTime,
                                AB_crossingTime, BA_crossingTime,
                                entryTimeA, entryTimeB,
                                exitTimeA, exitTimeB,
                                currentStation - 1, totalStations, cache)
                                + BA_crossingTime[currentStation] + stationATime[currentStation];

            //last station
            if (currentStation == totalStations - 1)
            {
                prevMinA += exitTimeA;
                prevMinB += exitTimeB;
            }

            var min = Math.Min(prevMinA, prevMinB);

            cache.Add(cacheKey, min);

            return min;
        }

        private static int MinTimeStationB(int[] stationATime, int[] stationBTime,
            int[] AB_crossingTime, int[] BA_crossingTime,
            int entryTimeA, int exitTimeA,
            int entryTimeB, int exitTimeB,
            int currentStation, int totalStations, Dictionary<string, int> cache)
        {
            //first station
            if (currentStation == 0)
            {
                return entryTimeB + stationBTime[0];
            }

            var cacheKey = $"B-{currentStation}";

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            var prevMinB = MinTimeStationB(stationATime, stationBTime,
                                AB_crossingTime, BA_crossingTime,
                                entryTimeA, entryTimeB,
                                exitTimeA, exitTimeB,
                                currentStation - 1, totalStations, cache)
                                + stationBTime[currentStation];

            var prevMinA = MinTimeStationA(stationATime, stationBTime,
                                AB_crossingTime, BA_crossingTime,
                                entryTimeA, entryTimeB,
                                exitTimeA, exitTimeB,
                                currentStation - 1, totalStations, cache)
                                + AB_crossingTime[currentStation] + stationBTime[currentStation];

            //last station
            if (currentStation == totalStations - 1)
            {
                prevMinA += exitTimeA;
                prevMinB += exitTimeB;
            }

            var min = Math.Min(prevMinA, prevMinB);

            cache.Add(cacheKey, min);

            return min;
        }
    }
}
