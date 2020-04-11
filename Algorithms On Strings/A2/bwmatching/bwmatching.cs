
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
            cell3.n = 0;
            cell3.firstColumn = new List<cell3>();
            cell3.lastColumn = new List<cell3>();

            DeployCells(text);

            for(long i = 0; i < n; i++){
                Console.Write(findExactMatches(patterns[i]));
                Console.Write(" ");
            }

        }
        private static void DeployCells(string text)
        {
           for(int i = 0; i < text.Length; i++)
           {
                new cell3(i, text[i]);
           }
           cell3.firstColumn = cell3.lastColumn;
           cell3.firstColumn = cell3.firstColumn.OrderBy(x => x.character).ToList();
           for(int i = 0; i < text.Length; i++)
           {
                cell3.firstColumn[i].first_index = i;
           }
        }
        private static long findExactMatches(string pattern)
        {
            int ind = pattern.Length - 1;
            int top = cell3.firstColumn.FindIndex(0,x => x.character == pattern[ind]);
            if(top == -1)
                return 0;
            int bottom = cell3.firstColumn.FindIndex(top, x => x.character != pattern[ind]) - 1;
            if (bottom == -2)
                bottom = cell3.n - 1;
            int temp, temp2;
            for(ind = pattern.Length - 2; ind >= 0; ind--)
            {
                temp = top;
                top = cell3.lastColumn.FindIndex(top, bottom - top + 1, x => x.character == pattern[ind]);
                if(top == -1)
                    return 0;
                temp2 = bottom;
                bottom = FindLast(cell3.lastColumn, pattern[ind], top, bottom);
                top = cell3.lastColumn[top].first_index;
                bottom = cell3.lastColumn[bottom].first_index;
            }
            return bottom - top + 1;
        }

        private static int FindLast(List<cell3> lastColumn, char v, int top, int bottom)
        {
            for(int i = bottom; i >= top; i--)
            {
                if (lastColumn[i].character == v)
                    return i;
            }
            return top;
        }
    }
    public class cell3{
        public static List<cell3> firstColumn;
        public static List<cell3> lastColumn;
        public static int n;
        public int first_index;
        public int last_index;
        public char character;

        public cell3(int li, char c)
        {
            this.last_index = li;
            this.character = c;
            lastColumn.Add(this);
            n++; 
        }

    }
}
