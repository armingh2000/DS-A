using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fibParSum
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
            if (a < b)
            {
                long c = a;
                a = b;
                b = c;
            }
            List<long> fibList = new List<long>();
            fibList.Add(0);
            fibList.Add(1);
            long listSum = fib(fibList, b, 10);
            long sum = 0;
            sum += (listSum * (a / (fibList.Count - 2)));
            sum -= (listSum * (b / (fibList.Count - 2)));

            for (int i = 0; i < (b % (fibList.Count - 2)); i++)
            {
                sum = (sum - fibList[i]) % 10;
            }

            for (int i = 0; i <= (a % (fibList.Count - 2)); i++)
            {
                sum = (sum + fibList[i]) % 10;
            }

            //for (int i = ; i <= (a % (fibList.Count - 2)); i++)
            //{
            //    sum = (sum - fibList[i]) % 10;
            //}

            if (sum >= 0)
                return sum;
            else
                throw new Exception();
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
            if (sum - 1 >= 0)
                return sum - 1;
            else
                throw new Exception();
            //return sum - 1 + 10;
        }

    }
}
