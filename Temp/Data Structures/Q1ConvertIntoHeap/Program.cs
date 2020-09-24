using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1ConvertIntoHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            var v = Console.ReadLine().Split();
            long[] input = new long[n];
            for(long i=0;i<n;i++)
            {
                input[i] = long.Parse(v[i]);
            }

            var v2 = Solve(input);

            Console.WriteLine(v2.Length);
            v2.ToList().ForEach(x => Console.WriteLine(x.Item1 + " " + x.Item2));
            

        }

        public static Tuple<long, long>[] Solve(long[] array)
        {
            List<Tuple<long, long>> tuple = new List<Tuple<long, long>>();
            tuple = buildHeap(array, array.Length);
            return tuple.ToArray();
        }

        public static int leftChild(int i)
        {
            return (i + 1) * 2 - 1;
        }

        public static int rightChild(int i)
        {
            return (i + 1) * 2;
        }

        public static void siftDown(long[] a, int size, int i, ref int swapCount, List<Tuple<long, long>> tuple)
        {
            int minIdx = i;
            int l = leftChild(i);
            if (l < size && a[l] < a[minIdx])
            {
                minIdx = l;
            }
            int r = rightChild(i);
            if (r < size && a[r] < a[minIdx])
            {
                minIdx = r;
            }
            if (i != minIdx)
            {
                tuple.Add(new Tuple<long, long>(i, minIdx));
                long temp = a[minIdx];
                a[minIdx] = a[i];
                a[i] = temp;
                swapCount++;
                siftDown(a, size, minIdx, ref swapCount, tuple);

            }
        }

        public static List<Tuple<long, long>> buildHeap(long[] a, int size)
        {
            int swapCount = 0;
            List<Tuple<long, long>> tuple = new List<Tuple<long, long>>();
            for (int i = (size - 1) / 2; i >= 0; i--)
            {
                siftDown(a, size, i, ref swapCount, tuple);
            }
            //return a;
            return tuple;
        }

        public class Pair
        {
            public long first;
            public long second;
        }
    }
}
