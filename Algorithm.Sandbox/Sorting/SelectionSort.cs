using System;

namespace Algorithm.Sandbox.Sorting
{
    public class SelectionSort<T> where T : IComparable
    {
        //O(n^2)
        public static T[] Sort(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                //select the smallest item in sub array and move it to front
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[i]) < 0)
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            return array;
        }
    }
}
