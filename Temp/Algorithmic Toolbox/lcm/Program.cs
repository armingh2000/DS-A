using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcm
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
            return (a * b) / gcd(a, b);
        }

        private static long gcd(long v1, long v2)
        {
            if (v1 < v2)
                return gcd(v2, v1);
            if (v2 == 0)
                return v1;
            if (v2 == 1)
                return 1;
            return gcd(v2, v1 % v2);
        }
    }
}
