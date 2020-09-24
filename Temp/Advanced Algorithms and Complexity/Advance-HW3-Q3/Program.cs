using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW3_Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            long e, v;
            string[] fLine = Console.ReadLine().Split();
            e = long.Parse(fLine[0]);
            v = long.Parse(fLine[1]);
            long[][] A = new long[e][];
            long[] b = new long[e];
            string[] line;
            for(long i=0;i<e;i++)
            {
                line = Console.ReadLine().Split();
                A[i] = new long[v];
                for (long j=0;j<v;j++)
                {
                    A[i][j] = long.Parse(line[j]);
                }
            }
            line = Console.ReadLine().Split();
            for (long j = 0; j < e; j++)
            {
                b[j] = long.Parse(line[j]);
            }
            var res = Solve(e, v, A, b).ToList();
            res.Take(1).ToList().ForEach(x=>Console.WriteLine(x));
            res.Skip(1).ToList().ForEach(x => Console.WriteLine(x + " 0"));
        }

        public static string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            string[] unsaticfiable = new string[3];
            unsaticfiable[0] = "2 1";
            unsaticfiable[1] = "1";
            unsaticfiable[2] = "-1";
            List<string> result = new List<string>();
            NoneZeroCoefficient nzc = new NoneZeroCoefficient();

            for (long i = 0; i < eqCount; i++)
            {
                nzc.countOfNoneZero = 0;
                for (long j = 0; j < A[0].Length; j++)
                {
                    if (A[i][j] != 0)
                    {
                        nzc.coefficient[nzc.countOfNoneZero] = A[i][j];
                        nzc.indeces[nzc.countOfNoneZero] = j + 1;
                        nzc.countOfNoneZero++;
                    }
                }

                if (nzc.countOfNoneZero == 0 && b[i] < 0)
                {
                    return unsaticfiable;
                }
                if (nzc.countOfNoneZero == 3)
                {
                    if (nzc.coefficient[0] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString() + " " + (nzc.indeces[1].Value).ToString() + " " + (nzc.indeces[2].Value).ToString());
                    }
                    if (nzc.coefficient[1] > b[i])
                    {
                        result.Add((-nzc.indeces[1].Value).ToString() + " " + (nzc.indeces[0].Value).ToString() + " " + (nzc.indeces[2].Value).ToString());
                    }
                    if (nzc.coefficient[2] > b[i])
                    {
                        result.Add((-nzc.indeces[2].Value).ToString() + " " + (nzc.indeces[0].Value).ToString() + " " + (nzc.indeces[1].Value).ToString());
                    }
                    if (nzc.coefficient[0] + nzc.coefficient[1] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString() + " " + (-nzc.indeces[1].Value).ToString() + " " + (nzc.indeces[2].Value).ToString());
                    }
                    if (nzc.coefficient[0] + nzc.coefficient[2] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString() + " " + (-nzc.indeces[2].Value).ToString() + " " + (nzc.indeces[1].Value).ToString());
                    }
                    if (nzc.coefficient[1] + nzc.coefficient[2] > b[i])
                    {
                        result.Add((-nzc.indeces[1].Value).ToString() + " " + (-nzc.indeces[2].Value).ToString() + " " + (nzc.indeces[0].Value).ToString());
                    }
                    if (nzc.coefficient[0] + nzc.coefficient[1] + nzc.coefficient[2] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString() + " " + (-nzc.indeces[1].Value).ToString() + " " + (-nzc.indeces[2].Value).ToString());
                    }
                    if (b[i] < 0)
                    {
                        result.Add((nzc.indeces[0].Value).ToString() + " " + (nzc.indeces[1].Value).ToString() + " " + (nzc.indeces[2].Value).ToString());
                    }
                }
                else if (nzc.countOfNoneZero == 2)
                {
                    if (nzc.coefficient[0] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString() + " " + (nzc.indeces[1].Value).ToString());
                    }
                    if (nzc.coefficient[1] > b[i])
                    {
                        result.Add((-nzc.indeces[1].Value).ToString() + " " + (nzc.indeces[0].Value).ToString());
                    }
                    if (nzc.coefficient[0] + nzc.coefficient[1] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString() + " " + (-nzc.indeces[1].Value).ToString());
                    }
                    if (b[i] < 0)
                    {
                        result.Add((nzc.indeces[0].Value).ToString() + " " + (nzc.indeces[1].Value).ToString());
                    }
                }
                else if (nzc.countOfNoneZero == 1)
                {
                    if (nzc.coefficient[0] > b[i])
                    {
                        result.Add((-nzc.indeces[0].Value).ToString());
                    }
                    else if (b[i] < 0)
                    {
                        result.Add((nzc.indeces[0].Value).ToString());
                    }
                }
            }

            result.Add(result.Count.ToString() + " " + varCount);
            result.Reverse();
            if (result.Count == 1)
            {
                string[] q = new string[2];
                q[0]="1 1";
                q[1]="1";
                return q;
                
            }
            return result.ToArray();
        }
        
        public class NoneZeroCoefficient
        {
            public long countOfNoneZero;
            public long?[] indeces = new long?[3];
            public long?[] coefficient = new long?[3];
        }

    }
}
