using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW2_Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            double[,] matrix = new double[n, n + 1];
            string[] line;
            for (long i=0;i<n;i++)
            {
                line = Console.ReadLine().Split();
                for(long j=0;j<n+1;j++)
                {
                    matrix[i, j] = long.Parse(line[j]);
                }
            }
            Solve(n, matrix).ToList().ForEach(x => Console.Write(x + " "));
        }

        public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            // Comment the line below and write your code here
            //throw new NotImplementedException();
            double[,] ans = RowReduce(matrix);
            double[] res = new double[MATRIX_SIZE];
            for (long i = 0; i < MATRIX_SIZE; i++)
            {
                double temp = ans[i, MATRIX_SIZE];
                res[i] = temp;
                #region
                //if (temp > 0)
                //{
                //    if (temp * 100 % 100 < 25)
                //    {
                //        res[i] = (int)(temp);
                //    }
                //    else if (temp * 100 % 100 > 75)
                //    {
                //        res[i] = (int)(temp) + 1;
                //    }
                //    else
                //    {
                //        res[i] = (int)(temp) + 0.5;
                //    }
                //}
                //else
                //{
                //    //if(Math.Abs(temp * 100 % 100) == 0)
                //    //{
                //    //    res[i] = (int)(temp);
                //    //}
                //    if (Math.Abs(temp * 100 % 100) < 25)
                //    {
                //        res[i] = -(int)(Math.Abs(temp));
                //    }
                //    else if (Math.Abs(temp * 100 % 100) > 75)
                //    {
                //        res[i] = -((int)(Math.Abs(temp)) + 1);
                //    }
                //    else
                //    {
                //        res[i] = -((int)(Math.Abs(temp))) - 0.5;
                //    }
                //    //if (Math.Abs(temp * 100 % 100) == 0)
                //    //{
                //    //    res[i] = (int)(temp);
                //    //}
                //    //else if (Math.Abs(temp * 100 % 100) < 25)
                //    //{
                //    //    res[i] = (int)(temp) + 1;
                //    //}
                //    //else if (Math.Abs(temp * 100 % 100) > 75)
                //    //{
                //    //    res[i] = (int)(temp);
                //    //}
                //    //else
                //    //{
                //    //    res[i] = (int)(temp) + 0.5;
                //    //}
                //}
                #endregion

            }
            return res;

        }

        public static double[,] RowReduce(double[,] matrix)
        {
            long n = matrix.GetLength(0);
            long m = matrix.GetLength(1);
            long temp;
            #region
            //for (long i=0;i<n;i++)
            //{
            //    temp = FindLeftMostNonZeroRow(i,matrix,n,m);
            //    SwapRowToTopOfNonPivotRows(i, temp, matrix, n, m);
            //    Rescale(i, matrix, n, m);
            //    SubtractRowFromOthers(i, matrix, n, m);
            //}
            #endregion
            long i = 0;
            long checkColumn = 0;
            while (checkColumn < m - 1)
            {
                temp = FindLeftMostNonZeroRow(i, checkColumn, matrix, n, m);
                if (temp != -1)
                {
                    if (i != temp)
                    {
                        SwapRowToTopOfNonPivotRows(i, temp, matrix, n, m);
                    }
                    Rescale(i, checkColumn, matrix, n, m);
                    SubtractRowFromOthers(i, checkColumn, matrix, n, m);
                    i++;
                    checkColumn++;
                }
                else
                {
                    checkColumn++;
                }
            }
            return matrix;

        }

        public static void SubtractRowFromOthers(long i, long col, double[,] matrix, long n, long m)
        {
            double ratio;
            for (long j = 0; j < n; j++)
            {
                if (i != j)
                {
                    ratio = matrix[j, col];
                    SubtractRows(i, j, ratio, matrix, n, m);
                }
            }
        }

        public static void SubtractRows(long i, long j, double ratio, double[,] matrix, long n, long m)
        {
            for (long k = 0; k < m; k++)
            {
                matrix[j, k] = matrix[j, k] - ratio * matrix[i, k];
            }
        }

        public static void Rescale(long i, long col, double[,] matrix, long n, long m)
        {
            double ratio = 1 / matrix[i, col];
            for (long j = 0; j < m; j++)
            {
                matrix[i, j] *= ratio;
            }
        }

        public static void SwapRowToTopOfNonPivotRows(long i, long j, double[,] matrix, long n, long m)
        {
            double temp;
            for (long k = 0; k < m; k++)
            {
                temp = matrix[i, k];
                matrix[i, k] = matrix[j, k];
                matrix[j, k] = temp;
            }
        }

        public static long FindLeftMostNonZeroRow(long i, long col, double[,] matrix, long n, long m)
        {
            for (long j = i; j < n; j++)
            {
                if (matrix[j, col] != 0)
                {
                    return j;
                }
            }
            return -1;
        }

    }
}
