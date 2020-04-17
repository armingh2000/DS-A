using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace connecting_points
{
    public class Program
    {
        static void Main()
        {
            long pointCount = long.Parse(Console.ReadLine());
            long[][] points = new long[pointCount][];

            string line;

            for(long i = 0; i < pointCount; i++)
            {
                points[i] = new long[2];
                line = Console.ReadLine();
                points[i][0] = long.Parse(line.Split()[0]);
                points[i][1] = long.Parse(line.Split()[1]);
            }
            Console.Write(Solve(pointCount, points));

        }

        public static double[] cost;
        public static PriorityQueue<long> MH;
        public static long proc_num;
        public static long[] parent;

        public static double Solve(long pointCount, long[][] points)
        {
            proc_num = 1;
            cost = new double[pointCount];
            MH = new PriorityQueue<long>();

            for (long i = 0; i < pointCount; i++)
            {
                cost[i] = long.MaxValue;
                MH.Enqueue((float)cost[i], i);
            }

            parent = new long[pointCount];
            
            cost[0] = 0;
            long current = 0;
            double answer;
	    MH.UpdatePriority(0, 0);

            while(proc_num < pointCount)
            {
                current = MH.Dequeue();
                proc_num++;
                for (long i = 0; i < pointCount; i++)
                {
                    if (MH.IsInQueue(i) && (cost[i] > dist(current, i, points)))
                    {
                        cost[i] = dist(current, i, points);
                        parent[i] = current;
                        MH.UpdatePriority(i, (float)cost[i]);
                    }
                }
            }

            answer = cost.Sum();
            return Math.Round(answer, 7);
            //return answer;
        }

        private static double dist(long current, long i, long[][] points)
        {
            long x1 = points[current][0];
            long y1 = points[current][1];
            long x2 = points[i][0];
            long y2 = points[i][1];

            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }
    public class PriorityQueue<T>
{
    class Node
    {
        public float Priority { get; set; }
        public T Object { get; set; }
    }

    //object array
    List<Node> queue = new List<Node>();
    int heapSize = -1;
    bool _isMinPriorityQueue;
    public int Count { get { return queue.Count; } }

    /// <summary>
    /// If min queue or max queue
    /// </summary>
    /// <param name="isMinPriorityQueue"></param>
    public PriorityQueue(bool isMinPriorityQueue = true)
    {
        _isMinPriorityQueue = isMinPriorityQueue;
    }

    private void Swap(int i, int j)
    {
        var temp = queue[i];
        queue[i] = queue[j];
        queue[j] = temp;
    }
    private int ChildL(int i)
    {
        return i * 2 + 1;
    }
    private int ChildR(int i)
    {
        return i * 2 + 2;
    }
    private void MaxHeapify(int i)
{
    int left = ChildL(i);
    int right = ChildR(i);

    int heighst = i;

    if (left <= heapSize && queue[heighst].Priority < queue[left].Priority)
        heighst = left;
    if (right <= heapSize && queue[heighst].Priority < queue[right].Priority)
        heighst = right;

    if (heighst != i)
    {
        Swap(heighst, i);
        MaxHeapify(heighst);
    }
}
private void MinHeapify(int i)
{
    int left = ChildL(i);
    int right = ChildR(i);

    int lowest = i;

    if (left <= heapSize && queue[lowest].Priority > queue[left].Priority)
        lowest = left;
    if (right <= heapSize && queue[lowest].Priority > queue[right].Priority)
        lowest = right;

    if (lowest != i)
    {
        Swap(lowest, i);
        MinHeapify(lowest);
    }
}
private void BuildHeapMax(int i)
{
    while (i >= 0 && queue[(i - 1) / 2].Priority < queue[i].Priority)
    {
        Swap(i, (i - 1) / 2);
        i = (i - 1) / 2;
    }
}
/// <summary>
/// Maintain min heap
/// </summary>
/// <param name="i"></param>
private void BuildHeapMin(int i)
{
    while (i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority)
    {
        Swap(i, (i - 1) / 2);
        i = (i - 1) / 2;
    }
}
public void Enqueue(float priority, T obj)
{
    Node node = new Node() { Priority = priority, Object = obj };
    queue.Add(node);
    heapSize++;
    //Maintaining heap
    if (_isMinPriorityQueue)
        BuildHeapMin(heapSize);
    else
        BuildHeapMax(heapSize);
}
public T Dequeue()
{
    if (heapSize > -1)
    {
        var returnVal = queue[0].Object;
        queue[0] = queue[heapSize];
        queue.RemoveAt(heapSize);
        heapSize--;
        //Maintaining lowest or highest at root based on min or max queue
        if (_isMinPriorityQueue)
            MinHeapify(0);
        else
            MaxHeapify(0);
        return returnVal;
    }
    else
        throw new Exception("Queue is empty");
}
public void UpdatePriority(T obj, float priority)
{
    int i = 0;
    for (; i <= heapSize; i++)
    {
        Node node = queue[i];
        if (object.ReferenceEquals(node.Object, obj))
        {
            node.Priority = priority;
            if (_isMinPriorityQueue)
            {
                BuildHeapMin(i);
                MinHeapify(i);
            }
            else
            {
                BuildHeapMax(i);
                MaxHeapify(i);
            }
        }
    }
}
/// <summary>
/// Searching an object
/// </summary>
/// <param name="obj"></param>
/// <returns></returns>
public bool IsInQueue(T obj)
{
    foreach (Node node in queue)
        if (object.ReferenceEquals(node.Object, obj))
            return true;
    return false;
}
  }
}
