using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negative_cycle
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

            Console.Write(Solve(nodeCount, edges));
        }

        static public long[] dist;
        static long n;
        static long[][] pub_edges;
        static bool[] visited;
        static long current;


        public static long Solve(long nodeCount, long[][] edges)
        {

            visited = new bool[nodeCount];
            pub_edges = edges;

            dist = new long[nodeCount];
            for (long i = 0; i < n; i++)
                dist[i] = long.MaxValue;

            n = nodeCount;

            bool res = bellmanFord();
            if (res)
                return 1;
            return 0;
        }

        private static bool bellmanFord()
        {

            dist[current] = 0;
            for (long i = 0; i < n - 1; i++)
            {
                foreach (long[] edge in pub_edges)
                {
                    relax(edge);
                }
            }
            visCheck();
            dist[current] = 0;
            for (long i = 0; i < n - 1; i++)
            {
                foreach (long[] edge in pub_edges)
                {
                    relax(edge);
                }
            }

            return changeCheck();
        }

        private static bool changeCheck()
        {
            foreach (long[] edge in pub_edges)
            {
                if (check(edge))
                    return true;
            }
            return false;
        }

        private static bool check(long[] edge)
        {
            long u = edge[0] - 1;
            long v = edge[1] - 1;
            if (dist[v] > dist[u] + edge[2])
                return true;
            return false;
        }

        private static void relax(long[] edge)
        {
            long u = edge[0] - 1;
            long v = edge[1] - 1;
            if(dist[u] != long.MaxValue)
            {
                if (dist[v] > dist[u] + edge[2])
                    dist[v] = dist[u] + edge[2];
                visited[u] = true;
                visited[v] = true;
            }
        }

        private static bool visCheck()
        {
            for (long i = 0; i < n; i++)
                if (!visited[i])
                {
                    current = i;
                    return true;
                }
            return false;
        }
    }
}
