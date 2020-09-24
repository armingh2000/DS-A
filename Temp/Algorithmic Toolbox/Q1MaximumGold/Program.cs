using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1MaximumGold
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long w = long.Parse(line.Split()[0]);
            long g = long.Parse(line.Split()[1]);

            line = Console.ReadLine();
            var v = line.Split();

            long[] goldBars = new long[g];

            for (long i=0;i<g;i++)
            {
                goldBars[i] = long.Parse(v[i]);
            }

            Console.WriteLine(Solve(w, goldBars));
        }

        public static long Solve(long W, long[] goldBars)
        {
            long[,] res = new long[goldBars.Length + 1, W + 1];
            for (long i = 0; i < goldBars.Length + 1; i++)
            {
                res[i, 0] = 0;
            }
            for (long i = 0; i < W + 1; i++)
            {
                res[0, i] = 0;
            }
            for (long i = 1; i < goldBars.Length + 1; i++)
            {
                for (long j = 1; j < W + 1; j++)
                {
                    res[i, j] = Math.Max(res[i, j], res[i - 1, j]);
                    if (goldBars[i - 1] <= j)
                    {
                        res[i, j] = Math.Max(res[i, j]
                        , res[i - 1, j - goldBars[i - 1]] + goldBars[i - 1]);
                    }
                }
            }
            return res[goldBars.Length, W];
        }
    }
}
