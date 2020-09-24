using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2PrimitiveCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());

            var v = Solve(n);
            Console.WriteLine(v.Length-1);
            v.ToList().ForEach(x => Console.Write(x + " "));
        }

        public static long[] Solve(long n)
        {
            //2x,3x,x+1
            long[] nums = new long[n];
            nums[0] = 0;

            for (long i = 1; i < n; i++)
            {
                nums[i] = n + 1;
                nums[i] = Math.Min(nums[i], nums[i - 1] + 1);
                if ((i + 1) % 2 == 0)
                {
                    nums[i] = Math.Min(nums[i], nums[(i + 1) / 2 - 1] + 1);
                }
                if ((i + 1) % 3 == 0)
                {
                    nums[i] = Math.Min(nums[i], nums[(i + 1) / 3 - 1] + 1);
                }
            }

            long[] final = new long[nums[n - 1] + 1];
            long idx = n;
            final[nums[n - 1]] = n;

            for (long i = nums[n - 1] - 1; i >= 0; i--)
            {
                if ((idx % 3 == 0) && (nums[idx - 1] == nums[idx / 3 - 1] + 1))
                {
                    final[i] = idx / 3;
                    idx /= 3;
                }
                else if ((idx % 2 == 0) && (nums[idx - 1] == nums[idx / 2 - 1] + 1))
                {
                    final[i] = idx / 2;
                    idx /= 2;
                }
                else
                {
                    final[i] = idx - 1;
                    idx--;
                }
            }

            long[] res = new long[final.Length + 1];
            res[0] = nums[n - 1];

            for (long i = 1; i < final.Length; i++)
            {
                res[i] = final[i - 1];
            }

            return final;
        }
    }
}
