﻿using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            var result = Parallel.For(0, m2.ColCount, i =>
            {
                var innerResult = Parallel.For(0, m2.ColCount, j =>
                {
                    long sum = 0;
                    var innestResult = Parallel.For(0, m1.ColCount, k =>
                    {
                        sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                    });
                    resultMatrix.SetElement(i, j, sum);
                });
            });
            return resultMatrix;
        }
    }
}
