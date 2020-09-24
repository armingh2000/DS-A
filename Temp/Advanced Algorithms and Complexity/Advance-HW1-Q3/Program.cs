using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW1_Q3
{
    class Program
    {
        static void Main(string[] args)
        {
            long s, p;
            string[] fLine = Console.ReadLine().Split();
            s = long.Parse(fLine[0]);
            p = long.Parse(fLine[1]);
            string[] line;
            long[][] matrix = new long[s][];
            for(long i=0;i<s;i++)
            {
                matrix[i] = new long[p];
                line = Console.ReadLine().Split();
                for(long j=0;j<p;j++)
                {
                    matrix[i][j] = long.Parse(line[j]);
                }
            }
            Console.WriteLine(Solve(s, p, matrix));
        }

        public static long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            long n = stockCount;
            long m = pointCount;

            Graph g = new Graph(2 * stockCount + 2);

            for (long i = 1; i <= n; i++)
            {
                g.AddEdge(0, i, 1);
                g.AddEdge(i + n, 2 * n + 1, 1);
            }

            ConstructinGraphFromMatrix(matrix, n, m, g);

            long temp = g.EdmondsKarp(0, 2 * n + 1);
            return n - temp;
        }

        public static void ConstructinGraphFromMatrix(long[][] matrix, long n, long m, Graph g)
        {
            bool flag;
            for (long i = 0; i < n; i++)
            {
                for (long j = 0; j < n; j++)
                {
                    flag = true;
                    if (i != j)
                    {
                        for (long k = 0; k < m; k++)
                        {
                            flag = flag && CheckInequality(matrix[i][k], matrix[j][k]);
                        }
                        if (flag)
                        {
                            g.AddEdge(i + 1, j + n + 1, 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// if state=1 means that the first element was bigger and if state=-1 means that the second element was bigger
        /// and in the returning value it check that based on first conditions to diagram has common point or no
        /// </summary>
        /// <param name="state"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static bool CheckInequality(long i, long j)
        {
            if (i > j)
            {
                return true;
            }
            return false;
        }

        public class Graph
        {
            long NodeCount;
            public List<List<long>> Edges = new List<List<long>>();
            public List<Edge> AllEdges = new List<Edge>();
            public double[] dist;
            public long?[] parent;

            #region Consructors

            public Graph()
            { }

            public Graph(long nodeCount)
            {
                this.NodeCount = nodeCount;
                dist = new double[nodeCount];
                parent = new long?[nodeCount];
                for (long i = 0; i < nodeCount; i++)
                {
                    Edges.Add(new List<long>());
                }
            }

            #endregion Constructors

            public void AddEdge(long u, long v, long c)
            {
                for (long i = 0; i < Edges[(int)u].Count; i++)
                {
                    if (AllEdges[(int)Edges[(int)u][(int)i]].Target == v)
                    {
                        AllEdges[(int)Edges[(int)u][(int)i]].Capacity += c;
                        AllEdges[(int)Edges[(int)u][(int)i] ^ 1].Capacity += c;
                        AllEdges[(int)Edges[(int)u][(int)i] ^ 1].Flow += c;
                        return;
                    }
                }
                Edges[(int)u].Add(AllEdges.Count);
                AllEdges.Add(new Edge(u, v, 0, c));
                Edges[(int)v].Add(AllEdges.Count);
                AllEdges.Add(new Edge(v, u, c, c));

            }

            public void AddFlowWithID(long id, long flow)
            {
                AllEdges[(int)id].Flow += flow;
                AllEdges[(int)id ^ 1].Flow -= flow;
            }

            public long GetID(long from, long to)
            {
                for (long i = 0; i < Edges[(int)from].Count; i++)
                {
                    if (AllEdges[(int)Edges[(int)from][(int)i]].Target == to)
                    {
                        return Edges[(int)from][(int)i];
                    }
                }
                return -1;
            }

            public void AddFlow(long from, long to, long flow)
            {
                long id = GetID(from, to);
                AddFlowWithID(id, flow);
            }

            public bool BFS(long s, long t)
            {
                for (long i = 0; i < NodeCount; i++)
                {
                    dist[i] = long.MaxValue;
                    parent[i] = null;
                }

                dist[s] = 0;
                Queue<long> myQueue = new Queue<long>();
                myQueue.Enqueue(s);
                long temp;

                while (myQueue.Count > 0)
                {
                    temp = myQueue.Dequeue();

                    for (long i = 0; i < Edges[(int)temp].Count; i++)
                    {
                        if ((dist[AllEdges[(int)Edges[(int)temp][(int)i]].Source] + 1
                            <
                            dist[AllEdges[(int)Edges[(int)temp][(int)i]].Target])
                            &&
                            (AllEdges[(int)Edges[(int)temp][(int)i]].Flow
                            <
                            AllEdges[(int)Edges[(int)temp][(int)i]].Capacity
                            ))
                        {
                            dist[AllEdges[(int)Edges[(int)temp][(int)i]].Target] =
                                dist[AllEdges[(int)Edges[(int)temp][(int)i]].Source] + 1;

                            parent[AllEdges[(int)Edges[(int)temp][(int)i]].Target] = AllEdges[(int)Edges[(int)temp][(int)i]].Source;

                            myQueue.Enqueue(AllEdges[(int)Edges[(int)temp][(int)i]].Target);
                        }
                    }
                }

                if (dist[t] != long.MaxValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public long[] GetPath(long s, long t)
            {
                long temp = t;
                List<long> res = new List<long>();
                while (parent[temp] != null)
                {
                    res.Add(temp);
                    temp = (long)parent[temp];
                }
                res.Add(temp);
                res.Reverse();
                return res.ToArray();
            }

            public long GetMinFlow(long[] nodesInPath)
            {
                long id;
                long minFlow = long.MaxValue;
                for (long i = 0; i < nodesInPath.Length - 1; i++)
                {
                    id = GetID(nodesInPath[i], nodesInPath[i + 1]);
                    if (AllEdges[(int)id].Capacity - AllEdges[(int)id].Flow < minFlow)
                    {
                        minFlow = AllEdges[(int)id].Capacity - AllEdges[(int)id].Flow;
                    }
                }
                return minFlow;
            }

            public void AddFlowInPath(long[] path, long flow)
            {
                for (long i = 0; i < path.Length - 1; i++)
                {
                    AddFlow(path[i], path[i + 1], flow);
                }
            }

            public long EdmondsKarp(long s, long t)
            {
                long f = 0;
                long minFlowForEachIteration;
                while (true)
                {
                    if (!BFS(s, t))
                    {
                        return f;
                    }
                    else
                    {
                        long[] path = GetPath(s, t);
                        minFlowForEachIteration = GetMinFlow(path);
                        AddFlowInPath(path, minFlowForEachIteration);
                        f += minFlowForEachIteration;
                    }
                }
            }

            public long[] BipartiteMatching(long s, long t, long n)
            {
                long f = 0;
                long[] res = new long[n];
                for (long i = 0; i < n; i++)
                {
                    res[i] = -1;
                }

                long minFlowForEachIteration;
                while (true)
                {
                    if (!BFS(s, t))
                    {
                        //return res;
                        for (long i = 1; i <= n; i++)
                        {
                            for (long j = 0; j < Edges[(int)i].Count; j++)
                            {
                                if (AllEdges[(int)Edges[(int)i][(int)j]].Flow == AllEdges[(int)Edges[(int)i][(int)j]].Capacity
                                    &&
                                    AllEdges[(int)Edges[(int)i][(int)j]].Source < AllEdges[(int)Edges[(int)i][(int)j]].Target
                                    &&
                                    AllEdges[(int)Edges[(int)i][(int)j]].Source > 0
                                    &&
                                    AllEdges[(int)Edges[(int)i][(int)j]].Source <= n
                                    &&
                                    AllEdges[(int)Edges[(int)i][(int)j]].Target > n
                                    &&
                                    AllEdges[(int)Edges[(int)i][(int)j]].Target < Edges.Count - 1)
                                {
                                    //res[AllEdges[(int)Edges[(int)i][(int)j]].Source - 1] = AllEdges[(int)Edges[(int)i][(int)j]].Target - n;
                                    res[i - 1] = AllEdges[(int)Edges[(int)i][(int)j]].Target - n;
                                }
                            }
                        }
                        return res;
                    }
                    else
                    {
                        long[] path = GetPath(s, t);
                        minFlowForEachIteration = GetMinFlow(path);
                        AddFlowInPath(path, minFlowForEachIteration);
                        //res[path[1] - 1] = (path[2] - n);
                        f += minFlowForEachIteration;
                    }
                }
            }

        }

        public class DoubleEdges
        {
            Edge MainEdge;
            Edge ReverseEdge;

            public DoubleEdges()
            { }

            public DoubleEdges(Edge ME, Edge RE)
            {
                this.MainEdge = ME;
                this.ReverseEdge = RE;
            }
        }

        public class Edge
        {
            public long Source;
            public long Target;
            public long Capacity;
            public long Flow;

            public Edge()
            { }

            public Edge(long u, long v)
            {
                this.Source = u;
                this.Target = v;
            }

            public Edge(long u, long v, long c) : this(u, v)
            {
                this.Capacity = c;
            }

            public Edge(long u, long v, long f, long c) : this(u, v, c)
            {
                this.Flow = f;
            }
        }

    }
}
