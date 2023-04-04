/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var taskA = Task.Run(() =>
            {
                Console.WriteLine("This is the very first task");
                throw new Exception();
            }, token);
            var lazyTask = taskA.ContinueWith((result) =>
            {
                Console.WriteLine("The second task will be executed regardless parent task completion status");

            }, TaskContinuationOptions.LazyCancellation);

            var taskB = Task.Run(() =>
            {
                Console.WriteLine("this is a second task");
                source.Cancel();
            }, token);
            var notCompletedTask = taskB.ContinueWith((result) =>
            {
                Console.WriteLine("If you see this, that means the parent task finished without success");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);


            //don't really know how to make parent task thread should be reused for continuation.");
            var taskC = Task.Run(() =>
            {
                Console.WriteLine("parent thread id {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("this is a third task, and it will fail");
                throw new Exception("parent exception");
            }, token);
            var fallenTask = taskC.ContinueWith((result) =>
            {
                Console.WriteLine(result.AsyncState);
                Console.WriteLine("child thread id {0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("the parent task fails");
            }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.ExecuteSynchronously);

            //Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            var taskD = new Task(() =>
            {
                Console.WriteLine("this is a fourth task and it will be canceled");
                source.Cancel();
            }, token, TaskCreationOptions.LongRunning );
            var outsideTask = taskD.ContinueWith((result) =>
            {
                var currentThread = Thread.CurrentThread;
                Console.WriteLine($"this thread is {(currentThread.IsThreadPoolThread ? "threadpool thread" : "not a threadpool thread")}");
                Console.WriteLine("the parent task was canceled");
            }, TaskContinuationOptions.LongRunning);

            Console.ReadLine();
        }
    }
}
