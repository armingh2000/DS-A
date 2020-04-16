using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shortest_path
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
                edges[i] = new long[3];
                edges[i][0] = long.Parse(line.Split()[0]);
                edges[i][1] = long.Parse(line.Split()[1]);
                edges[i][2] = long.Parse(line.Split()[2]);
            }

            line = Console.ReadLine();
            long startNode = long.Parse(line);
            Solve(nodeCount, edges, startNode);
        }

        static bool[] changed;
        static bool[] changed2;
        static long[] dist;
        static long n;
        static long[] prev;
        static List<long>[] graph;
        static List<long> cycle_nodes;

        public static void Solve(long nodeCount, long[][] edges, long startNode)
        {
            changed = new bool[nodeCount];
            changed2 = new bool[nodeCount];
            dist = new long[nodeCount];
            n = nodeCount;
            prev = new long[nodeCount];
            cycle_nodes = new List<long>();
            graph = new List<long>[nodeCount];

            for (long i = 0; i < n; i++)
                graph[i] = new List<long>();

            foreach (long[] edge in edges)
            {
                graph[edge[0] - 1].Add(edge[1] - 1);
            }

            for (long i = 0; i < nodeCount; i++)
                dist[i] = long.MaxValue;

            dist[startNode - 1] = 0;

            bellManFord(edges);
            changeCheck(edges);
            cycleCheck();
            foreach (long cycle_node in cycle_nodes)
            {
                bfs(cycle_node);
            }


            for (long i = 0; i < nodeCount; i++) 
            {
                bool changd = changed2[i];
                if (changd)
                    Console.WriteLine("-");
                else
                {
                    if (dist[i] == long.MaxValue)
                        Console.WriteLine("*");
                    else
                        Console.WriteLine(dist[i]);
                }
            }
        }

        private static void bfs(long cycle_node)
        {
            bool[] visited = new bool[n];
            List<long> must_see = new List<long>();

            must_see.Add(cycle_node);
            long current;
            long size = 1;
            visited[cycle_node] = true;
            changed2[cycle_node] = true;

            while (must_see.Count > 0)
            {
                for (long i = 0; i < size; i++)
                {
                    current = must_see[0];
                    must_see.RemoveAt(0);
                    foreach(long node2 in graph[current])
                    {
                        if (!visited[node2])
                        {
                            must_see.Add(node2);
                            visited[node2] = true;
                            changed2[node2] = true;
                        }
                    }
                }
                size = must_see.Count();
            }

        }

        private static void cycleCheck()
        {
            for (long i = 0; i < n; i++)
            {
                if (changed[i])
                {
                    long changed_node = i;
                    long u = cycleFind(changed_node);

                    if (!changed2[u])
                    {
                        cycle_nodes.Add(u);
                        changed2[u] = true;
                        long node2 = prev[u];
                        while(node2 != u)
                        {
                            changed2[node2] = true;
                            node2 = prev[node2];
                        }
                    }
                }
            }
        }

        private static long cycleFind(long changed_node)
        {
            long ans = changed_node;
            for (long i = 0; i < n; i++)
                ans = prev[ans];
            return ans;
        }

        private static void changeCheck(long[][] edges)
        {
            foreach (long[] edge in edges)
            {
                if (edgeChangeCheck(edge))
                {
                    changed[edge[0] - 1] = true;
                    changed[edge[1] - 1] = true;
                }
            }
        }

        private static bool edgeChangeCheck(long[] edge)
        {
            if (dist[edge[0] - 1] != long.MaxValue)
            {
                if (dist[edge[1] - 1] > dist[edge[0] - 1] + edge[2])
                    return true;
            }
            return false;
        }

        private static void bellManFord(long[][] edges)
        {
            for (long i = 0; i < n - 1; i++)
            {
                foreach (long[] edge in edges)
                {
                    relax(edge);
                }
            }
        }

        private static void relax(long[] edge)
        {
            long u = edge[0] - 1;
            long v = edge[1] - 1;
            if (dist[u] != long.MaxValue)
            {
                if (dist[v] > dist[u] + edge[2])
                {
                    dist[v] = dist[u] + edge[2];
                    prev[v] = u;
                }
            }
        }
    }
}
