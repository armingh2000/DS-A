using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2PartitioningSouvenirs
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long souvenirsCount = long.Parse(line);

            line = Console.ReadLine();
            var v = line.Split();

            long[] souvenirs = new long[souvenirsCount];

            for (long i = 0; i < souvenirsCount; i++)
            {
                souvenirs[i] = long.Parse(v[i]);
            }

            Console.WriteLine(Solve(souvenirsCount, souvenirs));
        }

        public static long Solve(long souvenirsCount, long[] souvenirs)
        {
            long sum = 0;
            for (long i = 0; i < souvenirsCount; i++)
            {
                sum += souvenirs[i];
            }

            if (sum % 3 != 0)
            {
                return 0;
            }

            long[,] res = new long[sum + 1, souvenirsCount + 1];

            for (long i = 0; i <= souvenirsCount; i++)
            {
                res[0, i] = 1;
            }

            for (long j = 1; j <= souvenirsCount; j++)
            {
                for (long i = 1; i <= sum; i++)
                {
                    if (souvenirs[j - 1] <= i)
                    {
                        if (res[i - souvenirs[j - 1], j - 1] == 1)
                        {
                            res[i, j] = 1;
                        }
                    }
                    res[i, j] = Math.Max(res[i, j], res[i, j - 1]);
                }
            }


            long fidx3 = -1, lidx23 = -1;
            bool flag = false;
            bool t = false;

            for (long i = 0; i <= souvenirsCount; i++)
            {
                if (!flag && res[sum / 3, i] == 1)
                {
                    fidx3 = i;
                    flag = true;
                }
                if (res[(sum * 2) / 3, i] == 1)
                {
                    lidx23 = i;
                }
            }

            if ((lidx23 > -1) && (fidx3 > -1))
            {
                if (((fidx3 <= lidx23) && (lidx23 < souvenirsCount))
                    ||
                    ((fidx3 < lidx23) && (lidx23 == souvenirsCount)))
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
