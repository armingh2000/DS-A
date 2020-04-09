
using System;
using System.Collections.Generic;
using System.Linq;

namespace bwt 
{
    public class Program 
    {
        static void Main()
        {
            string bwt = Console.ReadLine();
            Console.WriteLine(Solve(bwt));

        }

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public static string Solve(string bwt)
        {
            cell.cells = new List<cell>();
            for (int i = 0; i < bwt.Length; i++)
            {
                new cell(i, bwt[i]);
            }
            return ReverseBtw();
        }
        public static string ReverseBtw() {
            char[] rb = new char[cell.cells.Count];
            cell.cells = cell.cells.OrderBy(x => x.character).ToList();
            int curr = 0;
            for (int i = 0; i < cell.cells.Count; i++)
            {
                rb[i] = cell.cells[curr].character;
                curr = cell.cells[curr].first_ind;
            }
            return new string(rb, 1, rb.Length - 1) + "$";
        }
    }
    public class cell
    {
        public static List<cell> cells = new List<cell>();
        public int first_ind;
        public char character;

        public cell(int fi, char c){
            this.first_ind = fi;
            this.character = c; 
            cells.Add(this);
        }
    }
}
