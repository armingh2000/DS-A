using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmp
{
    public class Program
    {

        static void Main()
        {
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();
            Solve(text, pattern);
        }

        public static void Solve(string text, string pattern)
        {
            // write your code here
            string s = pattern + "$" + text;
            int[] prefix_array = ComputePrefixArray(s);
            for(int i = 0; i < prefix_array.Length; i++)
            {
                if(prefix_array[i] == pattern.Length)
                {
                    Console.Write((i - 2 * pattern.Length).ToString() + " ");
                }
            }
        }
        
        public static int[] ComputePrefixArray(string p)
        {
            int[] s = new int[p.Length];
            s[0] = 0;
            int border = 0;

            for(int i = 1; i < p.Length; i++)
            {
                while((border > 0) && (p[i] != p[border]))
                    border = s[border - 1];
                if(p[i] == p[border])
                    border++;
                else
                    border = 0;
                s[i] = border;
            }
            return s;
        }
    }
}

