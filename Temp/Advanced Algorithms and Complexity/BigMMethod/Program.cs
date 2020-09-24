using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigMMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = Console.ReadLine().Split().Select(long.Parse).ToArray();
            long C = input[0];
            long V = input[1];
            double[,] matrix = new double[C + 1, V + 1];
            for (long i = 0; i < C; i++)
            {
                long[] Row = Console.ReadLine().Split().Select(long.Parse).ToArray();
                //int j = 0;
                //foreach (long l in Row)
                //{
                //    matrix[i, j] = l;
                //    j++;
                //}
                for(long j=0;j<V;j++)
                {
                    matrix[i, j] = Row[j];
                }
            }

            string[] rightSide = Console.ReadLine().Split();
            for(long i=0;i<C;i++)
            {
                matrix[i, matrix.GetLength(1) - 1] = double.Parse(rightSide[i]);
            }

            string[] obj = Console.ReadLine().Split();
            for (long j = 0; j < V; j++)
            {
                matrix[matrix.GetLength(0) - 1, j] = double.Parse(obj[j]);
            }

            Solve(C, V, matrix);


            //double[] LastRow = Console.ReadLine().Split().Select(double
            //    .Parse).ToArray();

            //for (long j = 0; j < N; j++)
            //{
            //    matrix[j, M] = LastRow[j];
            //}


            //double[] LastLastRow = Console.ReadLine().Split().Select(double.Parse).ToArray();

            //for (long j = 0; j < M; j++)
            //{
            //    matrix[N, j] = LastLastRow[j];
            //}
            
        }

        public static void Solve(long c, long v, double[,] matrix1)
        {
            //double[,] matrix = new double[3, 3] { { 1, 1, 1 }, { -1, -1, -2 }, { 1, 1, 0 } };
            //double[,] matrix = new double[2, 4] { { 0, 0, 1, 3 }, { 1, 1, 1, 0 } };
            
            //double[,] matrix = new double[4, 4] { { 1, 1, 0, 20 }, { 1, 0, 1, 5 }, { 0, 1, 1, -10 }, { -1, 1, -3, 0 } };
            //double[,] matrix = new double[4, 3] { { -1, -1, -1 }, { 1, 0, 2 }, { 0, 1, 2 }, { -1, 2, 0 } };
            //double[,] matrix = new double[4, 3] { { -1, -1, -1 }, { 1, 0, 2 }, { 0, 1, 2 }, { -1, 2, 0 } };
            
            var m = ConstructEqualitiesMatrix(matrix1, v, c);

            var t = GetMIndex(m);
            for (long i = 0; i < t.Length; i++)
            {
                SubtractRowFromOthers(t[i].Item1, t[i].Item2, m, m.GetLength(0), m.GetLength(1));
            }

            double[] tempAns;
            for (long i = 0; i < v; i++)
            {
                if(matrix1[matrix1.GetLength(0)-1,i]>0)
                {
                    tempAns = new double[v];
                    tempAns[i] = long.MaxValue;
                    if (CheckInAllInequalities(tempAns, matrix1))
                    {
                        Console.WriteLine("Infinity");
                        return;
                    }
                }
            }


            //var v = ConstructBasicVariablesVector(m);
            var s = BigM(m, v);
            if (s == null)
            {
                Console.WriteLine("Infinity");
                return;
            }
            if (s[0] == null)
            {
                Console.WriteLine("No solution");
                return;
            }
            if (CheckInAllInequalities(s, matrix1))
            {
                Console.WriteLine("Bounded solution");
                s.ToList().ForEach(x => Console.Write(x + " "));
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }

        public static double?[] BigM(double[,] matrix, long variableCount)
        {
            long n = matrix.GetLength(0);
            long m = matrix.GetLength(1);
            long[] basicVariables = ConstructBasicVariablesVector(matrix);

            double?[] ans = new double?[variableCount];
            //double[,] equalitiesMatrix = ConstructEqualitiesMatrix(matrix, inequalityCount, n, m);
            //Normalize(equalitiesMatrix, n, m);
            //if (HasSolution(matrix, basicVariables))
            //{
                for(long i=0;i<ans.Length;i++)
                {
                    ans[i] = 0;
                }

                long entering = ChooseEntering(matrix, n, m);
                long departing = -1;
                if (entering != -1)
                {
                    departing = ChooseDeparting(matrix, entering, n, m);
                    if (departing < 0)
                        return null;
                }

                while (entering >= 0 && departing >= 0)
                {
                    Rescale(departing, entering, matrix, n, m);
                    SubtractRowFromOthers(departing, entering, matrix, n, m);
                    Normalize(matrix, n, m);
                    basicVariables[departing] = entering;

                    entering = ChooseEntering(matrix, n, m);
                    if (entering != -1)
                    {
                        departing = ChooseDeparting(matrix, entering, n, m);
                        if (departing < 0)
                            return null;
                    }
                }

                
                long temp;

                for (long i = 0; i < n - 1; i++)
                {
                    temp = basicVariables[i];
                    if (temp < variableCount)
                    {
                        ans[temp] = matrix[i, m - 1] / matrix[i, temp];
                    }
                }

                //return ans;
            //}
            return ans;
        }

        public static bool HasSolution(double[,] matrix, long[] basicVariables)
        {
            for (long i = 0; i < matrix.GetLength(0); i++) 
            {
                if (matrix[i, matrix.GetLength(1) - 1] / matrix[i, basicVariables[i]] < 0 && basicVariables[i] != matrix.GetLength(1) - 2) 
                {
                    return false;
                }
            }
            return true;
        }

        public static double[,] ConstructEqualitiesMatrix(double[,] matrix, long variableCount, long inequalityCount/* , long negativeCoeficient*//*, long n, long m*/)
        {
            //double[,] equalitiesMatrix = new double[n, m + 1];
            long negativeCoeficientCount = CalculateNegCoefNum(matrix);
            double[,] equalitiesMatrix = new double[matrix.GetLength(0), inequalityCount+variableCount+ negativeCoeficientCount+2];
            long n = inequalityCount;
            long m = inequalityCount + variableCount + negativeCoeficientCount + 1;

            long negNum = 0;

            for (long i = 0; i < inequalityCount; i++)
            {
                if (matrix[i, matrix.GetLength(1) - 1] >= 0)
                {
                    for (long j = 0; j < m - 1 - inequalityCount - negativeCoeficientCount; j++)//for x
                    {
                        equalitiesMatrix[i, j] = matrix[i, j];
                    }
                    for (long j = m - inequalityCount - 1 - negativeCoeficientCount; j < m - 1 - negativeCoeficientCount; j++)//for s
                    {
                        if (j == matrix.GetLength(1) + i - 1)
                        {
                            equalitiesMatrix[i, j] = 1;
                        }
                        else
                        {
                            equalitiesMatrix[i, j] = 0;
                        }
                    }
                    for (long j = m - 1 - negativeCoeficientCount; j < m - 1; j++)//for a
                    {
                        equalitiesMatrix[i, j] = 0;
                    }
                    equalitiesMatrix[i, m] = matrix[i, matrix.GetLength(1) - 1];
                }
                else
                {
                    for (long j = 0; j < m - 1 - inequalityCount - negativeCoeficientCount; j++)
                    {
                        equalitiesMatrix[i, j] = -matrix[i, j];
                    }
                    for (long j = m - inequalityCount - 1 - negativeCoeficientCount; j < m - 1 - negativeCoeficientCount; j++)
                    {
                        if (j == matrix.GetLength(1) + i - 1)
                        {
                            equalitiesMatrix[i, j] = -1;
                        }
                        else
                        {
                            equalitiesMatrix[i, j] = 0;
                        }
                    }
                    for (long j = m - 1 - negativeCoeficientCount; j < m - 1 ; j++)
                    {
                        if (j == variableCount + inequalityCount + negNum) 
                        {
                            equalitiesMatrix[i, j] = 1;
                            negNum++;
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////new added
                            break;
                        }
                        else
                        {
                            equalitiesMatrix[i, j] = 0;
                        }
                    }
                    equalitiesMatrix[i, m ] = -matrix[i, matrix.GetLength(1) - 1];
                    //equalitiesMatrix[i, m] = 0;

                }
            }


            //long lastLine = n - 1;
            long lastLine = inequalityCount;

            for (long j = 0; j <= m; j++)
            {
                if (j < matrix.GetLength(1) - 1)
                {
                    equalitiesMatrix[lastLine, j] = -matrix[lastLine, j];
                }
                else
                {
                    if (j >= inequalityCount && j < variableCount + inequalityCount)
                        equalitiesMatrix[lastLine, j] = 0;
                    else if (j >= variableCount + inequalityCount && j < m - 1)
                        equalitiesMatrix[lastLine, j] = int.MaxValue;
                    else if (j == m - 1)
                        equalitiesMatrix[lastLine, j] = 1;
                    else
                        equalitiesMatrix[lastLine, j] = 0;
                }
            }

            return equalitiesMatrix;
        }

        public static long CalculateNegCoefNum(double[,] matrix)
        {
            long res = 0;
            for(long i=0;i<matrix.GetLength(0);i++)
            {
                if(matrix[i,matrix.GetLength(1)-1]<0)
                {
                    res++;
                }
            }
            return res;
        }

        public class Column
        {
            char letter { get; set; }
            long index { get; set; }
        }

        public static Tuple<long,long>[] GetMIndex(double[,] matrix)
        {
            long x = matrix.GetLength(0) - 1;
            List<Tuple<long,long>> res = new List<Tuple<long,long>>();
            for(long j=0;j<matrix.GetLength(1);j++)
            {
                if(matrix[x,j]==int.MaxValue)
                {
                    //res.Add(j);
                    for(long i=0;i<matrix.GetLength(0)-1;i++)
                    {
                        if(matrix[i,j]==1)
                        {
                            res.Add(new Tuple<long, long>(i, j));
                        }
                    }
                }
            }
            return res.ToArray();
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

        public static double[] Simplex(double[,] matrix, long inequalityCount, long variableCount)
        {
            long n = matrix.GetLength(0);
            long m = matrix.GetLength(1) + inequalityCount;
            long[] basicVariables = ConstructBasicVariablesVector(n, variableCount);

            double[,] equalitiesMatrix = ConstructEqualitiesMatrix(matrix, inequalityCount, n, m);
            Normalize(equalitiesMatrix, n, m);

            long entering = ChooseEntering(equalitiesMatrix, n, m);
            long departing = -1;
            if (entering != -1)
            {
                departing = ChooseDeparting(equalitiesMatrix, entering, n, m);
                if (departing < 0)
                    return null;
            }

            while (entering >= 0 && departing >= 0)
            {
                Rescale(departing, entering, equalitiesMatrix, n, m);
                SubtractRowFromOthers(departing, entering, equalitiesMatrix, n, m);
                Normalize(equalitiesMatrix, n, m);
                basicVariables[departing] = entering;

                entering = ChooseEntering(equalitiesMatrix, n, m);
                if (entering != -1)
                {
                    departing = ChooseDeparting(equalitiesMatrix, entering, n, m);
                    if (departing < 0)
                        return null;
                }
            }

            double[] ans = new double[variableCount];
            long temp;

            for (long i = 0; i < n - 1; i++)
            {
                temp = basicVariables[i];
                if (temp < variableCount)
                {
                    ans[temp] = equalitiesMatrix[i, m - 1] / equalitiesMatrix[i, temp];
                }
            }

            return ans;
        }
        
        public static long[] ConstructBasicVariablesVector(double[,] matrix)
        {
            long temp = 0;
            long row = -1;
            //List<long> basicVariables = new List<long>();
            long[] basicVariables = new long[matrix.GetLength(0)];

            for(long j=0;j<matrix.GetLength(1)-1;j++)
            {
                temp = 0;
                for(long i=0;i<matrix.GetLength(0);i++)
                {
                    if(matrix[i,j]!=0)
                    {
                        temp++;
                        row = i;
                        if(temp>1)
                        {
                            break;
                        }
                    }
                }

                if(temp==1)
                {
                    //basicVariables.Add(j);
                    basicVariables[row] = j;
                }
            }
            return basicVariables;
        }

        public static long[] ConstructBasicVariablesVector(long n, long variableCount)
        {
            long[] basicVariables = new long[n - 1];
            for (long i = 0; i < n - 1; i++)
            {
                basicVariables[i] = variableCount + i;
            }
            return basicVariables;
        }

        public static void Normalize(double[,] equalitiesMatrix, long n, long m)
        {
            for (long i = 0; i < n - 1; i++)
            {
                if (equalitiesMatrix[i, m - 1] < 0)
                {
                    NormalizeRow(equalitiesMatrix, i, n, m);
                }
            }
        }

        public static void NormalizeRow(double[,] equalitiesMatrix, long i, long n, long m)
        {
            for (long j = 0; j < m; j++)
            {
                equalitiesMatrix[i, j] *= -1;
            }
        }

        #region
        //public static long ChooseDeparting(double[,] equalitiesMatrix, long entering, long numberOfLastColumn, long NumberOfLastRow)
        //{
        //    long departing = -1;
        //    double min = long.MaxValue;
        //    for(long i=0;i<NumberOfLastRow;i++)
        //    {
        //        if(equalitiesMatrix[i,numberOfLastColumn]/equalitiesMatrix[i,entering]>0 
        //            && 
        //           equalitiesMatrix[i, numberOfLastColumn] / equalitiesMatrix[i, entering]<min)
        //        {
        //            min = equalitiesMatrix[i, numberOfLastColumn] / equalitiesMatrix[i, entering];
        //            departing = i;
        //        }
        //    }
        //    return departing;
        //}

        //public static long ChooseEntering(double[,] equalitiesMatrix, long numberOfRow, long lastNumberOfColumn)
        //{
        //    long entering = -1;
        //    double min = long.MaxValue;
        //    for(long j=0;j<=lastNumberOfColumn;j++)
        //    {
        //        if (equalitiesMatrix[numberOfRow, j] < 0 && equalitiesMatrix[numberOfRow, j] < min)
        //        {
        //            min = equalitiesMatrix[numberOfRow, j];
        //            entering = j;
        //        }
        //    }
        //    return entering;
        //}
        #endregion

        public static long ChooseDeparting(double[,] equalitiesMatrix, long entering, long n, long m)
        {
            long departing = -1;
            double min = long.MaxValue;
            for (long i = 0; i < n - 1; i++)
            {
                if (equalitiesMatrix[i, entering] > 0
                    &&
                   //equalitiesMatrix[i, m-1] / equalitiesMatrix[i, entering] > 0
                   //&&
                   equalitiesMatrix[i, m - 1] / equalitiesMatrix[i, entering] < min)
                {
                    min = equalitiesMatrix[i, m - 1] / equalitiesMatrix[i, entering];
                    departing = i;
                }
            }
            return departing;
        }

        public static long ChooseEntering(double[,] equalitiesMatrix, long n, long m)
        {
            long entering = -1;
            double min = long.MaxValue;
            for (long j = 0; j < m - 1; j++)
            {
                if (equalitiesMatrix[n - 1, j] < -0.000000001 && equalitiesMatrix[n - 1, j] < min)
                {
                    min = equalitiesMatrix[n - 1, j];
                    entering = j;
                }
            }
            return entering;
        }

        public static double[,] ConstructEqualitiesMatrix(double[,] matrix, long inequalityCount, long n, long m)
        {
            double[,] equalitiesMatrix = new double[n, m];

            for (long i = 0; i < n - 1; i++)
            {
                for (long j = 0; j < m - 1 - inequalityCount; j++)
                {
                    equalitiesMatrix[i, j] = matrix[i, j];
                }
                for (long j = m - inequalityCount - 1; j < m - 1; j++)
                {
                    if (j == matrix.GetLength(1) + i - 1)
                    {
                        equalitiesMatrix[i, j] = 1;
                    }
                    else
                    {
                        equalitiesMatrix[i, j] = 0;
                    }
                }
                equalitiesMatrix[i, m - 1] = matrix[i, matrix.GetLength(1) - 1];
            }
            long lastLine = n - 1;
            for (long j = 0; j < m; j++)
            {
                if (j < matrix.GetLength(1) - 1)
                {
                    equalitiesMatrix[lastLine, j] = -matrix[lastLine, j];
                }
                else
                {
                    equalitiesMatrix[lastLine, j] = 0;
                }
            }
            return equalitiesMatrix;
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

        public static double Round(double temp)
        {
            if (temp > 0)
            {
                if (temp * 100 % 100 < 25)
                {
                    return (int)(temp);
                }
                else if (temp * 100 % 100 > 75)
                {
                    return (int)(temp) + 1;
                }
                else
                {
                    return (int)(temp) + 0.5;
                }
            }
            else
            {
                if (Math.Abs(temp * 100 % 100) < 25)
                {
                    return -(int)(Math.Abs(temp));
                }
                else if (Math.Abs(temp * 100 % 100) > 75)
                {
                    return -((int)(Math.Abs(temp)) + 1);
                }
                else
                {
                    return -((int)(Math.Abs(temp))) - 0.5;
                }
            }
        }

        public static bool CheckInAllInequalities(double?[] res, double[,] initialMatrix)
        {
            //for (long i = 0; i < res.Length; i++)
            //{
            //    if (res[i] < 0)
            //        return false;
            //}
            double sum = 0;
            for (long i = 0; i < initialMatrix.GetLength(0) - 1; i++)
            {
                sum = 0;
                for (long j = 0; j < res.Length; j++)
                {
                    sum += (double)(res[j] * initialMatrix[i, j]);
                }
                if (Round(sum) > initialMatrix[i, initialMatrix.GetLength(1) - 1])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckInAllInequalities(double[] res, double[,] initialMatrix)
        {
            //for (long i = 0; i < res.Length; i++)
            //{
            //    if (res[i] < 0)
            //        return false;
            //}
            double sum = 0;
            for (long i = 0; i < initialMatrix.GetLength(0) - 1; i++)
            {
                sum = 0;
                for (long j = 0; j < res.Length; j++)
                {
                    sum += (double)(res[j] * initialMatrix[i, j]);
                }
                if (sum > initialMatrix[i, initialMatrix.GetLength(1) - 1])
                {
                    return false;
                }
            }
            return true;
        }


        //static void Main(string[] args)
        //{
        //    long[] input = Console.ReadLine().Split().Select(long.Parse).ToArray();
        //    long N = input[0];
        //    long M = input[1];
        //    double[,] matrix = new double[N, M];
        //    for (long i = 0; i < N; i++)
        //    {
        //        long[] Row = Console.ReadLine().Split().Select(long.Parse).ToArray();
        //        int j = 0;
        //        foreach (long l in Row)
        //        {
        //            matrix[i, j] = l;
        //            j++;
        //        }
        //    }

        //    double[] LastRow = Console.ReadLine().Split().Select(double
        //        .Parse).ToArray();

        //    //for (long j = 0; j < N; j++)
        //    //{
        //    //    matrix[j, M] = LastRow[j];
        //    //}


        //    double[] LastLastRow = Console.ReadLine().Split().Select(double.Parse).ToArray();

        //    //for (long j = 0; j < M; j++)
        //    //{
        //    //    matrix[N, j] = LastLastRow[j];
        //    //}

        //    var s = new Simplex(
        //      LastLastRow,
        //      matrix,
        //      LastRow
        //    );

        //    var answer = s.maximize();
        //    string result = "";
        //    //Console.WriteLine(answer.Item1);
        //    if (answer.Item2 == null)
        //    {
        //        Console.WriteLine("Infinity");
        //    }
        //    else
        //    {
        //        bool HasSolution = CheckSolution(N, M, LastRow, answer.Item2, matrix);
        //        if (HasSolution)
        //        {
        //            Console.WriteLine("Bounded solution");
        //            foreach (double d in answer.Item2)
        //            {
        //                result += d.ToString("F3") + " ";
        //            }
        //            Console.WriteLine(result);
        //        }
        //        else
        //        {
        //            Console.WriteLine("No solution");
        //        }
        //    }
        //}


    }
}
