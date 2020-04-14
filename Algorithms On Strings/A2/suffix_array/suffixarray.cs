using System;
using System.Collections.Generic;
using System.Linq;

namespace suffixarray 
{
    public class Program
    {
        static void Main()
        {
            string text = Console.ReadLine();
            long[] ans = Solve(text);
            for(int i = 0; i < ans.Length; i++)
            {
                Console.Write(ans[i].ToString() + " ");
            }
        }


        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public static long[] Solve(string text)
        {
            List<suffix> sl = new List<suffix>();
            for(int i = 0; i < text.Length; i++)
            {
                sl.Add(new suffix(i, text.Substring(i)));
            }
            sl = sl.OrderBy(x => x.suf).ToList();
            long[] ans = new long[text.Length];
            for(int i = 0; i < sl.Count; i++)
            {
                ans[i] = sl[i].index;
            }
            return ans;
        }
    }
    public class suffix
    {
        public int index;
        public string suf;

        public suffix(int i, string s)
        {
            this.index = i;
            this.suf = s;
        }
    }
}
