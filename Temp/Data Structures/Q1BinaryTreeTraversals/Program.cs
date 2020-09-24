using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1BinaryTreeTraversals
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());

            long[][] nodes = new long[n][];
            string[] line;

            for(long i=0;i<n;i++)
            {
                nodes[i] = new long[3];
                line = Console.ReadLine().Split();
                nodes[i][0] = long.Parse(line[0]);
                nodes[i][1] = long.Parse(line[1]);
                nodes[i][2] = long.Parse(line[2]);
            }

            long[][] res = Solve(nodes);

            for(long i=0;i<3;i++)
            {
                for(long j=0;j<n;j++)
                {
                    Console.Write(res[i][j] + " ");
                }
                Console.WriteLine();
            }
        }


        public static long[][] Solve(long[][] nodes)
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
            return new long[][] { InOrder(arrayNodes[0]), PreOrder(arrayNodes[0]), PostOrder(arrayNodes[0]) };

        }

        public static long[] InOrder(Node i)
        {
            List<long> res = new List<long>();
            Stack<Node> s = new Stack<Node>();
            Node current = i;
            while (current != null || s.Count > 0)
            {
                while (current != null)
                {
                    s.Push(current);
                    current = current.Left;
                }

                Node t = s.Pop();
                res.Add(t.Key);
                current = t.Right;
            }

            return res.ToArray();
        }

        public static long[] PreOrder(Node i)
        {
            List<long> res = new List<long>();
            Stack<Node> s = new Stack<Node>();
            Node current = i;
            while (current != null || s.Count > 0)
            {
                while (current != null)
                {
                    s.Push(current);
                    res.Add(current.Key);
                    current = current.Left;
                }

                Node t = s.Pop();

                current = t.Right;
            }

            return res.ToArray();
        }

        public static long[] PostOrder(Node i)
        {
            List<long> res = new List<long>();
            Stack<Node> s = new Stack<Node>();
            Node current = i;
            while (current != null || s.Count > 0)
            {
                while (current != null)
                {
                    s.Push(current);
                    res.Add(current.Key);
                    current = current.Right;
                }

                Node t = s.Pop();

                current = t.Left;
            }

            res.Reverse();
            return res.ToArray();
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
