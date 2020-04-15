using System;
using System.Collections.Generic;

namespace acyclicity
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
                edges[i] = new long[2];
                line = Console.ReadLine();
                edges[i][0] = long.Parse(line.Split()[0]);
                edges[i][1] = long.Parse(line.Split()[1]);

            }
            Console.WriteLine(Solve(nodeCount, edges));
        }

        static List<long>[] adjs;
        static long answer;
        static bool[] visited;

        public static long Solve(long nodeCount, long[][] edges)
        {
            adjs = new List<long>[nodeCount];
            answer = 0;

            for (long i = 0; i < nodeCount; i++)
                adjs[i] = new List<long>();

            foreach(long[] edge in edges)
                adjs[edge[0] - 1].Add(edge[1] - 1);

            for (long i = 0; i < nodeCount; i++)
            {
                visited = new bool[nodeCount];
                Explore(i);
            }

            return answer;
        }

        private static void Explore(long i)
        {
            if (answer == 0)
            {
                visited[i] = true;
                foreach (long node2 in adjs[i]){
                    if (!visited[node2])
                        Explore(node2);
                    else if(adjs[node2].Count > 0)
                    {
                        answer = 1;
                        return;
                    }
                }
            }
        }
    }
}
