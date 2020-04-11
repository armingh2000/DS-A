
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A1
{
    public class ConnectedComponents 
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

        static List<long>[] adjs;
        static bool[] visited;
        static long CCnum;

        public static long Solve(long nodeCount, long[][] edges)
        {
            adjs = new List<long>[nodeCount];
            visited = new bool[nodeCount];
            CCnum = 0;

            for (long i = 0; i < nodeCount; i++)
                adjs[i] = new List<long>();

            for (long i = 0; i < edges.Length; i++)
            {
                adjs[edges[i][0] - 1].Add(edges[i][1] - 1);
                adjs[edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            for (long i = 0; i < nodeCount; i++)
                if (!visited[i])
                {
                    Explore(i);
                    CCnum++;
                }
            return CCnum;
        }

        private static void Explore(long i)
        {
            visited[i] = true;
            foreach (long node2 in adjs[i])
                if (!visited[node2])
                    Explore(node2);
        }
    }
}
