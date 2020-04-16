using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace bfs
{
    public class Program
    {
        static void Main()
        {
            string firstLine = Console.ReadLine();
            long NodeCount = long.Parse(firstLine.Split()[0]);
            long EdgeCount = long.Parse(firstLine.Split()[1]);
            long[][] edges = new long[EdgeCount][];
            string line;

            for(long i = 0; i < EdgeCount; i++)
            {
                line = Console.ReadLine();
                edges[i] = new long[2];
                edges[i][0] = long.Parse(line.Split()[0]);
                edges[i][1] = long.Parse(line.Split()[1]);
            }
            line = Console.ReadLine();
            long StartNode = long.Parse(line.Split()[0]);
            long EndNode = long.Parse(line.Split()[1]);
            Console.WriteLine(Solve(NodeCount, edges, StartNode, EndNode));
        }

        static List<long>[] graph;
        static bool[] visited;
        public static long Solve(long NodeCount, long[][] edges,
                          long StartNode, long EndNode)
        {
            graph = new List<long>[NodeCount];
            for (long i = 0; i < NodeCount; i++)
                graph[i] = new List<long>();
            visited = new bool[NodeCount];

            foreach(long[] edge in edges)
            {
                graph[edge[0] - 1].Add(edge[1] - 1);
                graph[edge[1] - 1].Add(edge[0] - 1);
            }

            return Bfs(StartNode - 1, EndNode - 1);
        }

        private static long Bfs(long startNode, long endNode)
        {
            List<long> must_see = new List<long>();
            must_see.Add(startNode);

            long current;
            long level = 0;
            visited[startNode] = true;
            long size;

            while(must_see.Count > 0)
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
                        }
                    }
                    if (current == endNode)
                        return level;
                }
                level++;
            }
            return -1;
        }
    }
}
