using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sum
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            long a = long.Parse(line.Split()[0]);
            long b = long.Parse(line.Split()[1]);

            Console.WriteLine(a + b);
        }
    }
}
