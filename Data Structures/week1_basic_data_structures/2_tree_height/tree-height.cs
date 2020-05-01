using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tree_height
{
    public class Program
    {
        static void Main()
        {
            int nodeCount = int.Parse(Console.ReadLine());
            int[] tree = new int[nodeCount];
            int i = 0;
            foreach(string p in Console.ReadLine().Split())
            {
                tree[i] = int.Parse(p);
                i++;
            }

            Console.WriteLine(Solve(nodeCount, tree));
            
        }

        public static int Solve(int nodeCount, int[] tree)
        {
            if(nodeCount == 0)
                return 0;

            node[] node_tree = new node[nodeCount];
            
            int root = 0;

            for(int i = 0; i < nodeCount; i++)
                node_tree[i] = new node();
            
            for(int i = 0; i < nodeCount; i++)
            {
                if(tree[i] == -1)
                    root = i;
                else
                {
                    node_tree[i].parent = tree[i];
                    node_tree[tree[i]].children.Add(i);
                }
            }

            return BFSLevel(nodeCount, node_tree, root);
        }

        public static int BFSLevel(int n, node[] nt, int root)
        {
            List<int> q = new List<int>(n);
            q.Add(root);
            int node;
            int level = 0;
            int size;
            while(q.Count > 0)
            {
                size = q.Count;
                for(int j = 0; j < size; j++)
                {
                    node = q[0];
                    q.RemoveAt(0);
                    if(nt[node].children.Count > 0)
                    {
                        for(int i = 0; i < nt[node].children.Count; i++)
                            q.Add(nt[node].children[i]);
                    }
                }
                level++;
            }

            return level;
        }
    }
    public class node
    {
        public List<int> children;
        public int parent;

        public node()
        {
            children = new List<int>();
        }
    }
}
