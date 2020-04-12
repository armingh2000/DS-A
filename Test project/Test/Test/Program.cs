
using System;
using System.Collections.Generic;
using System.Linq;

namespace bwm
{
    public class Program
    {
        public static void Main()
        {
            string text = Console.ReadLine();
            long n = long.Parse(Console.ReadLine());
            string[] patterns = Console.ReadLine().Split();

            Solve(text, n, patterns);
        }

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string BWT(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public static void Solve(string text, long n, String[] patterns)
        {
            symbols.Construct(text);
            int top, bottom, curr, ind;
            int len = text.Length;
            char sym;
            bool had = false;
            for (int i = 0; i < n; i++)
            {
                curr = patterns[i].Length - 1;
                bottom = len - 1;
                top = 0;
                had = false;
                while (top <= bottom)
                {
                    if (curr >= 0)
                    {
                        sym = patterns[i][curr];
                        curr--;
                        ind = symbols.IndexOf(sym);
                        top = symbols.FirstOccur[ind] + symbols.count[ind][top] + 1;
                        bottom = symbols.FirstOccur[ind] + symbols.count[ind][bottom + 1];
                    }
                    else
                    {
                        Console.Write(bottom - top + 1);
                        Console.Write(" ");
                        had = true;
                        break;
                    }

                }
                if (!had)
                {
                    Console.Write(0);
                    Console.Write(" ");
                }

            }

        }

    }
    public class symbols
    {
        public static int[] FirstOccur = new int[5] { -1, -1, -1, -1, -1 };
        public static int[][] count = new int[5][];

        public static void Construct(string text)
        {
            for (int i = 0; i < 5; i++)
            {
                count[i] = new int[text.Length + 1];
            }
            int ind;
            for (int i = 0; i < text.Length; i++)
            {
                ind = IndexOf(text[i]);
                if (FirstOccur[ind] == -1)
                    FirstOccur[ind] = i;
                for (int j = 0; j < 5; j++)
                {
                    count[j][i + 1] = count[j][i];
                }
                count[ind][i + 1] = count[ind][i] + 1;
            }
        }

        public static int IndexOf(char c)
        {
            switch (c)
            {
                case '$':
                    return 0;
                case 'A':
                    return 1;
                case 'C':
                    return 2;
                case 'G':
                    return 3;
                default:
                    return 4;
            }

        }
    }
}
