using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie_Matching 
{
    public class Program
    {

        static void Main()
        {
            string text = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());
            string[] patterns = new string[n];
            for(int i = 0; i < n; i++)
            {
                patterns[i] = Console.ReadLine();
            }
            long[] res = Solve(text, n, patterns);
            for(int i = 0; i < res.Length; i++)
            {
                Console.WriteLine(res[i]);
            }

        }

        static List<List<int[]>> Nodes;
        static int nodeNum;
        static int edgeNum;
        static List<bool> isLeaf;

        public static long[] Solve(string text, long n, string[] patterns)
        {
            isLeaf = new List<bool>
            {
                false
            };
            List<long> indexes = new List<long>();
            Nodes = new List<List<int[]>>();
            Nodes.Add(new List<int[]>());
            nodeNum = 1;
            edgeNum = 0;
            foreach(string pattern in patterns)
            {
                constructTrie(pattern);
            }
            for(int i = 0; i < text.Length; i++)
            {
                if(isInTrie(text, i)) { indexes.Add((long)i); }
            }
            if(indexes.Count == 0)
            {
                return new long[0];
            }
            
            return indexes.ToArray();
        }

        private static bool isInTrie(string text, int s)
        {
            bool ok = false;
            string pattern = text.Substring(s);
            bool loop = true;
            int i = 0;
            char[] chrs = pattern.ToCharArray();
            int reps = 0;
            while ((loop) && (reps < chrs.Length))
            {
                loop = false;
                for (int j = 0; j < Nodes[i].Count; j++)
                {
                    if (Nodes[i][j][1] == (int)chrs[reps])
                    {
                        loop = true;
                        i = Nodes[i][j][0];
                        reps++;
                        break;
                    }
                }
                if (isLeaf[i] == true) { ok = true; break; }
            }

            return ok;
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
                for (int j = 0; j < Nodes[i].Count; j++)
                {
                    if (Nodes[i][j][1] == (int)chrs[reps])
                    {
                        loop = true;
                        i = Nodes[i][j][0];
                        reps++;
                        break;
                    }
                }
            }

            if (reps == chrs.Length) { isLeaf[i] = true; return; }

            for (int j = reps; j < chrs.Length; j++)
            {
                Nodes[i].Add(new int[2] { nodeNum, (int)chrs[j] });
                Nodes.Add(new List<int[]>());
                isLeaf.Add(false);
                i = nodeNum;
                nodeNum++;
                edgeNum++;
            }
            isLeaf[i] = true;
        }
    }
}
