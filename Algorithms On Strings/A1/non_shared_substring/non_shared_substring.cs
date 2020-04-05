using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoneSharedSubstring
{
    class Program
    {
        static void Main()
        {
            long n = long.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for(long i = 0; i < n; i++)
            {
                patterns[i]=Console.ReadLine();
            }
            //Program p = new Program();
            string[] res = Solve(n, patterns);//p.Solve(n, patterns);
            for(int i = 0; i < res.Length; i++){
                Console.WriteLine(res[i]);
            }
        
        }
        static private List<List<int[]>> Nodes;
        static private int nodeNum;
        static private int edgeNum;

        public static string[] Solve(long n, string[] patterns)
        {
            Nodes = new List<List<int[]>>();
            nodeNum = 1;
            edgeNum = 0;
            Nodes.Add(new List<int[]>());
            foreach(string pattern in patterns)
            {
                constructTrie(pattern);
            }

            return res();
        }

        private static string[] res()
        {
            string[] result = new string[edgeNum];
            int ind = 0;
            for(int i = 0; i < nodeNum; i++)
            {
                for(int j = 0; j < Nodes[i].Count; j++)
                {
                    result[ind] = String.Format("{0}->{1}:{2}", i, Nodes[i][j][0], (char)Nodes[i][j][1]);
                    ind++;
                }
            }
            return result;
        }

        private static void constructTrie(string pattern)
        {
            bool loop = true;
            int i = 0;
            char[] chrs = pattern.ToCharArray();
            int reps = 0;
            while ((loop) && (reps < chrs.Length))
            {
                loop = false;
                for(int j = 0; j < Nodes[i].Count; j++)
                {
                    if(Nodes[i][j][1] == (int)chrs[reps])
                    {
                        loop = true;
                        i = Nodes[i][j][0];
                        reps++;
                        break;
                    }
                }
            }
            
            if(reps == chrs.Length) { return; }

            for(int j = reps; j < chrs.Length; j++)
            {
                Nodes[i].Add(new int[2] { nodeNum, (int)chrs[j] });
                Nodes.Add(new List<int[]>());
                i = nodeNum;
                nodeNum++;
                edgeNum++;
            }
        }
    }
}
