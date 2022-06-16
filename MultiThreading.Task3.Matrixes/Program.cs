/*
 * 3. Write a program, which multiplies two matrices and uses class Parallel.
 * a. Implement logic of MatricesMultiplierParallel.cs
 *    Make sure that all the tests within MultiThreading.Task3.MatrixMultiplier.Tests.csproj run successfully.
 * b. Create a test inside MultiThreading.Task3.MatrixMultiplier.Tests.csproj to check which multiplier runs faster.
 *    Find out the size which makes parallel multiplication more effective than the regular one.
 */

using System;
using System.Diagnostics;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3.	Write a program, which multiplies two matrices and uses class Parallel. ");
            Console.WriteLine();

            TestMatrixes();

            const byte matrixSize = 7; // todo: use any number you like or enter from console
            CreateAndProcessMatrices(matrixSize);
            Console.ReadLine();
        }

        private static void TestMatrixes()
        {
            int i = 1000;

            var normalTimer = new Stopwatch();
            var parallelTimer = new Stopwatch();

            while (true)
            {
                var testMatrix1 = new Matrix(i, i, true);
                var testMatrix2 = new Matrix(i, i, true);
                var originalMultiplier = new MatricesMultiplier();
                var parallelMultiplier = new MatricesMultiplierParallel();
                normalTimer.Restart();
                originalMultiplier.Multiply(testMatrix1, testMatrix2);
                normalTimer.Stop();
                parallelTimer.Restart();
                parallelMultiplier.Multiply(testMatrix1, testMatrix2);
                parallelTimer.Stop();
                if (parallelTimer.ElapsedTicks < normalTimer.ElapsedTicks)
                {
                    Console.WriteLine(i);
                    break;
                }
                i += 100;
            }
            Console.WriteLine("i equals {0}", i);
        }

        private static void CreateAndProcessMatrices(byte sizeOfMatrix)
        {
            Console.WriteLine("Multiplying...");
            var firstMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix, true);
            var secondMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix, true);
            
            IMatrix resultMatrix = new MatricesMultiplier().Multiply(firstMatrix, secondMatrix);

            Console.WriteLine("firstMatrix:");
            firstMatrix.Print();
            Console.WriteLine("secondMatrix:");
            secondMatrix.Print();
            Console.WriteLine("resultMatrix:");
            resultMatrix.Print();
        }
    }
}
