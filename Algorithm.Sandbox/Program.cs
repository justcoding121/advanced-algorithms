using Algorithm.Sandbox.DataStructures;
using Algorithm.Sandbox.DynamicProgramming;
using System;

namespace Algorithm.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var linkedList = new AsSinglyLinkedList<string>();
           

            linkedList.AddFirst("JT");
            linkedList.AddFirst("JK");
            linkedList.AddLast("JM");

            linkedList.printAllNodes();

            linkedList.Remove("JT");
            linkedList.Remove("JM");

            linkedList.printAllNodes();

            linkedList.AddFirst("JT");


            KnackSackProblems.KnackSack10();
            KnackSackProblems.KnackSack_Fractional();
        }

    }
}
