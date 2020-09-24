using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q5MaximizeNumberOfPrizePlaces
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            var v=Solve(n).ToList();
            Console.WriteLine(v.Count());
            v.ForEach(x => Console.Write(x + " "));
        }
        public static long[] Solve(long n)
        {
            List<long> res = new List<long>();
            long remain = n;
            long t = 0;
            long k = 1;
            while (remain >= k)
            {
                res.Add(k);
                t++;
                remain -= k;
                k++;
            }
            if (k != 0)
            {
                res.Remove(k - 1);
                res.Add(k + remain - 1);
            }
            return res.ToArray();
        }
    }
}
