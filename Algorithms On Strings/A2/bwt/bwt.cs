
using System;
using System.Collections.Generic;   
using System.Linq;

namespace bwt
{
    public class Program 
    {
        static void Main()
        {
            string text = Console.ReadLine();
            Console.WriteLine(Solve(text));
        }
        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public static string Solve(string text)
        {
            List<string> prot = Rotate(text);
            return ConstructBtw(prot);
        }

        private static List<string> Rotate(string text)
        {
            List<string> rotates = new List<string>();
            string temp;
            for (int i = 0; i < text.Length; i++)
            {
                temp = text.Substring(i);
                temp += text.Substring(0, i);
                rotates.Add(temp);
            }
            return rotates;
        }

        private static string ConstructBtw(List<string> prot)
        {
            prot.Sort();
            string lc = "";
            for(int i = 0; i < prot.Count; i++)
            {
                lc += prot[i].Last();
            }
            return lc;
        }
    }
}
