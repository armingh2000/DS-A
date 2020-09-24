using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2MajorityElement
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] a = new long[n];
            string line = Console.ReadLine();
            var v = line.Split();
            for (long i=0;i<n;i++)
            {
                a[i] = long.Parse(v[i]);
            }

            Console.WriteLine(Solve(n, a));
        }

        public static long Solve(long n, long[] a)
        {
            long majElem = DandCMaj(a, 0, n - 1);
            long majElemCount = 0;
            for (long i = 0; i < n; i++)
            {
                if (a[i] == majElem)
                {
                    majElemCount++;
                }
            }
            if (majElemCount > (n / 2))
                return 1;
            return 0;
        }

        public static long DandCMaj(long[] a, long low, long high)
        {
            if (low == high)
            {
                return a[low];
            }


            long mid = (low + high) / 2;
            long m1 = DandCMaj(a, low, mid);
            long m2 = DandCMaj(a, mid + 1, high);
            if (m1 == m2)
            {
                return m1;
            }
            else
            {
                long m1count = 0, m2count = 0;
                for (long i = low; i <= high; i++)
                {
                    if (a[i] == m1)
                    {
                        m1count++;
                    }
                    else if (a[i] == m2)
                    {
                        m2count++;
                    }
                }
                if (m1count > m2count)
                {
                    return m1;
                }
                else
                {
                    return m2;
                }
            }
        }
    }
}
