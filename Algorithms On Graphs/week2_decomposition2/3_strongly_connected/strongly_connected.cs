using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace strongly_connected
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
            Console.WriteLine(Solve(nodeCount, edges));
        }

        static long SccNum;
        static bool[] visited;
        static List<long>[] adjs;
        static List<long>[] adjs2;
        static long[] TPSort;
        static long last;


        public static long Solve(long nodeCount, long[][] edges)
        {
            SccNum = 0;
            last = 0;
            visited = new bool[nodeCount];
            adjs = new List<long>[nodeCount];
            TPSort = new long[nodeCount];
            adjs2 = new List<long>[nodeCount];

            for (long i = 0; i < nodeCount; i++)
                adjs[i] = new List<long>();

            foreach (long[] edge in edges)
                adjs[edge[0] - 1].Add(edge[1] - 1);
            
            for (long i = 0; i < nodeCount; i++)
                adjs2[i] = new List<long>();

            foreach (long[] edge in edges)
                adjs2[edge[1] - 1].Add(edge[0] - 1);

            DfSPostOrder();

            visited = new bool[nodeCount];

            for (long i = nodeCount - 1; i >= 0; i--)
            {
                if (!visited[TPSort[i] - 1])
                {
                    Explore(TPSort[i] - 1);
                    SccNum++;
                }
            }
            return SccNum;
        }

        private static void DfSPostOrder()
        {
            for (long i = 0; i < visited.Length; i++)
                if (!visited[i])
                    Explore2(i);
        }

        private static void Explore2(long i)
        {
            visited[i] = true;
            foreach (long node2 in adjs2[i])
            {
                if (!visited[node2])
                    Explore2(node2);
            }
            TPSort[last] = i + 1;
            last++;
        }

        private static void Explore(long v)
        {
            visited[v] = true;
            foreach (long node2 in adjs[v])
                if (!visited[node2])
                    Explore(node2);
        }
    }
}
