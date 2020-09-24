using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW4_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            string[] secLine = Console.ReadLine().Split();
            long[] funFactors = new long[n];
            for(long i=0;i<n;i++)
            {
                funFactors[i] = long.Parse(secLine[i]);
            }
            long[][] h = new long[n - 1][];
            string[] line;
            for (long i = 0; i < n - 1; i++) 
            {
                h[i] = new long[2];
                line = Console.ReadLine().Split();
                h[i][0] = long.Parse(line[0]);
                h[i][1] = long.Parse(line[1]);
            }
            Console.WriteLine(Solve(n, funFactors, h));
        }

        public static long Solve(long n, long[] funFactors, long[][] hierarchy)
        {
            Graph g = new Graph(n);
            for (long i = 0; i < n - 1; i++)
            {
                g.AddEdge(hierarchy[i][0] - 1, hierarchy[i][1] - 1);
            }
            Tree tree = g.BFS(0);
            tree.InitializeFunFactors(funFactors);
            var res = tree.FunParty(tree.root);
            return res;
        }
        
        class Graph
        {
            public long NodeCount;
            public List<List<long>> Edges = new List<List<long>>();

            public Graph() { }

            public Graph(long nodeCount)
            {
                this.NodeCount = nodeCount;
                for (long i = 0; i < NodeCount; i++)
                {
                    Edges.Add(new List<long>());
                }
            }

            public void AddEdge(long u, long v)
            {
                Edges[(int)u].Add(v);
                Edges[(int)v].Add(u);
            }
            
            public Tree BFS(long Source)
            {
                Tree tree = new Tree(NodeCount);
                tree.root = Source;
                long[] dist = new long[NodeCount];

                for (long i = 0; i < NodeCount; i++)
                {
                    dist[i] = long.MaxValue;
                }

                dist[Source] = 0;
                Queue<long> Q = new Queue<long>();
                Q.Enqueue(Source);

                while (Q.Count > 0)
                {
                    long u = Q.Dequeue();
                    for (long v = 0; v < Edges[(int)u].Count; v++)
                    {
                        if (dist[Edges[(int)u][(int)v]] == long.MaxValue)
                        {
                            Q.Enqueue(Edges[(int)u][(int)v]);
                            dist[Edges[(int)u][(int)v]] = dist[u] + 1;
                            tree.AddEdge(u, Edges[(int)u][(int)v]);
                        }
                    }
                }
                //return dist;
                return tree;
            }
            
        }

        public class Tree
        {
            public long root;
            public long NodeCount;
            public List<List<long>> Edges = new List<List<long>>();
            public bool[] Visited;
            public long?[] D;
            public long[] W;

            public Tree() { }

            public Tree(long nodeCount)
            {
                this.NodeCount = nodeCount;
                Visited = new bool[nodeCount];
                D = new long?[nodeCount];
                W = new long[NodeCount];
                for (long i = 0; i < NodeCount; i++)
                {
                    Edges.Add(new List<long>());
                }
            }

            public void AddEdge(long u, long v)
            {
                Edges[(int)u].Add(v);
            }
            
            //public long FunParty2(long v)
            //{
            //    if (!D[v].HasValue)
            //    {
            //        long m0 = 0, m1 = 0;
            //        Visited[v] = true;
            //        if (!HasChildren(v))
            //        {
            //            D[v] = W[v];
            //        }
            //        else
            //        {
            //            m1 = W[v];
            //            for (long i = 0; i < Edges[(int)v].Count; i++)
            //            {
            //                if (!Visited[Edges[(int)v][(int)i]])
            //                {
            //                    for (long j = 0; j < Edges[(int)v].Count; j++)
            //                    {
            //                        if (!Visited[Edges[(int)i][(int)j]])
            //                        {
            //                            m1 += FunParty(Edges[(int)i][(int)j]);
            //                        }
            //                    }
            //                }
            //            }
            //            m0 = 0;

            //            for (long i = 0; i < Visited.Length; i++)
            //            {
            //                Visited[i] = false;
            //            }

            //            for (long i = 0; i < Edges[(int)v].Count; i++)
            //            {
            //                if (!Visited[Edges[(int)v][(int)i]])
            //                {
            //                    m0 += FunParty2(Edges[(int)v][(int)i]);
            //                }
            //            }
            //        }
            //        D[v] = Math.Max(m1, m0);

            //    }
            //    return D[v].Value;
            //}

            public long FunParty(long v)
            {
                if (!D[v].HasValue)
                {
                    long m0 = 0, m1 = 0;
                    //Visited[v] = true;
                    if (!HasChildren(v))
                    {
                        D[v] = W[v];
                    }
                    else
                    {
                        m1 = W[v];
                        for (long i = 0; i < Edges[(int)v].Count; i++)
                        {
                            if (HasChildren(Edges[(int)v][(int)i]))
                            {
                                long children = Edges[(int)v][(int)i];
                                for (long j = 0; j < Edges[(int)children].Count; j++)
                                {
                                    m1 += FunParty(Edges[(int)children][(int)j]);
                                }
                            }

                        }

                        m0 = 0;

                        for (long i = 0; i < Edges[(int)v].Count; i++)
                        {
                            m0 += FunParty(Edges[(int)v][(int)i]);
                        }

                        D[v] = Math.Max(m1, m0);

                    }

                }
                return D[v].Value;
            }

            public bool HasChildren(long v)
            {
                //for (long i = 0; i < Edges[(int)v].Count; i++)
                //{
                //    if (!Visited[Edges[(int)v][(int)i]])
                //    {
                //        return true;
                //    }
                //}
                //return false;


                //if (Edges[(int)v].Count > 1)
                //{
                //    return true;
                //}
                //else if (!Visited[Edges[(int)v][0]])
                //{
                //    return true;
                //}
                if (Edges[(int)v].Count >= 1)
                    return true;
                return false;
            }

            public void InitializeFunFactors(long[] funFactors)
            {
                for (long i = 0; i < funFactors.Length; i++)
                {
                    W[i] = funFactors[i];
                }
            }
            
        }
        
    }
}
