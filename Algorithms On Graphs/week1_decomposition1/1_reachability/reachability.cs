using System;
using System.Collections.Generic;

namespace A1
{
    public class Program
    {
        static void Main()
        {
            string firstLine = Console.ReadLine();
            long nodeCount = long.Parse(firstLine[0]);
            long edgeCount = long.Parse(firstLine[1]);

            string line;
            long[][] edges = new long[edgeCount][];
            for(long i = 0; i < edgeCount; i++){
                line = Console.ReadLine();
                edges[i][0] = long.Parse(line[0]);
                edges[i][1] = long.Parse(line[1]);
            }
            line = Console.ReadLine();
            Console.WriteLine(Solve(nodeCount, edges, long.Parse(line[0]), long.Parse(line[1])));
        }
        static List<long>[] adjs;
        static bool[] visited;
        static long endN;

        public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            endN = EndNode - 1;
            adjs = new List<long>[nodeCount];
            visited = new bool[nodeCount];

            for (long i = 0; i < nodeCount; i++)
                adjs[i] = new List<long>();

            for (long i = 0; i < edges.Length; i++)
            {
                adjs[edges[i][0] - 1].Add(edges[i][1] - 1);
                adjs[edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            Explore(StartNode - 1);
            if (visited[EndNoade - 1])
                return 1;
            return 0;
        }

        private static void Explore(long v)
        {
            if (visited[endN])
                return;
            visited[v] = true;
            foreach(long node2 in adjs[v])
            {
                if (!visited[node2])
                    Explore(node2);
            }
        }
    }
}

