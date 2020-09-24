using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace changeMoney
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            Console.WriteLine(Solve(n));
        }
        public static long Solve(long money)
        {
            //var coins = new [] {10, 5, 1};
            //long counts = 0;

            //for (int i = 0; i < coins.Length; i++)
            //{
            //    if (money == 0)
            //        break;

            //    counts += (money / coins[i]);
            //    money %= coins[i];
            //}

            //return counts;
            return money / 10 + (money % 10) / 5 + (money % 10) % 5;
        }
    }
}
