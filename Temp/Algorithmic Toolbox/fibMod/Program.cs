using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibMod
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long a = long.Parse(line.Split()[0]);
            long b = long.Parse(line.Split()[1]);
            Console.WriteLine(Solve(a, b));
        }
        public static long Solve(long a, long b)
        {
            //long[] fibNums = new long[a + 1];
            //fibNums[0] = 0;
            //if (a >= 1)
            //    fibNums[1] = 1;
            //return fib(fibNums, a,b);
            List<long> fibList = new List<long>();
            fibList.Add(0);
            fibList.Add(1);
            return fib(fibList, a, b);

        }

        public static long fib(List<long> fibNums, long n, long r)
        {
            //for (long i = 2; i <= n; i++)
            //    fibNums.Add ( (fibNums[(int)i - 1] + fibNums[i - 2]) % r);
            int t = 0;
            int i = 2;
            while (t != 2)
            {
                fibNums.Add((fibNums[(int)i - 1] + fibNums[i - 2]) % r);
                if ((fibNums[i] == 1) && (fibNums[i - 1] == 0))
                    t++;


                i++;
            }
            long w = n % (fibNums.Count - 2);
            int e = (int)w;
            return fibNums[e];
        }
    }
}
