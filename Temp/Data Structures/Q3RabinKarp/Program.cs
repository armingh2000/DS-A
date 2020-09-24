using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3RabinKarp
{
    class Program
    {
        static void Main(string[] args)
        {
            string p = Console.ReadLine();
            string t = Console.ReadLine();

            Solve(p, t).ToList().ForEach(x => Console.Write(x + " "));
        }


        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;
        public static long[] Solve(string pattern, string text)
        {
            List<long> occurrences = new List<long>();
            long pHash = Q2HashingWithChain.PolyHash(pattern, 0, pattern.Length, BigPrimeNumber, ChosenX);
            var H = PreComputeHashes(text, pattern.Length, BigPrimeNumber, ChosenX);
            for (long i = 0; i < text.Length - pattern.Length + 1; i++)
            {
                if (pHash != H[i])
                    continue;
                if (text.Substring((int)i, pattern.Length) == pattern)
                {
                    occurrences.Add(i);
                }
            }
            return occurrences.ToArray();
        }



        public static long[] PreComputeHashes(
            string T,
            int P,
            long p,
            long x)
        {
            long[] res = new long[T.Length - P + 1];
            string s = T.Substring(T.Length - P, P);

            res[T.Length - P] = Q2HashingWithChain.PolyHash(s, 0, (int)P, p, (int)x);

            long y = 1;

            for (long i = 0; i < P; i++)
            {
                y = (y * x) % p;
            }

            for (long i = T.Length - P - 1; i >= 0; i--)
            {
                res[i] = (x * res[i + 1] + T[(int)i] - y * T[(int)i + P]) % p;
                while (res[i] < 0)
                {
                    res[i] += p;
                }
            }

            return res;
        }

        public class Q2HashingWithChain
        {
            public List<string>[] hashTable;
            public string[] Solve(long bucketCount, string[] commands)
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

            public void Add(string str)
            {
                long hash = PolyHash(str, 0, str.Length) % hashTable.Length;
                if (!hashTable[hash].Contains(str))
                {
                    hashTable[hash].Add(str);
                }
            }

            public string Find(string str)
            {
                long hash = PolyHash(str, 0, str.Length) % hashTable.Length;
                if (hashTable[hash].Contains(str))
                {
                    return "yes";
                }
                return "no";
            }

            public void Delete(string str)
            {
                long hash = PolyHash(str, 0, str.Length) % hashTable.Length;
                if (hashTable[hash].Contains(str))
                {
                    hashTable[hash].Remove(str);
                }
            }

            public string Check(int i)
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
}
