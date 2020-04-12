
using System;
using System.Collections.Generic;
using System.Linq;

namespace bwm
{
    public class Program 
    {
        static void Main()
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
            for(int i = 0; i < n; i++)
            {
                curr = patterns[i].Length - 1;
                bottom = len - 1;
                top = 0;
                while(top <= bottom)
                {
                    if(curr >= 0)
                    {
                        sym = patterns[i][curr];
                        curr--;
                        ind = symbols.IndexOf(sym);
                        top = symbols.FirstOccur[ind] + symbols.count[ind][top];
                        bottom = symbols.FirstOccur[ind] + symbols.count[ind][bottom + 1] - 1;
                    }
                    else
                    {
                        Console.Write(bottom - top + 1 );
                        Console.Write(" ");
                    }

                }
            }

        }

    }
    public class symbols
    {
        public static int[] FirstOccur = new int[4] { -1, -1, -1, -1 };
        public static int[][] count = new int[4][];

        public static void Construct(string text)
        {
            for(int i = 0; i < 4; i++)
            {
                count[i] = new int[text.Length + 1];
            }
            for(int i = 0; i < text.Length; i++)
            {
                int ind = IndexOf(text[i]);
                if(FirstOccur[ind] == -1) 
                    FirstOccur[ind] = i;
                count[ind][i + 1]++;
            }
        }

        public static int IndexOf(char c)
        {
            switch(c)
            {
                case 'a':
                    return 0;
                case 'c':
                    return 1;
                case 'g':
                    return 2;
                default:
                    return 3;
            }

        }
    }
}
