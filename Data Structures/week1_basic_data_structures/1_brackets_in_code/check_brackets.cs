using System;
using System.Collections.Generic;
using System.Text;

namespace check_brackets
{
    public class Program
    {
        static void Main()
        {
            string str = Console.ReadLine();
            long s = Solve(str);
            if(s == -1)
                Console.WriteLine("Success");
            else
                Console.WriteLine(s);
        }

        public static long Solve(string str)
        {
            Stack<char> char_stck = new Stack<char>();
            Stack<int> index_stck = new Stack<int>();
            List<char> open_brackets = new List<char>() { '{', '[', '(' };
            List<char> close_brackets = new List<char>() { '}', ']', ')' };
            for (int i = 0; i < str.Length; i++) 
            {
                if (open_brackets.Contains(str[i]))
                {
                    char_stck.Push(str[i]);
                    index_stck.Push(i);
                }
                else if (close_brackets.Contains(str[i]))
                {
                    if (char_stck.Count != 0)
                    {
                        if (str[i] == close_brackets[open_brackets.IndexOf(char_stck.Peek())])
                        {
                            char_stck.Pop();
                            index_stck.Pop();
                        }
                        else
                            return i + 1;
                    }
                    else
                    {
                        return i + 1;
                    }
                }
            }
            if (char_stck.Count == 0)
                return -1;
            else
                return index_stck.Pop() + 1;
        }
    }
}
