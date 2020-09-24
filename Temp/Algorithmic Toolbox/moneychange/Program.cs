using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneychange
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
            long[] coins = new long[] { 1, 3, 4 };
            long[] money = new long[n + 1];
            for (long i = 1; i <= n; i++)
            {
                money[i] = n + 1;
                for (long j = 0; j < 3; j++)
                {
                    if (i >= coins[j])
                    {
                        money[i] = Math.Min(money[i - coins[j]] + 1, money[i]);
                    }
                }
            }
            return money[n];
        }
    }
}
