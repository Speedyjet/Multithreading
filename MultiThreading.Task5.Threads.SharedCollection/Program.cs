/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static ReaderWriterLock rwl = new ReaderWriterLock();
        private static int[] _initialArray = new int[10];
        private static bool running = true;
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var writerThread = new Thread(new ThreadStart(WriteToResource));
            writerThread.Start();
            var readerThread = new Thread(new ThreadStart(ReadFromResource));
            readerThread.Start();
            Console.ReadLine();
        }

        private static void ReadFromResource()
        {
            rwl.AcquireReaderLock(int.MaxValue);
            try
            {
                foreach (var item in _initialArray)
                {
                    Console.WriteLine(item);
                }
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }

        private static void WriteToResource()
        {
            rwl.AcquireWriterLock(int.MaxValue);
            try
            {
                for (int i = 0; i < _initialArray.Length; i++)
                {
                    _initialArray[i] = rnd.Next(10);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }
    }
}
