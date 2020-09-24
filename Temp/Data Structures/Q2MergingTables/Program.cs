using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2MergingTables
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();

            long n = long.Parse(line.Split()[0]);
            long m = long.Parse(line.Split()[1]);


            var v = Console.ReadLine().Split();

            long[] tableSizes = new long[n];
            long[] targetTables = new long[m];
            long[] sourceTables = new long[m];


            for (long i=0;i<n;i++)
            {
                tableSizes[i] = long.Parse(v[i]);
            }

            for (long i = 0; i < m; i++)
            {
                line = Console.ReadLine();

                targetTables[i] = long.Parse(line.Split()[0]);
                sourceTables[i] = long.Parse(line.Split()[1]);
            }


            Solve(tableSizes, targetTables, sourceTables).ToList().ForEach(x => Console.WriteLine(x));
        }


        public static long[] Solve(long[] tableSizes, long[] targetTables, long[] sourceTables)
        {
            long[] res = new long[targetTables.Length];
            long[] Parent = new long[tableSizes.Length];
            long[] Rank = new long[tableSizes.Length];
            long max = 0;


            for (long i = 0; i < tableSizes.Length; i++)
            {
                Parent[i] = i;
                Rank[i] = tableSizes[i];
                if (max < Rank[i])
                {
                    max = Rank[i];
                }
            }

            for (long i = 0; i < targetTables.Length; i++)
            {

                Union(Parent, Rank, targetTables[i] - 1, sourceTables[i] - 1, ref max);
                res[i] = max;
            }
            return res;
        }

        public static void Union(long[] parent, long[] rank, long i, long j, ref long max)
        {
            long i_ID = Find(parent, i);
            long j_ID = Find(parent, j);

            if (i_ID == j_ID)
                return;

            if (rank[i_ID] > rank[j_ID])
            {
                parent[j_ID] = i_ID;
                rank[i_ID] = rank[i_ID] + rank[j_ID];
                if (rank[i_ID] > max)
                {
                    max = rank[i_ID];
                }
            }
            else
            {
                parent[i_ID] = j_ID;
                rank[j_ID] = rank[i_ID] + rank[j_ID];
                if (rank[j_ID] > max)
                {
                    max = rank[j_ID];
                }
            }

        }

        public static long Find(long[] parent, long i)
        {
            while (parent[i] != i)
            {
                i = parent[i];
            }
            return i;
        }
    }
}
