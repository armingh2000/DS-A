using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1CheckBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            var output = Solve(input);
            if (output == -1)
                Console.WriteLine("Success");
            else
                Console.WriteLine(output);
        }

        public static long Solve(string str)
        {
            int n = str.Length;
            Stack s = new Stack();
            s.Size = -1;
            //s.Property=new List<char>
            for (int i = 0; i < n; i++)
            {
                if (str[i] == '[' || str[i] == '{' || str[i] == '(')
                {
                    s.Property.Add(str[i]);
                    s.Index.Add(i);
                    s.Size++;
                }
                else if (str[i] == ']')
                {
                    if (s.Size >= 0 && s.Property[s.Size] == '[')
                    {
                        s.Property.RemoveAt(s.Size);
                        s.Index.RemoveAt(s.Size);
                        s.Size--;
                    }
                    else
                    {
                        return i + 1;
                    }
                }
                else if (str[i] == '}')
                {
                    if (s.Size >= 0 && s.Property[s.Size] == '{')
                    {
                        s.Property.RemoveAt(s.Size);
                        s.Index.RemoveAt(s.Size);
                        s.Size--;
                    }
                    else
                    {
                        return i + 1;
                    }
                }
                else if (str[i] == ')')
                {
                    if (s.Size >= 0 && s.Property[s.Size] == '(')
                    {
                        s.Property.RemoveAt(s.Size);
                        s.Index.RemoveAt(s.Size);
                        s.Size--;
                    }
                    else
                    {
                        return i + 1;
                    }
                }
            }
            if (s.Size == -1)
                return -1;
            else
                return s.Index[0] + 1;
        }

        public class Stack
        {
            public int Size { get; set; }
            public List<char> Property = new List<char>();
            public List<int> Index = new List<int>();

        }
    }
}
