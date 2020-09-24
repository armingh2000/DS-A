using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long n=long.Parse(line.Split()[0]), m = long.Parse(line.Split()[1]);
            long[] startSegments = new long[n], endSegment = new long[n],points=new long[m];


            for (long i = 0; i < n; i++)
            {
                line = Console.ReadLine();
                startSegments[i] = long.Parse(line.Split()[0]);
                endSegment[i] = long.Parse(line.Split()[1]);
            }

            line = Console.ReadLine();
            var v = line.Split();

            for (long i = 0; i < m; i++)
            {
                points[i] = long.Parse(v[i]);
            }

            Solve(points, startSegments, endSegment).ToList().ForEach(x => Console.Write(x + " "));

        }

        public static long[] Solve(long[] points, long[] startSegments, long[] endSegment)
        {
            long n = startSegments.Length;
            OrderedPair[] c = new OrderedPair[points.Length + startSegments.Length + endSegment.Length];
            for (long i = 0; i < points.Length; i++)
            {
                c[i] = new OrderedPair(points[i], 2, i);

            }
            for (long i = 0; i < startSegments.Length; i++)
            {
                c[points.Length + i] = new OrderedPair(startSegments[i], 1);
            }
            for (long i = 0; i < endSegment.Length; i++)
            {
                c[points.Length + startSegments.Length + i] = new OrderedPair(endSegment[i], 3);
            }
            c = c.ToList().OrderBy(x => x.Y).OrderBy(x => x.X).ToArray();
            long[] res = new long[points.Length];
            long left = 0;
            long right = 0;
            for (long i = 0; i < points.Length + startSegments.Length + endSegment.Length; i++)
            {
                if (c[i].Y == 1)
                {
                    left++;
                }
                else if (c[i].Y == 3)
                {
                    right++;
                }
                else
                {
                    res[c[i].Idx] = left - right;
                }
            }
            return res;
        }

        public class OrderedPair
        {
            public OrderedPair()
            {

            }
            public OrderedPair(double s, double e)
            {
                this.X = s;
                this.Y = e;
            }
            public OrderedPair(double s, double e, long i)
            {
                this.X = s;
                this.Y = e;
                this.Idx = i;
            }
            public double X { get; set; }
            public double Y { get; set; }
            public long Idx { get; set; }
        }

    }
}
