using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3EditDistance
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = Console.ReadLine(), str2 = Console.ReadLine();
            Console.WriteLine(Solve(str1, str2));
        }

        public static long Solve(string str1, string str2)
        {
            long[,] res = new long[str1.Length + 1, str2.Length + 1];

            for (long i = 1; i <= str2.Length; i++)
            {
                res[0, i] = i;
            }

            for (long i = 1; i <= str1.Length; i++)
            {
                res[i, 0] = i;
            }

            for (long i = 1; i <= str1.Length; i++)
            {
                for (long j = 1; j <= str2.Length; j++)
                {
                    res[i, j] = Math.Min(res[i - 1, j] + 1, res[i, j - 1] + 1);
                    if (str1[(int)i - 1] == str2[(int)j - 1])
                    {
                        res[i, j] = Math.Min(res[i - 1, j - 1], res[i, j]);
                    }
                    else
                    {
                        res[i, j] = Math.Min(res[i - 1, j - 1] + 1, res[i, j]);
                    }
                }
            }

            return res[str1.Length, str2.Length];
        }
    }
}
