using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2HashingWithChain
{
    class Program
    {
        static void Main(string[] args)
        {
            long bucketCount = long.Parse(Console.ReadLine());
            long queriesCount = long.Parse(Console.ReadLine());
            string[] commands = new string[queriesCount];
            for(long i=0;i<queriesCount;i++)
            {
                commands[i] = Console.ReadLine();
            }

            Solve(bucketCount, commands).ToList().ForEach(x => Console.WriteLine(x));
        }

        public static List<string>[] hashTable;
        public static string[] Solve(long bucketCount, string[] commands)
        {
            hashTable = new List<string>[bucketCount];
            for (int i = 0; i < bucketCount; i++)
            {
                hashTable[i] = new List<string>();
            }

            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];

                switch (cmdType)
                {
                    case "add":
                        Add(arg);
                        break;
                    case "del":
                        Delete(arg);
                        break;
                    case "find":
                        result.Add(Find(arg));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str,
            int start,
            int count,
            long p = BigPrimeNumber,
            long x = ChosenX)
        {
            long hash = 0;
            for (int i = count - 1; i >= 0; i--)
            {
                hash = ((hash * x) + str[i + start]) % p;
            }

            return hash;
        }

        public static void Add(string str)
        {
            long hash = PolyHash(str, 0, str.Length) % hashTable.Length;
            if (!hashTable[hash].Contains(str))
            {
                hashTable[hash].Add(str);
            }
        }

        public static string Find(string str)
        {
            long hash = PolyHash(str, 0, str.Length) % hashTable.Length;
            if (hashTable[hash].Contains(str))
            {
                return "yes";
            }
            return "no";
        }

        public static void Delete(string str)
        {
            long hash = PolyHash(str, 0, str.Length) % hashTable.Length;
            if (hashTable[hash].Contains(str))
            {
                hashTable[hash].Remove(str);
            }
        }

        public static string Check(int i)
        {
            string res = null;

            if (hashTable[i].Count != 0)
            {
                for (int j = hashTable[i].Count - 1; j >= 0; j--)
                {
                    res += (hashTable[i][j]);
                    if (j != 0)
                        res += " ";
                }
            }
            if (res != null)
            {
                return res;
            }
            return "-";
        }

    }
}
