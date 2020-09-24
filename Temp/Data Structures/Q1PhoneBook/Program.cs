using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            string[] commands = new string[n];
            for (long i=0;i<n;i++)
            {
                commands[i] = Console.ReadLine();
            }

            Solve(commands).ToList().ForEach(x => Console.WriteLine(x));

        }

        public static Dictionary<int, string> PhoneBookList;


        public static string[] Solve(string[] commands)
        {
            PhoneBookList = new Dictionary<int, string>();

            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var args = toks.Skip(1).ToArray();
                int number = int.Parse(args[0]);
                switch (cmdType)
                {
                    case "add":
                        Add(args[1], number);
                        break;
                    case "del":
                        Delete(number);
                        break;
                    case "find":
                        result.Add(Find(number));
                        break;
                }
            }
            return result.ToArray();
        }

        public static void Add(string name, int number)
        {
            //if (!PhoneBookList.ContainsKey(number))
            //{
            //    PhoneBookList.Add(number, name);
            //}
            PhoneBookList[number] = name;

        }

        public static string Find(int number)
        {
            if (PhoneBookList.ContainsKey(number))
            {
                return PhoneBookList[number];
            }
            return "not found";
        }

        public static void Delete(int number)
        {
            //if (PhoneBookList.ContainsKey(number))
            //{
            PhoneBookList.Remove(number);
            //}
        }
    }
}
