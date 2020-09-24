using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advance_HW4_Q4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fLine = Console.ReadLine().Split();
            char[] sLine = Console.ReadLine().ToCharArray();
            int v = int.Parse(fLine[0]);
            int c = int.Parse(fLine[1]);
            long[][] matrix = new long[c][];
            string[] line;
            for (long i = 0; i < c; i++)
            {
                line = Console.ReadLine().Split();
                matrix[i] = new long[2];
                matrix[i][0] = long.Parse(line[0]);
                matrix[i][1] = long.Parse(line[1]);
            }
            var res = Solve(v, sLine, matrix);
            res.ToList().ForEach(x => Console.Write(x));
            //var res = Solve(v, c, matrix);
            //if (res.Item1)
            //{
            //    Console.WriteLine("SATISFIABLE");
            //}
            //else
            //{
            //    Console.WriteLine("UNSATISFIABLE");
            //}
            //res.Item2.ToList().ForEach(x => Console.Write(x + " "));
        }


        public static char[] Solve(long nodeCount, char[] colors, long[][] edges)
        {
            List<long[]> cnf = new List<long[]>();
            for (long i = 0; i < colors.Length; i++)
            {
                if (colors[i] == 'R')
                {
                    cnf.Add(new long[] { -(i * 3 + 1), -(i * 3 + 1) });
                    cnf.Add(new long[] { (i * 3 + 2), (i * 3 + 3) });
                    cnf.Add(new long[] { -(i * 3 + 2), -(i * 3 + 3) });
                    //cnf.Add(new List<long>() { i})
                }
                else if (colors[i] == 'G')
                {
                    cnf.Add(new long[] { -(i * 3 + 2), -(i * 3 + 2) });
                    cnf.Add(new long[] { (i * 3 + 1), (i * 3 + 3) });
                    cnf.Add(new long[] { -(i * 3 + 1), -(i * 3 + 3) });
                }
                else
                {
                    cnf.Add(new long[] { -(i * 3 + 3) , -(i * 3 + 3) });
                    cnf.Add(new long[] { (i * 3 + 1), (i * 3 + 2) });
                    cnf.Add(new long[] { -(i * 3 + 1), -(i * 3 + 2) });
                }
            }
            
            for (long i = 0; i < edges.Length; i++)
            {
                cnf.Add(new long[] { -MapAndGetIndex(edges[i][0] - 1, 0), -MapAndGetIndex(edges[i][1] - 1, 0) });
                cnf.Add(new long[] { -MapAndGetIndex(edges[i][0] - 1, 1), -MapAndGetIndex(edges[i][1] - 1, 1) });
                cnf.Add(new long[] { -MapAndGetIndex(edges[i][0] - 1, 2), -MapAndGetIndex(edges[i][1] - 1, 2) });
            }
            
            var res = Solve(3 * nodeCount, cnf.Count, cnf.ToArray());
            
            if (res.Item1)
            {
                char[] assignment = new char[nodeCount];

                for (long i = 0; i < nodeCount; i++)
                {
                    for (long j = 0; j < 3; j++)
                    {
                        if (res.Item2[3 * i + j] > 0)
                        {
                            if (j == 0 && colors[i]!='R')
                            {
                                assignment[i] = 'R';
                                break;
                            }
                            else if (j == 1 && colors[i] != 'G')
                            {
                                assignment[i] = 'G';
                                break;
                            }
                            else
                            {
                                assignment[i] = 'B';
                                break;
                            }
                        }
                    }
                }

                return assignment;
            }
            else
            {
                return new char[] { 'I', 'm', 'p', 'o', 's', 's', 'i', 'b', 'l', 'e' };
            }

        }

        public static long MapColorToNum(char c)
        {
            if (c == 'R')
                return 1;
            if (c == 'G')
                return 2;
            return 3;
        }

        public static long MapAndGetIndex(long i, long j)
        {
            return (i * 3) + j + 1;
        }

        //bool[] HasEdge; 

        public static Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
        {
            Graph g = new Graph(v);

            for (long i = 0; i < c; i++)
            {
                g.AddEdge(cnf[i][0], cnf[i][1]);
            }
            g.DFSRev();
            var t = g.TopologicalSort();
            g.DFSForStronglyConnectedComponent(t);
            bool f = g.CheckSatisfiability();
            if (!f)
            {
                return new Tuple<bool, long[]>(f, new long[0]);
            }
            //Tuple<bool, long[]> res = new Tuple<bool, long[]>();
            long[] res = new long[v];
            for (long i = v; i < g.NodeCount; i++)
            {
                if (g.Assigns[i] == 1)
                {
                    res[i - v] = i - v + 1;
                }
                else
                {
                    res[i - v] = v - i - 1;
                }
            }

            return new Tuple<bool, long[]>(f, res);
        }

        public class OrderedPair
        {
            public long NodeNum { get; set; }
            public long PostVisitValue { get; set; }
        }

        public class Graph
        {
            public long NodeCount;
            public List<List<long>> Edges = new List<List<long>>();
            public List<List<long>> ReverseEdges = new List<List<long>>();
            long n;
            public long[] PreVisit;
            public long[] PostVisit;
            public long clock = 1;
            public OrderedPair[] ForTopologicalSortSet;
            public List<int> Visited = new List<int>();
            public long ConnectedComponent;
            public long StronglyConnectedComponent;
            public long[] SCC;
            List<List<long>> SCCPartitioning = new List<List<long>>();
            public long[] TopologicalOreder;

            public Graph() { }

            public Graph(long v)
            {
                n = v;
                this.NodeCount = v * 2;
                ForTopologicalSortSet = new OrderedPair[NodeCount];
                TopologicalOreder = new long[NodeCount];
                PostVisit = new long[NodeCount];
                PreVisit = new long[NodeCount];
                SCCPartitioning = new List<List<long>>();

                for (long i = 0; i < NodeCount; i++)
                {
                    Edges.Add(new List<long>());
                    ReverseEdges.Add(new List<long>());
                    ForTopologicalSortSet[i] = new OrderedPair();
                    Visited.Add(0);
                    //SCCPartitioning.Add(new List<long>());
                }
                SCC = new long[NodeCount];
            }

            public long IndexToNodeNum(long idx)
            {
                if (idx < n)
                {
                    return idx - n;
                }
                else
                {
                    return idx - (n - 1);
                }
            }

            public long NodeNumToIndex(long nodeNum)
            {
                if (nodeNum > 0)
                {
                    return nodeNum + n - 1;
                }
                else
                {
                    return nodeNum + n;
                }
            }

            public void AddEdge(long l1, long l2)
            {
                Edges[(int)NodeNumToIndex(-l1)].Add(NodeNumToIndex(l2));
                Edges[(int)NodeNumToIndex(-l2)].Add(NodeNumToIndex(l1));
                ReverseEdges[(int)NodeNumToIndex(l2)].Add(NodeNumToIndex(-l1));
                ReverseEdges[(int)NodeNumToIndex(l1)].Add(NodeNumToIndex(-l2));
            }

            public void Previsit(long v)
            {
                PreVisit[v] = clock;
                clock++;
            }

            public void Postvisit(long v)
            {
                PostVisit[v] = clock;
                clock++;
                ForTopologicalSortSet[v].NodeNum = v;
                ForTopologicalSortSet[v].PostVisitValue = PostVisit[v];
            }

            public void Explore(long v)
            {
                Stack<long> s = new Stack<long>();
                s.Push(v);
                while (s.Count > 0)
                {
                    long u = s.Pop();
                    Visited[(int)u] = 1;
                    for (long i = 0; i < Edges[(int)u].Count; i++)
                    {
                        long child = Edges[(int)u][(int)i];
                        if (Visited[(int)child] == 0)
                        {
                            s.Push(child);
                            //ExploreRev(child);
                        }
                    }
                }
                //Visited[(int)v] = 1;
                ////Previsit(v);
                //for (long i = 0; i < Edges[(int)v].Count; i++)
                //{
                //    long child = Edges[(int)v][(int)i];
                //    if (Visited[(int)child] == 0)
                //    {
                //        Explore(child);
                //    }
                //}
                ////Postvisit(v);
            }

            public void ExploreRev(long v)
            {
                //Stack<long> s = new Stack<long>();
                //s.Push(v);
                //while(s.Count>0)
                //{
                //    long u = s.Pop();
                //    Visited[(int)u] = 1;
                //    Previsit(u);
                //    for (long i = 0; i < ReverseEdges[(int)u].Count; i++)
                //    {
                //        long child = ReverseEdges[(int)u][(int)i];
                //        if (Visited[(int)child] == 0)
                //        {
                //            s.Push(child);
                //            //ExploreRev(child);
                //        }
                //    }

                //}

                Visited[(int)v] = 1;
                Previsit(v);
                for (long i = 0; i < ReverseEdges[(int)v].Count; i++)
                {
                    long child = ReverseEdges[(int)v][(int)i];
                    if (Visited[(int)child] == 0)
                    {
                        ExploreRev(child);
                    }
                }
                Postvisit(v);

            }

            public void DFS()
            {
                ConnectedComponent = 0;
                for (long i = 0; i < NodeCount; i++)
                {
                    Visited[(int)i] = 0;
                }

                for (long i = 0; i < NodeCount; i++)
                {
                    if (Visited[(int)i] == 0)
                    {
                        ConnectedComponent++;
                        Explore(i);
                    }
                }
            }

            public void DFSRev()
            {
                //ConnectedComponent = 0;
                for (long i = 0; i < NodeCount; i++)
                {
                    Visited[(int)i] = 0;
                }

                for (long i = 0; i < NodeCount; i++)
                {
                    if (Visited[(int)i] == 0)
                    {
                        //ConnectedComponent++;
                        ExploreRev(i);
                    }
                }
            }

            public void ExploreSCC(long v, long scc)
            {
                Stack<long> s = new Stack<long>();

                s.Push(v);
                while (s.Count > 0)
                {
                    long u = s.Pop();
                    Visited[(int)u] = 1;
                    SCC[(int)u] = scc;
                    SCCPartitioning[(int)scc - 1].Add(u);
                    for (long i = 0; i < Edges[(int)u].Count; i++)
                    {
                        if (Visited[(int)Edges[(int)u][(int)i]] == 0)
                        {
                            s.Push((int)Edges[(int)u][(int)i]);
                        }
                    }
                }

                //Visited[(int)v] = 1;
                //SCC[(int)v] = scc;
                //SCCPartitioning[(int)scc - 1].Add(v);

                //for (long i = 0; i < /*Reverse*/Edges[(int)v].Count; i++)
                //{
                //    long child = /*Reverse*/Edges[(int)v][(int)i];
                //    if (Visited[(int)child] == 0)
                //    {
                //        ExploreSCC(child, scc);
                //    }
                //}
            }

            public void DFSForStronglyConnectedComponent(long[] reversPostOrder)
            {
                StronglyConnectedComponent = 0;
                for (long i = 0; i < NodeCount; i++)
                {
                    Visited[(int)i] = 0;
                }

                foreach (var v in reversPostOrder)
                {
                    if (Visited[(int)v] == 0)
                    {
                        SCCPartitioning.Add(new List<long>());
                        StronglyConnectedComponent++;
                        ExploreSCC(v, StronglyConnectedComponent);
                    }
                }
            }

            public long[] TopologicalSort()
            {
                TopologicalOreder = ForTopologicalSortSet.OrderByDescending(x => x.PostVisitValue).Select(x => x.NodeNum).ToArray();
                return TopologicalOreder;
            }

            public bool CheckSatisfiability()
            {
                for (long i = 0; i < n; i++)
                {
                    if (SCC[(i)] == SCC[NodeNumToIndex(-IndexToNodeNum(i))])
                    {
                        return false;
                    }
                }

                //AreAssign = new bool[StronglyConnectedComponent];
                Assigns = new int[NodeCount];

                for (long i = 0; i < NodeCount; i++)
                {
                    Visited[(int)i] = 0;
                }

                foreach (var node in TopologicalOreder)
                {
                    if (Visited[(int)node] == 0/*!AreAssign[SCC[node]]*/)
                    {
                        ExploreNodeInSCC(node, 1);
                    }
                }

                return true;
            }

            public void ExploreNode(long v, int assign)
            {
                Visited[(int)v] = 1;
                Assigns[v] = assign;

                //Previsit(v);
                for (long i = 0; i < Edges[(int)v].Count; i++)
                {
                    long child = Edges[(int)v][(int)i];
                    if (Visited[(int)child] == 0)
                    {
                        ExploreNode(child, assign);
                    }

                }
                if (Visited[(int)NodeNumToIndex(-IndexToNodeNum(v))] == 0)
                {
                    ExploreNode(NodeNumToIndex(-IndexToNodeNum(v)), assign * (-1));
                }


                //Postvisit(v);
            }

            public void ExploreNodeInSCC(long v, int assign)
            {
                //Visited[(int)v] = 1;
                //Assigns[v] = assign;

                long sccNum = SCC[v];
                var inSameSCC = SCCPartitioning[(int)SCC[v] - 1];
                int negate;

                foreach (var node in inSameSCC)
                {
                    if (Visited[(int)node] == 0)
                    {
                        Visited[(int)node] = 1;
                        Assigns[node] = assign;
                        negate = (int)NodeNumToIndex(-IndexToNodeNum(node));
                        Visited[negate] = 1;
                        Assigns[negate] = assign * (-1);
                    }
                }

                //foreach (var node in inSameSCC)
                //{
                //    if (Visited[(int)NodeNumToIndex(-IndexToNodeNum(node))] == 0)
                //    {
                //        ExploreNodeInSCC(NodeNumToIndex(-IndexToNodeNum(node)), assign * (-1));
                //    }
                //}

                #region
                //Previsit(v);
                //for (long i = 0; i < Edges[(int)v].Count; i++)
                //{
                //    long child = Edges[(int)v][(int)i];
                //    if (Visited[(int)child] == 0)
                //    {
                //        ExploreNode(child, assign);
                //    }

                //}
                //if (Visited[(int)NodeNumToIndex(-IndexToNodeNum(v))] == 0)
                //{
                //    ExploreNode(NodeNumToIndex(-IndexToNodeNum(v)), assign * (-1));
                //}

                //Postvisit(v);
                #endregion
            }

            public void ExploreNodeInSCC2(long v, int assign)
            {
                Visited[(int)v] = 1;
                Assigns[v] = assign;

                long sccNum = SCC[v];
                var inSameSCC = SCCPartitioning[(int)SCC[v] - 1];

                foreach (var node in inSameSCC)
                {
                    if (Visited[(int)node] == 0)
                    {
                        Assigns[node] = assign;
                    }
                }

                foreach (var node in inSameSCC)
                {
                    if (Visited[(int)NodeNumToIndex(-IndexToNodeNum(node))] == 0)
                    {
                        ExploreNodeInSCC(NodeNumToIndex(-IndexToNodeNum(node)), assign * (-1));
                    }
                }
                #region
                //Previsit(v);
                //for (long i = 0; i < Edges[(int)v].Count; i++)
                //{
                //    long child = Edges[(int)v][(int)i];
                //    if (Visited[(int)child] == 0)
                //    {
                //        ExploreNode(child, assign);
                //    }

                //}
                //if (Visited[(int)NodeNumToIndex(-IndexToNodeNum(v))] == 0)
                //{
                //    ExploreNode(NodeNumToIndex(-IndexToNodeNum(v)), assign * (-1));
                //}

                //Postvisit(v);
                #endregion
            }



            //public bool[] AreAssign;
            public int[] Assigns;

        }

    }
}
