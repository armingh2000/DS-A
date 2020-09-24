using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW4_Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            long nodeCount, edgeCount;
            string[] fLine = Console.ReadLine().Split();
            nodeCount = long.Parse(fLine[0]);
            edgeCount = long.Parse(fLine[1]);
            long[][] edges = new long[edgeCount][];
            string[] line;
            for (long i=0;i<edgeCount;i++)
            {
                line = Console.ReadLine().Split();
                edges[i] = new long[3];
                edges[i][0] = long.Parse(line[0]);
                edges[i][1] = long.Parse(line[1]);
                edges[i][2] = long.Parse(line[2]);
            }
            var res = Solve(nodeCount, edges);
            Console.WriteLine(res.Item1);
            if(res.Item1!=-1)
            {
                res.Item2.ToList().ForEach(x => Console.Write(x + " "));
            }

        }

        public static Tuple<long, long[]> Solve(long nodeCount, long[][] edges)
        {
            long?[,] edgeArr = new long?[nodeCount, nodeCount];

            for (long i = 0; i < edges.Length; i++)
            {
                edgeArr[edges[i][0] - 1, edges[i][1] - 1] = edgeArr[edges[i][1] - 1, edges[i][0] - 1] = edges[i][2];
            }

            long upPow2 = (long)Math.Pow(2, nodeCount);
            long?[,] xParents = new long?[upPow2, nodeCount];
            long?[,] yParents = new long?[upPow2, nodeCount];
            
            var C = TSP3(edgeArr, nodeCount, ref xParents, ref yParents);

            long min = long.MaxValue;
            List<long> res = new List<long>();
            
            long xBest = -1, yBest = -1;

            long m = C.GetLength(0) - 1;
            for (long i = 0; i < nodeCount; i++)
            {
                if (C[m, i] != long.MaxValue && edgeArr[i, 0].HasValue && C[m, i] + edgeArr[i, 0].Value < min)
                {
                    min = C[m, i] + edgeArr[i, 0].Value;
                    xBest = m;
                    yBest = i;
                }
            }

            if ((xBest == -1) || (yBest == -1))
            {
                return new Tuple<long, long[]>(-1, new long[0]);
            }
            
            long temp;
            while (xParents[xBest, yBest].HasValue && yParents[xBest, yBest].HasValue)
            {
                res.Add(yBest + 1);
                temp = xBest;
                xBest = xParents[xBest, yBest].Value;
                yBest = yParents[temp, yBest].Value;
            }
            res.Reverse();
            
            if (res.Count == 0)
            {
                return new Tuple<long, long[]>(-1, res.ToArray());
            }

            bool[] isUsed = new bool[nodeCount];
            
            for (long i = 0; i < res.Count; i++)
            {
                isUsed[res[(int)i] - 1] = true;
            }

            for (long i = 0; i < res.Count; i++)
            {
                if (!isUsed[i])
                {
                    res.Add(i + 1);
                }
            }

            return new Tuple<long, long[]>(min, res.ToArray());
        }
        
        public static long[,] TSP3(long?[,] edges, long n, ref long?[,] xParents, ref long?[,] yParents)
        {
            long temp1;
            long upperBound = (long)Math.Pow(2, n);
            long[,] C = new long[upperBound, n];
            
            for (long i = 0; i < upperBound; i++)
            {
                for (long j = 0; j < n; j++)
                {
                    C[i, j] = long.MaxValue;
                }
            }

            long[] AllSubsets = CreateSubsetsLong(n);
            long[] CountOfOne = CreateCountOfOneArray(AllSubsets);

            for (long i = 0; i < n; i++)
            {
                C[1, i] = 0;
            }

            for (long s = 2; s <= n; s++)
            {
                long[] subs = CreateSubsetsWith_m_Member_also1IsMember2(n, s, AllSubsets, CountOfOne);

                foreach (var p in subs)
                {
                    C[p, 0] = long.MaxValue;
                    long[] f = NonZeroIndices(p);

                    foreach (var i in f)
                    {
                        if (i != 0)
                        {
                            foreach (var j in f)
                            {
                                if (i != j && edges[i, j].HasValue)
                                {
                                    temp1 = GetFilped(p, i);
                                    if (C[temp1, j] != long.MaxValue)
                                    {
                                        if (C[temp1, j] + edges[i, j].Value <= C[p, i])
                                        {
                                            C[p, i] = C[temp1, j] + edges[i, j].Value;
                                            xParents[p, i] = temp1;
                                            yParents[p, i] = j;
                                        }
                                    }
                                }


                            }
                        }
                    }
                }

            }

            return C;
        }

        public static long[] CreateSubsetsWith_m_Member_also1IsMember2(long n, long s, long[] allSubsets, long[] countOfOne)
        {
            List<long> res = new List<long>();
            for (long i = 0; i < allSubsets.Length; i++)
            {
                if (countOfOne[i] == s && (allSubsets[i] & 1) == 1)
                {
                    res.Add(allSubsets[i]);
                }
            }
            return res.ToArray();
        }

        public static long[] CreateCountOfOneArray(long[] allSubsets)
        {
            long[] res = new long[allSubsets.Length];
            for (long i = 0; i < allSubsets.Length; i++)
            {
                res[i] = CountOfOne(allSubsets[i]);
            }
            return res;
        }

        private static long[] CreateSubsetsLong(long n)
        {
            long temp = 0;
            long upperBound = (long)Math.Pow(2, n);
            long[] result = new long[upperBound];
            for (long i = 0; i < upperBound - 1; i++)
            {
                result[i] = temp;
                temp++;
            }
            result[result.Length - 1] = temp;
            return result;
        }
        
        public static long[,] TSP(long?[,] edges, long n, ref Parent[,] parents)
        {
            long[,] C = new long[(long)Math.Pow(2, n), n];
            int[] temp1;
            long temp2;
            long upperBound = (long)Math.Pow(2, n);

            for (long i = 0; i < upperBound; i++)
            {
                for (long j = 0; j < n; j++)
                {
                    C[i, j] = long.MaxValue;
                    parents[i, j] = new Parent();
                }
            }

            int[][] AllSubsets = CreateSubsets2(n);
            long[] CountOfOne = CreateCountOfOneArray(AllSubsets);

            for (long i = 0; i < n; i++)
            {
                C[1, i] = 0;
            }

            for (long s = 2; s <= n; s++)
            {
                int[][] subs = CreateSubsetsWith_m_Member_also1IsMember2(n, s, AllSubsets, CountOfOne);

                foreach (var p in subs)
                {
                    C[GetIndex(p), 0] = long.MaxValue;
                    long[] f = NonZeroIndices(p);

                    foreach (var i in f)
                    {
                        if (i != 0)
                        {
                            foreach (var j in f)
                            {
                                if (i != j && edges[i, j].HasValue)
                                {
                                    temp1 = GetFilped(p, i);
                                    temp2 = GetIndex(temp1);
                                    if (C[temp2, j] != long.MaxValue)
                                    {
                                        if (C[temp2, j] + edges[i, j].Value <= C[GetIndex(p), i])
                                        {
                                            C[GetIndex(p), i] = C[temp2, j] + edges[i, j].Value;
                                            parents[GetIndex(p), i].X = temp2;
                                            parents[GetIndex(p), i].Y = j;
                                        }
                                    }
                                }


                            }
                        }
                    }
                }

            }

            return C;
        }
        
        public static long[,] TSP(long?[,] edges, long n, ref long?[,] xParents, ref long?[,] yParents)
        {
            int[] temp1;
            long temp2;
            long temp3;
            long upperBound = (long)Math.Pow(2, n);
            long[,] C = new long[upperBound, n];
            
            for (long i = 0; i < upperBound; i++)
            {
                for (long j = 0; j < n; j++)
                {
                    C[i, j] = long.MaxValue;
                }
            }

            int[][] AllSubsets = CreateSubsets2(n);
            long[] CountOfOne = CreateCountOfOneArray(AllSubsets);

            for (long i = 0; i < n; i++)
            {
                C[1, i] = 0;
            }

            for (long s = 2; s <= n; s++)
            {
                int[][] subs = CreateSubsetsWith_m_Member_also1IsMember2(n, s, AllSubsets, CountOfOne);

                foreach (var p in subs)
                {
                    temp3 = GetIndex(p);
                    C[temp3, 0] = long.MaxValue;
                    long[] f = NonZeroIndices(p);

                    foreach (var i in f)
                    {
                        if (i != 0)
                        {
                            foreach (var j in f)
                            {
                                if (i != j && edges[i, j].HasValue)
                                {
                                    temp1 = GetFilped(p, i);
                                    temp2 = GetIndex(temp1);
                                    if (C[temp2, j] != long.MaxValue)
                                    {
                                        if (C[temp2, j] + edges[i, j].Value <= C[temp3, i])
                                        {
                                            C[temp3, i] = C[temp2, j] + edges[i, j].Value;
                                            xParents[temp3, i] = temp2;
                                            yParents[temp3, i] = j;
                                        }
                                    }
                                }


                            }
                        }
                    }
                }

            }

            return C;
        }

        private static long[] GetIndexOfSubsetsWith_m_Member_also1IsMember(long n, long s, int[][] allSubsets, long[] countOfOne)
        {
            List<long> res = new List<long>();
            for (long i = 0; i < allSubsets.Length; i++)
            {
                if (countOfOne[i] == s && allSubsets[i][0] == 1)
                {
                    res.Add(i);

                }
            }
            return res.ToArray();
        }

        public static long[] CreateCountOfOneArray(int[][] allSubsets)
        {
            long[] res = new long[allSubsets.Length];
            for (long i = 0; i < allSubsets.Length; i++)
            {
                res[i] = CountOfOne(allSubsets[i]);
            }
            return res;
        }

        public static void Copy(int[] res, int[] temp)
        {
            for (long i = 0; i < temp.Length; i++)
            {
                res[i] = temp[i];
            }
        }
        
        public static long GetFilped(long p, long i)
        {
            return (p ^ (long)Math.Pow(2, i));
        }

        public static int[] GetFilped(int[] num, long i)
        {
            int[] res = (int[])(num.Clone());

            if (num[i] == 0)
                res[i] = 1;
            else
                res[i] = 0;
            
            return res;
        }

        public static long[] NonZeroIndices(int[] num)
        {
            List<long> res = new List<long>();
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] != 0)
                    res.Add(i);
            }
            return res.ToArray();
        }

        public static long GetIndex(int[] num)
        {
            long res = 0;
            long temp = 1;

            for (long i = 0; i < num.Length; i++)
            {
                if (num[i] == 1)
                    res += temp;
                temp *= 2;
            }

            return res;
        }

        public static int[][] CreateSubsets2(long n)
        {
            int[] temp = new int[n];
            long upperBound = (long)Math.Pow(2, n);
            int[][] result = new int[upperBound][];
            for (long i = 0; i < upperBound - 1; i++)
            {
                result[i] = new int[n];
                Copy(result, temp, i);
                temp = IncreaseByOne(temp);
            }
            result[result.Length - 1] = temp;
            return result;
        }

        public static void Copy(int[][] res, int[] temp, long i)
        {
            for (long j = 0; j < temp.Length; j++)
            {
                res[i][j] = temp[j];
            }
        }

        public static int[] IncreaseByOne(int[] num)
        {
            num[0]++;
            int t = 0;
            while (num[t] > 1)
            {
                num[t] -= 2;
                num[t + 1]++;
                t++;
            }

            return num;
        }

        public static long CountOfOne(int[] num)
        {
            long res = 0;

            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] == 1)
                    res++;
            }

            return res;
        }

        public static int[][] CreateSubsetsWith_m_Member_also1IsMember2(long n, long m, int[][] initial)
        {
            List<int[]> res = new List<int[]>();
            for (long i = 0; i < initial.Length; i++)
            {
                if (CountOfOne(initial[i]) == m && initial[i][0] == 1)
                {
                    res.Add(initial[i]);
                }
            }
            return res.ToArray();
        }

        public static int[][] CreateSubsetsWith_m_Member_also1IsMember2(long n, long m, int[][] initial, long[] count)
        {
            List<int[]> res = new List<int[]>();
            for (long i = 0; i < initial.Length; i++)
            {
                if (count[i] == m && initial[i][0] == 1)
                {
                    res.Add(initial[i]);
                }
            }
            return res.ToArray();
        }
        
        public static long[] NonZeroIndices(long num)
        {
            List<long> res = new List<long>();
            long temp = 1;
            long t = 0;

            while (temp < num)
            {
                if ((num & temp) == temp)
                {
                    res.Add(t);
                }
                temp *= 2;
                t++;
            }

            return res.ToArray();
        }

        public static long GetIndex(long bin)
        {
            return Convert.ToInt64(bin.ToString(), 2);
        }

        public static long[] CreateSubsets(long n)
        {
            long temp = 0;
            long[] result = new long[(long)Math.Pow(2, n)];
            for (long i = 0; i < Math.Pow(2, n); i++)
            {
                result[i] = temp;
                temp = IncreaseByOne(temp);
            }
            return result;
        }

        public static long IncreaseByOne(long binary)
        {
            binary++;
            long tTen = 10;
            while (((binary % (tTen)) / (tTen / 10)) % 2 == 0)
            {
                binary = binary - (2 * tTen / 10) + tTen;
                tTen *= 10;
            }
            return binary;
        }

        public static long CountOfOne(long binary)
        {
            long res = 0;
            long temp = 1;

            while (temp < binary)
            {
                if ((temp & binary) == temp)
                {
                    res++;
                }
                temp *= 2;

            }

            return res;
        }

        public static long[] CreateSubsetsWith_m_Member(long n, long m)
        {
            long[] initial = CreateSubsets(n);
            List<long> res = new List<long>();
            for (long i = 0; i < initial.Length; i++)
            {
                if (CountOfOne(initial[i]) == m)
                {
                    res.Add(initial[i]);
                }
            }
            res.Reverse();
            return res.ToArray();
        }

        public static long[] CreateSubsetsWith_m_Member_also1IsMember(long n, long m, long[] initial)
        {
            List<long> res = new List<long>();
            for (long i = 0; i < initial.Length; i++)
            {
                if (CountOfOne(initial[i]) == m && initial[i] % 10 == 1)
                {
                    res.Add(initial[i]);
                }
            }
            return res.ToArray();
        }

        public class Parent
        {
            public long? X { get; set; }
            public long? Y { get; set; }
        }
        
    }
}
