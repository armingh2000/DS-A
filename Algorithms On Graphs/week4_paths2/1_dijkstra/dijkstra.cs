using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dijkstra
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
            long startNode = line.Split()[0];
            long endNode = line.Split()[1];
            Console.Write(Solve(nodeCount, edges, startNode, endNode));

        }

        static List<long[]>[] graph;
        static long[] dist;
        static long n;


        public static long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            graph = new List<long[]>[nodeCount];

            for (long i = 0; i < nodeCount; i++)
                graph[i] = new List<long[]>();

            foreach (long[] edge in edges)
                graph[edge[0] - 1].Add(new long[2] { edge[1] - 1, edge[2] });

            dist = new long[nodeCount];
            for (long i = 0; i < nodeCount; i++)
                dist[i] = long.MaxValue;
            dist[startNode - 1] = 0;

            n = nodeCount;

            Dijk(endNode - 1);

            if (dist[endNode - 1] != long.MaxValue)
                return dist[endNode - 1];
            return -1;
        }

        private static void Dijk(long v1)
        {
            SimplePriorityQueue<long> H = new SimplePriorityQueue<long>();
            for (long i = 0; i < n; i++)
                H.Enqueue(i, dist[i]);
            for (long i = 0; i < n; i++) 
            {
                long u = H.Dequeue();
                if (u == v1)
                    break;
                if (dist[u] == long.MaxValue)
                    break;
                foreach (long[] edge in graph[u])
                {
                    if(dist[edge[0]] > dist[u] + edge[1])
                    {
                        dist[edge[0]] = dist[u] + edge[1];
                        H.UpdatePriority(edge[0], dist[edge[0]]);
                    }
                }

            }
        }
    }
}
