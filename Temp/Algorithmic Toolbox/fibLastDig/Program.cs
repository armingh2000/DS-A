using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibLastDig
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
            return fib(n);
        }

        public static long fib(long n)
        {
            if (n == 0 || n == 1)
                return n;
            long a = 0;
            long b = 1;
            long c;
            for (int i = 2; i <= n; i++)
            {
                c = (a + b) % 10;
                a = b;
                b = c;
            }

            return b;
        }
    }
}
