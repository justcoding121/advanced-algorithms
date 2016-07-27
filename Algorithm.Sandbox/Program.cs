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

            linkedList.PrintAllNodes();

            linkedList.Remove("JT");
            linkedList.Remove("JM");

            linkedList.PrintAllNodes();

            linkedList.AddFirst("JT");


            var arrayList = new AsArrayList<string>();

            arrayList.AddItem("Hello");
            arrayList.AddItem("My name is billa");
            arrayList.AddItem("3");

            for (int i = 0; i < arrayList.Length; i++)
            {
                Console.WriteLine(arrayList.ItemAt(i));
            }

            arrayList.RemoveItem(0);

            for (int i = 0; i < arrayList.Length; i++)
            {
                Console.WriteLine(arrayList.ItemAt(i));
            }

            arrayList.RemoveItem(0);


            for (int i = 0; i < arrayList.Length; i++)
            {
                Console.WriteLine(arrayList.ItemAt(i));
            }

            var hashSet = new AsHashSet<string, string>(10);

            hashSet.Add("key1", "blah");
            hashSet.Add("key2", "Ya man!");

            if (hashSet.HasKey("key2"))
            {
                Console.WriteLine(hashSet.GetValue("key2"));
            }

            hashSet.Remove("key2");

            Console.WriteLine(hashSet.HasKey("key2"));

            var builder = new AsStringBuilder();

            for (int i = 0; i < 500; i++)
            {
                builder.Append("Haha");
            }

            Console.WriteLine(builder.ToString());


            KnackSackProblems.KnackSack10();
            KnackSackProblems.KnackSack_Fractional();
        }

    }
}
