using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibNum
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            Console.WriteLine(Solve(n));
        }

        public static long Solve(long n)
        {
            long[] fibNums = new long[n + 1];
            fibNums[0] = 0;
            if (n >= 1)
                fibNums[1] = 1;
            return fib(fibNums, n);
        }

        public static long fib(long[] fibNums, long n)
        {
            for (int i = 2; i <= n; i++)
                fibNums[i] = fibNums[i - 1] + fibNums[i - 2];
            return fibNums[n];
        }
    }
}
