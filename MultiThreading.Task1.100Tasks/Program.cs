﻿/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;
        private static readonly List<Task> Tasks = new List<Task>(100);

        static void Main(string[] args)
        {
            for (int i = 0; i < TaskAmount; i++)
            {
                var task = new Task(() =>
                {
                    for (int j = 0; j < MaxIterationsCount; j++)
                    {
                        var taskId = Task.CurrentId;
                        Output(taskId, j);
                    }
                });
                
                Tasks.Add(task);
            }

            foreach (var task in Tasks)
            {
                task.Start();
            }

            
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();
            Task.WaitAll();
            Console.ReadLine();
        }

        private static void HundredTasks()
        {

        }


        static void Output(int? taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
