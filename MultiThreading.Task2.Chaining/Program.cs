/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        private static Random Rnd = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var firstTask = Task.Factory.StartNew(() => 
            {
                var ints = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    int randomInt = Rnd.Next(1, 10);
                        Console.WriteLine("random int is {0} ", randomInt);
                        ints[i] = randomInt;
                        Console.WriteLine(String.Concat(ints));
                }
                return ints;
            });
            Task<int[]> secondTask = firstTask.ContinueWith((prevTask) =>
            {
                var intArray = prevTask.Result;
                   for (int i = 0; i < intArray.Length; i++)
                    {

                        var multiplier = Rnd.Next(1, 10);
                        Console.WriteLine("multipliier is {0}", multiplier);
                        Console.WriteLine("before {0}", intArray[i]);
                        intArray[i] = intArray[i] * multiplier;
                        Console.WriteLine("after {0}", intArray[i]);
                    }
                Console.WriteLine(String.Concat(intArray));
                return intArray;
            });
            var thirdTask = secondTask.ContinueWith((result) =>
            {
                int[] unsortedArray = result.Result;
                Array.Sort(unsortedArray);
                Console.WriteLine(String.Concat(unsortedArray));
                return unsortedArray;
            });
            var fourthTask = thirdTask.ContinueWith((result) =>
            {
                var sum = 0;
                foreach (var item in result.Result)
                {
                    sum += item;
                }
            Console.WriteLine("Average is {0}", sum / result.Result.Length);
            });
            Task.WaitAll();
            Console.ReadKey();
        }
    }
}
