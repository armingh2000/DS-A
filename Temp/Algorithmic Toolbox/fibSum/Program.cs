using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibSum
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
            List<long> fibList = new List<long>();
            fibList.Add(0);
            fibList.Add(1);
            long listSum = fib(fibList, n, 10);
            long sum = 0;
            sum += (listSum * (n / (fibList.Count - 2)));
            for (int i = 0; i <= (n % (fibList.Count - 2)); i++)
            {
                sum = (sum + fibList[i]) % 10;
            }
            return sum;
        }

        public static long fib(List<long> fibNums, long n, long r)
        {
            long sum = 1;
            int t = 0;
            int i = 2;
            while (t != 1)
            {
                fibNums.Add((fibNums[(int)i - 1] + fibNums[i - 2]) % r);
                if ((fibNums[i] == 1) && (fibNums[i - 1] == 0))
                    t++;

                sum = (sum + fibNums[i]) % 10;
                i++;
            }

            return sum - 1;
        }

    }
}
