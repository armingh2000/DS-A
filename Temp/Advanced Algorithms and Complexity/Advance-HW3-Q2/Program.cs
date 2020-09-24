using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW3_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fLine = Console.ReadLine().Split();
            int V = int.Parse(fLine[0]);
            int E = int.Parse(fLine[1]);
            long[,] matrix = new long[E, 2];
            string[] line;
            for (long i = 0; i < E; i++)
            {
                line = Console.ReadLine().Split();
                matrix[i, 0] = long.Parse(line[0]);
                matrix[i, 1] = long.Parse(line[1]);
            }
            var res = Solve(V, E, matrix);

            string result = string.Empty;

            res.Take(1).ToList().ForEach(x => Console.Write(x + "\n"));
            //Console.WriteLine();
            res.Skip(1).ToList().ForEach(x => Console.Write(x + " 0\n"));
        }

        public static String[] Solve(int V, int E, long[,] matrix)
        {
            Graph g = new Graph(V);

            for (long i = 0; i < E; i++)
            {
                g.AddEdge(matrix[i, 0] - 1, matrix[i, 1] - 1);
            }

            List<string> result = new List<string>();

            AtLeastOneInEachRowAndEachColumn(V, result);

            AtMostOneInEachRowAndEachColumn(V, result);

            for (long i = 0; i < V; i++)
            {
                for (long j = 0; j < V; j++)
                {
                    if (AreNotAdjacentNodes(i, j, g))
                    {
                        for (long k = 0; k < V - 1; k++)
                        {
                            result.Add((-GetVarNum(i, k, V)) + " " + (-GetVarNum(j, k + 1, V)));
                        }
                    }
                }
            }

            result.Add(result.Count.ToString() + " " + V * V);
            result.Reverse();
            return result.ToArray();

        }

        public static bool AreNotAdjacentNodes(long i, long j, Graph g)
        {
            return !g.EdgesArray[i, j];
        }

        public static void AtMostOneInEachRowAndEachColumn(int V, List<string> result)
        {
            for (long i = 0; i < V; i++)
            {
                for (long j = 0; j < V; j++)
                {
                    for (long k = j + 1; k < V; k++)
                    {
                        result.Add(((-GetVarNum(i, j, V)) + " " + (-GetVarNum(i, k, V))));
                        result.Add(((-GetVarNum(j, i, V)) + " " + (-GetVarNum(k, i, V))));
                    }
                }
            }
        }

        public static void AtLeastOneInEachRowAndEachColumn(int V, List<string> result)
        {
            string temp1 = string.Empty;
            string temp2 = string.Empty;

            for (long i = 0; i < V; i++)
            {
                temp1 = string.Empty;
                temp2 = string.Empty;
                for (long j = 0; j < V; j++)
                {
                    temp1 += (GetVarNum(i, j, V) + " ");
                    temp2 += (GetVarNum(j, i, V) + " ");
                }
                result.Add(temp1);
                result.Add(temp2);
            }
        }

        public static long GetVarNum(long i, long j, long nodeCount)
        {
            return i * nodeCount + j + 1;
        }

        public class Graph
        {
            public long NodeCount { get; set; }
            //public List<List<long>> Edges = new List<List<long>>();
            public bool[,] EdgesArray;

            Graph()
            { }

            public Graph(long nodeCount)
            {
                this.NodeCount = nodeCount;
                //for (long i = 0; i < nodeCount; i++)
                //{
                //    Edges.Add(new List<long>());
                //}
                EdgesArray = new bool[NodeCount, NodeCount];
            }

            public void AddEdge(long u, long v)
            {
                //Edges[(int)u].Add(v);
                //Edges[(int)v].Add(u);
                EdgesArray[u, v] = true;
                EdgesArray[v, u] = true;
            }
        }


    }
}
