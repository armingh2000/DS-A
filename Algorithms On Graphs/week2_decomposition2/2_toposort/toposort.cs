using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace toposort 
{
    public class Program
    {
        static void Main()
        {
            string firstLine = Console.ReadLine();
            long nodeCount = long.Parse(firstLine.Split()[0]);
            long edgeCount = long.Parse(firstLine.Split()[1]);
            
            long[][] edges = new long[edgeCount][];
            string line;

            for(long i = 0; i < edgeCount; i++)
            {
                line = Console.ReadLine();
                edges[i] = new long[2];
                edges[i][0] = long.Parse(line.Split()[0]);
                edges[i][1] = long.Parse(line.Split()[1]);
            }

            long[] ans = Solve(nodeCount, edges);
            for(long i = 0; i < ans.Length; i++)
            {
                Console.Write(ans[i].ToString() + " ");
            }
        }


        static long[] TPSort;
        static List<long>[] adjs;
        static bool[] visited;
        static long last;

        public static long[] Solve(long nodeCount, long[][] edges)
        {
            TPSort = new long[nodeCount];
            adjs = new List<long>[nodeCount];
            visited = new bool[nodeCount];
            last = 0;
            
            for (long i = 0; i < nodeCount; i++)
                adjs[i] = new List<long>();

            foreach (long[] edge in edges)
                adjs[edge[0] - 1].Add(edge[1] - 1);

            DfSPostOrder();
            
            return TPSort.Reverse().ToArray();
        }

        private static void DfSPostOrder()
        {
            for (long i = 0; i < visited.Length; i++)
                if (!visited[i])
                    Explore(i);
        }

        private static void Explore(long i)
        {
            visited[i] = true;
            foreach(long node2 in adjs[i])
            {
                if (!visited[node2])
                    Explore(node2);
            }
            TPSort[last] = i + 1;
            last++;
        }
    }
}

