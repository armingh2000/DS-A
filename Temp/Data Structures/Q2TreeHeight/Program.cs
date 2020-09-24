using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2TreeHeight
{
    class Program
    {
        static void Main(string[] args)
        {
            long nodeCount = long.Parse(Console.ReadLine());
            var v = Console.ReadLine().Split();
            long[] tree = new long[v.Length];

            for(long i=0;i<v.Length;i++)
            {
                tree[i] = long.Parse(v[i]);
            }

            Console.WriteLine(Solve(nodeCount, tree));

        }

        public static long Solve(long nodeCount, long[] tree)
        {
            int maxHeight = 0;

            List<List<int>> child = new List<List<int>>();
            for (int i = 0; i < nodeCount; i++)
            {
                child.Add(new List<int>());
            }
            //4 -1 4 1 1
            int root = 0;
            for (int i = 0; i < nodeCount; i++)
            {

                if (tree[i] == -1)
                {
                    root = i;
                }
                else
                {
                    child[(int)tree[i]].Add(i);
                }
            }

            int[] height = new int[(int)nodeCount];
            height[root] = 1;
            //List<int> list = new List<int>();
            Queue<int> list = new Queue<int>();
            list.Enqueue(root);

            while (list.Count != 0)
            {

                foreach (var t in child[list.Peek()])
                {
                    list.Enqueue(t);
                    height[t] = height[list.Peek()] + 1;
                    if (maxHeight < height[t])
                    {
                        maxHeight = height[t];
                    }
                }
                list.Dequeue();
            }

            return maxHeight;

        }

    }
}
