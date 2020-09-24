using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2IsItBST
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());

            long[][] nodes = new long[n][];
            string[] line;

            for (long i = 0; i < n; i++)
            {
                nodes[i] = new long[3];
                line = Console.ReadLine().Split();
                nodes[i][0] = long.Parse(line[0]);
                nodes[i][1] = long.Parse(line[1]);
                nodes[i][2] = long.Parse(line[2]);
            }
            
            if(n==0)
            {
                Console.WriteLine("CORRECT");
                return;
            }

            var res = Solve(nodes);

            if (res==true)
            {
                Console.WriteLine("CORRECT");
            }
            else
            {
                Console.WriteLine("INCORRECT");
            }
        }



        public static bool Solve(long[][] nodes)
        {
            Node[] arrayNodes = new Node[nodes.Length];
            for (long i = 0; i < nodes.Length; i++)
            {
                arrayNodes[i] = new Node(nodes[i][0]);
            }
            for (long i = 0; i < nodes.Length; i++)
            {
                if (nodes[i][1] != -1)
                {
                    arrayNodes[i].Left = arrayNodes[nodes[i][1]];
                }
                if (nodes[i][2] != -1)
                {
                    arrayNodes[i].Right = arrayNodes[nodes[i][2]];
                }
            }

            return CheckTree(arrayNodes[0], long.MinValue, long.MaxValue);
        }

        public static bool CheckTree(Node i, long min, long max)
        {
            if (i.Left == null && i.Right == null && (i.Key>min&&i.Key<max))
            {
                return true;
            }
            if (i.Key <= min || i.Key >= max)
            {
                return false;
            }
            if (i.Left == null)
            {
                return CheckTree(i.Right, i.Key, max);
            }
            else if (i.Right == null)
            {
                return CheckTree(i.Left, min, i.Key);
            }
            return CheckTree(i.Left, min, i.Key) && CheckTree(i.Right, i.Key, max);
        }

        public class Node
        {
            public long Key { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node()
            { }

            public Node(long k)
            {
                Key = k;
            }
            public Node(long k, Node l, Node r)
            {
                Key = k;
                Left = l;
                Right = r;
            }
        }
    }
}
