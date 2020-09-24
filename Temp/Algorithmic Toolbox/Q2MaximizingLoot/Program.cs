using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2MaximizingLoot
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long n = long.Parse(line.Split()[0]);
            long cap = long.Parse(line.Split()[1]);
            long[] w = new long[n];
            long[] v = new long[n];

            for(long i=0;i<n;i++)
            {
                line = Console.ReadLine();
                v[i] = long.Parse(line.Split()[0]);
                w[i] = long.Parse(line.Split()[1]);
            }

            Console.WriteLine(Math.Round(Solve(cap, w, v),3));
        }

        public static double Solve(long capacity, long[] weights, long[] values)
        {
            Item[] items = new Item[weights.Length];

            for (long i = 0; i < weights.Length; i++)
            {
                items[i] = new Item();
                items[i].Value = values[i];
                items[i].Weight = weights[i];
                items[i].ValuePerWeight = (double)values[i] / weights[i];
            }

            double worth = 0;
            var v = items.OrderByDescending(x => x.ValuePerWeight).ToArray();

            //while (capacity > 0)
            //{
            for (long i = 0; i < weights.Length && capacity>0; i++)
            {
                if (v[i].Weight <= capacity)
                {
                    capacity -= v[i].Weight;
                    worth += v[i].Value;
                }
                else
                {
                    worth += (((double)v[i].Value * capacity)/ (double)v[i].Weight);
                    capacity = 0;
                }

                v[i].ValuePerWeight = 0;
            }
            //}

            return (worth);
        }

        public class Item
        {
            public double ValuePerWeight { get; set; }
            public long Weight { get; set; }
            public long Value { get; set; }

        }

    }
}
