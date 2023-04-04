/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static readonly ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
        private static int[] _initialArray = new int[10];
        private static readonly Random rnd = new Random();

        static void Main()
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
            rwl.EnterReadLock();
            try
            {
                foreach (var item in _initialArray)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        private static void WriteToResource()
        {
            
            try
            {
                for (int i = 0; i < _initialArray.Length; i++)
                {
                    rwl.EnterWriteLock();
                    _initialArray[i] = rnd.Next(10);

                    rwl.ExitWriteLock();
                    ReadFromResource();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { 
                if (rwl.IsWriteLockHeld)
                {
                    rwl.ExitWriteLock();
                }
            }
        }
    }
}
