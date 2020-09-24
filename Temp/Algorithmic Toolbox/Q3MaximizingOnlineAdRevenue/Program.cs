using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3MaximizingOnlineAdRevenue
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long n = long.Parse(line.Split()[0]);
            //long cap = long.Parse(line.Split()[1]);
            long[] a = new long[n];
            long[] b = new long[n];
            line = Console.ReadLine();

            for (long i = 0; i < n; i++)
            {
                //line = Console.ReadLine();
                a[i] = long.Parse(line.Split()[i]);
                //w[i] = long.Parse(line.Split()[1]);
            }

            line = Console.ReadLine();

            for (long i = 0; i < n; i++)
            {
                //line = Console.ReadLine();
                b[i] = long.Parse(line.Split()[i]);
                //w[i] = long.Parse(line.Split()[1]);
            }

            Console.WriteLine(Solve(n,a,b));
        }
        public static long Solve(long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
            Array.Sort(adRevenue);
            Array.Sort(averageDailyClick);
            long sum = 0;
            for (int i = 0; i < Math.Min(slotCount, averageDailyClick.Length); i++)
            {
                sum += (adRevenue[i] * averageDailyClick[i]);
            }
            return sum;
        }
    }
}
