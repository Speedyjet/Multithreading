/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static Semaphore _semaphore = new Semaphore(1, 1);
        private static int _counter = 10;
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            TypeA(_counter);
            TypeB(_counter);

            Console.ReadLine();
        }

        private static void TypeB(object counter)
        {
            if ((int)counter > 0)
            {
                _semaphore.WaitOne();
                Console.WriteLine("obj = {0}", counter);
                int innerCounter = (int)counter;
                innerCounter--;
                _semaphore.Release();
                ThreadPool.QueueUserWorkItem(new WaitCallback(TypeB), innerCounter);
            }
        }

        private static void TypeA(object obj)
        {
            {
                if ((int) obj > 0)
                {
                    Console.WriteLine("obj = {0}", obj);
                    var innerThread = new Thread(TypeA);
                    innerThread.Start((int)obj - 1);
                    innerThread.Join();
                }
            }
        }
    }
}
