using System;

namespace Algorithm.Sandbox.Sorting
{
    public class BubbleSort<T> where T : IComparable
    {
        //O(n^2)
        public static T[] Sort(T[] array)
        {
            var swapped = true;

            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    //compare adjacent elements 
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        var temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
            }

            return array;
        }
    }
}
