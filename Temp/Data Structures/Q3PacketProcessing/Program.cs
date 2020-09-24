using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q3PacketProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long s = long.Parse(line.Split()[0]);
            long n = long.Parse(line.Split()[1]);
            long[] arrivalTimes = new long[n];
            long[] processingTimes = new long[n];

            for (long i=0;i<n;i++)
            {
                line = Console.ReadLine();

                arrivalTimes[i]= long.Parse(line.Split()[0]);
                processingTimes[i]= long.Parse(line.Split()[1]);
            }

            Solve(s, arrivalTimes, processingTimes).ToList().ForEach(x => Console.WriteLine(x));
        }

        public static long[] Solve(long bufferSize,
            long[] arrivalTimes,
            long[] processingTimes)
        {
            Queue<Buffer> buffer = new Queue<Buffer>();
            long time = 0;
            long current = 0;
            long[] res = new long[arrivalTimes.Length];

            if (bufferSize < arrivalTimes.Length)
            {
                while (current < arrivalTimes.Length || buffer.Count != 0)
                {
                    if (buffer.Count != 0)
                    {
                        time = buffer.Peek().End;
                    }
                    else
                    {
                        while (current < arrivalTimes.Length && buffer.Count == 0)
                        {
                            Buffer k = new Buffer();
                            k.Start = arrivalTimes[current];
                            k.ProcessTime = processingTimes[current];
                            k.End = arrivalTimes[current] + processingTimes[current];
                            k.Index = current;
                            if (k.ProcessTime != 0)
                            {
                                buffer.Enqueue(k);
                                time = k.End;
                            }
                            else
                            {
                                res[current] = k.Start;
                                time = k.End;
                            }
                            current++;
                        }
                    }

                    while (buffer.Count != 0 && buffer.Peek().End < time)
                    {
                        res[buffer.Peek().Index] = buffer.Peek().Start;
                        buffer.Dequeue();
                    }

                    while (current < arrivalTimes.Length && arrivalTimes[current] < time)
                    {
                        if (buffer.Count < bufferSize)
                        {
                            Buffer b = new Buffer();
                            if (buffer.Count != 0)
                            {
                                b.Start = Math.Max(buffer.Last().End, arrivalTimes[current]);
                                b.ProcessTime = processingTimes[current];
                                b.End = b.Start + processingTimes[current];
                                b.Index = current;
                                buffer.Enqueue(b);
                            }
                            else
                            {
                                b.Start = arrivalTimes[current];
                                b.ProcessTime = processingTimes[current];
                                b.End = arrivalTimes[current] + processingTimes[current];
                                b.Index = current;
                                if (b.ProcessTime != 0)
                                {
                                    buffer.Enqueue(b);
                                }
                                else
                                {
                                    res[current] = b.Start;
                                }
                            }
                        }
                        else
                        {
                            res[current] = -1;
                        }
                        current++;
                    }
                    while (buffer.Count != 0 && buffer.Peek().End <= time)
                    {
                        res[buffer.Peek().Index] = buffer.Peek().Start;
                        buffer.Dequeue();
                    }
                }
            }
            else
            {
                long[] finish = new long[arrivalTimes.Length];
                if (arrivalTimes.Length != 0)
                {
                    res[0] = arrivalTimes[0];
                    finish[0] = arrivalTimes[0] + processingTimes[0];
                    for (long i = 1; i < arrivalTimes.Length; i++)
                    {
                        res[i] = Math.Max(finish[i - 1], arrivalTimes[i]);
                        finish[i] = res[i] + processingTimes[i];
                    }
                }
            }

            return res;
        }

        public class Buffer
        {
            public long Start { get; set; }
            public long End { get; set; }
            public long ArrivalTime { get; set; }
            public long ProcessTime { get; set; }
            public long Index { get; set; }
        }
    }
}
