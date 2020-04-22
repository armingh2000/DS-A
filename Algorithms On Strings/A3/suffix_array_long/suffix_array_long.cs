using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suffix_array_long
{
    public class Program
    {
        static void Main()
        {
            string text = Console.ReadLine();
            long[] ans = Solve(text);

            for(int i = 0; i < text.Length; i++)
            {
                Console.Write(ans[i].ToString() + " ");
            }
        }

        public static long[] Solve(string text)
        {
            // write your code here        
            long[] order = SortCharacters(text);
            long[] classes = ComputeCharClasses(text, order);
            int l = 1;

            while(l < text.Length)
            {
                order = SortedDouble(text, l, order, classes);
                classes = UpdateClasses(order, classes, l);
                l *= 2;
            }
            return order;
        }

        public static long[] UpdateClasses(long[] newOrder, long[] classes, int l)
        {
            int n = newOrder.Length;
            long[] newClass = new long[n];
            newClass[newOrder[0]] = 0;
            long cur, prev, mid, midPrev;

            for(int i = 1; i < n; i++)
            {
                cur = newOrder[i];
                prev = newOrder[i - 1];
                mid = (cur + (long)l) % (long)n;
                midPrev = (prev + (long)l) % (long)n;
                if((classes[cur] != classes[prev]) || (classes[mid] != classes[midPrev]))
                    newClass[cur] = newClass[prev] + 1;
                else
                    newClass[cur] = newClass[prev];
            }
            return newClass;
        }

        public static long[] SortedDouble(string s, int l, long[] order, long[] classes)
        {
            long[] count = new long[s.Length];
            long[] newOrder = new long[s.Length];

            for(int i = 0; i < s.Length; i++)
                count[classes[i]]++;

            for(int j = 1; j < s.Length; j++)
                count[j] += count[j - 1];

            long start, cl;
            for(int i = s.Length - 1; i >= 0; i--)
            {
                start = (order[i] - l + s.Length) % (s.Length);
                cl = classes[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }

        public static long[] SortCharacters(string S)
        {
            long[] order = new long[S.Length];
            long[] count = new long[5];
            for(int i = 0; i < S.Length; i++)
            {
                count[IndexOf(S[i])]++;
            }

            for(int j = 1; j < count.Length; j++)
            {
                count[j] += count[j - 1];
            }

            int ind;

            for(int i = S.Length - 1; i >= 0; i--)
            {
                ind = IndexOf(S[i]);
                count[ind]--;
                order[count[ind]] = i;
            }

            return order;
        }

        public static long[] ComputeCharClasses(string s, long[] order)
        {
            long[] classes = new long[s.Length];
            
            for(int i = 1; i < s.Length; i++)
            {
                if(s[(int)order[i]] != s[(int)order[i - 1]])
                    classes[order[i]] = classes[order[i - 1]] + 1;
                else
                    classes[order[i]] = classes[order[i - 1]];
            }

            return classes;
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
