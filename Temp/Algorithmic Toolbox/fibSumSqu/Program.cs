using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibSumSqu
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
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            List<long> fibList = new List<long>();
            fibList.Add(0);
            fibList.Add(1);
            fib(fibList, n, 10);
            int idx1 = (int)(n % (fibList.Count - 2));
            int idx2 = (int)((n - 1) % (fibList.Count - 2));
            return (fibList[idx1] * (fibList[idx2] + fibList[idx1])) % 10;
        }

        public static void fib(List<long> fibNums, long n, long r)
        {
            int t = 0;
            int i = 2;
            while (t != 2)
            {
                fibNums.Add((fibNums[(int)i - 1] + fibNums[i - 2]) % r);
                if ((fibNums[i] == 1) && (fibNums[i - 1] == 0))
                    t++;
                i++;
            }
        }

    }
}
