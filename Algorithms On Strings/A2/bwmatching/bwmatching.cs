
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
            bool had;
            for(int i = 0; i < n; i++)
            {
                had = false;
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
                        had = true;
                        break;
                    }

                }
                if(!had)
                {
                    Console.Write(0);
                    Console.Write(" ");
                }
                
            }

        }

    }
    public class symbols
    {
        public static int[] FirstOccur = new int[5] { 0, -1, -1, -1, -1 };
        public static int[][] count = new int[5][];

        public static void Construct(string text)
        {
            List<char> temp = new List<char>();
            for(int i = 0; i < text.Length; i++)
            {
                temp.Add(text[i]);
            }
            temp.Sort();
            int seen = 0;
            int curr = 0;
            while((seen < 4) && (curr < text.Length))
            {
                if(FirstOccur[IndexOf(temp[curr])] == -1)
                {
                    FirstOccur[IndexOf(temp[curr])] = curr;
                    seen++;
                }
                curr++;
            }


            for(int i = 0; i < 5; i++)
            {
                count[i] = new int[text.Length + 1];
            }
            int ind;
            for(int i = 0; i < text.Length; i++)
            {
                ind = IndexOf(text[i]);
                for(int j = 0; j < 5; j++)
                {
                    count[j][i + 1] = count[j][i];
                }
                count[ind][i + 1] = count[ind][i] + 1;
            }
        }

        public static int IndexOf(char c)
        {
            switch(c)
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
