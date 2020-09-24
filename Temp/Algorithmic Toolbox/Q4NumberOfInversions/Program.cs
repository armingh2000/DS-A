using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NumberOfInversions
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long[] a = new long[n];
            string line = Console.ReadLine();
            var v = line.Split();

            for (long i = 0; i < n; i++)
            {
                a[i] = long.Parse(v[i]);
            }

            Console.WriteLine(Solve(n, a));
        }

        public static long Solve(long n, long[] a)
        {
            long invCount = 0;
            long[] t = MergeSort(a, 0, n - 1, out invCount);
            return invCount;
        }



        public static long[] MergeSort(long[] nums, long low, long high, out long invCount)
        {
            invCount = 0;
            if (low == high)
                return new long[] { nums[low] };
            long size = (high - low);
            long mid = (low + high) / 2;
            long[] left = new long[size / 2];
            long[] right = new long[size - size / 2];

            long leftInvCount = 0;
            long rightInvCount = 0;
            left = MergeSort(nums, low, mid, out leftInvCount);
            right = MergeSort(nums, mid + 1, high, out rightInvCount);
            long mergeInvCount = 0;
            long[] r = Merge(left, right, out mergeInvCount);
            invCount = leftInvCount + rightInvCount + mergeInvCount;
            return r;
        }


        public static long[] Merge(long[] a, long[] b, out long invCount)
        {
            invCount = 0;
            long size = a.Length + b.Length;
            long[] res = new long[size];
            long i = 0, j = 0;
            while (i + j < size)
            {
                if (i < a.Length && j < b.Length)
                {
                    if (a[i] <= b[j])
                    {
                        res[i + j] = a[i];
                        i++;
                    }
                    else
                    {
                        res[i + j] = b[j];
                        invCount += (a.Length - i);
                        j++;

                    }
                }
                else if (i == a.Length)
                {
                    long k;
                    for (k = j; k < b.Length; k++)
                    {
                        res[i + k] = b[k];
                    }
                    break;
                }
                else if (j == b.Length)
                {
                    long k;
                    for (k = i; k < a.Length; k++)
                    {
                        res[j + k] = a[k];
                    }
                    break;
                }
            }
            return res;
        }
    }
}
