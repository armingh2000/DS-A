using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gsm
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //}
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
            var res = Solve2(V, E, matrix);

            string result = string.Empty;
            
            res.Take(1).ToList().ForEach(x => Console.Write(x + "\n"));
            //Console.WriteLine();
            res.Skip(1).ToList().ForEach(x => Console.Write(x + " 0\n"));
        }

        public static String[] Solve(int V, int E, long[,] matrix)
        {
            List<string> result = new List<string>();

            string atLeastOneForEachCell = string.Empty;
            string atMostOne = string.Empty;
            string atLeastOneForAdjacents0 = string.Empty;
            string atLeastOneForAdjacents1 = string.Empty;
            string atLeastOneForAdjacents2 = string.Empty;

            string add = string.Empty;
            string add2 = string.Empty;
            string add3 = string.Empty;

            Graph g = new Graph(V);

            for (long i = 0; i < E; i++)
            {
                g.AddEdge(matrix[i, 0] - 1, matrix[i, 1] - 1);
            }

            for (long i = 0; i < V; i++)
            {
                atLeastOneForEachCell = string.Empty;

                #region colors for each cell
                for (long k = 0; k < 3; k++)
                {
                    atLeastOneForEachCell = atLeastOneForEachCell + GetVarNum(i, k) + " ";
                }
                result.Add(atLeastOneForEachCell);

                result.Add((-GetVarNum(i, 0)) + " " + (-GetVarNum(i, 1)));
                result.Add((-GetVarNum(i, 0)) + " " + (-GetVarNum(i, 2)));
                result.Add((-GetVarNum(i, 1)) + " " + (-GetVarNum(i, 2)));

                #endregion
                
                add = string.Empty;
                add2 = string.Empty;
                add3 = string.Empty;

                //atLeastOneForAdjacents0 = GetVarNum(i,0).ToString();
                //atLeastOneForAdjacents1 = GetVarNum(i, 1).ToString();
                //atLeastOneForAdjacents2 = GetVarNum(i, 2).ToString();

                for (long j = 0; j < g.Edges[(int)i].Count; j++)
                {
                    #region colors for adjacents
                    //for (long k=0;k<3;k++)
                    //{
                    //    atLeastOneForAdjacents = atLeastOneForAdjacents + GetVarNum(i, k) + " " + GetVarNum(g.Edges[(int)i][(int)j], k);
                    //}
                    //atLeastOneForAdjacents0 = atLeastOneForAdjacents0 + " " + GetVarNum(g.Edges[(int)i][(int)j], 0);
                    //atLeastOneForAdjacents1 = atLeastOneForAdjacents1 + " " + GetVarNum(g.Edges[(int)i][(int)j], 1);
                    //atLeastOneForAdjacents2 = atLeastOneForAdjacents2 + " " + GetVarNum(g.Edges[(int)i][(int)j], 2);

                    result.Add((-GetVarNum(i, 0)) + " " + (-GetVarNum(g.Edges[(int)i][(int)j], 0)));
                    result.Add((-GetVarNum(i, 1)) + " " + (-GetVarNum(g.Edges[(int)i][(int)j], 1)));
                    result.Add((-GetVarNum(i, 2)) + " " + (-GetVarNum(g.Edges[(int)i][(int)j], 2)));

                    //for(long k=j+1;k< g.Edges[(int)i].Count;k++)
                    //{
                    //    result.Add((-GetVarNum(i, 0)) + " " + -GetVarNum(g.Edges[(int)i][(int)j], 0));
                    //    result.Add((-GetVarNum(i, 1)) + " " + -GetVarNum(g.Edges[(int)i][(int)j], 1));
                    //    result.Add((-GetVarNum(i, 2)) + " " + -GetVarNum(g.Edges[(int)i][(int)j], 2));
                    //}

                    #endregion
                }
                //result.Add(atLeastOneForAdjacents0);
                //result.Add(atLeastOneForAdjacents1);
                //result.Add(atLeastOneForAdjacents2);
            }


            result.Add(result.Count.ToString() + " " + 3 * V);
            result.Reverse();
            return result.ToArray();
        }

        public static String[] Solve2(int V, int E, long[,] matrix)
        {
            List<string> result = new List<string>();

            string atLeastOneForEachCell = string.Empty;
            
            for (long i = 0; i < V; i++)
            {
                atLeastOneForEachCell = string.Empty;

                #region colors for each cell
                for (long k = 0; k < 3; k++)
                {
                    atLeastOneForEachCell = atLeastOneForEachCell + GetVarNum(i, k) + " ";
                }
                result.Add(atLeastOneForEachCell);

                result.Add((-GetVarNum(i, 0)) + " " + (-GetVarNum(i, 1)));
                result.Add((-GetVarNum(i, 0)) + " " + (-GetVarNum(i, 2)));
                result.Add((-GetVarNum(i, 1)) + " " + (-GetVarNum(i, 2)));
                #endregion
                
            }

            for (long i = 0; i < E; i++)
            {
                result.Add((-GetVarNum(matrix[i, 0] - 1, 0)) + " " + (-GetVarNum(matrix[i, 1] - 1, 0)));
                result.Add((-GetVarNum(matrix[i, 0] - 1, 1)) + " " + (-GetVarNum(matrix[i, 1] - 1, 1)));
                result.Add((-GetVarNum(matrix[i, 0] - 1, 2)) + " " + (-GetVarNum(matrix[i, 1] - 1, 2)));
            }

            result.Add(result.Count.ToString() + " " + 3 * V);
            result.Reverse();
            return result.ToArray();
        }


        public static long GetVarNum(long i, long j)
        {
            return i * 3 + j + 1;
        }

        public class Graph
        {
            public long NodeCount { get; set; }
            public List<List<long>> Edges = new List<List<long>>();

            Graph()
            { }

            public Graph(long nodeCount)
            {
                this.NodeCount = nodeCount;
                for (long i = 0; i < nodeCount; i++)
                {
                    Edges.Add(new List<long>());
                }
            }

            public void AddEdge(long u, long v)
            {
                Edges[(int)u].Add(v);
                Edges[(int)v].Add(u);
            }
        }

    }
}
