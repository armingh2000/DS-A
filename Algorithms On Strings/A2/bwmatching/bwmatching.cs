
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

        }

    }
    public class symbols
    {
        static int[] FirstOccur = new int[4] { -1, -1, -1, -1 };
        static int[][] count = new int[4][];

        public void Construct(string text)
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

        public int IndexOf(char c)
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
