using System;
using System.Collections.Generic;

namespace bipartite
{
    public class Program
    {
        static void Main()
        {
            string firstLine = Console.ReadLine();
            long NodeCount = long.Parse(firstLine.Split()[0]);
            long EdgeCount = long.Parse(firstLine.Split()[1]);

            string line;
            long[][] edges = new long[EdgeCount][];

            for(long i = 0; i < EdgeCount; i++)
            {
                line = Console.ReadLine();
                edges[i] = new long[2];
                edges[i][0] = long.Parse(line.Split()[0]);
                edges[i][1] = long.Parse(line.Split()[1]);
            }
            Console.Write(Solve(NodeCount, edges));
        }

        static List<long>[] graph;
        static bool[] visited;
        static int[] color;

        public static long Solve(long NodeCount, long[][] edges)
        {
            graph = new List<long>[NodeCount];
            for (long i = 0; i < NodeCount; i++)
                graph[i] = new List<long>();
            visited = new bool[NodeCount];
            color = new int[NodeCount];

            foreach (long[] edge in edges)
            {
                graph[edge[0] - 1].Add(edge[1] - 1);
                graph[edge[1] - 1].Add(edge[0] - 1);
            }
            return Bipart();
        }

        private static long Bipart()
        {
            List<long> must_see = new List<long>();
            must_see.Add(0);

            long current;
            visited[0] = true;
            long size;
            int current_color = 0;

            while (must_see.Count > 0)
            {
                size = must_see.Count;
                for (long i = 0; i < size; i++)
                {
                    current = must_see[0];
                    must_see.RemoveAt(0);
                    foreach (long node2 in graph[current])
                    {
                        if (!visited[node2])
                        {
                            must_see.Add(node2);
                            visited[node2] = true;
                            color[node2] = (current_color + 1) % 2;
                        }
                        else
                        {
                            if (color[node2] == current_color)
                                return 0;
                        }
                    }
                    color[current] = current_color;
                }
                current_color++;
                current_color %= 2;
            }
            return 1;
        }
    }
}
